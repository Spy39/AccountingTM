//Сотрудники
const initTableEmployees = () => {

    let tableEmployees = new DataTable('#employees', {
        paging: true,
        serverSide: true,
        responsive: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            filter.searchQuery = $("#search-input-employee").val()
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
                orderable: false,
                searchable: false,
                className: 'text-nowrap',
                width: '1%',
                render: (data, type, row, meta) => {
                    return `<button class="btn btn-secondary edit employee" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                    <button class="btn btn-danger delete employee" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-employee").click(function () {
        tableEmployees.ajax.reload()
    })

    //Добавление
    $("#createEmployeeBtn").click(function () {
        axios.post("Employee/Create", {
            lastName: $("#lastName").val(),
            firstName: $("#firstName").val(),
            fatherName: $("#fatherName").val(),
            position: $("#position").val(),
        }).then(function () {
            tableEmployees.draw(false)
            $("#createEmployee").modal("hide");
        })
    })

    $("#createEmployee").on('hidden.bs.modal', () => {
        $("#lastName").val("");
        $("#firstName").val("");
        $("#fatherName").val("");
        $("#position").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.employee", function () {
        let id = this.dataset.id;
        axios.get('/Employee/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const employee = response.data;
            $("#editEmployeeId").val(employee.id);
            $("#editLastName").val(employee.lastName);
            $("#editFirstName").val(employee.firstName);
            $("#editFatherName").val(employee.fatherName);
            $("#editPosition").val(employee.position);
            $("#editEmployee").modal("show");
        })
    })

    $("#editEmployeeBtn").click(function () {
        axios.post("Employee/Update", {
            id: +$("#editEmployeeId").val(),
            lastName: $("#editLastName").val(),
            firstName: $("#editFirstName").val(),
            fatherName: $("#editFatherName").val(),
            position: $("#editPosition").val()
        }).then(function () {
            tableEmployees.draw(false)
            $("#editEmployee").modal("hide");
        })
    })

    $("#editEmployee").on('hidden.bs.modal', () => {
        $("#editLastName").val("");
        $("#editFirstName").val("");
        $("#editFatherName").val("");
        $("#editPosition").val("");
        $("#editEmployeeId").val("");
    });

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