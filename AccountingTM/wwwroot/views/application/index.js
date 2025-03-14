﻿//Заявки - Главная
let tableClients = new DataTable('#applicationsTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    bAutoWidth: false,
    order: [],
    ajax: function (data, callback, settings) {
        var filter = {
            searchQuery: $("#search-input").val(),
            maxResultCount: data.length || 10,
            skipCount: data.start
        };
        axios.get('/Application/GetAll', { params: filter })
            .then(function (result) {
                console.log(result);
                callback({
                    recordsTotal: result.data.totalCount,
                    recordsFiltered: result.data.totalCount,
                    data: result.data.items
                });
            })
            .catch(function (error) {
                console.error("Ошибка загрузки данных:", error);
            });
    },
    buttons: [
        {
            name: 'refresh',
            text: '<i class="fas fa-redo-alt"></i>',
            action: () => tableClients.draw(false)
        }
    ],
    initComplete: function () {
    //    $('[data-bs-toggle="tooltip"]').tooltip();
    },
    // Подсветка строк: если статус решена (4) – зеленый, иначе по приоритету
    rowCallback: function (row, data, index) {
        if (data.status === 4) {
            // Если заявка решена, выделяем строку зеленым
            $(row).addClass("table-success");
        } else {
            // Иначе подсвечиваем по приоритету:
            // 0 - Критический: красный (table-danger)
            // 1 - Высокий: желтый (table-warning)
            // 2 - Нормальный: голубой (table-info)
            // 3 - Низкий: серый (table-secondary)
            if (data.priority === 0) {
                $(row).addClass("table-danger");
            } else if (data.priority === 1) {
                $(row).addClass("table-warning");
            } else if (data.priority === 2) {
                $(row).addClass("table-info");
            } else if (data.priority === 3) {
                $(row).addClass("table-secondary");
            }
        }
    },
    columnDefs: [
        {
            targets: 0,
            data: 'applicationNumber'
        },
        {
            targets: 1,
            data: 'dateOfCreation',
            render: (data, type, row, meta) => data ? dayjs(data).format("DD.MM.YYYY HH:mm") : ""
        },
        {
            targets: 2,
            data: 'dateOfChange',
            render: (data, type, row, meta) => data ? dayjs(data).format("DD.MM.YYYY HH:mm") : ""
        },
        {
            targets: 3,
            data: 'category.name'
        },
        {
            targets: 4,
            data: 'subject'
        },
        {
            targets: 5,
            data: 'status',
            render: (data, type, row, meta) => {
                switch (data) {
                    case 0: return "Новая";
                    case 1: return "Получен комментарий";
                    case 2: return "Комментарий отправлен";
                    case 3: return "В работе";
                    case 4: return "Приостановлена";
                    case 5: return "Передана";
                    case 6: return "Решена";
                    default: return "Неизвестно";
                }
            }
        },
        {
            targets: 6,
            data: 'author'
        },
        {
            targets: 7,
            data: 'location.name'
        },
        {
            targets: 8,
            data: 'priority',
            render: (data, type, row, meta) => {
                switch (data) {
                    case 0: return "Критический";
                    case 1: return "Высокий";
                    case 2: return "Нормальный";
                    case 3: return "Низкий";
                    default: return "";
                }
            }
        },
        {
            targets: 9,
            data: null,
            orderable: false,
            searchable: false,
            className: 'text-nowrap',
            width: '1%',
            render: (data, type, row, meta) => {
                return `<a href="application/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о заявке"><i class="fa-solid fa-circle-info"></i></a>
                            <button class="btn btn-danger delete" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
            }
        }
    ]
});

$("#searchApplicationBtn").click(function () {
    tableClients.ajax.reload()
})


//Создание новой заявки
$("#create-btn").click(function () {
    axios.post("Application/Create", {
        categoryId: +$("#category").val(),
        subject: $("#subject").val(),
        description: $("#description").val(),
        author: $("#author").val(),
        locationId: +$("#location").val(),
        priority: +$("#priority").val(),
    }).then(function () {
        location.reload()
    })
})


//Удаление заявки
$(document).on("click", ".delete", function () {
    let name = this.dataset.name;
    Swal.fire({
        title: "Вы уверены?",
        text: `Заявка ${name} будет удалена!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да",
        cancelButtonText: "Нет",
    }).then((result) => {
        if (result.isConfirmed) {
            let id = this.dataset.id;
            axios.delete("Application/Delete?id=" + id).then(function () {
                tableClients.draw(false)
                $(".tooltip").removeClass("show")
                toastr.success('Заявка успешно удалена!')
            })
        }
    });
})

$("#category").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Категория',
    ajax: {
        transport: (data, success, failure) => {
            let params = data.data;
            let maxResultCount = 30;

            params.page = params.page || 1;

            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term
            axios.get("Category/GetAll", { params: filter }).then(function (result) {

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
            axios.get("Location/GetAll", { params: filter }).then(function (result) {

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

// Функции для статусов
document.getElementById("selectAllStatus").addEventListener("click", function () {
    document.querySelectorAll(".status-checkbox").forEach(checkbox => checkbox.checked = true);
});
document.getElementById("clearAllStatus").addEventListener("click", function () {
    document.querySelectorAll(".status-checkbox").forEach(checkbox => checkbox.checked = false);
});

// Функции для приоритетов
document.getElementById("selectAllPriority").addEventListener("click", function () {
    document.querySelectorAll(".priority-checkbox").forEach(checkbox => checkbox.checked = true);
});
document.getElementById("clearAllPriority").addEventListener("click", function () {
    document.querySelectorAll(".priority-checkbox").forEach(checkbox => checkbox.checked = false);
});

// Применение фильтров
document.getElementById("applyFilters").addEventListener("click", function () {
    const selectedStatuses = Array.from(document.querySelectorAll(".status-checkbox:checked")).map(cb => cb.value);
    const selectedPriorities = Array.from(document.querySelectorAll(".priority-checkbox:checked")).map(cb => cb.value);

    console.log("Выбранные статусы:", selectedStatuses);
    console.log("Выбранные приоритеты:", selectedPriorities);

    // Здесь можно отправить выбранные фильтры на сервер или использовать для фильтрации на клиенте
    alert("Фильтры применены!");
});

document.getElementById('selectAllShow').addEventListener('click', function (e) {
    e.preventDefault();
    document.querySelectorAll('#FilterModal input[type="checkbox"]').forEach(cb => cb.checked = true);
});

document.getElementById('deselectAllShow').addEventListener('click', function (e) {
    e.preventDefault();
    document.querySelectorAll('#FilterModal input[type="checkbox"]').forEach(cb => cb.checked = false);
});