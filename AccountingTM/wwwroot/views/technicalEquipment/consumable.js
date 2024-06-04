$(function () {
    $('#calendar').datepicker({
        range: true,
        multipleDatesSeparator: ' - ',
    });

    $('#AddOrderDate').datepicker({ container: '#AddOrderModal .modal-body', dateFormat: "dd.mm.yyyy" });

    //Вывод данных о техничесих средствах в таблицу
    let tableConsumables = new DataTable('#consumableTable', {
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.searchQuery = $("#search-input").val()
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/Consumable/GetAll', {
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
                data: 'employee',
                render: (data, type, row, meta) => {
                    return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
                }
            },
            {
                targets: 5,
                data: 'location.name',
            },
            {
                targets: 6,
                data: 'unit.name',
            },
            {
                targets: 7,
                data: 'quantity',
            },
            {
                targets: 8,
                data: null,
                render: (data, type, row, meta) => {
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-regular fa-address-card"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });
    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление
    $("#create-btn").click(function () {
        axios.post("Consumable/Create", {
            typeId: +$("#typeConsumable").val(),
            brandId: +$("#brand").val(),
            model: $("#model").val(),
            serialNumber: $("#serialNumber").val(),
            employeeId: +$("#employee").val(),
            locationId: +$("#location").val(),
            unit: +$("#unit").val(),
            quantity: $("#quantity").val(),
            
            
            
            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление
    $(document).on("click", ".delete", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Расходный материал ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Consumable/Delete?id=" + id).then(function () {
                    tableClients.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('Расходный материал успешно удален!')
                })
            }
        });
    })

    //Вывод Select

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
                    axios.get("TypeConsumable/GetAll", { params: filter }).then(function (result) {

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

    //Единица измерения
    $("#unit").select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Единица измерения',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("Unit/GetAll", { params: filter }).then(function (result) {

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
