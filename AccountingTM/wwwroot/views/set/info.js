//Информация о комплекте

//Вывод данных в таблицу с составом комплекта
let tableCompoundSets = new DataTable('#compoundSetTable', {
    paging: true,
    serverSide: true,

    ajax: function (data, callback, settings) {
        var filter = {};
        filter.setId = +$("#SetId").val();
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
    drawCallback: function () {
        if ($('[data-bs-toggle="tooltip"]')) {
            setTimeout(() => {
                $('[data-bs-toggle="tooltip"]').tooltip();
            },1000)
        }
    },
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
            data: 'location.name',
        },
        {
            targets: 6,
            data: null,
            render: (data, type, row, meta) => {
                return `<a href="/technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-solid fa-circle-info"></i></a>
                        <button class="btn btn-danger delete compoundSet" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});

//Вывод данных в таблицу с изменениями над комплектом
let tableChangesSets = new DataTable('#changesSetTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        filter.setId = +$("#SetId").val();
        axios.get('/Set/GetAllHistoryOfChangesSet', {
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
            action: () => tableChangesSets.draw(false)
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
            data: 'dateOfOperation',
        },
        {
            targets: 1,
            data: 'typeOfOperation',
        },
        {
            targets: 2,
            data: 'comment',
        },
        {
            targets: 3,
            data: 'emplouee.fullName',
        },
        {
            targets: 4,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete changesCompoundSet" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});
$("#search-btn").click(function () {
    tableConsumables.ajax.reload()
})

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


//Добавление в состав комплекта
$("#create-btn").click(function () {
    axios.post("/Set/CreateCompoundSet", {
        setId: +$("#SetId").val(),
        technicalEquipmentIds: tabletechnicalEquipments.rows({ selected: true }).data().map(x => x.id).toArray()
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
            axios.delete("/Set/DeleteCompoundSet?id=" + id).then(function () {
                tableCompoundSets.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`${name} успешно удален!`)
            })
        }
    });
})

//Select

//Помещение
$("#location").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Помещение',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Location/GetAll", { params: filter }).then(function (result) {

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

//Ответственный
$("#employee").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Ответственный',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("Employee/GetAll", { params: filter }).then(function (result) {

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
    templateResult: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,
    templateSelection: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,

})