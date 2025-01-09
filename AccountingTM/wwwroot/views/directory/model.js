//Модели технических средств
const initTableModels = () => {
    
    let tableModels = new DataTable('#models', {
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
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

    //Добавление
    $("#createModelBtn").click(function () {

        axios.post("Model/Create", {
            name: $("#model").val(),
            description: $("#modelDescription").val(),
        }).then(function () {
            location.reload()
        })
    })

    //Редактирование
    $(document).on("click", ".edit.model", function () {
        let id = this.dataset.id;
        axios.get('/Model/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const model = response.data;
            $("#model").val(model.name);
            $("#modelDescription").val(model.description);
            $("#createModel").modal("show");
        })
    })

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