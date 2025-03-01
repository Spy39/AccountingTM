//Единицы измерения
const initTableUnits = () => {
    
    let tableUnits = new DataTable('#units', {
        paging: true,
        serverSide: true,
        responsive: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            filter.searchQuery = $("#search-input-unit").val()
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
                action: () => tableUnits.draw(false)
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
                orderable: false,
                searchable: false,
                className: 'text-nowrap',
                width: '1%',
                render: (data, type, row, meta) => {
                    return `<button class="btn btn-secondary edit unit" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete unit" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-unit").click(function () {
        tableUnits.ajax.reload()
    })

    //Добавление
    $("#createUnitBtn").click(function () {
        axios.post("Unit/Create", {
            name: $("#unit").val(),
        }).then(function () {
            tableUnits.draw(false)
            $("#createUnits").modal("hide");
        })
    })

    $("#createUnits").on('hidden.bs.modal', () => {
        $("#unit").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.unit", function () {
        let id = this.dataset.id;
        axios.get('/Unit/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const unit = response.data;
            $("#editNameUnit").val(unit.name);
            $("#editUnitId").val(unit.id);
            $("#editUnit").modal("show");
        })
    })

    $("#editUnitBtn").click(function () {
        axios.post("Unit/Update", {
            id: +$("#editUnitId").val(),
            name: $("#editNameUnit").val(),
        }).then(function () {
            tableUnits.draw(false)
            $("#editUnit").modal("hide");
        })
    })

    $("#editUnit").on('hidden.bs.modal', () => {
        $("#editNameUnit").val("");
        $("#editUnitId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.unit", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Единица измерения ${name} будет удалена!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Unit/Delete?id=" + id).then(function () {
                    tableUnits.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Единица измерения ${name} успешно удалена!`)
                })
            }
        });
    })
}

export default initTableUnits;