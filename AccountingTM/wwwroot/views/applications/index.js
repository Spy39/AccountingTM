$(function () {
    /*$('#AddRecipeDate').datepicker({ container: '#RecieptsClientModal .modal-body', dateFormat: "dd.mm.yyyy" });*/

    var language = {
        emptyTable: "Нет данных для отображения",
        info: "_START_-_END_ из _TOTAL_ элементов",
        infoEmpty: "Нет записей",
        infoFiltered: "(отфильтровано из _MAX_ записей)",
        infoPostFix: "",
        infoThousands: ",",
        lengthMenu: "Показано _MENU_ записей",
        loadingRecords: "Загрузка...",
        processing: '<i class="fas fa-refresh fa-spin"></i>',
        search: "Искать:",
        zeroRecords: "Нет записей, удовлетворяющих поиску",
        paginate: {
            first: '<i class="fas fa-angle-double-left"></i>',
            last: '<i class="fas fa-angle-double-right"></i>',
            next: '<i class="fas fa-chevron-right"></i>',
            previous: '<i class="fas fa-chevron-left"></i>'
        },
        aria: {
            sortAscending: ": " + "для сортировки столбцов по возрастанию</",
            sortDescending: ": " + "для сортировки столбцов по убыванию"
        }
    };

    let tableClients = new DataTable('#applicationsTable', {
        searching: false,
        language: language,
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.searchQuery = $("#search-input").val()
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/Applications/GetAll', {
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
        dom: [
            "<'row'<'col-md-12'f>>",
            "<'row'<'col-md-12't>>",
            "<'row mt-2'",
            "<'col-lg-1 col-xs-12'<'float-left text-center data-tables-refresh'B>>",
            "<'col-lg-3 col-xs-12'<'float-left text-center'i>>",
            "<'col-lg-3 col-xs-12'<'text-center'l>>",
            "<'col-lg-5 col-xs-12'<'float-right'p>>",
            ">"
        ].join(''),
        initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
        columnDefs: [
            {
                searchable: false,
                orderable: false,
                targets: 0,
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
            },
            {
                targets: 1,
                data: 'dateOfCreation',
            },
            {
                targets: 2,
                data: 'dateOfChange',
            },
            {
                targets: 3,
                data: 'category',
            },
            {
                targets: 4,
                data: 'subject',
            },
            {
                targets: 5,
                data: 'status',
            },
            {
                targets: 6,
                data: 'author',
            },
            {
                targets: 7,
                data: 'location',
            },
            {
                targets: 8,
                data: 'priority',
            },
            {
                targets: 9,
                data: null,
                render: (data, type, row, meta) => {
                    return `<a href="applications/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о заявке"><i class="fa-regular fa-address-card"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

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
                axios.delete("Applications/Delete?id=" + id).then(function () {
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