import initTableBrands from "./brand.js"
import initTableCategories from "./category.js"
import initTableEmployees from "./employee.js"
import initTableIndicators from "./indicator.js"
import initTableLocations from "./location.js"
import initTableSets from "./set.js"
import initTableConsumables from "./typeConsumable.js"
import initTableTypes from "./typeEquipment.js"
import initTableUnits from "./unit.js"

$("#nav-page .nav-link").click(function () {
    const url = $(this).data("url")
    axios.get(url)
        .then(function (result) {
            $("#page-content").html(result.data)

            //Типы технических средств
            initTableTypes();

            //Типы расходных материалоы
            initTableConsumables();

            //Бренды>
            initTableBrands();

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

})

$('[data-url="Directory/TypeEquipment"]').trigger("click")