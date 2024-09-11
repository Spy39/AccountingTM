
//Вывод сведений о ремонте технического средства в таблицу
    let tableRepairs = new DataTable('#repairTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/TechnicalEquipmentInfo/GetAllRepair', {
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
            action: () => tableRepairs.draw(false)
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
            data: 'company',
        },
        {
            targets: 2,
            data: 'reasonForRepair',
        },
        {
            targets: 3,
            data: 'repairInformation',
        },
        {
            targets: 4,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete repair" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});


    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление нового технического средства
    $("#createRepairBtn").click(function () {
        axios.post("TechnicalEquipmentInfo/CreateRepair", {
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
    $(document).on("click", ".delete.repair", function () {
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
                axios.delete("TechnicalEquipmentInfo/DeleteRepair?id=" + id).then(function () {
                    tableClients.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('ТС успешно удален!')
                })
            }
        });
    })