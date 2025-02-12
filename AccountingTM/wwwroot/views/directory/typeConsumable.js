//Типы расходных материалов
const initTableConsumables = () => {

    let tableTypeConsumables = new DataTable('#typeConsumables', {
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
            filter.searchQuery = $("#search-input-typeConsumable").val()
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
                action: () => tableTypeConsumables.draw(false)
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
                    return `<button class="btn btn-secondary edit typeConsumable" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete typeConsumable" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-typeConsumable").click(function () {
        tableTypeConsumables.ajax.reload()
    })

    //Добавление
    $("#createTypeConsumableBtn").click(function () {
        axios.post("TypeConsumable/Create", {
            name: $("#typeConsumable").val(),
        }).then(function () {
            tableTypeConsumables.draw(false)
            $("#createTypeConsumable").modal("hide");
        })
    })

    $("#createTypeConsumable").on('hidden.bs.modal', () => {
        $("#typeConsumable").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.typeConsumable", function () {
        let id = this.dataset.id;
        axios.get('/TypeConsumable/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const typeConsumable = response.data;
            $("#editNameTypeConsumable").val(typeConsumable.name);
            $("#editTypeConsumableId").val(typeConsumable.id);
            $("#editTypeConsumable").modal("show");
        })
    })

    $("#editTypeConsumableBtn").click(function () {
        axios.post("TypeConsumable/Update", {
            id: +$("#editTypeConsumableId").val(),
            name: $("#editNameTypeConsumable").val(),
        }).then(function () {
            tableTypeConsumables.draw(false)
            $("#editTypeConsumable").modal("hide");
        })
    })

    $("#editTypeConsumable").on('hidden.bs.modal', () => {
        $("#editNameTypeConsumable").val("");
        $("#editTypeConsumableId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.typeConsumable", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Тип расходного материала ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("TypeConsumable/Delete?id=" + id).then(function () {
                    tableTypeConsumables.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Тип расходного материала ${name} успешно удален!`)
                })
            }
        });
    })
}

export default initTableConsumables;