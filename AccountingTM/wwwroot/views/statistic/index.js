document.addEventListener("DOMContentLoaded", function () {
    if (!window.chartData || !window.chartData.tech || !window.chartData.application || !window.chartData.consumable) {
        console.error("chartData не определён или содержит ошибки!");
        return;
    }


    $(document).ready(function () {
        // 🗓️ Инициализация datepicker
        $('#dateStatistic').datepicker({
            range: true,
            multipleDatesSeparator: ' - ',
            dateFormat: 'dd.mm.yyyy'
        });

        // 🔄 Применение фильтров
        $('#applyFiltersBtn').click(function () {
            let dateRange = $('#dateStatistic').val().split(' - '); // Получаем даты
            let startDate = dateRange.length > 0 ? dateRange[0] : null;
            let endDate = dateRange.length > 1 ? dateRange[1] : null;

            $.ajax({
                url: '/Statistic/GetStatistics',
                method: 'POST',
                data: { startDate: startDate, endDate: endDate },
                success: function (response) {
                    updateStatistics(response);
                },
                error: function (xhr, status, error) {
                    console.error('Ошибка получения данных:', error);
                }
            });
        });

        // 📤 Экспорт и печать
        $('#exportExcelBtn').click(() => alert('Экспорт в Excel'));
        $('#exportPdfBtn').click(() => alert('Экспорт в PDF'));
        $('#printBtn').click(() => window.print());

        // 🚀 Первичное обновление данных
        updateStatistics();
    });

    // 🔄 Функция обновления UI после получения данных
    function updateStatistics(data) {
        if (!data) return;

        // 🟢 Обновление данных карточек
        $('#techTotal').text(data.techicalEquipment.totalCount);
        $('#techFault').text(data.techicalEquipment.faultCount);
        $('#techActive').text(data.techicalEquipment.activeCount);
        $('#techWorkable').text(data.techicalEquipment.workableCount);
        $('#techInoperable').text(data.techicalEquipment.inoperableCount);
        $('#techWrittenOff').text(data.techicalEquipment.writtenOffCount);

        $('#appTotal').text(data.application.totalCount);
        $('#appSolved').text(data.application.solvedCount);
        $('#appNew').text(data.application.newCount);
        $('#appInProgress').text(data.application.inProgressRequestsCount);
        $('#appTransferred').text(data.application.transferredCount);
        $('#appSuspended').text(data.application.suspendedCount);

        $('#consumableTotal').text(data.consumable.totalCount);
        $('#consumableInStock').text(data.consumable.inStockCount);
        $('#consumableLowStock').text(data.consumable.lowStockCount);
        $('#consumableOutOfStock').text(data.consumable.outOfStockCount);
        $('#consumableAvgUsage').text(data.consumable.avgUsagePerMonth.toFixed(2) + ' шт./мес.');
        $('#mostUsedConsumable').text(data.consumable.mostUsedConsumable);

        console.log('Данные обновлены:', data);
    }

    // Функция для получения контекста холста
    function getCanvasContext(id) {
        let canvas = document.getElementById(id);
        return canvas ? canvas.getContext("2d") : null;
    }

    // Общие параметры для всех графиков
    const defaultChartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        animation: {
            duration: 1500,
            easing: 'easeInOutQuart'
        }
    };

    // ✅ График "Технические средства" (Doughnut)
    try {
        let techData = window.chartData.tech;
        if (!techData) throw new Error("Данные techData отсутствуют!");

        let ctxTech = getCanvasContext("techEquipChart");
        if (ctxTech) {
            new Chart(ctxTech, {
                type: 'doughnut',
                data: {
                    labels: ['Исправные', 'Неисправные', 'Работоспособные', 'Неработоспособные', 'Списанные'],
                    datasets: [{
                        data: [
                            techData.activeCount,
                            techData.faultCount,
                            techData.workableCount,
                            techData.inoperableCount,
                            techData.writtenOffCount
                        ],
                        backgroundColor: [
                            'rgba(40,167,69,0.7)',
                            'rgba(220,53,69,0.7)',
                            'rgba(23,162,184,0.7)',
                            'rgba(255,193,7,0.7)',
                            'rgba(108,117,125,0.7)'
                        ]
                    }]
                },
                options: defaultChartOptions
            });
        }
    } catch (error) {
        console.error("Ошибка при отрисовке techEquipChart:", error);
    }

    // ✅ График "Заявки" (Bar Chart)
    try {
        let appData = window.chartData.application;
        if (!appData) throw new Error("Данные appData отсутствуют!");

        let ctxApp = getCanvasContext("applicationsChart");
        if (ctxApp) {
            new Chart(ctxApp, {
                type: 'bar',
                data: {
                    labels: ['Решены', 'Новые', 'В работе', 'Переданы', 'Приостановлены'],
                    datasets: [{
                        label: 'Количество заявок',
                        data: [
                            appData.solvedCount,
                            appData.newCount,
                            appData.inProgressCount,
                            appData.transferredCount,
                            appData.suspendedCount
                        ],
                        backgroundColor: [
                            'rgba(0,123,255,0.7)',
                            'rgba(108,117,125,0.7)',
                            'rgba(40,167,69,0.7)',
                            'rgba(220,53,69,0.7)',
                            'rgba(255,193,7,0.7)'
                        ]
                    }]
                },
                options: defaultChartOptions
            });
        }
    } catch (error) {
        console.error("Ошибка при отрисовке applicationsChart:", error);
    }

    // ✅ График "Расходные материалы" (Doughnut)
    try {
        let consData = window.chartData.consumable;
        if (!consData) throw new Error("Данные consData отсутствуют!");

        let ctxCons = getCanvasContext("consumablesChart");
        if (ctxCons) {
            new Chart(ctxCons, {
                type: 'doughnut',
                data: {
                    labels: ['В наличии', 'Малый запас', 'Отсутствуют'],
                    datasets: [{
                        data: [
                            consData.inStockCount,
                            consData.lowStockCount,
                            consData.outOfStockCount
                        ],
                        backgroundColor: [
                            'rgba(23,162,184,0.7)',
                            'rgba(255,193,7,0.7)',
                            'rgba(220,53,69,0.7)'
                        ]
                    }]
                },
                options: defaultChartOptions
            });
        }
    } catch (error) {
        console.error("Ошибка при отрисовке consumablesChart:", error);
    }


    // 🔴 Топ-5 самых ненадёжных ТС (Bar Chart)
    try {
        let faultyEquip = window.chartData.faultyEquipment;
        if (!Array.isArray(faultyEquip) || faultyEquip.length === 0) {
            console.warn("Нет данных для faultyEquipmentChart");
        } else {
            let labels = faultyEquip.map(x => `${x.Brand} ${x.EquipmentModel}`);
            let data = faultyEquip.map(x => x.FaultCount);

            renderBarChart("faultyEquipmentChart", "Частота поломок", labels, data);
        }
    } catch (error) {
        console.error("Ошибка при отрисовке faultyEquipmentChart:", error);
    }

    // 🔴 Неисправности за 12 месяцев (Line Chart)
    try {
        let faultsByMonth = window.chartData.faultsByMonth;
        if (!Array.isArray(faultsByMonth) || faultsByMonth.length === 0) {
            console.warn("Нет данных для faultsByMonthChart");
        } else {
            let labels = faultsByMonth.map(x => x.Month);
            let data = faultsByMonth.map(x => x.FaultCount);

            renderLineChart("faultsByMonthChart", "Неисправности", labels, data);
        }
    } catch (error) {
        console.error("Ошибка при отрисовке faultsByMonthChart:", error);
    }

    // 🟢 Топ-5 расходников (Bar Chart)
    try {
        let topCons = window.chartData.topConsumables;
        if (!Array.isArray(topCons) || topCons.length === 0) {
            console.warn("Нет данных для topConsumablesChart");
        } else {
            let limitedData = topCons.slice(0, 5);
            let labels = limitedData.map(x => x.ConsumableName);
            let data = limitedData.map(x => x.UsageCount);

            renderBarChart("topConsumablesChart", "Частое использование", labels, data);
        }
    } catch (error) {
        console.error("Ошибка при отрисовке topConsumablesChart:", error);
    }

    // 🟠 Среднее время закрытия заявки (Line Chart)
    try {
        let avgClosure = window.chartData.avgClosureTime;
        if (!Array.isArray(avgClosure) || avgClosure.length === 0) {
            console.warn("Нет данных для avgClosureTimeChart");
        } else {
            let labels = avgClosure.map(x => x.Category);
            let data = avgClosure.map(x => x.AvgDays);

            renderLineChart("avgClosureTimeChart", "Среднее время закрытия (дней)", labels, data);
        }
    } catch (error) {
        console.error("Ошибка при отрисовке avgClosureTimeChart:", error);
    }

    // 🟠 Категории неисправностей (Pie Chart)
    try {
        let faultCats = window.chartData.faultCategories;
        if (!Array.isArray(faultCats) || faultCats.length === 0) {
            console.warn("Нет данных для faultCategoriesChart");
        } else {
            let labels = faultCats.map(x => x.CategoryName);
            let data = faultCats.map(x => x.Count);

            renderPieChart("faultCategoriesChart", labels, data);
        }
    } catch (error) {
        console.error("Ошибка при отрисовке faultCategoriesChart:", error);
    }

});

