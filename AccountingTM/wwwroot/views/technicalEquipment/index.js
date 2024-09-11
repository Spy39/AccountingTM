$(function () {
    $('#calendar').datepicker({
        range: true,
        multipleDatesSeparator: ' - ',
    });

    $('#AddOrderDate').datepicker({ container: '#AddOrderModal .modal-body', dateFormat: "dd.mm.yyyy" });

    //Вывод данных о техничесих средствах в таблицу
    let tableClients = new DataTable('#technicalEquipmentTable', {
        paging: true,
        serverSide: true,
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
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.searchQuery = $("#search-input").val()
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/TechnicalEquipment/GetAll', {
                params: filter
            })
                .then(function (result) {
                    console.log(result);
                    callback({
                        recordsTotal: result.data.totalCount,
                        recordsFiltered: result.data.totalCount,
                        data: result.data.items
                    });
                })

        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => tableClients.draw(false)
            }
        ],
        initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
        columnDefs: [
            {
                targets: 0,
                data: 'type.name',
            },
            {
                targets: 1,
                data: 'brand.name',
            },
            {
                targets: 2,
                data: 'model',
            },
            {
                targets: 3,
                data: 'serialNumber',
            },
            {
                targets: 4,
                data: 'state',
                render: (data, type, row, meta) => {
                    switch (data) {
                        case 0: return "Исправно";
                        case 1: return "Неисправно";
                        case 2: return "Работоспособно";
                        case 3: return "Неработоспособно";
                    }
                }

            },
            {
                targets: 5,
                data: 'employee',
                render: (data, type, row, meta) => {
                    return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
                }
            },
            {
                targets: 6,
                data: 'location.name',
            },
            {
                targets: 7,
                data: null,
                render: (data, type, row, meta) => {
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-solid fa-circle-info"></i></a>
                            <button class="btn btn-danger delete technicalEquipment" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });
    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление нового технического средства
    $("#create-btn").click(function () {
        axios.post("TechnicalEquipment/Create", {
            typeId: +$("#typeEquipment").val(),
            brandId: +$("#brand").val(),
            model: $("#model").val(),
            serialNumber: $("#serialNumber").val(),
            inventoryNumber: $("#inventoryNumber").val(),
            state: +$("#state").val(),
            employeeId: +$("#employee").val(),
            locationId: +$("#location").val(),
            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление технического средства
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
                    tableClients.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`ТС ${name} успешно удалено!`)
                })
            }
        });
    })

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

})
