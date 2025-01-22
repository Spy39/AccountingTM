//Вывод данных о характеристиках технического средства в таблицу

let tableCharacteristics = new DataTable('#characteristic', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
    var filter = {};
    filter.searchQuery = $("#search-input").val()
    filter.maxResultCount = data.length || 10;
    filter.skipCount = data.start;
    filter.technicalEquipmentId = +$("#technicalEquipmentId").val();
    axios.get('/TechnicalEquipmentInfo/GetAllCharacteristic', {
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
        action: () => tableCharacteristics.draw(false)
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
        data: 'indicator.name',
    },
    {
        targets: 1,
        data: 'unit.name',
    },
    {
        targets: 2,
        data: 'meaning',
    },
    {
        targets: 3,
        data: null,
        render: (data, type, row, meta) => {
            return `<button class="btn btn-danger delete сharacteristic" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
        }
    }]
});


//Добавление
$("#createCharacteristicBtn").click(function () {
    axios.post("/TechnicalEquipmentInfo/CreateCharacteristic", {
        technicalEquipmentId: +$("#technicalEquipmentId").val(),
        indicatorId: +$("#indicator").val(),
        unitId: +$("#unit").val(),
        meaning: $("#meaning").val(),
    }).then(function () {
        tableCharacteristics.draw(false);
        $("#createCharacteristicModal").modal("hide")
    })
})
$("#createCharacteristicModal").on("hide.bs.modal", function () {
    $("#unit").val(null);
    $("#unit").trigger("change");
    $("#indicator").val(null);
    $("#indicator").trigger("change");
    $("#meaning").val('');
})


//Удаление
$(document).on("click", ".delete.сharacteristic", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Запись будет удалена!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("/TechnicalEquipmentInfo/DeleteCharacteristic?id=" + id).then(function () {
                tableCharacteristics.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Запись удалена!`)
            })
        }
    });
})


//Select
//Показатели
$("#indicator").select2({
    width: '100%',
    placeholder: 'Показатель',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Indicator/GetAll", { params: filter }).then(function (result) {

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

//Единицы измерения
$("#unit").select2({
    width: '100%',
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