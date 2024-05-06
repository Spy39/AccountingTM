$("#nav-page .nav-link").click(function () {
    const url = $(this).data("url")
    axios.get(url)
        .then(function (result) {
            $("#page-content").html(result.data)
        })

})

$(document).on("click", "#createBrandBtn", function () {
    
    axios.post("Brand/Create", {
        name: $("#brand").val(),

    }).then(function () {
        location.reload()
    })
})