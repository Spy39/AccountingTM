
//Вывод данных о выполненных работах над техническим средством в таблицу
    let tableCompletedWorks = new DataTable('#completedWorkTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        filter.technicalEquipmentId = +$("#technicalEquipmentId").val();
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
            action: () => tableCompletedWorks.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            targets: 0,
            data: 'dateOfCreation',
            render: (data, type, row, meta) => {
                return data ? dayjs(data).format("DD.MM.YYYY") : "";
            }

        },
        {
            targets: 1,
            data: 'applicationNumber',
        },
        {
            targets: 2,
            data: 'subject',
        },
        {
            targets: 3,
            data: 'employee.fullName',
            
        },
        {
            targets: 4,
            data: 'status',
            render: (data, type, row, meta) => {
                switch (data) {
                    case 0: return "Новая";
                    case 1: return "Получен комментарий";
                    case 2: return "Комментарий отправлен";
                    case 3: return "В работе";
                    case 4: return "Приостановлена";
                    case 5: return "Передана";
                    case 6: return "Решена";
                }
            }
        },
        {
            targets: 5,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});

    $("#search-btn").click(function () {
        tableCompletedWorks.ajax.reload()
    })

    //Добавление
    $('#createCompletedWorkBtn').click(function () {

        axios.post("/Application/CreateCompletedWork", {
            technicalEquipmentId: +$("#technicalEquipmentId").val(),
            categoryId: +$("#category").val(),
            subject: $("#subject").val(),
            description: $("#description").val(),
            author: $("#author").val(),
            locationId: +$("#location").val(),
            priority: +$("#priority").val(),
            dateOfCreation: moment($("#dateCompletedWork").val(), 'DD.MM.YYYY').toDate()
        }).then(function () {
            location.reload()
        })
    })

    //Удаление
    $(document).on("click", ".delete", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `ТС ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("TechnicalEquipmentInfo/Delete?id=" + id).then(function () {
                    tableCompletedWorks.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('ТС успешно удален!')
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

    //Категории
    $("#category").select2({
        width: '100%',
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
                axios.get("/Category/GetAll", { params: filter }).then(function (result) {

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

    //Местоположение
    $("#location").select2({
        width: '100%',
        placeholder: 'Местоположение',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("/Location/GetAll", { params: filter }).then(function (result) {

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