$(document).ready(function () {
    let tableClients = new DataTable('#technicalEquipmentTable', {
        paging: true,
        serverSide: true,
        responsive: true,
        bAutoWidth: false,
        aoColumns: [
            { sWidth: '14%' },
            { sWidth: '14%' },
            { sWidth: '14%' },
            { sWidth: '14%' },
            { sWidth: '10%' },
            { sWidth: '17%' },
            { sWidth: '10%' },
            { sWidth: '7%' }
        ],
        ajax: function (data, callback) {
            let filter = {
                searchQuery: $("#search-input").val(),
                maxResultCount: data.length || 10,
                skipCount: data.start
            };
            axios.get('/TechnicalEquipment/GetAll', { params: filter })
                .then(function (result) {
                    callback({
                        recordsTotal: result.data.totalCount,
                        recordsFiltered: result.data.totalCount,
                        data: result.data.items
                    });
                })
                .catch(function (error) {
                    console.error("Ошибка загрузки данных:", error);
                });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => tableClients.draw(false)
            }
        ],
        initComplete: function () {
            $('[data-bs-toggle="tooltip"]').tooltip();
        },
        // Выделение строк в зависимости от состояния
        rowCallback: function (row, data, index) {
            // Предполагается, что data.state — числовое значение:
            // 0 - "Исправно", 1 - "Неисправно", 2 - "Работоспособно", 3 - "Неработоспособно"
            if (data.state === 1) {
                $(row).addClass("table-warning");
            } else if (data.state === 3) {
                $(row).addClass("table-danger");
            }
        },
        columnDefs: [
            { targets: 0, data: 'type.name' },
            { targets: 1, data: 'brand.name' },
            { targets: 2, data: 'model.name' },
            { targets: 3, data: 'serialNumber' },
            {
                targets: 4,
                data: 'state',
                render: function (data) {
                    const states = ["Исправно", "Неисправно", "Работоспособно", "Неработоспособно"];
                    return states[data] || "Неизвестно";
                }
            },
            {
                targets: 5,
                data: 'employee',
                render: function (data) {
                    return `${data?.lastName || ''} ${data?.firstName || ''} ${data?.fatherName || ''}`;
                }
            },
            { targets: 6, data: 'location.name' },
            {
                targets: 7,
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-nowrap',
                width: '1%',
                render: function (data, type, row) {
                    return `
                        <a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС">
                            <i class="fa-solid fa-circle-info"></i>
                        </a>
                        <button class="btn btn-danger delete technicalEquipment" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить">
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                }
            }
        ]
    });

    // Обновление таблицы при поиске
    $("#search-btn").click(function () {
        tableClients.ajax.reload();
    });

    // Экспорт данных в Excel
    $('#exportExcelBtn').on('click', function () {
        try {
            // Получаем заголовки таблицы из thead
            const headers = [];
            $('#technicalEquipmentTable thead tr th').each(function () {
                headers.push($(this).text().trim());
            });

            // Получаем данные из tbody
            const tableData = [];
            $('#technicalEquipmentTable tbody tr').each(function () {
                const row = [];
                $(this).find('td').each(function () {
                    row.push($(this).text().trim());
                });
                tableData.push(row);
            });

            if (tableData.length === 0) {
                alert("Нет данных для экспорта!");
                return;
            }

            // Формируем рабочий лист Excel с помощью библиотеки XLSX
            const ws = XLSX.utils.aoa_to_sheet([headers, ...tableData]);
            const wb = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(wb, ws, "TechnicalEquipment");
            XLSX.writeFile(wb, "technical_equipment.xlsx");
        } catch (ex) {
            console.error("Ошибка экспорта в Excel:", ex);
        }
    });

    // Печать PDF
    $('#printTableBtn').on('click', function () {
        let printWindow = window.open('', '', 'width=900,height=700');
        printWindow.document.write(`
        <html>
            <head>
                <title>Печать таблицы</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 20px; }
                    table { width: 100%; border-collapse: collapse; margin-top: 10px; }
                    th, td { border: 1px solid black; padding: 8px; text-align: left; }
                    th { background-color: #f2f2f2; }
                </style>
            </head>
            <body>
                <h2>Учет технических средств</h2>
                <table>
                    <thead>${$('#technicalEquipmentTable thead').html()}</thead>
                    <tbody>${$('#technicalEquipmentTable tbody').html()}</tbody>
                </table>
            </body>
        </html>
        `);
        printWindow.document.close();
        printWindow.onload = function () {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        };
    });

    // Создание нового технического средства
    $("#create-btn").click(function () {
        axios.post("TechnicalEquipment/Create", {
            typeId: +$("#typeEquipment").val(),
            brandId: +$("#brand").val(),
            modelId: +$("#model").val(),
            serialNumber: $("#serialNumber").val(),
            inventoryNumber: $("#inventoryNumber").val(),
            state: +$("#state").val(),
            employeeId: +$("#employee").val(),
            locationId: +$("#location").val(),
            isDeleted: false
        }).then(function () {
            location.reload();
        });
    });

    // Удаление технического средства
    $(document).on("click", ".delete.technicalEquipment", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `ТС ${name} будет удалено!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("TechnicalEquipment/Delete?id=" + id).then(function () {
                    tableClients.draw(false);
                    $(".tooltip").removeClass("show");
                    toastr.success(`ТС ${name} успешно удалено!`);
                });
            }
        });
    });

    // Импорт технического средства
    $("#upload-btn").click(function () {
        const formData = new FormData();
        formData.append('file', document.getElementById("formFile").files[0]);
        axios.post("TechnicalEquipment/UploadExcel", formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            }
        }).then(function () {
            location.reload();
        });
    });
});



    //Вывод Select

    //Тип технического средства
        $("#typeEquipment").select2({
            width: '100%',
            allowClear: true,
            placeholder: 'Наименование типа',
            ajax: {
                transport: (data, success, failure) => {
                    let params = data.data;
                    let maxResultCount = 30;

                    params.page = params.page || 1;

                    let filter = {};
                    filter.maxResultCount = maxResultCount;
                    filter.skipCount = (params.page - 1) * maxResultCount;
                    filter.keyword = params.term
                    axios.get("TypeEquipment/GetAll", { params: filter }).then(function (result) {

                        success({
                            results: result.data.items,
                            pagination: {
                                more: (params.page * maxResultCount) < result.data.totalCount
                            }
                        });
                    });
                },
                cache: true
            },
            templateResult: (data) => data.name,
            templateSelection: (data) => data.name
        })

    //Бренд техниеского средства
        $("#brand").select2({
            width: '100%',
            allowClear: true,
            placeholder: 'Наименование бренда',
            ajax: {
                transport: (data, success, failure) => {
                    let params = data.data;
                    let maxResultCount = 30;

                    params.page = params.page || 1;

                    let filter = {};
                    filter.maxResultCount = maxResultCount;
                    filter.skipCount = (params.page - 1) * maxResultCount;
                    filter.keyword = params.term
                    axios.get("Brand/GetAll", { params: filter }).then(function (result) {

                        success({
                            results: result.data.items,
                            pagination: {
                                more: (params.page * maxResultCount) < result.data.totalCount
                            }
                        });
                    });
                },
                cache: true
            },
            templateResult: (data) => data.name,
            templateSelection: (data) => data.name
        })

    //Модель техниеского средства
    $("#model").select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Наименование модели',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("Model/GetAll", { params: filter }).then(function (result) {

                    success({
                        results: result.data.items,
                        pagination: {
                            more: (params.page * maxResultCount) < result.data.totalCount
                        }
                    });
                });
            },
            cache: true
        },
        templateResult: (data) => data.name,
        templateSelection: (data) => data.name
    })

    //Помещение
    $("#location").select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Помещение',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("Location/GetAll", { params: filter }).then(function (result) {

                    success({
                        results: result.data.items,
                        pagination: {
                            more: (params.page * maxResultCount) < result.data.totalCount
                        }
                    });
                });
            },
            cache: true
        },
        templateResult: (data) => data.name,
        templateSelection: (data) => data.name
    })

    //Ответственный
    $("#employee").select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Ответственный',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("Employee/GetAll", { params: filter }).then(function (result) {

                    success({
                        results: result.data.items,
                        pagination: {
                            more: (params.page * maxResultCount) < result.data.totalCount
                        }
                    });
                });
            },
            cache: true
        },
        templateResult: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,
        templateSelection: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,

    })

