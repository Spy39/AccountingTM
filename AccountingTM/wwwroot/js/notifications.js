$(document).ready(function () {
    loadUserNotifications();

    $("#markAllRead").click(function () {
        markAllNotificationsAsRead();
    });
});

function loadUserNotifications() {
    $.get('/Notification/GetAllUserNotifications', function (data) {
        let tableBody = $("#notificationTableBody");
        tableBody.empty();

        if (data.length === 0) {
            tableBody.append('<tr><td colspan="3" class="text-center">Нет уведомлений</td></tr>');
            return;
        }

        data.forEach(notification => {
            let status = notification.isRead ? '<span class="badge bg-success">Прочитано</span>' : '<span class="badge bg-warning">Непрочитано</span>';
            tableBody.append(`
                <tr>
                    <td>${notification.message}</td>
                    <td>${new Date(notification.createdAt).toLocaleString()}</td>
                    <td>${status}</td>
                </tr>
            `);
        });
    });
}

function markAllNotificationsAsRead() {
    $.post('/Notification/MarkAsRead', function () {
        loadUserNotifications();
    });
}
