$(document).ready(function () {

    $("#btn-create-excel").click(function () {

        $.ajax({
            url: "/Home/CreateExcel",
            type: "GET",
            dataType: "json",
            success: function (data) {

                $(".toast-body").html("Excel olusturma istegi alinmistir, tamamlandiginda bilgilendirileceksiniz");

                $("#liveToast").show();

               toastTimeOut = setTimeout(() => {

                    $("#liveToast").hide();

                }, 3000)
            },
            error: function (err) {
                console.error(err);
            }
        })

    })

})
