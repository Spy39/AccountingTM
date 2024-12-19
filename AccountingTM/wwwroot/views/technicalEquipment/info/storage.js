//Вывод данных о хранении технического средства в таблицу

    let tableStorages = new DataTable('#storageTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/TechnicalEquipmentInfo/GetAllStorage', {
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
            action: () => tableStorages.draw(false)
        }
    ],
        drawCallback: function () {
            if ($('[data-bs-toggle="tooltip"]')) {
                setTimeout(() => {
                    $('[data-bs-toggle="tooltip"]').tooltip();
                }, 1000)
            }
        },
    columnDefs: [
        {
            targets: 0,
            data: 'acceptance',
            render: (data, type, row, meta) => {
                return data ? dayjs(data).format("DD.MM.YYYY") : "";
            }
        },
        {
            targets: 1,
            data: 'removal',
            render: (data, type, row, meta) => {
                return data ? dayjs(data).format("DD.MM.YYYY") : "";
            }
        },
        {
            targets: 2,
            data: 'storageConditions',
        },
        {
            targets: 3,
            data: 'typeOfStorage',
        },
        {
            targets: 4,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete storage" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});


    $("#search-btn").click(function () {
        tableStorages.ajax.reload()
    })

    //Добавление
    $("#createStorageBtn").click(function () {
        axios.post("/TechnicalEquipmentInfo/CreateStorage", {
            technicalEquipmentId: +$("#technicalEquipmentId").val(),
            acceptance: moment($("#dateAcceptance").val(), 'DD.MM.YYYY').toDate(),
            removal: moment($("#dateRemoval").val(), 'DD.MM.YYYY').toDate(),
            storageConditions: $("#storageConditions").val(),
            typeOfStorage: $("#typeOfStorage").val(),
            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление
    $(document).on("click", ".delete.storage", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Запись ${name} будет удалена!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("/TechnicalEquipmentInfo/DeleteStorage?id=" + id).then(function () {
                    tableStorages.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('Запись удалена!')
                })
            }
        });
    })