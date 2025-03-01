//Расходные материалы

//Вывод данных в таблицу
let tableConsumables = new DataTable('#consumableTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    ajax: function (data, callback) {
        let filter = {
            searchQuery: $("#search-input").val(),
            maxResultCount: data.length || 10,
            skipCount: data.start
        };

        console.log("Отправка запроса с параметрами:", filter);

        axios.get('/Consumable/GetAll', { params: filter })
            .then(function (result) {
                if (!result.data || !result.data.items) {
                    throw new Error("Некорректные данные от сервера");
                }
                callback({
                    recordsTotal: result.data.totalCount,
                    recordsFiltered: result.data.totalCount,
                    data: result.data.items
                });
            })
            .catch(function (error) {
                console.error("Ошибка загрузки данных:", error);
                toastr.error("Ошибка загрузки данных", "Ошибка!");
            });
    },
    buttons: [
        {
            name: 'refresh',
            text: '<i class="fas fa-redo-alt"></i>',
            action: () => tableConsumables.draw(false)
        }
    ],
    // Подсветка строк по статусу
    rowCallback: function (row, data, index) {
        if (data.status === "В наличии") {
            $(row).addClass("table-success");
        } else if (data.status === "Отсутствует") {
            $(row).addClass("table-danger");
        } else if (data.status === "Малый запас") {
            $(row).addClass("table-warning");
        }
    },
    columnDefs: [
        { targets: 0, data: 'typeConsumable.name', defaultContent: "—" },
        { targets: 1, data: 'brand.name', defaultContent: "—" },
        { targets: 2, data: 'model', defaultContent: "—" },
        { targets: 3, data: 'location.name', defaultContent: "—" },
        { targets: 4, data: 'quantity', defaultContent: "0" },
        { targets: 5, data: 'unit.name', defaultContent: "—" },
        { targets: 6, data: 'status', defaultContent: "—" },
        {
            targets: 7,
            data: 'dateLatestAddition',
            render: (data) => data ? dayjs(data).format("DD.MM.YYYY HH:mm") : "Нет данных"
        },
        {
            targets: 8,
            data: null,
            orderable: false,
            searchable: false,
            className: 'text-nowrap',
            render: (data) => `
                <a href="/ConsumableHistory/${data.id}" class="btn btn-secondary" data-bs-toggle="tooltip" title="Информация о РМ">
                    <i class="fa-solid fa-circle-info"></i>
                </a>
                <button class="btn btn-danger delete-consumable" data-id="${data.id}" data-name="${data.model || "Неизвестно"}" data-bs-toggle="tooltip" title="Удалить">
                    <i class="fa-solid fa-trash"></i>
                </button>`
        }
    ]
});



// Поиск
$("#searchConsumableBtn").click(() => tableConsumables.ajax.reload());

// Добавление нового расходного материала
$("#create-btn").click(function () {
    axios.post("/Consumable/Create", {
        typeConsumableId: +$("#typeConsumable").val(),
        brandId: +$("#brand").val(),
        model: $("#model").val(),
        locationId: +$("#location").val(),
        unitId: +$("#unit").val()
    }).then(() => location.reload())
        .catch(error => {
            console.error("Ошибка добавления:", error);
            toastr.error("Ошибка добавления расходного материала");
        });
});

// Удаление расходного материала
$(document).on("click", ".delete-consumable", function () {
    let id = $(this).data("id");
    let name = $(this).data("name");

    Swal.fire({
        title: "Вы уверены?",
        text: `Расходный материал ${name} будет удален!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет"
    }).then((result) => {
        if (result.isConfirmed) {
            axios.delete(`/Consumable/Delete?id=${id}`)
                .then(() => {
                    tableConsumables.draw(false);
                    toastr.success(`Расходный материал ${name} успешно удален!`);
                })
                .catch(error => {
                    console.error("Ошибка удаления:", error);
                    toastr.error("Ошибка удаления расходного материала");
                });
        }
    });
});

//Select

//Тип расходного материала
$("#typeConsumable").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Наименование типа',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/TypeConsumable/GetAll", { params: filter }).then(function (result) {

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

//Бренд техниеского средства
$("#brand").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Наименование бренда',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Brand/GetAll", { params: filter }).then(function (result) {

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

//Единица измерения
$("#unit").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Единица измерения',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Unit/GetAll", { params: filter }).then(function (result) {

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