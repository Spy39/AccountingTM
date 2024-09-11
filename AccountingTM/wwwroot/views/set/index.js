//Комплекты

//Вывод данных в таблицу комплекты
let tableSets = new DataTable('#setTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/Set/GetAllSet', {
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
            action: () => tableSets.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            targets: 0,
            data: 'name',
        },
        {
            targets: 1,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete set" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});

//Вывод данных в таблицу с составом комплекта
let tableCompoundSets = new DataTable('#compoundSetTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/Set/GetAllCompoundSet', {
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
            action: () => tableCompoundSets.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            targets: 0,
            data: 'typeEquipment.name',
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
            data: 'location.name',
        },
        {
            targets: 6,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete compoundSet" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});

    $("#search-btn").click(function () {
        tableSets.ajax.reload()
    })

//Добавление нового комплекта
$("#create-btn").click(function () {
    axios.post("Set/CreateSet", {
        typeConsumableId: +$("#typeEquipment").val(),
        brandId: +$("#brand").val(),
        model: $("#model").val(),
        serialNumber: $("#serialNumber").val(),
        employeeId: +$("#state").val(),
        locationId: +$("#location").val(),

        isDeleted: false
    }).then(function () {
        location.reload()
    })
})

//Удаление комплекта
$(document).on("click", ".delete.set", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Комплект ${name} будет удален!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("Set/DeleteSet?id=" + id).then(function () {
                tableClients.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Комплект ${name} успешно удален!`)
            })
        }
    });
})

    //Добавление в состав комплекта
    $("#create-btn").click(function () {
        axios.post("Set/CreateCompoundSet", {
            typeConsumableId: +$("#typeEquipment").val(),
            brandId: +$("#brand").val(),
            model: $("#model").val(),
            serialNumber: $("#serialNumber").val(),
            employeeId: +$("#state").val(),
            locationId: +$("#location").val(),

            isDeleted: false
        }).then(function () {
            location.reload()
        })
    })

    //Удаление из состава комплекта
    $(document).on("click", ".delete.compoundSet", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `${name} будет удален из комплекта!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Set/DeleteCompoundSet?id=" + id).then(function () {
                    tableClients.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`${name} успешно удален!`)
                })
            }
        });
    })