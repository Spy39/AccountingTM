
//Вывод данных о консервации техничесокого средства в таблицу
    let tableConservations = new DataTable('#conservationTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/TechnicalEquipmentInfo/GetAllConservation', {
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
            action: () => tableConservations.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            targets: 0,
            data: 'date',
        },
        {
            targets: 1,
            data: 'nameOfWork',
        },
        {
            targets: 2,
            data: 'validity',
        },
        {
            targets: 3,
            data: 'employee',
            render: (data, type, row, meta) => {
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
            }
        },
        {
            targets: 4,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete conservation" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});


    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление
    $("#createConservationBtn").click(function () {
        axios.post("/TechnicalEquipmentInfo/CreateConservation", {
            technicalEquipmentId: +$("#technicalEquipmentId").val(),
            date: moment($("#dateConservation").val(), 'DD.MM.YYYY').toDate(),
            validity: moment($("#datePeriod").val(), 'DD.MM.YYYY').toDate(),
            nameOfWorks: $("#nameOfWork").val(),
            employee: $("#employee").val(),
            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление
    $(document).on("click", ".delete.conservation", function () {
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
                axios.delete("TechnicalEquipmentInfo/DeleteConservation?id=" + id).then(function () {
                    tableConservations.draw(false)
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