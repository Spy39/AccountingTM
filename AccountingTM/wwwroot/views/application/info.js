//Заявки - Информация

let tableTechnicalEquipment = new DataTable('#technicalEquipmentTable', {
    paging: true,
    serverSide: true,
    bAutoWidth: false,
    aoColumns: [
        { sWidth: '14%' },
        { sWidth: '14%' },
        { sWidth: '14%' },
        { sWidth: '14%' },
        { sWidth: '10%' },
        { sWidth: '17%' },
        { sWidth: '10%' },
        { sWidth: '7%' }
    ],
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/TechnicalEquipment/GetAll', {
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
            targets: 5,
            data: 'employee',
            render: (data, type, row, meta) => {
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
            }
        },
        {
            targets: 6,
            data: 'location.name',
        },
        {
            targets: 7,
            data: null,
            render: (data, type, row, meta) => {
                return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Прикрепить к заявке"><i class="fa-solid fa-circle-plus"></i></a>`;
            }
        }]
});

//Добавление комментария
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

//Удаление комментария
$(document).on("click", ".delete", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Комментарий  будет удален!`,
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
                toastr.success('Комментарий успешно удален!')
            })
        }
    });
})