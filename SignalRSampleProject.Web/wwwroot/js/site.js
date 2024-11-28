﻿var toastTimeOut;

$(document).ready(function () {

    const connection = new window.signalR.HubConnectionBuilder().withUrl("/hub").build();

    connection.start().then(() => { console.log("baglanti saglandi") })

    connection.on("AlertCompleteFile", (downloadPath) => {

        clearTimeout(toastTimeOut);

        $(".toast-body").html(`<p>Excel oluşturma işlemi tamamlanmıştır. Aşağıdaki link ile excel dosyasını indirebilirsiniz<p>
        <a href="${downloadPath}">indir</a>
        `);

        $("#liveToast").show();
    })
})