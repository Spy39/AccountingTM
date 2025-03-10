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
        updateTable('#faultyEquipmentTable tbody', data.faultyEquipment);
        updateTable('#topConsumablesTable tbody', data.topConsumables);
        updateTable('#faultCategoriesTable tbody', data.faultCategories);

        // 🔄 Обновление графиков
        renderLineChart("faultsByMonthChart", "Неисправности", data.faultsByMonth.labels, data.faultsByMonth.values);
        renderPieChart("equipmentStateChart", ["Исправные", "Неисправные"], [data.tech.ActiveCount, data.tech.FaultCount]);
        renderBarChart("monthlyConsumablesChart", "Средний расход", data.monthlyConsumables.labels, data.monthlyConsumables.values);
        renderBarChart("avgClosureTimeChart", "Среднее время закрытия", data.avgClosureTime.labels, data.avgClosureTime.values);
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