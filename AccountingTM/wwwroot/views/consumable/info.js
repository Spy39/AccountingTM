// Вывод данных в таблицу истории операций
// Вывод данных в таблицу истории операций
let tableConsumables = new DataTable('#transactionHistoryTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        const filterOperationValue = $("#filterOperationValue").val();
        var filter = {};
        filter.searchQuery = $("#search-input").val(),
            filter.maxResultCount = data.length || 10,
            filter.skipCount = data.start,
            filter.consumableId = +$("#consumableId").val();
        if (filterOperationValue) {
            filter.isSupply = filterOperationValue == "supply"
        }

        axios.get('/ConsumableHistory/GetAll', {
            params: filter
        })
            .then(function (result) {
                callback({
                    recordsTotal: result.data.totalCount,
                    recordsFiltered: result.data.totalCount,
                    data: result.data.items
                });
            })
            .catch(function (error) {
                console.error("Ошибка загрузки данных:", error);
                toastr.error("Ошибка загрузки данных");
            });
    },
    buttons: [
        {
            name: 'refresh',
            text: '<i class="fas fa-redo-alt"></i>',
            action: () => tableConsumables.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    rowCallback: function (row, data) {
        if (data.isSupply) {
            $(row).addClass('table-success'); // Зеленый цвет для пополнения
        } else {
            $(row).addClass('table-danger'); // Красный цвет для списания
        }
    },
    columnDefs: [
        { targets: 0, data: 'dateOfOperation', render: (data) => data ? dayjs(data).format("DD.MM.YYYY HH:mm") : "Нет данных" },
        { targets: 1, data: 'isSupply', render: (data) => data ? "Пополнение" : "Списание" },
        { targets: 2, data: 'quantity' },
        { targets: 3, data: 'employee.fullName', defaultContent: "—" },
        {
            targets: 4,
            data: 'technicalEquipment',
            render: (data) => {
                if (!data) return "Не привязано";
                const brand = data.brand?.name || "Неизвестный бренд";
                const model = data.model?.name || "Неизвестная модель";
                const serial = data.serialNumber || "Без серийного номера";
                return `${brand} ${model} (${serial})`;
            }
        },
        { targets: 5, data: 'comment', defaultContent: "—" },
        {
            targets: 6,
            data: null,
            orderable: false,
            searchable: false,
            className: 'text-center',
            render: (data) => `<button class="btn btn-danger delete consumable" data-id="${data.id}" title="Удалить">
                                  <i class="fa-solid fa-trash"></i>
                              </button>`
        }
    ]
});


// Фильтр по кнопке
$("#search-btn").click(function () {
    tableConsumables.ajax.reload();
});

// Обработчик клика для выбора фильтра по типу операции
$(document).on("click", ".dropdown-menu .dropdown-item", function (e) {
    e.preventDefault();
    let selectedType = $(this).data("type"); // "supply" или "writeoff"
    $("#filterOperationValue").val(selectedType);
    // Обновляем текст кнопки, чтобы показать выбранный фильтр
    $("#btnGroupDrop1").html('<i class="fa-solid fa-filter me-1"></i> ' + $(this).text());
    tableConsumables.ajax.reload();
});

//пополнение
function handleSupply() {
    const consumableId = +$("#consumableId").val();
    const quantity = +$("#quantitySupply").val();
    const comment = $("#commentSupply").val().trim();
    if (!consumableId || isNaN(consumableId)) {
        toastr.warning("Ошибка: ID расходного материала пустой");
        return;
    }
    if (!quantity || quantity <= 0) {
        toastr.warning("Ошибка: Некорректное количество");
        return;
    }

    axios.post("/ConsumableHistory/Supply", {
        consumableId: consumableId,
        quantity: quantity,
        comment: comment
    }, {
        headers: {
            'Content-Type': 'application/json'
        }

    })
        .then(() => {
            toastr.success("Расходный материал успешно пополнен!");
            $("#SupplyConsumableModal").modal("hide");
            setTimeout(() => tableConsumables.ajax.reload(), 500);
        })
        .catch((error) => {
            toastr.error("Ошибка при пополнении расходного материала");
            console.error(error);
        });
}
//списание
function handleWriteOff() {
    const comment = ($("#commentWriteOff").val() || "").trim();
    let quantity = +$("#quantityWriteOff").val();
    if (quantity <= 0) {
        toastr.warning("Количество должно быть больше 0");
        return;
    }

    axios.post("/ConsumableHistory/WriteOff", {
        consumableId: +$("#consumableId").val(),
        technicalEquipmentId: +$("#technicalEquipment").val(),
        quantity: quantity,
        comment: comment
    }, {
        headers: { 'Content-Type': 'application/json' }
    })
        .then(() => {
            toastr.success("Расходный материал успешно списан!");
            $("#WriteOffConsumableModal").modal("hide");
            setTimeout(() => tableConsumables.ajax.reload(), 500);
        })
        .catch((error) => {
            toastr.error("Ошибка при списании расходного материала");
            console.error(error);
        });
}

