import initTableBrands from "./brand.js"
import initTableCategories from "./category.js"
import initTableEmployees from "./employee.js"
import initTableIndicators from "./indicator.js"
import initTableLocations from "./location.js"
import initTableModels from "./model.js"
import initTableConsumables from "./typeConsumable.js"
import initTableTypes from "./typeEquipment.js"
import initTableUnits from "./unit.js"


$("#nav-page .nav-link").on("click", function () {
    $("#nav-page .nav-link")
        .removeClass("active bg-secondary  text-white border-start border-3 border-warning");
    
    $(this).addClass("active bg-secondary  text-white border-start border-3 border-warning");

    // Получаем URL из data-атрибута ссылки и загружаем содержимое
    const url = $(this).data("url");
    axios.get(url)
        .then(function (result) {
            $("#page-content").html(result.data);

            //Типы технических средств
            initTableTypes();

            //Типы расходных материалоы
            initTableConsumables();

            //Бренды
            initTableBrands();

            //Модели
            initTableModels();

            //Помещения
            initTableLocations();

            //Сотрудники
            initTableEmployees();

            //Показатели
            initTableIndicators();

            //Единицы измерения
            initTableUnits();

            //Категории заявок
            initTableCategories();
        })
        .catch(function (error) {
            console.error("Ошибка при загрузке страницы:", error);
        });
});

$('[data-url="Directory/TypeEquipment"]').trigger("click")