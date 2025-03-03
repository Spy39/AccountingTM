////Анализ
//$(document).ready(function () {
//    // Инициализация datepicker для поля выбора периода
//    $('#dateAnalysis').datepicker({
//        range: true,
//        multipleDatesSeparator: ' - ',
//        dateFormat: 'dd.mm.yyyy'
//    });
//});

//    //Вывод данных в таблицу
//    let tableAdministrations = new DataTable('#administrationTable', {
//        paging: true,
//        serverSide: true,
//        ajax: function (data, callback, settings) {
//            var filter = {};
//            filter.searchQuery = $("#search-input").val()
//            filter.maxResultCount = data.length || 10;
//            filter.skipCount = data.start;
//            axios.get('/Administration/GetAll', {
//                params: filter
//            })
//                .then(function (result) {
//                    console.log(result);
//                    callback({
//                        recordsTotal: result.data.totalCount,
//                        recordsFiltered: result.data.totalCount,
//                        data: result.data.items
//                    });
//                })
//        },
//        buttons: [
//            {
//                name: 'refresh',
//                text: '<i class="fas fa-redo-alt"></i>',
//                action: () => tableAdministrations.draw(false)
//            }
//        ],
//        initComplete: function () { $('[data-bs-toggle="tooltip"]').tooltip(); },
//        columnDefs: [
//            {
//                targets: 0,
//                data: 'fio',
//                render: (data, type, row, meta) => {
//                    return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
//                }
//            },
//            {
//                targets: 1,
//                data: 'login',
//            },
//            {
//                targets: 2,
//                data: 'password',
//            },
//            {
//                targets: 3,
//                data: 'role',
//            },
//            {
//                targets: 4,
//                data: 'employee',
//                render: (data, type, row, meta) => {
//                    return `${data.lastName || ''} ${data.firstName || ''} ${data.fatherName || ''}`;
//                }
//            },
//            {
//                targets: 5,
//                data: null,
//                render: (data, type, row, meta) => {
//                    return `<a href="technicalEquipment/${row.id}" class="btn btn-secondary" data-bs-toggle="tooltip" data-bs-title="Информация о ТС"><i class="fa-regular fa-address-card"></i></a>
//                            <button class="btn btn-danger delete administration" data-id="${row.id}" data-name="${row.name}" data-bs-toggle="tooltip" data-bs-title="Удалить"><i class="fa-solid fa-trash"></i></button>`;
//                }
//            }]
//    });
//    $("#search-btn").click(function () {
//        tableAdministrations.ajax.reload()
//    })



//$("#CalculateBtn").click(function () {
//    const dates = $("#date").val().split(" - ");
//    axios.post("/Analysis/Calculate", {
//        Category: +$("#categories").val(),
//        TypeConsumableId: +$("#typeConsumable").val(),
//        BrandId: +$("#brand").val(),
//        Model: $("#model").val(),
//        DateStart: moment(dates[0], 'DD.MM.YYYY').toDate(),
//        DateEnd: moment(dates[1], 'DD.MM.YYYY').toDate(),
//    }).then(function () {    })
//})

////Вывод Select
////Select

////Тип расходного материала
//$("#typeConsumable").select2({
//    width: '100%',
//    allowClear: true,
//    placeholder: 'Наименование типа',
//    ajax: {
//        transport: (data, success, failure) => {
//            let params = data.data;
//            let maxResultCount = 30;

//            params.page = params.page || 1;

//            let filter = {};
//            filter.maxResultCount = maxResultCount;
//            filter.skipCount = (params.page - 1) * maxResultCount;
//            filter.keyword = params.term
//            axios.get("/TypeConsumable/GetAll", { params: filter }).then(function (result) {

//                success({
//                    results: result.data.items,
//                    pagination: {
//                        more: (params.page * maxResultCount) < result.data.totalCount
//                    }
//                });
//            });
//        },
//        cache: true
//    },
//    templateResult: (data) => data.name,
//    templateSelection: (data) => data.name
//})

////Бренд техниеского средства
//$("#brand").select2({
//    width: '100%',
//    allowClear: true,
//    placeholder: 'Наименование бренда',
//    ajax: {
//        transport: (data, success, failure) => {
//            let params = data.data;
//            let maxResultCount = 30;

//            params.page = params.page || 1;

//            let filter = {};
//            filter.maxResultCount = maxResultCount;
//            filter.skipCount = (params.page - 1) * maxResultCount;
//            filter.keyword = params.term
//            axios.get("/Brand/GetAll", { params: filter }).then(function (result) {

//                success({
//                    results: result.data.items,
//                    pagination: {
//                        more: (params.page * maxResultCount) < result.data.totalCount
//                    }
//                });
//            });
//        },
//        cache: true
//    },
//    templateResult: (data) => data.name,
//    templateSelection: (data) => data.name
//})

////Модель техниеского средства
//$("#model").select2({
//    width: '100%',
//    allowClear: true,
//    placeholder: 'Наименование модели',
//    ajax: {
//        transport: (data, success, failure) => {
//            let params = data.data;
//            let maxResultCount = 30;

//            params.page = params.page || 1;

//            let filter = {};
//            filter.maxResultCount = maxResultCount;
//            filter.skipCount = (params.page - 1) * maxResultCount;
//            filter.keyword = params.term
//            axios.get("/TechnicalEquipment/GetAllModel", { params: filter }).then(function (result) {

