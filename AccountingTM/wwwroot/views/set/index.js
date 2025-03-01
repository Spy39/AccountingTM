//Комплекты

//Вывод данных в таблицу комплекты
let tableSets = new DataTable('#setTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        axios.get('/Set/GetAllSet', {
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
            action: () => tableSets.draw(false)
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
            data: 'statusSet',
        },
        {
            targets: 2,
            data: 'employee.fullName',
        },
        {
            targets: 3,
            data: 'location.name',
        },
        {
            targets: 4,
            data: null,
            orderable: false,
            searchable: false,
            className: 'text-nowrap',
            width: '1%',
            render: (data, type, row, meta) => {
                return `<a href="set/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о комплекте"><i class="fa-solid fa-circle-info"></i></a>
                        <button class="btn btn-danger delete set" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});
$("#searchSetBtn").click(function () {
    tableSets.ajax.reload()
})


//Добавление нового комплекта
$("#create-btn").click(function () {
    axios.post("/Set/CreateSet", {
        name: $("#name").val(),
        employeeId: +$("#employee").val(),
        locationId: +$("#location").val(),
        isDeleted: false
    }).then(function () {
        location.reload()
    })
})

//Удаление комплекта
$(document).on("click", ".delete.set", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Комплект ${name} будет удален!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("Set/DeleteSet?id=" + id).then(function () {
                tableSets.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Комплект ${name} успешно удален!`)
            })
        }
    });
})

//Select

//Помещение
$("#location").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Помещение',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("/Location/GetAll", { params: filter }).then(function (result) {

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

//Ответственный
$("#employee").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Ответственный',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("Employee/GetAll", { params: filter }).then(function (result) {

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