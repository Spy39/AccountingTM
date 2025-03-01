//Типы технических средств
const initTableTypes = () => {
    
    let tableTypes = new DataTable('#types', {
        paging: true,
        serverSide: true,
        responsive: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            filter.searchQuery = $("#search-input-typeEquipment").val()
            axios.get('/TypeEquipment/GetAll', {
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
                action: () => tableTypes.draw(false)
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
                    return `<button class="btn btn-secondary edit typeEquipment" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete typeEquipment" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-typeEquipment").click(function () {
        tableTypes.ajax.reload()
    })

    //Добавление
    $("#createTypeEquipmentBtn").click(function () {
        axios.post("TypeEquipment/Create", {
            name: $("#typeEquipment").val(),
        }).then(function () {
            tableTypes.draw(false)
            $("#createType").modal("hide");
        })
    })

    $("#createType").on('hidden.bs.modal', () => {
        $("#typeEquipment").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.typeEquipment", function () {
        let id = this.dataset.id;
        axios.get('/TypeEquipment/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const typeEquipment = response.data;
            $("#editNameTypeEquipment").val(typeEquipment.name);
            $("#editTypeEquipmentId").val(typeEquipment.id);
            $("#editTypeEquipment").modal("show");
        })
    })

    $("#editTypeEquipmentBtn").click(function () {
        axios.post("TypeEquipment/Update", {
            id: +$("#editTypeEquipmentId").val(),
            name: $("#editNameTypeEquipment").val(),
        }).then(function () {
            tableTypes.draw(false)
            $("#editTypeEquipment").modal("hide");
        })
    })

    $("#editTypeEquipment").on('hidden.bs.modal', () => {
        $("#editNameTypeEquipment").val("");
        $("#editTypeEquipmentId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.typeEquipment", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Тип технического средства ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("TypeEquipment/Delete?id=" + id).then(function () {
                    tableTypes.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Тип технического средства ${name} успешно удален!`)
                })
            }
        });
    })
}

export default initTableTypes;