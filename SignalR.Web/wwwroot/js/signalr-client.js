$(document).ready(function () {

    const connection = new signalR.HubConnectionBuilder().withUrl("/examplehub").configureLogging(signalR.LogLevel.Information).build();

    function start() {
        connection.start().then(() => console.log("hub ile baglanti kuruldu"));
    }
    try {
        start();
    }
    catch {
        setTimeout(()=>start(),5000)
    }
})