// 🔥 Функции отрисовки
function renderBarChart(id, label, labels, data) {
    let ctx = getCanvasContext(id);
    if (!ctx) return;

    new Chart(ctx, {
        type: "bar",
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                backgroundColor: "rgba(75, 192, 192, 0.6)"
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            animation: {
                duration: 1000,
                easing: 'easeOutQuart'
            }
        }
    });
}

function renderLineChart(id, label, labels, data) {
    try {
        let canvas = document.getElementById(id);
        if (!canvas) {
            console.warn(`Элемент с id '${id}' не найден. График '${label}' не будет построен.`);
            return;
        }

        let ctx = canvas.getContext("2d");

        new Chart(ctx, {
            type: "line",
            data: {
                labels: labels,
                datasets: [{
                    label: label,
                    data: data,
                    borderColor: "rgba(255, 99, 132, 1)",
                    backgroundColor: "rgba(255, 99, 132, 0.2)", // Полупрозрачный фон линии
                    pointBackgroundColor: "rgba(255, 99, 132, 1)", // Цвет точек
                    pointBorderColor: "#fff", // Белая граница точек
                    pointRadius: 5, // Увеличенный размер точек
                    fill: true, // Закрашенная область под графиком
                    tension: 0.3 // Гладкие линии
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                animation: {
                    duration: 1200,
                    easing: 'easeOutQuart'
                },
                plugins: {
                    legend: {
                        display: true,
                        position: "top"
                    }
                }
            }
        });
    } catch (error) {
        console.error(`Ошибка при отрисовке '${id}':`, error);
    }
}

function renderPieChart(id, labels, data) {
    try {
        let canvas = document.getElementById(id);
        if (!canvas) {
            console.warn(`Элемент с id '${id}' не найден. График Pie Chart не будет построен.`);
            return;
        }

        let ctx = canvas.getContext("2d");

        new Chart(ctx, {
            type: "pie",
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: [
                        "#FF6384", "#36A2EB", "#FFCE56", "#4BC0C0", "#9966FF",
                        "#FF9F40", "#4D5360", "#C9CB3E", "#B3A369", "#C87070"
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                animation: {
                    duration: 1000,
                    easing: 'easeOutQuart'
                },
                plugins: {
                    legend: {
                        display: true,
                        position: "top"
                    }
                }
            }
        });
    } catch (error) {
        console.error(`Ошибка при отрисовке '${id}':`, error);
    }
}
