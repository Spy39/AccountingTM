
//Вывод данных о приеме и передаче технического средства в таблицу
    let tableReceptionAndTransmissions = new DataTable('#receptionAndTransmissionTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/TechnicalEquipmentInfo/GetAllReceptionAndTransmission', {
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
            action: () => tableReceptionAndTransmissions.draw(false)
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
            targets: 2,
            data: 'base',
        },
        {
            targets: 3,
            data: 'passed',
        },
        {
            targets: 4,
            data: 'accepted',
        },
        {
            targets: 5,
            data: 'note',
        },
        {
            targets: 6,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete receptionAndTransmission" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});


    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление нового технического средства
    $("#createReceptionAndTransmissionBtn").click(function () {
        axios.post("/TechnicalEquipmentInfo/CreateReceptionAndTransmission", {
            technicalEquipmentId: +$("#technicalEquipmentId").val(),
            date: moment($("#dateReceptionAndTransmission").val(), 'DD.MM.YYYY').toDate(),
            productCondition: $("#state").val(),
            base: $("#base").val(),
            passed: $("#passed").val(),
            accepted: $("#accepted").val(),
            note: $("#note").val(),
            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление технического средства
    $(document).on("click", ".delete.receptionAndTransmission", function () {
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
                axios.delete("TechnicalEquipmentInfo/DeleteReceptionAndTransmission?id=" + id).then(function () {
                    tableReceptionAndTransmissions.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('ТС успешно удален!')
                })
            }
        });
    })