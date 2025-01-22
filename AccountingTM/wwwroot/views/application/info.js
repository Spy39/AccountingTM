//Заявки - Информация

//Вывод данных о техничесих средствах в таблицу
let tabletechnicalEquipments = new DataTable('#technicalEquipmentTable', {
    paging: true,
    serverSide: true,
    bAutoWidth: false,
    select: {
        selector: 'td:first-child',
        style: 'multi'
    },
    fixedColumns: {
        start: 2
    },
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        filter.isWithoutSet = true;
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
            action: () => tableClients.draw(false)
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
            orderable: false,
            render: DataTable.render.select(),
            targets: 0
        },
        {
            targets: 1,
            data: 'type.name',
        },
        {
            targets: 2,
            data: 'brand.name',
        },
        {
            targets: 3,
            data: 'model',
        },
        {
            targets: 4,
            data: 'serialNumber',
        },
        {
            targets: 5,
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
            targets: 6,
            data: 'employee',
            render: (data, type, row, meta) => {
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
            }
        },
        {
            targets: 7,
            data: 'location.name',
        },
        {
            targets: 8,
            data: ``,
            render: (data, type, row, meta) => {
                return ``;
            }
        }]
});

//Добавление к ТС
$("#create-btn").click(function () {
    axios.post("/Set/CreateCompoundSet", {
        setId: +$("#SetId").val(),
        technicalEquipmentIds: tabletechnicalEquipments.rows({ selected: true }).data().map(x => x.id).toArray()
    }).then(function () {
        location.reload()
    })
})


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