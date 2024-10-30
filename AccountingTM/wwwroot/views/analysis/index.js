//Анализ
$('#date').datepicker({
    range: true,
    multipleDatesSeparator: ' - ',
});

    //Вывод данных в таблицу
    let tableAdministrations = new DataTable('#administrationTable', {
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.searchQuery = $("#search-input").val()
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/Administration/GetAll', {
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
                action: () => tableAdministrations.draw(false)
            }
        ],
        initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
        columnDefs: [
            {
                targets: 0,
                data: 'fio',
                render: (data, type, row, meta) => {
                    return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
                }
            },
            {
                targets: 1,
                data: 'login',
            },
            {
                targets: 2,
                data: 'password',
            },
            {
                targets: 3,
                data: 'role',
            },
            {
                targets: 4,
                data: 'employee',
                render: (data, type, row, meta) => {
                    return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
                }
            },
            {
                targets: 5,
                data: null,
                render: (data, type, row, meta) => {
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-regular fa-address-card"></i></a>
                            <button class="btn btn-danger delete administration" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });
    $("#search-btn").click(function () {
        tableAdministrations.ajax.reload()
    })



$("#CalculateBtn").click(function () {
    const dates = $("#date").val().split(" - ");
    axios.post("/Analysis/Calculate", {
        Category: +$("#categories").val(),
        TypeConsumableId: +$("#typeConsumableId").val(),
        BrandId: +$("#brand").val(),
        //Model: $("#model").val(),
        DateStart: moment(dates[0], 'DD.MM.YYYY').toDate(),
        DateEnd: moment(dates[1], 'DD.MM.YYYY').toDate(),
    }).then(function () {    })
})

//Вывод Select
//Select

//Тип расходного материала
$("#typeConsumable").select2({
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
            axios.get("/TypeConsumable/GetAll", { params: filter }).then(function (result) {

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
            axios.get("/Brand/GetAll", { params: filter }).then(function (result) {

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
