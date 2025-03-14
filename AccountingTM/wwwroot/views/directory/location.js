﻿//Помещения
const initTableLocations = () => {

    let tableLocations = new DataTable('#locations', {
        paging: true,
        serverSide: true,
        responsive: true,
        ajax: function (data, callback, settings) {
            var filter = {};
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start;
            filter.searchQuery = $("#search-input-location").val()
            axios.get('/Location/GetAll', {
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
                action: () => tableLocations.draw(false)
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
                    return `<button class="btn btn-secondary edit location" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete location" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
                }
            }]
    });

    $("#button-search-location").click(function () {
        tableLocations.ajax.reload()
    })

    //Добавление
    $("#createLocationBtn").click(function () {
        axios.post("Location/Create", {
            name: $("#location").val(),
        }).then(function () {
            tableLocations.draw(false)
            $("#createLocation").modal("hide");
        })
    })

    $("#createLocation").on('hidden.bs.modal', () => {
        $("#location").val("");
    });

    //Редактирование
    $(document).on("click", ".edit.location", function () {
        let id = this.dataset.id;
        axios.get('/Location/Get', {
            params: {
                id
            }
        }).then(function (response) {
            const location = response.data;
            $("#editNameLocation").val(location.name);
            $("#editLocationId").val(location.id);
            $("#editLocation").modal("show");
        })
    })

    $("#editLocationBtn").click(function () {
        axios.post("Location/Update", {
            id: +$("#editLocationId").val(),
            name: $("#editNameLocation").val(),
        }).then(function () {
            tableLocations.draw(false)
            $("#editLocation").modal("hide");
        })
    })

    $("#editLocation").on('hidden.bs.modal', () => {
        $("#editNameLocation").val("");
        $("#editLocationId").val("");
    });

    //Удаление
    $(document).on("click", ".delete.location", function () {
        let name = this.dataset.name;
        Swal.fire({
            title: "Вы уверены?",
            text: `Помещение ${name} будет удалено!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                let id = this.dataset.id;
                axios.delete("Location/Delete?id=" + id).then(function () {
                    tableLocations.draw(false)
                    $(".tooltip").removeClass("show")
                    toastr.success(`Помещение ${name} успешно удалено!`)
                })
            }
        });
    })
}

export default initTableLocations;