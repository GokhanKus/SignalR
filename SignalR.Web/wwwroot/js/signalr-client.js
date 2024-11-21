$(document).ready(function () {

    const broadcastMessageToAllClientHubMethodCall = "BroadcastMessageToAllClient";
    const receiveMessageForAllClientMethodCall = "ReceiveMessageForAllClient";

    const broadcastMessageToCallerClientMethodCall = "BroadcastMessageToCallerClient";
    const receiveMessageForCallerClient = "ReceiveMessageForCallerClient";

    const receiveCountOfAllConnectedClient = "ReceiveCountOfAllConnectedClient";

    const connection = new signalR.HubConnectionBuilder().withUrl("/exampleTypeSafehub").configureLogging(signalR.LogLevel.Information).build();

    function start() {
        connection.start().then(() => console.log("hub ile baglanti kuruldu"));
    }
    try {
        start();
    }
    catch {
        setTimeout(() => start(), 5000)
    }

    //subscribe mesaji client olarak tuket..
    connection.on(receiveMessageForAllClientMethodCall, (message) => {
        console.log("gelen mesaj", message);
    })

    connection.on(receiveMessageForCallerClient, (message) => {
        console.log("(Caller) gelen mesaj", message);
    })

    var span_client_count = $("#span-connected-client-count");
    connection.on(receiveCountOfAllConnectedClient, (clientCount) => {
        span_client_count.text(clientCount);
        console.log("connected client count: ", clientCount);
    })
    $("#btn-send-message-all-client").click(function () {
        const message = "hello world";
        connection.invoke(broadcastMessageToAllClientHubMethodCall, message).catch(err => console.error("hata", err))
    })
    $("#btn-send-message-caller-client").click(function () {
        const message = "hello world";
        connection.invoke(broadcastMessageToCallerClientMethodCall, message).catch(err => console.error("hata", err))
    })
})