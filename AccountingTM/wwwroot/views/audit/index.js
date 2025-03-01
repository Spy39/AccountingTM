$(document).ready(function () {
    // Инициализация datepicker для поля выбора периода статистики
    $('#dateAudit').datepicker({
        range: true,
        multipleDatesSeparator: ' - ',
        dateFormat: 'dd.mm.yyyy'
    });

    // Инициализация DataTable
    let tableAudit = new DataTable('#auditTable', {
        paging: true,
        serverSide: true,
        responsive: true,
        processing: true,
        ajax: function (data, callback, settings) {
            let filter = {};
            filter.searchQuery = $("#search-input").val();
            filter.maxResultCount = data.length || 10;
            filter.skipCount = data.start || 0;

            // Получаем выбранные даты из datepicker (если диапазон выбран)
            let datepickerData = $('#dateAudit').data('datepicker');
            if (datepickerData && datepickerData.selectedDates && datepickerData.selectedDates.length === 2) {
                // Передаем даты в формате ISO (или измените формат по необходимости)
                filter.startDate = datepickerData.selectedDates[0].toISOString();
                filter.endDate = datepickerData.selectedDates[1].toISOString();
            }

            axios.get('/Audit/GetAll', { params: filter })
                .then(function (result) {
                    callback({
                        recordsTotal: result.data.totalCount,
                        recordsFiltered: result.data.totalCount,
                        data: result.data.items
                    });
                })
                .catch(function (error) {
                    console.error(error);
                });
        },
        initComplete: function () {
            $('[data-bs-toggle="tooltip"]').tooltip();
        },
        columnDefs: [
            {
                // Колонка: Дата
                targets: 0,
                data: 'date',
                render: function (data, type, row, meta) {
                    if (!data) return '';
                    let dateObj = new Date(data);
                    return dateObj.toLocaleString();
                }
            },
            {
                // Колонка: Таблица
                targets: 1,
                data: 'tableName'
            },
            {
                // Колонка: Действие
                targets: 2,
                data: 'action'
            },
            {
                // Колонка: PrimaryKey
                targets: 3,
                data: 'primaryKey'
            },
            {
                // Колонка: Пользователь
                targets: 4,
                data: 'userName'
            }
        ]
    });

    // Обновление таблицы по нажатию кнопки поиска
    $("#search-btn").click(function () {
        tableAudit.ajax.reload();
    });

    // Обновление таблицы по нажатию кнопки поиска по дате
    $("#date-search-btn").click(function () {
        tableAudit.ajax.reload();
    });

    // Удаление записи аудита
    $(document).on("click", ".delete-log", function () {
        let id = $(this).data("id");

        Swal.fire({
            title: "Вы уверены?",
            text: `Запись ID ${id} будет удалена!`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Да",
            cancelButtonText: "Нет",
        }).then((result) => {
            if (result.isConfirmed) {
                axios.delete(`/Audit/Delete?id=${id}`)
                    .then(function () {
                        tableAudit.ajax.reload();
                        $(".tooltip").removeClass("show");
                        toastr.success(`Запись ID ${id} успешно удалена!`);
                    })
                    .catch(function () {
                        toastr.error("Ошибка при удалении!");
                    });
            }
        });
    });
});