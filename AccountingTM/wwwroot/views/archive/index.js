$(document).ready(function () {
    // Инициализация datepicker для поля выбора периода статистики
    $('#dateArchive').datepicker({
        range: true,
        multipleDatesSeparator: ' - ',
        dateFormat: 'dd.mm.yyyy'
    });

    let archiveTable = new DataTable('#archiveTETable', {
        paging: true,
        serverSide: true,
        responsive: true,
        bAutoWidth: false,
        // Определяем 7 колонок:
        aoColumns: [
            { sWidth: '15%' }, // Дата списания
            { sWidth: '15%' }, // Тип
            { sWidth: '15%' }, // Бренд
            { sWidth: '15%' }, // Модель
            { sWidth: '15%' }, // Серийный номер
            { sWidth: '15%' }, // Состояние
            { sWidth: '10%' }  // Действия
        ],
        ajax: function (data, callback) {
            try {
                let filter = {
                    searchQuery: $("#search-input").val(),
                    maxResultCount: data.length || 10,
                    skipCount: data.start
                };

                axios.get('/Archive/GetAllSet', { params: filter })
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
            } catch (ex) {
                console.error("Ошибка в ajax функции:", ex);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => archiveTable.draw(false)
            }
        ],
        // Подсветка строк по состоянию:
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
            {
                // Дата списания (поле deletedDate)
                targets: 0,
                data: 'deletedDate',
                render: (data) => data ? dayjs(data).format("DD.MM.YYYY HH:mm") : ""
            },
            { targets: 1, data: 'type.name' },
            { targets: 2, data: 'brand.name' },
            { targets: 3, data: 'model.name' },
            { targets: 4, data: 'serialNumber' },
            {
                targets: 5,
                data: 'state',
                render: function (data) {
                    const states = ["Исправно", "Неисправно", "Работоспособно", "Неработоспособно"];
                    return states[data] || "Неизвестно";
                }
            },
            {
                targets: 6,
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-nowrap',
                render: function (data, type, row) {
                    // В архиве кнопки редактирования/удаления недоступны – кнопка удаления будет заблокирована
                    return `
                        <a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС">
                            <i class="fa-solid fa-circle-info"></i>
                        </a>
                        <button class="btn btn-danger delete technicalEquipment" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить" disabled>
                            <i class="fa-solid fa-trash"></i>
                        </button>
                    `;
                }
            }
        ]
    });


    // Поиск по таблице архива
    $("#search-btn").click(function () {
        try {
            archiveTable.ajax.reload();
        } catch (ex) {
            console.error("Ошибка при перезагрузке таблицы:", ex);
        }
    });

    $('#exportExcelBtn').on('click', function () {
        try {
            // Получаем заголовки таблицы
            const headers = [];
            $('#archiveTETable thead tr th').each(function () {
                headers.push($(this).text().trim());
            });

            // Получаем данные из таблицы из DOM (из tbody)
            const tableData = [];
            $('#archiveTETable tbody tr').each(function () {
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
            XLSX.utils.book_append_sheet(wb, ws, "ArchivedTechnicalEquipment");
            XLSX.writeFile(wb, "archived_technical_equipment.xlsx");
        } catch (ex) {
            console.error("Ошибка экспорта в Excel:", ex);
        }
    });

    // Печать таблицы
    $('#printTableBtn').on('click', function () {
        let printWindow = window.open('', '', 'width=900,height=700');
        printWindow.document.write(`
            <html>
                <head>
                    <title>Печать списка архивированных ТС</title>
                    <style>
                        body { font-family: Arial, sans-serif; margin: 20px; }
                        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
                        th, td { border: 1px solid black; padding: 8px; text-align: left; }
                        th { background-color: #f2f2f2; }
                    </style>
                </head>
                <body>
                    <h2>Архив списанных ТС</h2>
                    <table>
                        <thead>${$('#archiveTETable thead').html()}</thead>
                        <tbody>${$('#archiveTETable tbody').html()}</tbody>
                    </table>
                </body>
            </html>
        `);
        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    });
});
