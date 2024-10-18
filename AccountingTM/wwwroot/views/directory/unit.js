//Единицы измерения
const initTableUnits = () => {
    
    let tableUnits = new DataTable('#units', {
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
                render: (data, type, row, meta) => {
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                            <button class="btn btn-danger delete unit" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    //Добавление
    $("#createUnitBtn").click(function () {

        axios.post("Unit/Create", {
            name: $("#unit").val(),

        }).then(function () {
            location.reload()
        })
    })

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