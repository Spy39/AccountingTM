//Бренды технических средств
const initTableBrands = () => {
    
    let tableBrands = new DataTable('#brands', {
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
            filter.searchQuery = $("#search-input-brand").val()
            axios.get('/Brand/GetAll', {
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
                action: () => tableBrands.draw(false)
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
                    return `<button class="btn btn-secondary edit brand" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete brand" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-brand").click(function () {
        tableBrands.ajax.reload()
    })

    //Добавление
    $("#createBrandBtn").click(function () {
        axios.post("Brand/Create", {
            name: $("#brand").val(),
        }).then(function () {
            tableBrands.draw(false)
            $("#createBrand").modal("hide");
        })
    })

    $("#createBrand").on('hidden.bs.modal', () => {
        $("#brand").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.brand", function () {
        let id = this.dataset.id;
        axios.get('/Brand/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const brand = response.data;
            $("#editNameBrand").val(brand.name);
            $("#editBrandId").val(brand.id);
            $("#editBrand").modal("show");
        })
    })

    $("#editBrandBtn").click(function () {
        axios.post("Brand/Update", {
            id: +$("#editBrandId").val(),
            name: $("#editNameBrand").val(),
        }).then(function () {
            tableBrands.draw(false)
            $("#editBrand").modal("hide");
        })
    })

    $("#editBrand").on('hidden.bs.modal', () => {
        $("#editNameBrand").val("");
        $("#editBrandId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.brand", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Бренд ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Brand/Delete?id=" + id).then(function () {
                    tableBrands.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Бренд ${name} успешно удален!`)
                })
            }
        });
    })
}

export default initTableBrands;