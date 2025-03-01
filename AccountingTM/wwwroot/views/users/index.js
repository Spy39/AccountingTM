//Пользователи

//Вывод данных в таблицу
let tableUsers = new DataTable('#usersTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/Users/GetAll', {
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
            action: () => tableUsers.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            // Колонка ФИО
            targets: 0,
            data: null, // получаем весь объект User
            render: (data, type, row, meta) => {
                // data = вся строка
                // Либо data.lastName/firstName/fatherName
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`.trim();
            }
        },
        {
            targets: 1,
            data: 'login',
        },
        {
            // Колонка Роль
            targets: 2,
            data: null,
            defaultContent: '',
            render: (data) => {
                // data.role может быть null
                return data.role ? data.role.name : '';
            }
        },
        {
            targets: 3,
            data: null,
            orderable: false,
            searchable: false,
            className: 'text-nowrap', // класс для запрета переноса строк
            width: '1%', // минимальная ширина колонки
            render: (data, type, row, meta) => {
                return `<button class="btn btn-secondary edit users" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                        <button class="btn btn-danger delete user" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});
$("#search-btn").click(function () {
    tableUsers.ajax.reload()
})

//Добавление
$("#createUserBtn").click(function () {
    axios.post("Users/Create", {
        employeeId: +$("#employee").val(),
        login: $("#login").val(),
        password: $("#password").val(),
        lastName: $("#lastName").val(),
        firstName: $("#firstName").val(),
        fatherName: $("#fatherName").val(),
        roleId: +$("#userRoles").val()
    }).then(function () {
        location.reload()
    })
})

$(document).on("click", ".edit.users", function () {
    let id = this.dataset.id;
    axios.get('/Users/Get', {
        params: { id }
    }).then(function (response) {
        const user = response.data;
        $("#editUserId").val(user.id);
        $("#editLastName").val(user.lastName);
        $("#editFirstName").val(user.firstName);
        $("#editFatherName").val(user.fatherName);
        $("#editLogin").val(user.login);
        $("#editPassword").val(user.password);

        // Для селекта ролей
        if (user.roleId && user.role) {
            let roleSelect = $("#editUserRoles");
            // Если опция с данным значением еще не добавлена, создаем её
            if (roleSelect.find("option[value='" + user.roleId + "']").length === 0) {
                var newOption = new Option(user.role.name, user.roleId, true, true);
                roleSelect.append(newOption);
            }
            roleSelect.val(user.roleId).trigger("change");
        }

        // Для селекта сотрудников
        if (user.employeeId && user.employee) {
            let employeeSelect = $("#editEmployee");
            if (employeeSelect.find("option[value='" + user.employeeId + "']").length === 0) {
                let fullName = `${user.employee.lastName || ''} ${user.employee.firstName || ''} ${user.employee.fatherName || ''}`.trim();
                var newOption = new Option(fullName, user.employeeId, true, true);
                employeeSelect.append(newOption);
            }
            employeeSelect.val(user.employeeId).trigger("change");
        }

        $("#editUser").modal("show");
    }).catch(function (error) {
        console.error("Ошибка получения данных пользователя:", error);
    });
});

$("#editUserBtn").click(function () {
    axios.post("/Users/Update", {
        id: +$("#editUserId").val(),
        employeeId: +$("#editEmployee").val(),
        login: $("#editLogin").val(),
        password: $("#editPassword").val(),
        lastName: $("#editLastName").val(),
        firstName: $("#editFirstName").val(),
        fatherName: $("#editFatherName").val(),
        roleId: +$("#editUserRoles").val()
    }).then(function () {
        location.reload()
    })
})

//Удаление
$(document).on("click", ".delete.user", function () {
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
            axios.delete("Users/Delete?id=" + id).then(function () {
                tableUsers.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Пользователь успешно удален!`)
            })
        }
    });
})

//Вывод Select
//Сотрудник - добавление
$("#employee").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Сотрудник',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Employee/GetAll", { params: filter }).then(function (result) {

                success({
                    results: result.data.items,
                    pagination: {
                        more: (params.page * maxResultCount) < result.data.totalCount
                    }
                });
            });
        },
        cache: true
    },
    templateResult: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,
    templateSelection: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,

})

//Роль - добавление
$("#userRoles").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Роли',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            //Исправить
            axios.get("/Roles/GetAll", { params: filter }).then(function (result) {
                    
                success({
                    results: result.data.items,
                    pagination: {
                        more: (params.page * maxResultCount) < result.data.totalCount
                    }
                });
            });
        },
        cache: true
    },
    templateResult: (data) => data.name,
    templateSelection: (data) => data.name
})

//Сотрудник -редактирование
$("#editEmployee").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Сотрудник',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Employee/GetAll", { params: filter }).then(function (result) {

                success({
                    results: result.data.items,
                    pagination: {
                        more: (params.page * maxResultCount) < result.data.totalCount
                    }
                });
            });
        },
        cache: true
    },
    templateResult: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,
    templateSelection: (data) => `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`,

})

//Роль - редактирование
$("#editUserRoles").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Роли',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            //Исправить
            axios.get("/Roles/GetAll", { params: filter }).then(function (result) {

                success({
                    results: result.data.items,
                    pagination: {
                        more: (params.page * maxResultCount) < result.data.totalCount
                    }
                });
            });
        },
        cache: true
    },
    templateResult: (data) => data.name,
    templateSelection: (data) => data.name
})