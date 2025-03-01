//Заявки - Информация

//Вывод данных о техничесих средствах в таблицу
let tabletechnicalEquipments = new DataTable('#technicalEquipmentTable', {
    paging: true,
    serverSide: true,
    responsive: true,
    bAutoWidth: false,
    select: {
        selector: 'td:first-child',
        style: 'multi'
    },
    fixedColumns: {
        start: 2
    },
    ajax: function (data, callback, settings) {
        var filter = {};
        filter.searchQuery = $("#search-input").val()
        filter.maxResultCount = data.length || 10;
        filter.skipCount = data.start;
        filter.isWithoutSet = true;
        axios.get('/TechnicalEquipment/GetAll', {
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
            action: () => tableClients.draw(false)
        }
    ],
    drawCallback: function () {
        if ($('[data-bs-toggle="tooltip"]')) {
            setTimeout(() => {
                $('[data-bs-toggle="tooltip"]').tooltip();
            }, 1000)
        }

    },
    columnDefs: [
        {
            orderable: false,
            render: DataTable.render.select(),
            targets: 0
        },
        {
            targets: 1,
            data: 'type.name',
        },
        {
            targets: 2,
            data: 'brand.name',
        },
        {
            targets: 3,
            data: 'model',
        },
        {
            targets: 4,
            data: 'serialNumber',
        },
        {
            targets: 5,
            data: 'state',
            render: (data, type, row, meta) => {
                switch (data) {
                    case 0: return "Исправно";
                    case 1: return "Неисправно";
                    case 2: return "Работоспособно";
                    case 3: return "Неработоспособно";
                }
            }

        },
        {
            targets: 6,
            data: 'employee',
            render: (data, type, row, meta) => {
                return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
            }
        },
        {
            targets: 7,
            data: 'location.name',
        },
        {
            targets: 8,
            data: ``,
            orderable: false,
            searchable: false,
            className: 'text-nowrap',
            width: '1%',
            render: (data, type, row, meta) => {
                return ``;
            }
        }]
});

//Добавление к ТС
$("#create-btn").click(function () {
    axios.post("/Set/CreateCompoundSet", {
        setId: +$("#SetId").val(),
        technicalEquipmentIds: tabletechnicalEquipments.rows({ selected: true }).data().map(x => x.id).toArray()
    }).then(function () {
        location.reload()
    })
})


////Добавление комментария
//$("#create-btn").click(function () {
//    axios.post("Application/Create", {
//        categoryId: +$("#category").val(),
//        subject: $("#subject").val(),
//        description: $("#description").val(),
//        author: $("#author").val(),
//        locationId: +$("#location").val(),
//        priority: +$("#priority").val(),
//    }).then(function () {
//        location.reload()
//    })
//})



////Удаление комментария
//$(document).on("click", ".delete", function () {
//    let name = this.dataset.name;
//    Swal.fire({
//        title: "Вы уверены?",
//        text: `Комментарий  будет удален!`,
//        icon: "warning",
//        showCancelButton: true,
//        confirmButtonColor: "#3085d6",
//        cancelButtonColor: "#d33",
//        confirmButtonText: "Да",
//        cancelButtonText: "Нет",
//    }).then((result) => {
//        if (result.isConfirmed) {
//            let id = this.dataset.id;
//            axios.delete("Application/Delete?id=" + id).then(function () {
//                tableClients.draw(false)
//                $(".tooltip").removeClass("show")
//                toastr.success('Комментарий успешно удален!')
//            })
//        }
//    });
//})


//обновление и изменение заявки
document.getElementById('save-changes-btn').addEventListener('click', function () {
    var applicationId = parseInt(document.getElementById("application").value);
    // Значения статус/приоритет могут быть строками, нужно привести к enum на бэкенде.
    // Можно передавать строками, если в DTO стоит string и потом мапить.
    // Или же сделать соответствие value="0,1,2..." (enum в C#)
    var status = document.getElementById('status').value;
    var priority = document.getElementById('priority').value;
    var categoryId = parseInt(document.getElementById('category').value);

    axios.post('/Application/Update', {
        applicationId: applicationId,
        status: status,     // если DTO enum — нужно убедиться, что значения совпадают
        priority: priority, // то же самое
        categoryId: categoryId
    })
        .then(function () {
            toastr.success('Изменения сохранены!');
            // При необходимости перезагрузить страницу или обновить нужные поля
            // location.reload();
        })
        .catch(function (error) {
            toastr.error('Ошибка: ' + error);
        });
});
//назначить себе заявку
document.getElementById('assign-to-me-btn').addEventListener('click', function () {
    var applicationId = parseInt(document.getElementById("application").value);

    axios.post('/Application/AssignToMe', applicationId)
        .then(function () {
            toastr.success('Заявка назначена вам!');
            // Можно обновить поля на форме
            // location.reload();
        })
        .catch(function (error) {
            toastr.error('Ошибка: ' + error);
        });
});

//пометить как решенную заявку
document.getElementById('mark-solved-btn').addEventListener('click', function () {
    var applicationId = parseInt(document.getElementById("application").value);

    axios.post('/Application/MarkAsSolved', applicationId)
        .then(function () {
            toastr.success('Заявка помечена как решённая!');
            // Перезагрузка или обновление полей
        })
        .catch(function (error) {
            toastr.error('Ошибка: ' + error);
        });
});
//подгружаем историю изменение
function loadHistory() {
    var applicationId = parseInt(document.getElementById("application").value);
    axios.get('/Application/GetHistory?applicationId=' + applicationId)
        .then(function (response) {
            var list = response.data;
            var html = "";
            list.forEach(function (item) {
                var dateStr = new Date(item.date).toLocaleString();
                html += `
                    <div class="card mb-2">
                        <div class="card-body">
                            <strong>${dateStr}</strong><br/>
                            <em>${item.typeOfOperation}</em><br/>
                            ${item.name}
                            ${item.employee
                        ? '<br/><small>Пользователь: ' + getEmployeeName(item.employee) + '</small>'
                        : ''}
                        </div>
                    </div>`;
            });
            document.getElementById("history-container").innerHTML = html;
        });
}

function getEmployeeName(emp) {
    return `${emp.lastName ?? ''} ${emp.firstName ?? ''} ${emp.fatherName ?? ''}`.trim();
}

document.addEventListener('DOMContentLoaded', function () {
    // ...
    loadHistory();
});



document.addEventListener('DOMContentLoaded', function () {
    const applicationId = +$("#application").val(); // Убедитесь, что ApplicationId доступен в модели
    const commentsList = document.getElementById('comments-list');
    const commentInput = document.getElementById('comment');
    const fileInput = document.getElementById('file');
    const addCommentBtn = document.getElementById('add-comment-btn');

    // Функция для загрузки комментариев
    function loadComments() {
        fetch(`/Application/GetComments?applicationId=${applicationId}`)
            .then(response => response.json())
            .then(comments => {
                commentsList.innerHTML = comments.map(comment => `
                    <div class="card mb-2">
                        <div class="card-body">
                            <p>${comment.text}</p>
                            <small>${new Date(comment.date).toLocaleString()}</small>
                            ${comment.pathToFile ? `<a href="${comment.pathToFile}" target="_blank">Скачать файл</a>` : ''}
                            <button class="btn btn-danger btn-sm delete-comment" data-id="${comment.id}">Удалить</button>
                        </div>
                    </div>
                `).join('');
            });
    }

    // Функция для добавления комментария
    addCommentBtn.addEventListener('click', function () {
        const commentText = commentInput.value.trim();
        const file = fileInput.files[0];

        if (commentText) {
            const formData = new FormData();
            formData.append('ApplicationId', applicationId);
            formData.append('Text', commentText);
            if (file) {
                formData.append('File', file);
            }

            fetch('/Application/AddComment', {
                method: 'POST',
                body: formData
            })
                .then(response => response.json())
                .then(comment => {
                    commentInput.value = '';
                    fileInput.value = '';
                    loadComments();
                });
        }
    });

    // Функция для удаления комментария
    commentsList.addEventListener('click', function (e) {
        if (e.target.classList.contains('delete-comment')) {
            const commentId = e.target.dataset.id;
            Swal.fire({
                title: "Вы уверены?",
                text: `Комментарий будет удален!`,
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Да",
                cancelButtonText: "Нет",
            }).then((result) => {
                if (result.isConfirmed) {
                    fetch(`/Application/DeleteComment?id=${commentId}`, {
                        method: 'DELETE'
                    })
                        .then(() => {
                            loadComments();
                            toastr.success('Комментарий успешно удален!');
                        });
                }
            });
        }
    });

    // Загрузка комментариев при открытии страницы
    loadComments();
});