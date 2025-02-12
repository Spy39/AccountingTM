//Категории заявок
const initTableCategories = () => {

    let tableCategories = new DataTable('#categories', {
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
            filter.searchQuery = $("#search-input-category").val()
            axios.get('/Category/GetAll', {
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
                action: () => tableCategories.draw(false)
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
                    return `<button class="btn btn-secondary edit category" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete category" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-category").click(function () {
        tableCategories.ajax.reload()
    })

    //Добавление
    $("#createCategoryBtn").click(function () {
        axios.post("Category/Create", {
            name: $("#category").val(),
        }).then(function () {
            tableCategories.draw(false)
            $("#createCategory").modal("hide");
        })
    })

    $("#createCategory").on('hidden.bs.modal', () => {
        $("#category").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.category", function () {
        let id = this.dataset.id;
        axios.get('/Category/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const category = response.data;
            $("#editNameCategory").val(category.name);
            $("#editCategoryId").val(category.id);
            $("#editCategory").modal("show");
        })
    })

    $("#editCategoryBtn").click(function () {
        axios.post("Category/Update", {
            id: +$("#editCategoryId").val(),
            name: $("#editNameCategory").val(),
        }).then(function () {
            tableCategories.draw(false)
            $("#editCategory").modal("hide");
        })
    })

    $("#editCategory").on('hidden.bs.modal', () => {
        $("#editNameCategory").val("");
        $("#editCategoryId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.category", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Категория ${name} будет удалена!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Category/Delete?id=" + id).then(function () {
                    tableCategories.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Категория ${name} успешно удалена!`)
                })
            }
        });
    })
}

export default initTableCategories;