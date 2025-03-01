//Пользователи

//Вывод данных в таблицу
let tableRoles = new DataTable('#rolesTable', {
    paging: true,
    serverSide: true,
    responsive: true,
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
            action: () => tableRoles.draw(false)
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
                return `<button class="btn btn-secondary edit role" data-id="${row.id}" data-bs-toggle="tooltip" data-bs-title="Редактировать"><i class="fa-solid fa-pen"></i></button>
                            <button class="btn btn-danger delete role" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }]
});

$("#search-btn").click(function () {
    tableRoles.ajax.reload()
})




//// Инициализация дерева прав
let addTree;

const initAddTree = () => {
    axios.get('/Permission/GetAll') // Загружаем все разрешения без привязки к роли
        .then(function (response) {
            const data = response.data;

            addTree = new Tree('#addPermissionTree', {
                data: data,
                loaded: null
            });

            for (let permission of data) {
                const $permissionLabel = $(`#addPermissionTree .treejs-label:contains("${permission.text}")`);
                $permissionLabel
                    .prepend(`<i class="fas fa-${permission.icon} ml-1 mr-1"></i>`);

                for (let childPermission of permission.children) {
                    $permissionLabel
                        .next()
                        .find(`.treejs-label:contains("${childPermission.text}")`)
                        .prepend(`<i class="fas fa-${childPermission.icon} ml-1 mr-1"></i>`);
                }
            }

            $("#addRole").modal("show");
        })
        .catch(function (error) {
            console.log(error);
        })
        .finally(function () {
            // always executed
        });
}

$("#createRoleBtn").click(function () {
    $("#roleName").val(""); // Очищаем поле имени роли
    $("#addRoleId").val(""); // Очищаем ID роли (новая роль)

    initAddTree();
});

$("#addRoleBtn").click(function () {
    axios.post("Roles/Create", {
        name: $("#roleName").val(),
        permissionNames: addTree.selectedNodes.map(x => x.text)
    }).then(function () {
        location.reload();
    }).catch(function (error) {
        console.log(error);
    });
});

//Редактирование

let editTree;

const initTree = (roleId) => {
    axios.get('/Permission/GetAllByRole?id=' + roleId)
        .then(function (response) {
            const data = response.data;

            const getSelectedPermissionIds = (nodes) => {
                let selectedIds = [];
                nodes.forEach(node => {
                    if (node.checked && !node.children.length) {
                        selectedIds.push(node.id);
                    }
                    if (node.children && node.children.length > 0) {
                        selectedIds = selectedIds.concat(getSelectedPermissionIds(node.children));
                    }
                });
                return selectedIds;
            }

            const selectedPermissionIds = getSelectedPermissionIds(data);

            editTree = new Tree('#editPermissionTree', {
                data: data,
                values: selectedPermissionIds,
                loaded: null
            });

            for (let permission of data) {
                const $permissionLabel = $(`#editPermissionTree .treejs-label:contains("${permission.text}")`);
                $permissionLabel
                    .prepend(`<i class="fas fa-${permission.icon} ml-1 mr-1"></i>`);

                for (let childPermission of permission.children) {
                    $permissionLabel
                        .next()
                        .find(`.treejs-label:contains("${childPermission.text}")`)
                        .prepend(`<i class="fas fa-${childPermission.icon} ml-1 mr-1"></i>`);
                }
            }
            $("#editRole").modal("show");
        })
        .catch(function (error) {
            // handle error
            console.log(error);
        })
        .finally(function () {
            // always executed
        });

}

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

        initTree(role.id);

        
    })
})

$("#editRoleBtn").click(function () {
    axios.post("Roles/Update", {
        id: +$("#editRoleId").val(),
        name: $("#editNameRole").val(),
        permissionNames: editTree.selectedNodes.map(x => x.text)
    }).then(function () {
        location.reload()
    })
})

//Удаление
$(document).on("click", ".delete.role", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Роль ${name} будет удалена!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("Roles/Delete?id=" + id).then(function () {
                tableRoles.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success(`Роль ${name} успешно удалена!`)
            })
        }
    });
})

