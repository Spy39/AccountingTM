//Информация о расходных материалах

//Вывод данных в таблицу
let tableConsumables = new DataTable('#transactionHistoryTable', {
    paging: true,
    serverSide: true,
    bAutoWidth: false,
    //aoColumns: [
    //    //{ sWidth: '19%' },
    //    //{ sWidth: '19%' },
    //    //{ sWidth: '19%' },
    //    //{ sWidth: '19%' },
    //    //{ sWidth: '11%' },
    //    //{ sWidth: '6%' },
    //    //{ sWidth: '7%' }
    //],
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        filter.consumableId = +$("#consumableId").val();
        axios.get('/ConsumableHistory/GetAll', {
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
            action: () => tableConsumables.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            targets: 0,
            data: 'dateOfOperation',
            render: (data, type, row, meta) => {
                return data ? dayjs(data).format("DD.MM.YYYY HH:mm") : "";
            }

        },
        {
            targets: 1,
            data: 'isSupply',
            render: (data, type, row, meta) => {
                return data ? "Приход" : "Списание";
            }
        },
        {
            targets: 2,
            data: 'quantity',
        },
        {
            targets: 3,
            data: 'employee.name',
        },
        {
            targets: 4,
            data: 'comment',
        },
        {
            targets: 5,
            data: null,
            render: (data, type, row, meta) => {
                return `<button class="btn btn-danger delete consumable" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});
$("#search-btn").click(function () {
    tableConsumables.ajax.reload()
})

//Поставка расходного материала
$("#supplyBtn").click(function () {
    axios.post("/ConsumableHistory/Supply", {
        consumableId: +$("#consumableId").val(),
        typeConsumableId: +$("#typeConsumableId").val(),
        brandId: +$("#brandId").val(),
        model: $("#model").val(),
        locationId: +$("#locationId").val(),
        unitId: +$("#unitId").val(),
        quantity: +$("#quantity").val(),
        comment: $("#comment").val(),
        isType: $("#isType").val(),
    }).then(function () {
        location.reload()
    })
})

//Списание расходного материала
$("#writeOffBtn").click(function () {
    axios.post("/ConsumableHistory/WriteOff", {
        consumableId: +$("#consumableId").val(),
        typeConsumableId: +$("#typeConsumableId").val(),
        brandId: +$("#brandId").val(),
        model: $("#model").val(),
        locationId: +$("#locationId").val(),
        unitId: +$("#unitId").val(),
        quantity: +$("#quantityWriteOff").val(),
        comment: $("#comment").val(),
        isType: $("#isType").val(),
    }).then(function () {
        location.reload()
    })
})

//Удаление из истории
$(document).on("click", ".delete.consumable", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Данная запись будет удалена!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("/ConsumableHistory/Delete?id=" + id).then(function () {
                tableConsumables.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Запись успешно удалена!`)
            })
        }
    });
})

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