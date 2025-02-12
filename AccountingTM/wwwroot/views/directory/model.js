//Модели технических средств
const initTableModels = () => {
    
    let tableModels = new DataTable('#models', {
        paging: true,
        serverSide: true,
        bAutoWidth: false,
        aoColumns: [
            { sWidth: '20%' },
            { sWidth: '70%' },
            { sWidth: '10%' }
        ],
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            filter.searchQuery = $("#search-input-model").val()
            axios.get('/Model/GetAll', {
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
                action: () => tableModels.draw(false)
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
                data: 'description',
            },
            {
                targets: 2,
                data: null,
                render: (data, type, row, meta) => {
                    return `<button class="btn btn-secondary edit model" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete model" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-model").click(function () {
        tableModels.ajax.reload()
    })

    //Добавление
    $("#createModelBtn").click(function () {
        axios.post("Model/Create", {
            name: $("#model").val(),
            description: $("#modelDescription").val(),
        }).then(function () {
            tableModels.draw(false)
            $("#createModel").modal("hide");
        })
    })

    $("#createModel").on('hidden.bs.modal', () => {
        $("#model").val("");
        $("#modelDescription").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.model", function () {
        let id = this.dataset.id;
        axios.get('/Model/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const model = response.data;
            $("#editNameModel").val(model.name);
            $("#editModelId").val(model.id);
            $("#editModelDescription").val(model.description);
            $("#editModel").modal("show");
        })
    })

    $("#editModelBtn").click(function () {
        axios.post("Model/Update", {
            id: +$("#editModelId").val(),
            name: $("#editNameModel").val(),
            description: $("#editModelDescription").val(),
        }).then(function () {
            tableModels.draw(false)
            $("#editModel").modal("hide");
        })
    })

    $("#editModel").on('hidden.bs.modal', () => {
        $("#editNameModel").val("");
        $("#editModelDescription").val("");
        $("#editModelId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.model", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Модель ${name} будет удалена!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Model/Delete?id=" + id).then(function () {
                    tableModels.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Модель ${name} успешно удалена!`)
                })
            }
        });
    })
}

export default initTableModels;