// Назначение обработчиков на кнопки пополнения и списания
$("#supplyBtn").click(handleSupply);
$("#writeOffBtn").click(handleWriteOff);

// Обработчик клика на редактирование расходного материала
$(document).on("click", ".edit.consumable", function () {
    let id = $(this).data("id"); // Получаем ID расходника

    axios.get('/ConsumableHistory/Get', { params: { id } })
        .then(function (response) {
            const c = response.data;

            // Заполняем поля в модальном окне
            $("#editConsumableId").val(c.id);

            // Для select2: brand, typeConsumable, location, unit
            $("#brand").val(c.brandId).trigger("change");
            $("#typeConsumable").val(c.typeConsumableId).trigger("change");
            $("#location").val(c.locationId).trigger("change");
            $("#unit").val(c.unitId).trigger("change");

            $("#model").val(c.model);

            $("#EditConsumableModal").modal("show");
        })
        .catch(function (error) {
            console.error("Ошибка при загрузке расходного материала:", error);
            toastr.error("Не удалось загрузить данные расходного материала.");
        });
});

// Обработчик кнопки "Сохранить" в модальном окне редактирования
$("#editConsumableBtn").click(function () {
    axios.post("/ConsumableHistory/Update", {
        id: +$("#editConsumableId").val(),
        brandId: +$("#brand").val(),
        typeConsumableId: +$("#typeConsumable").val(),
        locationId: +$("#location").val(),
        unitId: +$("#unit").val(),
        model: $("#model").val()
    }, {
        headers: { 'Content-Type': 'application/json' }
    })
        .then(function () {
            toastr.success("Расходный материал успешно обновлён!");
            $("#EditConsumableModal").modal("hide");
            // Обновляем таблицу или перезагружаем страницу
            location.reload();
        })
        .catch(function (error) {
            console.error("Ошибка при обновлении расходного материала:", error);
            toastr.error("Ошибка при обновлении расходного материала.");
        });
});



// Удаление записи из истории
$(document).on("click", "#transactionHistoryTable tbody .delete.consumable", function () {
    let id = $(this).data("id");
    console.log("Удаление расходника с ID:", id); // DEBUG

    if (!id || isNaN(id)) {  // ДОБАВЛЕНА ПРОВЕРКА
        toastr.warning("Ошибка: ID расходника отсутствует!");
        return;
    }

    Swal.fire({
        title: "Вы уверены?",
        text: "Данная запись будет удалена!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет"
    }).then((result) => {
        if (result.isConfirmed) {
            axios.delete(`/ConsumableHistory/Delete?id=${id}`)
                .then(() => {
                    toastr.success("Запись успешно удалена!");
                    setTimeout(() => tableConsumables.ajax.reload(), 500);
                })
                .catch((error) => {
                    toastr.error("Ошибка при удалении записи");
                    console.error(error);
                });
        }
    });
});




//Select

$("#technicalEquipment").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Выберите ТС',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/TechnicalEquipment/GetAll", { params: filter }).then(function (result) {

                success({

                    results: result.data.items.map(x => {
                        const brand = x.brand?.name || "Неизвестный бренд";
                        const model = x.model?.name || "Неизвестная модель";
                        const serial = x.serialNumber || "Без серийного номера";
                        const name = `${brand} ${model} (${serial})`;
                        return { id: x.id, name }
                    }),
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
            axios.get("/Employee/GetAll", { params: filter }).then(function (result) {

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