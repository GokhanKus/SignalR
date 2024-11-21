$(document).ready(function () {

    const broadcastMessageToAllClientHubMethodCall = "BroadcastMessageToAllClient";
    const receiveMessageForAllClientMethodCall = "ReceiveMessageForAllClient";

    const connection = new signalR.HubConnectionBuilder().withUrl("/exampleTypeSafehub").configureLogging(signalR.LogLevel.Information).build();

    function start() {
        connection.start().then(() => console.log("hub ile baglanti kuruldu"));
    }
    try {
        start();
    }
    catch {
        setTimeout(()=>start(),5000)
    }

    //subscribe mesaji client olarak tuket..
    connection.on(receiveMessageForAllClientMethodCall, (message) => {

        console.log("gelen mesaj", message);

    })
    $("#btn-send-message-all-client").click(function () {

        const message = "hello world";

        connection.invoke(broadcastMessageToAllClientHubMethodCall, message).catch(err => console.error("hata", err))

    })
})