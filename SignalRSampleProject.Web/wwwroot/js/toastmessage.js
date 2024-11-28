$(document).ready(function () {
    $("#btn-create-excel").click(function () {

        $.ajax({
            url: "/Home/CreateExcel",
            type: "GET",
            dataType: "json",
            success: function (data) {

                $(".toast-body").html("Excel oluşturma isteği alınmıştır. Tamamlandığında bilgilendirileceksiniz.");

                $("#liveToast").show();

                toastTimeOut = setTimeout(() => {
                    $("#liveToast").hide();
                }, 3000)

            },
            error: function (err) {
                alert("Excel dosyasını indirebilmeniz için üye olmanız gerekmektedir.")
            }
        })
    })
});