//                success({
//                    results: result.data.items.map(x => ({ id: x, name: x })),
//                    pagination: {
//                        more: (params.page * maxResultCount) < result.data.totalCount
//                    }
//                });
//            });
//        },
//        cache: true
//    },
//    templateResult: (data) => data.name,
//    templateSelection: (data) => data.name
//})


$(document).ready(function () {
    initSelects();

    // Клик по кнопке "Применить" для неисправностей
    $("#CalculateBtn").click(function () {
        $("#loadingSpinner").show();

        let equipmentType = $("#equipmentType").val();
        let brand = $("#brand").val();
        let model = $("#model").val();

        setTimeout(function () {
            $("#loadingSpinner").hide();

            let forecastData = generateFaultsForecast(equipmentType, brand, model);
            updateChart(faultsChart, forecastData.labels, forecastData.values);
            fillTable("#faultsForecastTable", forecastData.labels, forecastData.values);
        }, 2000);
    });

    // Клик по кнопке "Применить" для расходных материалов
    $("#CalculateConsumableBtn").click(function () {
        $("#loadingSpinner").show();

        let consumableType = $("#consumableType").val();
        let consumableBrand = $("#consumableBrand").val();
        let consumableModel = $("#consumableModel").val();

        setTimeout(function () {
            $("#loadingSpinner").hide();

            let forecastData = generateConsumableForecast(consumableType, consumableBrand, consumableModel);
            updateChart(consumablesChart, forecastData.labels, forecastData.values);
            fillTable("#consumablesForecastTable", forecastData.labels, forecastData.values);
        }, 2000);
    });

    // Клик по кнопке "Обучить"
    $("#TrainingBtn").click(function () {
        $("#loadingSpinner").show();

        setTimeout(function () {
            $("#loadingSpinner").hide();
            alert("Обучение завершено!");
        }, 1500);
    });
});

// Инициализация селектов из БД
function initSelects() {
    loadSelect("#equipmentType", "/TypeConsumable/GetAll", "Выберите тип...");
    loadSelect("#brand", "/Brand/GetAll", "Выберите бренд...");
    loadSelect("#model", "/TechnicalEquipment/GetAllModel", "Выберите модель...");

    loadSelect("#consumableType", "/TypeConsumable/GetAll", "Выберите тип расходника...");
    loadSelect("#consumableBrand", "/Brand/GetAll", "Выберите бренд расходника...");
    loadSelect("#consumableModel", "/TechnicalEquipment/GetAllModel", "Выберите модель расходника...");
}

// Функция загрузки данных в select2
function loadSelect(selector, url, placeholder) {
    $(selector).select2({
        width: '100%',
        allowClear: true,
        placeholder: placeholder,
        ajax: {
            transport: (params, success, failure) => {
                let filter = {
                    maxResultCount: 30,
                    skipCount: (params.page || 1 - 1) * 30,
                    keyword: params.term
                };
                axios.get(url, { params: filter }).then(result => {
                    success({
                        results: result.data.items.map(item => ({ id: item.id, text: item.name })),
                        pagination: { more: (params.page * 30) < result.data.totalCount }
                    });
                });
            },
            cache: true
        }
    });
}

// Функция генерации данных прогноза неисправностей
function generateFaultsForecast(equipmentType, brand, model) {
    let months = getNextMonths(5);
    let values = months.map(() => Math.floor(Math.random() * 30) + 5);

    return {
        labels: months,
        values: values
    };
}

// Функция генерации данных прогноза расходных материалов
function generateConsumableForecast(type, brand, model) {
    let months = getNextMonths(5);
    let values = months.map(() => Math.floor(Math.random() * 50) + 10);

    return {
        labels: months,
        values: values
    };
}

// Получение названий месяцев
function getNextMonths(count) {
    let monthNames = [
        "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
        "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
    ];
    let today = new Date();
    let months = [];

    for (let i = 0; i < count; i++) {
        let nextMonth = new Date(today.getFullYear(), today.getMonth() + i, 1);
        months.push(monthNames[nextMonth.getMonth()]);
    }

    return months;
}

// Функция обновления графика
function updateChart(chart, labels, data) {
    chart.data.labels = labels;
    chart.data.datasets[0].data = data;
    chart.update();
}

// Функция заполнения таблицы
function fillTable(tableId, labels, values) {
    let tbody = $(tableId).find("tbody");
    tbody.empty();
    for (let i = 0; i < labels.length; i++) {
        tbody.append(`<tr><td>${labels[i]}</td><td>${values[i]}</td></tr>`);
    }
}

// График неисправностей
let faultLineCtx = document.getElementById("faultLineChart").getContext("2d");
let faultsChart = new Chart(faultLineCtx, {
    type: 'line',
    data: {
        labels: [],
        datasets: [{
            label: 'Ожидаемые неисправности',
            data: [],
            borderColor: 'rgba(255, 99, 132, 1)',
            fill: false
        }]
    },
    options: { responsive: true }
});

// График расходных материалов
let consumablesCtx = document.getElementById("consumablesChart").getContext("2d");
let consumablesChart = new Chart(consumablesCtx, {
    type: 'bar',
    data: {
        labels: [],
        datasets: [{
            label: 'Прогноз расхода материалов',
            data: [],
            backgroundColor: 'rgba(54, 162, 235, 0.7)'
        }]
    },
    options: {
        responsive: true,
        scales: {
            y: { beginAtZero: true }
        }
    }
});