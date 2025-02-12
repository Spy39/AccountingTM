//Пользователи

//Вывод данных в таблицу
let tablerRoles = new DataTable('#rolesTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/Roles/GetAll', {
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
            action: () => tablerRoles.draw(false)
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
                return `<button class="btn btn-secondary edit role" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete brand" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});
$("#search-btn").click(function () {
    tablerRoles.ajax.reload()
})

//Добавление
$("#create-btn").click(function () {
    axios.post("Administration/Create", {
        employeeId: +$("#fio").val(),
        login: $("#login").val(),
        isDeleted: false
    }).then(function () {
        location.reload()
    })
})

//Редактирование
$(document).on("click", ".edit.role", function () {
    let id = this.dataset.id;
    axios.get('/Roles/Get', {
        params: {
            id
        }
    }).then(function (response) {
        const role = response.data;
        $("#editNameRole").val(role.name);
        $("#editRoleId").val(role.id);
        $("#editRole").modal("show");
    })
})

$("#editRoleBtn").click(function () {
    axios.post("Brand/Update", {
        id: +$("#editBrandId").val(),
        name: $("#editNameBrand").val(),
    }).then(function () {
        location.reload()
    })
})

//Удаление
$(document).on("click", ".delete.administration", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Пользователь ${name} будет удален!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("Administration/Delete?id=" + id).then(function () {
                tableClients.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Пользователь ${name} успешно удален!`)
            })
        }
    });
})

