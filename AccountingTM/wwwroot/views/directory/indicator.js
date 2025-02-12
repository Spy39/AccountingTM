//Показатели
const initTableIndicators = () => {

    let tableIndicators = new DataTable('#indicators', {
        paging: true,
        serverSide: true,
        bAutoWidth: false,
        aoColumns: [
            { sWidth: '80%' },
            { sWidth: '10%' }
        ],
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            filter.searchQuery = $("#search-input-indicator").val()
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
                action: () => tableIndicators.draw(false)
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
                    return `<button class="btn btn-secondary edit indicator" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                    <button class="btn btn-danger delete indicator" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-indicator").click(function () {
        tableIndicators.ajax.reload()
    })

    //Добавление
    $("#createIndicatorBtn").click(function () {
        axios.post("Indicator/Create", {
            name: $("#indicator").val(),
        }).then(function () {
            tableIndicators.draw(false)
            $("#createIndicator").modal("hide");
        })
    })

    $("#createIndicator").on('hidden.bs.modal', () => {
        $("#indicator").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.indicator", function () {
        let id = this.dataset.id;
        axios.get('/Indicator/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const indicator = response.data;
            $("#editNameIndicator").val(indicator.name);
            $("#editIndicatorId").val(indicator.id);
            $("#editIndicator").modal("show");
        })
    })

    $("#editIndicatorBtn").click(function () {
        axios.post("Indicator/Update", {
            id: +$("#editIndicatorId").val(),
            name: $("#editNameIndicator").val(),
        }).then(function () {
            tableIndicators.draw(false)
            $("#editIndicator").modal("hide");
        })
    })

    $("#editIndicator").on('hidden.bs.modal', () => {
        $("#editNameIndicator").val("");
        $("#editIndicatorId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.indicator", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Показатель ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Indicator/Delete?id=" + id).then(function () {
                    tableIndicators.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Показатель ${name} успешно удален!`)
                })
            }
        });
    })
}

export default initTableIndicators;