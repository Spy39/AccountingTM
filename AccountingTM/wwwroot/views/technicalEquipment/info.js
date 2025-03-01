$(function () {
    $('#date').datepicker({ dateFormat: "dd.mm.yyyy" });
    $('#dateStart').datepicker({ dateFormat: "dd.mm.yyyy" });

    $('#dateCompletedWork').datepicker({ container: '#addCompletedWorkModal', dateFormat: "dd.mm.yyyy" });
    $('#dateReceptionAndTransmission').datepicker({ container: '#addReceptionAndTransmissionModal', dateFormat: "dd.mm.yyyy" });
    $('#dateRepair').datepicker({ container: '#addRepairModal', dateFormat: "dd.mm.yyyy" });
    $('#dateAcceptance').datepicker({ container: '#addStorageModal', dateFormat: "dd.mm.yyyy" });
    $('#dateRemoval').datepicker({ container: '#addStorageModal', dateFormat: "dd.mm.yyyy" });
    $('#dateConservation').datepicker({ container: '#addConservationModal', dateFormat: "dd.mm.yyyy" });
    $('#datePeriod').datepicker({ container: '#addConservationModal', dateFormat: "dd.mm.yyyy" });
    $('#dateDisposalInformation').datepicker({ container: '#addDisposalInformationModal', dateFormat: "dd.mm.yyyy" });

    //Редактирование основной информации о ТС
    $(document).on("click", ".edit.basicEquipment", function () {
        let id = $(this).data("id"); // или this.dataset.id

        // Делаем GET-запрос, чтобы получить текущие данные о ТС
        axios.get('/TechnicalEquipmentInfo/Get', {
            params: { id }   // id: id
        })
            .then(function (response) {
                // response.data = объект TechnicalEquipment
                const te = response.data;

                // Заполняем скрытый input для ID
                $("#editBasicId").val(te.id);

                // Заполняем поля select:
                $("#typeEquipment").val(te.typeId).trigger("change");
                $("#brand").val(te.brandId).trigger("change");
                $("#model").val(te.modelId).trigger("change");

                // Заполняем select для состояния (используется enum State)
                $("#state").val(te.state);

                // Открываем модальное окно
                $("#editBasicModal").modal("show");
            })
            .catch(function (error) {
                console.error("Ошибка при получении основной инфы:", error);
                alert("Не удалось загрузить данные о техническом средстве");
            });
    });

    // Нажатие кнопки "Сохранить" в модальном окне editBasicModal
    $("#editBasicBtn").click(function () {
        // Собираем данные из полей
        let payload = {
            id: +$("#editBasicId").val(),
            typeId: +$("#typeEquipment").val(),
            brandId: +$("#brand").val(),
            modelId: +$("#model").val(),
            state: +$("#state").val()
        };

        // Делаем POST-запрос на сервер, чтобы обновить основную информацию
        axios.post("/TechnicalEquipmentInfo/Update", payload)
            .then(function () {
                // Успешно сохранили — перезагружаем страницу (или обновляем таблицу)
                location.reload();
            })
            .catch(function (error) {
                console.error("Ошибка при сохранении основной инфы:", error);
                alert("Не удалось сохранить изменения.");
            });
    });

    //Редактирование дополнительной информации о ТС
    $(document).on("click", ".edit.additionalEquipment", function () {
        let id = $(this).data("id");

        // Делаем GET-запрос, чтобы получить текущие данные (в том числе серийный номер и др.)
        axios.get('/TechnicalEquipmentInfo/GetAditional', {
            params: { id }
        })
            .then(function (response) {
                const te = response.data;

                // Заполняем скрытое поле с ID
                $("#editAdditionalId").val(te.id);

                // Заполняем формы дополнительной информации
                $("#serialNumber").val(te.serialNumber || "");
                $("#inventoryNumber").val(te.inventoryNumber || "");

                // Выпадающие списки для сотрудника и локации:
                $("#employee").val(te.employeeId).trigger("change");
                $("#location").val(te.locationId).trigger("change");

                // Даты
                // В зависимости от формата даты на сервере, возможно нужно форматирование
                if (te.date) $("#date").val(formatDateForInput(te.date));
                if (te.dateStart) $("#dateStart").val(formatDateForInput(te.dateStart));
                if (te.dateEnd) $("#dateEnd").val(formatDateForInput(te.dateEnd));
                if (te.dateGarant) $("#dateGarant").val(formatDateForInput(te.dateGarant));

                // Открываем модальное окно
                $("#editAdditionalModal").modal("show");
            })
            .catch(function (error) {
                console.error("Ошибка при получении доп. инфы:", error);
                alert("Не удалось загрузить данные о техническом средстве");
            });
    });

    // Нажатие кнопки "Сохранить" в модальном окне editAdditionalModal
    $("#editAdditionalBtn").click(function () {
        // Собираем данные
        let payload = {
            id: +$("#editAdditionalId").val(),
            serialNumber: $("#serialNumber").val(),
            inventoryNumber: $("#inventoryNumber").val(),
            employeeId: +$("#employee").val(),
            locationId: +$("#location").val(),
            date: $("#date").val(),
            dateStart: $("#dateStart").val(),
            dateEnd: $("#dateEnd").val(),
            dateGarant: $("#dateGarant").val()
        };

        // Делаем POST-запрос на сервер, чтобы обновить доп. информацию
        axios.post("/TechnicalEquipmentInfo/UpdateAditional", payload)
            .then(function () {
                // Успешно сохранили
                location.reload();
            })
            .catch(function (error) {
                console.error("Ошибка при сохранении доп. инфы:", error);
                alert("Не удалось сохранить изменения.");
            });
    });

    function formatDateForInput(dateStr) {
        // Если приходит "2025-02-15T00:00:00" или "2025-02-15" 
        // то вырезаем первые 10 символов:
        return dateStr.substring(0, 10);
    }

    //списание
    $(document).on("click", "#writeOffEquipmentBtn", function () {
        let equipmentId = $(this).data("id"); // Получаем ID ТС
        if (!equipmentId) {
            alert("Ошибка: не найден ID оборудования!");
            return;
        }

        Swal.fire({
            title: "Вы уверены?",
            text: "Вы действительно хотите списать это техническое средство?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Да, списать",
            cancelButtonText: "Отмена"
        }).then((result) => {
            if (result.isConfirmed) {
                axios.post("/TechnicalEquipment/WriteOff?id=" + equipmentId)
                    .then(() => {
                        Swal.fire("Списано!", "Техническое средство было успешно списано.", "success");
                        location.reload(); // Обновляем страницу
                    })
                    .catch(error => {
                        console.error("Ошибка при списании:", error);
                        Swal.fire("Ошибка", "Не удалось списать техническое средство.", "error");
                    });
            }
        });
    });

    $('#printTableBtn').on('click', function () {
        // Клонируем блок с данными из инпутов (первый такой блок на странице)
        let inputInfoClone = $('.shadow-sm.p-3.mb-1.bg-body-tertiary.rounded').first().clone();
        // Удаляем из него кнопки и ссылки, которые не нужны в печатной версии
        inputInfoClone.find('button, a').remove();

        // Клонируем основной контент (с таблицами)
        let equipmentInfoClone = $('#equipmentInfo').clone();
        // Удаляем из клона все кнопки, ссылки и элементы с классом .card-tools (панель управления карточками)
        equipmentInfoClone.find('button, a, .card-tools').remove();

        // Собираем итоговый HTML для печати с разделителем между блоками (если нужно, чтобы данные выводились на разных страницах)
        let printContent = `
        <div>
            ${inputInfoClone.prop('outerHTML')}
        </div>
        <div class="page-break"></div>
        <div>
            ${equipmentInfoClone.prop('outerHTML')}
        </div>
    `;

        // Открываем новое окно для печати
        let printWindow = window.open('', '', 'width=900,height=700');
        printWindow.document.write(`
        <html>
            <head>
                <title>Печать информации о ТС</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 20px; }
                    .page-break { page-break-after: always; }
                    h2 { text-align: center; }
                    /* Дополнительные стили для корректного отображения печатной версии */
                </style>
            </head>
            <body>
                ${printContent}
            </body>
        </html>
    `);
        printWindow.document.close();
        printWindow.onload = function () {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        };
    });

    //Вывод Select

    //Тип технического средства
        $("#typeEquipment").select2({
            width: '100%',
            allowClear: true,
            placeholder: 'Наименование типа',
            ajax: {
                transport: (data, success, failure) => {
                    let params = data.data;
                    let maxResultCount = 30;

                    params.page = params.page || 1;

                    let filter = {};
                    filter.maxResultCount = maxResultCount;
                    filter.skipCount = (params.page - 1) * maxResultCount;
                    filter.keyword = params.term
                    axios.get("/TypeEquipment/GetAll", { params: filter }).then(function (result) {

                        success({
                            results: result.data.items,
                            pagination: {
                                more: (params.page * maxResultCount) < result.data.totalCount
                            }
                        });
                    });
                },
                cache: true
            },
            templateResult: (data) => data.name,
            templateSelection: (data) => data.name
        })

    //Бренд техниеского средства
        $("#brand").select2({
            width: '100%',
            allowClear: true,
            placeholder: 'Наименование бренда',
            ajax: {
                transport: (data, success, failure) => {
                    let params = data.data;
                    let maxResultCount = 30;

                    params.page = params.page || 1;

                    let filter = {};
                    filter.maxResultCount = maxResultCount;
                    filter.skipCount = (params.page - 1) * maxResultCount;
                    filter.keyword = params.term
                    axios.get("/Brand/GetAll", { params: filter }).then(function (result) {

                        success({
                            results: result.data.items,
                            pagination: {
                                more: (params.page * maxResultCount) < result.data.totalCount
                            }
                        });
                    });
                },
                cache: true
            },
            templateResult: (data) => data.name,
            templateSelection: (data) => data.name
        })

    //Модель техниеского средства
    $("#model").select2({
        width: '100%',
        allowClear: true,
        placeholder: 'Наименование модели',
        ajax: {
            transport: (data, success, failure) => {
                let params = data.data;
                let maxResultCount = 30;

                params.page = params.page || 1;

                let filter = {};
                filter.maxResultCount = maxResultCount;
                filter.skipCount = (params.page - 1) * maxResultCount;
                filter.keyword = params.term
                axios.get("/Model/GetAll", { params: filter }).then(function (result) {

                    success({
                        results: result.data.items,
                        pagination: {
                            more: (params.page * maxResultCount) < result.data.totalCount
                        }
                    });
                });
            },
            cache: true
        },
        templateResult: (data) => data.name,
        templateSelection: (data) => data.name
    })

})
