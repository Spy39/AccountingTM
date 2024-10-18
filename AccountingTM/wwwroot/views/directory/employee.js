//Сотрудники
const initTableEmployees = () => {

    let tableEmployees = new DataTable('#employees', {
        paging: true,
        serverSide: true,
        bAutoWidth: false,
        aoColumns: [
            { sWidth: '22%' },
            { sWidth: '22%' },
            { sWidth: '22%' },
            { sWidth: '25%' },
            { sWidth: '9%' }
        ],
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            axios.get('/Employee/GetAll', {
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
                action: () => tableEmployees.draw(false)
            }
        ],
        initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
        columnDefs: [
            {
                targets: 0,
                data: 'lastName',
            },
            {
                targets: 1,
                data: 'firstName',
            },
            {
                targets: 2,
                data: 'fatherName',
            },
            {
                targets: 3,
                data: 'position',
            },
            {
                targets: 4,
                data: null,
                render: (data, type, row, meta) => {
                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></a>
                    <button class="btn btn-danger delete employee" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

//Добавление
    $("#createEmployeeBtn").click(function () {

        axios.post("Employee/Create", {
            lastName: $("#lastName").val(),
            firstName: $("#firstName").val(),
            fatherName: $("#fatherName").val(),
            position: $("#position").val(),

        }).then(function () {
            location.reload()
        })
    })

//Удаление
    $(document).on("click", ".delete.employee", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Сотрудник ${name} будет удален!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Employee/Delete?id=" + id).then(function () {
                    tableEmployees.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Сотрудник ${name} успешно удален!`)
                })
            }
        });
    })

}

export default initTableEmployees;