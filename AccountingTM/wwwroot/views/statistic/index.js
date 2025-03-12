document.addEventListener("DOMContentLoaded", function () {
    if (!window.chartData || !window.chartData.tech || !window.chartData.application || !window.chartData.consumable) {
        console.error("chartData не определён или содержит ошибки!");
        return;
    }


    $(document).ready(function () {
        // 🗓️ Инициализация Datepicker
        $('#dateStatistic').datepicker({
            range: true,
            multipleDatesSeparator: ' - ',
            dateFormat: 'dd.mm.yyyy'
        });

        // 🔄 Применение фильтров
        $('#applyFiltersBtn').click(function () {
            let dateRange = $('#dateStatistic').val().split(' - ');
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
        $('#printBtn').click(() => window.print());

        // 🚀 Первичное обновление данных
        updateStatistics(window.chartData);
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


        // 🔄 Обновление таблиц
        //Топ 5 ненадежных ТС
        //let tableBody = $('#faultyEquipmentTable tbody');
        //tableBody.empty();
        //data.faultyEquipment.forEach(item => {
        //    let row = `<tr>
        //        <td>${item.equipmentModel || "Неизвестно"}</td>
        //        <td>${item.brand || "Неизвестно"}</td>
        //        <td>${item.faultCount}</td>
        //    </tr>`;
        //    tableBody.append(row);
        //});

        updateTable('#topConsumablesTable tbody', data.topConsumables);
        updateTable('#faultCategoriesTable tbody', data.faultCategories);

        // 🔄 Обновление графиков
        renderLineChart("faultsByMonthChart", "Неисправности", data.faultsByMonth.labels, data.faultsByMonth.values);
        renderPieChart("equipmentStateChart", ["Исправные", "Неисправные"], [data.tech.ActiveCount, data.tech.FaultCount]);
        renderBarChart("monthlyConsumablesChart", "Средний расход", data.monthlyConsumables.labels, data.monthlyConsumables.values);
        renderBarChart("avgClosureTimeChart", "Среднее время закрытия", data.avgClosureTime.labels, data.avgClosureTime.values);
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

    // 🔄 Функция обновления таблиц
    function updateTable(selector, items) {
        let tableBody = $(selector);
        tableBody.empty();
        items.forEach(item => {
            let row = `<tr>
                <td>${item.Name || item.CategoryName || "Неизвестно"}</td>
                <td>${item.FaultCount || item.UsageCount || item.Count}</td>
            </tr>`;
            tableBody.append(row);
        });
    }

    // 🎨 Функции для рендеринга графиков
    function renderBarChart(id, label, labels, data) {
        let ctx = document.getElementById(id)?.getContext("2d");
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
                maintainAspectRatio: false
            }
        });
    }

    function renderLineChart(id, label, labels, data) {
        let ctx = document.getElementById(id)?.getContext("2d");
        if (!ctx) return;

        new Chart(ctx, {
            type: "line",
            data: {
                labels: labels,
                datasets: [{
                    label: label,
                    data: data,
                    borderColor: "red",
                    fill: false
                }]
            },
            options: { responsive: true }
        });
    }

    function renderPieChart(id, labels, data) {
        let ctx = document.getElementById(id)?.getContext("2d");
        if (!ctx) return;

        new Chart(ctx, {
            type: "pie",
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: ["green", "red"]
                }]
            },
            options: { responsive: true }
        });
    }
});