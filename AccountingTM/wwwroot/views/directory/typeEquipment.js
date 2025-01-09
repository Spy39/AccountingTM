//Типы технических средств
const initTableTypes = () => {
    
    let tableTypes = new DataTable('#types', {
        paging: true,
        serverSide: true,
        bAutoWidth: false,
        aoColumns: [
            { sWidth: '82%' },
            { sWidth: '8%' }
        ],
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
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
                render: (data, type, row, meta) => {
                    return `<button class="btn btn-secondary typeEquipment brand" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete typeEquipment" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    //Добавление
    $("#createTypeEquipmentBtn").click(function () {

        axios.post("TypeEquipment/Create", {
            name: $("#typeEquipment").val(),

        }).then(function () {
            location.reload()
        })
    })

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