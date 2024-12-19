//Администрирование

//Вывод данных в таблицу
let tableAdministrations = new DataTable('#administrationTable', {
    paging: true,
    serverSide: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/Administration/GetAll', {
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
            action: () => tableAdministrations.draw(false)
        }
    ],
    initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
    columnDefs: [
        {
            targets: 0,
            data: 'fio',
            render: (data, type, row, meta) => {
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
            }
        },
        {
            targets: 1,
            data: 'login',
        },
        {
            targets: 2,
            data: 'password',
        },
        {
            targets: 3,
            data: 'role',
        },
        {
            targets: 4,
            data: 'employee',
            render: (data, type, row, meta) => {
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
            }
        },
        {
            targets: 5,
            data: null,
            render: (data, type, row, meta) => {
                return `<a href="administration/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Изменить"><i class="fa-regular fa-address-card"></i></a>
                        <button class="btn btn-danger delete administration" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});
$("#search-btn").click(function () {
    tableAdministrations.ajax.reload()
})

//Добавление
$("#create-btn").click(function () {
    axios.post("Administration/Create", {
        employeeId: +$("#fio").val(),
        login: $("#login").val(),
        password: $("#password").val(),
        lastName: +$("#lastName").val(),
        firstName: +$("#firstName").val(),
        fatherName: +$("#fatherName").val(),
        userRoles: +$("#userRoles").val(),
        isDeleted: false
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

//Вывод Select
//Сотрудник
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

//Права
$("#role").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Права',
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
    templateResult: (data) => data.name,
    templateSelection: (data) => data.name
})