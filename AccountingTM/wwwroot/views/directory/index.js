$("#nav-page .nav-link").click(function () {
    const url = $(this).data("url")
    axios.get(url)
        .then(function (result) {
            $("#page-content").html(result.data)
        })

})
//Brand
$(document).on("click", "#createBrandBtn", function () {
    
    axios.post("Brand/Create", {
        name: $("#brand").val(),

    }).then(function () {
        location.reload()
    })
})
//Type
$(document).on("click", "#createTypeEquipmentBtn", function () {

    axios.post("TypeEquipment/Create", {
        name: $("#typeEquipment").val(),

    }).then(function () {
        location.reload()
    })
})
//Category
$(document).on("click", "#createCategoryBtn", function () {

    axios.post("Category/Create", {
        name: $("#category").val(),

    }).then(function () {
        location.reload()
    })
})
//Employee
$(document).on("click", "#createEmployeeBtn", function () {

    axios.post("Employee/Create", {
        name: $("#employee").val(),

    }).then(function () {
        location.reload()
    })
})
//Indicator
$(document).on("click", "#createIndicatorBtn", function () {

    axios.post("Indicator/Create", {
        name: $("#indicator").val(),

    }).then(function () {
        location.reload()
    })
})
//Location
$(document).on("click", "#createLocationBtn", function () {

    axios.post("Location/Create", {
        name: $("#location").val(),

    }).then(function () {
        location.reload()
    })
})
//Unit
$(document).on("click", "#createUnitBtn", function () {

    axios.post("Unit/Create", {
        name: $("#unit").val(),

    }).then(function () {
        location.reload()
    })
})