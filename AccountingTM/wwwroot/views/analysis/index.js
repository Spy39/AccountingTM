$(document).ready(function () {
    console.log("✅ Документ загружен");

    initSelects();

    // 🛠️ Клик по кнопке "Применить" (прогноз расходников)
    $("#CalculateBtn").click(function () {
        console.log("🛠️ Клик по кнопке 'Применить'");
        $("#loadingSpinner").show();

        let consumableType = $("#typeConsumable").val();
        let consumableBrand = $("#brand").val();
        let consumableModel = $("#model").val();

        if (!consumableModel) {
            alert("⚠️ Выберите модель расходника!");
            $("#loadingSpinner").hide();
            return;
        }

        console.log("📡 Отправка запроса в контроллер...");

        axios.post("/Analysis/GetConsumableForecast", {
            TypeConsumableId: consumableType,
            BrandId: consumableBrand,
            Model: consumableModel
        })
            .then(function (response) {
                $("#loadingSpinner").hide();
                console.log("📥 Ответ получен:", response.data);

                if (response.data.error) {
                    alert(`⚠️ Ошибка: ${response.data.error}`);
                    return;
                }

                let forecastData = response.data;
                updateChart(consumablesChart, forecastData.map(x => x.month), forecastData.map(x => x.usage));
                fillTable("#consumablesForecastTable", forecastData.map(x => x.month), forecastData.map(x => x.usage));

                $("#CalculateBtn").prop("disabled", true); // Блокируем кнопку после первого использования
            })
            .catch(function (error) {
                console.error("❌ Ошибка при получении прогноза:", error);
                alert("❌ Ошибка при расчете. Проверьте консоль.");
                $("#loadingSpinner").hide();
            });
    });

    // 🔬 Клик по кнопке "Обучить" (имитация)
    $("#TrainingBtn").click(function () {
        console.log("🧠 Обучение началось...");
        $("#loadingSpinner").show();
        setTimeout(function () {
            $("#loadingSpinner").hide();
            alert("🧠 Обучение завершено!");
        }, 1500);
    });
});

// 🎯 Функция обновления графика
function updateChart(chart, labels, data) {
    console.log(`📊 Обновление графика (${chart.canvas.id})`);
    chart.data.labels = labels;
    chart.data.datasets[0].data = data;
    chart.update();
}

// 📊 Функция заполнения таблицы
function fillTable(tableId, labels, values) {
    console.log(`📋 Заполнение таблицы (${tableId})`);
    let tbody = $(tableId).find("tbody");
    tbody.empty();
    for (let i = 0; i < labels.length; i++) {
        tbody.append(`<tr><td>${labels[i]}</td><td>${values[i]}</td></tr>`);
    }
}

// График расходных материалов
let consumablesCtx = document.getElementById("consumablesBarChart").getContext("2d");
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
