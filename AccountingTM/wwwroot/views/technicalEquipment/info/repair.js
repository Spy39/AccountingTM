﻿//Вывод сведений о ремонте технического средства в таблицу

let tableRepairs = new DataTable('#repairTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    ajax: function (data, callback, settings) {
    var filter = {};
    filter.searchQuery = $("#search-input").val()
    filter.maxResultCount = data.length || 10;
    filter.skipCount = data.start;
    filter.technicalEquipmentId = +$("#technicalEquipmentId").val();
    axios.get('/TechnicalEquipmentInfo/GetAllRepair', {
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
        action: () => tableRepairs.draw(false)
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
        data: 'company',
    },
    {
        targets: 2,
        data: 'reasonForRepair',
    },
    {
        targets: 3,
        data: 'repairInformation',
    },
    {
        targets: 4,
        data: null,
        orderable: false,
        searchable: false,
        className: 'text-nowrap',
        width: '1%',
        render: (data, type, row, meta) => {
            return `<button class="btn btn-danger delete repair" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
        }
    }]
});


//Добавление
$("#createRepairBtn").click(function () {
    axios.post("/TechnicalEquipmentInfo/CreateRepair", {
        technicalEquipmentId: +$("#technicalEquipmentId").val(),
        date: moment($("#dateRepair").val(), 'DD.MM.YYYY').toDate(),
        company: $("#company").val(),
        reasonForRepair: $("#reasonForRepair").val(),
        repairInformation: $("#repairInformation").val()
    }).then(function () {
        tableRepairs.draw(false);
        $("#addRepairModal").modal("hide")
    })
})
$("#addRepairModal").on("hide.bs.modal", function () {
    $("#dateRepair").val('');
    $("#company").val('');
    $("#reasonForRepair").val('');
    $("#repairInformation").val('');
})


//Удаление
$(document).on("click", ".delete.repair", function () {
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
            axios.delete("/TechnicalEquipmentInfo/DeleteRepair?id=" + id).then(function () {
                tableRepairs.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success('Запись удалена!')
            })
        }
    });
})