$(function () {
    $('#calendar').datepicker({
        range: true,
        multipleDatesSeparator: ' - ',
    });

    $('#AddOrderDate').datepicker({ container: '#AddOrderModal .modal-body', dateFormat: "dd.mm.yyyy" });

    let tableClients = new DataTable('#applicationsTable', {
        paging: true,
        serverSide: true,
        bAutoWidth: false,
        aoColumns: [
            { sWidth: '11%' },
            { sWidth: '10%' },
            { sWidth: '10%' },
            { sWidth: '9%' },
            { sWidth: '16%' },
            { sWidth: '8%' },
            { sWidth: '11%' },
            { sWidth: '9%' },
            { sWidth: '9%' },
            { sWidth: '7%' },
        ],
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.searchQuery = $("#search-input").val()
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/Application/GetAll', {
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
                data: 'applicationNumber',
            },
            {
                targets: 1,
                data: 'dateOfCreation',
                render: (data, type, row, meta) => {
                    return data ? dayjs(data).format("DD.MM.YYYY HH:mm") : "";
                }

            },
            {
                targets: 2,
                data: 'dateOfChange',
                render: (data, type, row, meta) => {
                    return data ? dayjs(data).format("DD.MM.YYYY HH:mm") : "";
                }


            },
            {
                targets: 3,
                data: 'category.name',
            },
            {
                targets: 4,
                data: 'subject',
            },
            {
                targets: 5,
                data: 'status',
                render: (data, type, row, meta) => {
                    switch (data) {
                        case 0: return "Новая";
                        case 1: return "В работе";
                        case 2: return "Приостановлена";
                        case 3: return "Передана";
                        case 4: return "Решена";
                    }
                }
            },
            {
                targets: 6,
                data: 'author',
            },
            {
                targets: 7,
                data: 'location.name',
            },
            {
                targets: 8,
                data: 'priority',
                render: (data, type, row, meta) => {
                    switch (data) {
                        case 0: return "Критический";
                        case 1: return "Высокий";
                        case 2: return "Нормальный";
                        case 3: return "Низкий";
                    }
                }
            },
            {
                targets: 9,
                data: null,
                render: (data, type, row, meta) => {
                    return `<a href="application/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о заявке"><i class="fa-solid fa-circle-info"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    //Создание новой заявки
    $("#create-btn").click(function () {
        axios.post("Application/Create", {
            categoryId: +$("#category").val(),
            subject: $("#subject").val(),
            description: $("#description").val(),
            author: $("#author").val(),
            locationId: +$("#location").val(),
            priority: +$("#priority").val(),
        }).then(function () {
            location.reload()
        })
    })


    $(document).on("click", ".delete", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Заявка ${name} будет удалена!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Application/Delete?id=" + id).then(function () {
                    tableClients.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('Заявка успешно удалена!')
                })
            }
        });
    })

    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    $("#category").select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Категория',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("Category/GetAll", { params: filter }).then(function (result) {

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
})