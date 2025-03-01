//Вывод данных о приеме и передаче технического средства в таблицу

let tableReceptionAndTransmissions = new DataTable('#receptionAndTransmissionTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    ajax: function (data, callback, settings) {
    var filter = {};
    filter.searchQuery = $("#search-input").val()
    filter.maxResultCount = data.length || 10;
    filter.skipCount = data.start;
    filter.technicalEquipmentId = +$("#technicalEquipmentId").val();
    axios.get('/TechnicalEquipmentInfo/GetAllReceptionAndTransmission', {
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
        action: () => tableReceptionAndTransmissions.draw(false)
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
        data: 'date',
        render: (data, type, row, meta) => {
            return data ? dayjs(data).format("DD.MM.YYYY") : "";
        }
    },
    {
        targets: 1,
        data: 'productConditionText',
    },
    {
        targets: 2,
        data: 'base',
    },
    {
        targets: 3,
        data: 'passed',
    },
    {
        targets: 4,
        data: 'accepted',
    },
    {
        targets: 5,
        data: 'note',
    },
    {
        targets: 6,
        data: null,
        orderable: false,
        searchable: false,
        className: 'text-nowrap',
        width: '1%',
        render: (data, type, row, meta) => {
            return `<button class="btn btn-danger delete receptionAndTransmission" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
        }
    }]
});


//Добавление
$("#createReceptionAndTransmissionBtn").click(function () {
    axios.post("/TechnicalEquipmentInfo/CreateReceptionAndTransmission", {
        technicalEquipmentId: +$("#technicalEquipmentId").val(),
        date: moment($("#dateReceptionAndTransmission").val(), 'DD.MM.YYYY').toDate(),
        productCondition: +$("#state").val(),
        base: $("#base").val(),
        passed: $("#passed").val(),
        accepted: $("#accepted").val(),
        note: $("#note").val()
    }).then(function () {
        tableReceptionAndTransmissions.draw(false);
        $("#addReceptionAndTransmissionModal").modal("hide")
    })
})
$("#addReceptionAndTransmissionModal").on("hide.bs.modal", function () {
    $("#dateReceptionAndTransmission").val('');
    $("#state").val('');
    $("#base").val('');
    $("#accepted").val('');
    $("#note").val('');
})


//Удаление технического средства
$(document).on("click", ".delete.receptionAndTransmission", function () {
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
            axios.delete("/TechnicalEquipmentInfo/DeleteReceptionAndTransmission?id=" + id).then(function () {
                tableReceptionAndTransmissions.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success('Запись удалена!')
            })
        }
    });
})