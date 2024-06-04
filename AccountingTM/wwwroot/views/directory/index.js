$("#nav-page .nav-link").click(function () {
    const url = $(this).data("url")
    axios.get(url)
        .then(function (result) {
            $("#page-content").html(result.data)

            //Типы технических средств
            let tableTypes = new DataTable('#types', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/TypeEquipment/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Типы расходных материалоы
            let tableTypeConsumables = new DataTable('#typeConsumables', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/TypeConsumable/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Бренды технических средств
            let tableBrands = new DataTable('#brands', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Brand/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Обозначения комплектов
            let tableSets = new DataTable('#sets', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Set/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Помещения
            let tableLocations = new DataTable('#locations', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Location/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Сотрудники
            let tableEmployees = new DataTable('#employees', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Employee/GetAll', {
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
                        data: 'lastName',
                    },
                    {
                        targets: 1,
                        data: 'firstName',
                    },
                    {
                        targets: 2,
                        data: 'fatherName',
                    },
                    {
                        targets: 3,
                        data: 'position',
                    },
                    {
                        targets: 4,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Показатели
            let tableIndicators = new DataTable('#indicators', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Indicator/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Единицы измерения
            let tableUnits = new DataTable('#units', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Unit/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });

            //Категории заявок
            let tableCategoriess = new DataTable('#categories', {
                paging: true,
                serverSide: true,
                ajax: function (data, callback, settings) {
                    var filter = {};
                    filter.maxResultCount = data.length || 10;
                    filter.skipCount = data.start;
                    axios.get('/Category/GetAll', {
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
                        data: 'name',
                    },
                    {
                        targets: 1,
                        data: null,
                        render: (data, type, row, meta) => {
                            return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                        }
                    }]
            });
        })

})

//Добавление
//Brand
$(document).on("click", "#createBrandBtn", function () {
    
    axios.post("Brand/Create", {
        name: $("#brand").val(),

    }).then(function () {
        location.reload()
    })
})
//Type
$(document).on("click", "#createTypeEquipmentBtn", function () {

    axios.post("TypeEquipment/Create", {
        name: $("#typeEquipment").val(),

    }).then(function () {
        location.reload()
    })
})
//Category
$(document).on("click", "#createCategoryBtn", function () {

    axios.post("Category/Create", {
        name: $("#category").val(),

    }).then(function () {
        location.reload()
    })
})
//Employee
$(document).on("click", "#createEmployeeBtn", function () {

    axios.post("Employee/Create", {
        lastName: $("#lastName").val(),
        firstName: $("#firstName").val(),
        fatherName: $("#fatherName").val(),
        position: $("#position").val(),

    }).then(function () {
        location.reload()
    })
})
//Indicator
$(document).on("click", "#createIndicatorBtn", function () {

    axios.post("Indicator/Create", {
        name: $("#indicator").val(),

    }).then(function () {
        location.reload()
    })
})
//Location
$(document).on("click", "#createLocationBtn", function () {

    axios.post("Location/Create", {
        name: $("#location").val(),

    }).then(function () {
        location.reload()
    })
})
//Unit
$(document).on("click", "#createUnitBtn", function () {

    axios.post("Unit/Create", {
        name: $("#unit").val(),

    }).then(function () {
        location.reload()
    })
})
////Set
//$(document).on("click", "#createSetBtn", function () {

//    axios.post("Set/Create", {
//        name: $("#set").val(),

//    }).then(function () {
//        location.reload()
//    })
//})
//TypeConsumable
$(document).on("click", "#createTypeConsumableBtn", function () {

    axios.post("TypeConsumable/Create", {
        name: $("#typeConsumable").val(),

    }).then(function () {
        location.reload()
    })
})

//Удаление
$(document).on("click", ".delete", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `${name} будет удален!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("TypeEquipment/Delete?id=" + id).then(function () {
                tableClients.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Тип ${name} успешно удален!`)
            })
        }
    });
})

$(document).on("click", ".delete", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Бренд ${name} будет удален!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("Brand/Delete?id=" + id).then(function () {
                tableClients.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success('ТС успешно удален!')
            })
        }
    });
})

$('[data-url="Directory/TypeEquipment"]').trigger("click")