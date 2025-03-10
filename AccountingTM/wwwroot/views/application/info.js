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
            action: () => tabletechnicalEquipments.draw(false)
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
         location.reload();
     });
 });


$("#category").select2({
    width: '100%',
    allowClear: true,
    placeholder: 'Выберите категорию',
    ajax: {
        // Транспорт – переопределяет логику отправки запроса
        transport: (data, success, failure) => {
            // Внутри data у Select2 хранится объект params:
            // { term: "введённый текст", page: 1, ... }
            let params = data.data;
            let maxResultCount = 30;

            // Если page не определён, ставим 1
            params.page = params.page || 1;

            // Подготавливаем фильтр для бэкенда
            let filter = {};
            filter.maxResultCount = maxResultCount;
            filter.skipCount = (params.page - 1) * maxResultCount;
            filter.keyword = params.term; // терм для поиска

            // Делаем GET-запрос
            axios.get("/Category/GetAll", { params: filter })
                .then(function (result) {
                    // Ожидаем, что result.data.items – список категорий,
                    // а result.data.totalCount – общее число.
                    success({
                        results: result.data.items,
                        pagination: {
                            // Вычисляем, есть ли ещё страницы
                            more: (params.page * maxResultCount) < result.data.totalCount
                        }
                    });
                })
                .catch(function (error) {
                    // Если ошибка – вызываем failure() или обрабатываем по-своему
                    console.error("Ошибка при загрузке категорий:", error);
                    failure(error);
                });
        },
        cache: true
    },
    // Как отобразить варианты в выпадающем списке
    templateResult: (data) => {
        // Если это служебный элемент (например, "Searching…")
        if (data.loading) return data.text;
        // Если real data – выводим data.name
        return data.name;
    },
    // Как отображать выбранный элемент в поле
    templateSelection: (data) => {
        return data.name || data.text;
    }
});



// 2. Обновление/изменение заявки
document.getElementById('save-changes-btn').addEventListener('click', function () {
    const applicationId = parseInt(document.getElementById("application").value);

    // >>> Изменение: Собираем статус/приоритет и пр. как enum/строку
    const status = document.getElementById('status').value;
    const priority = document.getElementById('priority').value;
    const categoryId = parseInt(document.getElementById('category').value);

    // >>> Добавили expirationDate
    const expirationDate = document.getElementById('expirationDate').value
        ? document.getElementById('expirationDate').value
        : null;

    axios.post('/Application/Update', {
        applicationId: applicationId,
        status: +status,
        priority: +priority,
        categoryId: categoryId,
        // >>> Изменение: Если добавили поле в DTO, нужно передавать его
        expirationDate: expirationDate
    })
        .then(function () {
            toastr.success('Изменения сохранены!');
            // location.reload(); // если нужно
            loadHistory(); // обновляем историю
        })
        .catch(function (error) {
            toastr.error('Ошибка: ' + error);
        });
});

// 3. Назначить себе заявку
document.getElementById('assign-to-me-btn').addEventListener('click', function () {
    const applicationId = parseInt(document.getElementById("application").value);

    axios.post('/Application/AssignToMe', applicationId)
        .then(function () {
            toastr.success('Заявка назначена вам!');
            loadHistory(); // обновляем историю
        })
        .catch(function (error) {
            toastr.error('Ошибка: ' + error);
        });
});

// 4. Пометить как решенную
document.getElementById('mark-solved-btn').addEventListener('click', function () {
    const applicationId = parseInt(document.getElementById("application").value);

    axios.post('/Application/MarkAsSolved', applicationId)
        .then(function () {
            toastr.success('Заявка помечена как решённая!');
            loadHistory();
        })
        .catch(function (error) {
            toastr.error('Ошибка: ' + error);
        });
});

// 5. Подгрузка истории изменений
function loadHistory() {
    const applicationId = parseInt(document.getElementById("application").value);
    axios.get('/Application/GetHistory?applicationId=' + applicationId)
        .then(function (response) {
            const list = response.data;
            let html = "";
            list.forEach(function (item) {
                const dateStr = new Date(item.date).toLocaleString();
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

// Вспомогательная функция для ФИО
function getEmployeeName(emp) {
    return `${emp.lastName ?? ''} ${emp.firstName ?? ''} ${emp.fatherName ?? ''}`.trim();
}

//КОММЕНТАРИИ

////Добавление комментария
//$("#add-comment-btn").click(function () {
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

// 6. Комментарии
document.addEventListener('DOMContentLoaded', function () {
    const applicationElement = document.getElementById("application");
    const applicationId = parseInt(applicationElement.value);
    const commentsList = document.getElementById('comments-list');
    const commentInput = document.getElementById('comment');
    let fileInput = document.getElementById('file');

    // Загрузка списка комментариев
    function loadComments() {
        axios.get(`/Application/GetComments?applicationId=${applicationId}`)
            .then(response => {
                commentsList.innerHTML = response.data.map(comment => `
                <div class="card mb-2">
                    <div class="card-body">
                        <p>${comment.text}</p>
                        <small><strong>${comment.author}</strong> | ${new Date(comment.date).toLocaleString()}</small>
                        ${comment.pathToFile ? `<br><a href="${comment.pathToFile}" target="_blank">📎 Скачать файл</a>` : ""}
                        <button class="btn btn-danger btn-sm delete-comment" data-id="${comment.id}">Удалить</button>
                    </div>
                </div>
            `).join('');
            })
            .catch(error => console.error("Ошибка при загрузке комментариев:", error));
    }


    // Добавление комментария
    function addComment() {
        const commentText = commentInput.value.trim();
        const file = fileInput.files[0];

        if (!commentText) {
            toastr.error("Введите комментарий перед отправкой.");
            return;
        }

        const formData = new FormData();
        formData.append('ApplicationId', applicationId);
        formData.append('Text', commentText);
        if (file) {
            formData.append('File', file);
        }

        axios.post('/Application/AddComment', formData)
            .then(() => {
                commentInput.value = '';
                fileInput.value = '';
                toastr.success("Комментарий добавлен!");
                loadComments();
                loadHistory(); // >>> Изменение: перезапрашиваем историю, т.к. мог смениться LastReply
            })
            .catch(error => console.error("Ошибка при добавлении комментария:", error));
    }

    // Обработка клика по кнопке "Добавить комментарий"
    document.addEventListener('click', function (event) {
        if (event.target && event.target.id === 'add-comment-btn') {
            event.preventDefault();
            addComment();
        }
    });

    // Удаление комментария
    commentsList.addEventListener('click', function (e) {
        if (e.target.classList.contains('delete-comment')) {
            const commentId = e.target.dataset.id;
            Swal.fire({
                title: "Вы уверены?",
                text: "Комментарий будет удален!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Да",
                cancelButtonText: "Нет",
            }).then((result) => {
                if (result.isConfirmed) {
                    axios.delete(`/Application/DeleteComment?id=${commentId}`)
                        .then(() => {
                            toastr.success("Комментарий успешно удален!");
                            loadComments();
                            loadHistory();
                        })
                        .catch(error => console.error("Ошибка при удалении комментария:", error));
                }
            });
        }
    });

    // При старте страницы грузим историю и комментарии
    loadHistory();
    loadComments();
});