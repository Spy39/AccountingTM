
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
        initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
        columnDefs: [
            {
                targets: 0,
                data: 'date',
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
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-regular fa-address-card"></i></a>
                            <button class="btn btn-danger delete disposalInformation" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });


    $("#search-btn").click(function () {
        tableClients.ajax.reload()
    })

    //Добавление сведений об утилизации
    $("#createDisposalInformationBtn").click(function () {
        axios.post("TechnicalEquipmentInfo/CreateDisposalInformation", {
            date: $("#date").val(),
            description: $("#description").val(),
            note: $("#note").val(),
        }).then(function () {
            location.reload()
        })
    })

    //Удаление технического средства
    $(document).on("click", ".delete.disposalInformation", function () {
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
                axios.delete("TechnicalEquipmentInfo/DeleteDisposalInformation?id=" + id).then(function () {
                    tableClients.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success('ТС успешно удален!')
                })
            }
        });
    })