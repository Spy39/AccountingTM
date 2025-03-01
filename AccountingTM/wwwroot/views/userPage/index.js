$(document).ready(function () {
    // Обработка отправки формы сброса пароля в модальном окне
    $("#resetPasswordForm").submit(function (event) {
        event.preventDefault();
        try {
            var newPassword = $("#newPassword").val().trim();
            var confirmPassword = $("#confirmPassword").val().trim();

            // Проверка заполненности полей
            if (newPassword === "" || confirmPassword === "") {
                toastr.error("Пожалуйста, заполните все поля");
                return;
            }

            // Проверка совпадения паролей
            if (newPassword !== confirmPassword) {
                toastr.error("Пароли не совпадают");
                return;
            }

            // Отправка запроса на сброс пароля через axios
            axios.post('/UserPage/ResetPassword', {
                NewPassword: newPassword,
                ConfirmPassword: confirmPassword
            })
                .then(function (response) {
                    toastr.success(response.data.message || "Пароль успешно изменен");
                    // Закрываем модальное окно и очищаем поля формы
                    $("#resetPasswordModal").modal("hide");
                    $("#newPassword").val("");
                    $("#confirmPassword").val("");
                })
                .catch(function (error) {
                    console.error("Ошибка при изменении пароля:", error);
                    if (error.response && error.response.data && error.response.data.message) {
                        toastr.error(error.response.data.message);
                    } else {
                        toastr.error("Ошибка при изменении пароля");
                    }
                });
        } catch (ex) {
            console.error("Ошибка в обработчике сброса пароля:", ex);
            toastr.error("Ошибка, попробуйте еще раз");
        }
    });
});