    //Вывод сведений об утилизации в таблицу
    let tableDisposalInformations = new DataTable('#disposalInformationTable', {
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.searchQuery = $("#search-input").val()
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/TechnicalEquipmentInfo/GetAllDisposalInformation', {
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
                action: () => tableDisposalInformations.draw(false)
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
                data: 'date',
                render: (data, type, row, meta) => {
                    return data ? dayjs(data).format("DD.MM.YYYY") : "";
                }
            },
            {
                targets: 1,
                data: 'description',
            },
            {
                targets: 2,
                data: 'note',
            },
            {
                targets: 3,
                data: null,
                render: (data, type, row, meta) => {
                    return `<button class="btn btn-danger delete disposalInformation" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });


    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление
    $("#createDisposalInformationBtn").click(function () {
        axios.post("/TechnicalEquipmentInfo/CreateDisposalInformation", {
            technicalEquipmentId: +$("#technicalEquipmentId").val(),
            date: moment($("#dateDisposalInformation").val(), 'DD.MM.YYYY').toDate(),
            description: $("#description").val(),
            note: $("#note").val(),
            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление
    $(document).on("click", ".delete.disposalInformation", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Запись ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("TechnicalEquipmentInfo/DeleteDisposalInformation?id=" + id).then(function () {
                    tableDisposalInformations.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('Запись удалена!')
                })
            }
        });
    })