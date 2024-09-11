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
                action: () => _$rolesTable.draw(false)
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
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-regular fa-address-card"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });