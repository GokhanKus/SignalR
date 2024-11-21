$(document).ready(function () {

    const broadcastMessageToAllClientHubMethodCall = "BroadcastMessageToAllClient";
    const receiveMessageForAllClientMethodCall = "ReceiveMessageForAllClient";

    const broadcastMessageToCallerClientMethodCall = "BroadcastMessageToCallerClient";
    const receiveMessageForCallerClient = "ReceiveMessageForCallerClient";

    const broadcastMessageToOtherClientMethodCall = "BroadcastMessageToOtherClient";
    const receiveMessageForOtherClient = "ReceiveMessageForOtherClient";

    const broadcastMessageToSpecificClientMethodCall = "BroadcastMessageToSpecificClient";
    const receiveMessageForSpecificClient = "ReceiveMessageForSpecificClient";

    const receiveCountOfAllConnectedClient = "ReceiveCountOfAllConnectedClient";

    const connection = new signalR.HubConnectionBuilder().withUrl("/exampleTypeSafehub").configureLogging(signalR.LogLevel.Information).build();

    function start() {
        connection.start().then(() => {
            console.log("hub ile baglanti kuruldu");
            $("#connectionId").html(`Connection Id : ${connection.connectionId}`);
        });
    }
    try {
        start();
    }
    catch {
        setTimeout(() => start(), 5000)
    }

    const span_client_count = $("#span-connected-client-count");
    connection.on(receiveCountOfAllConnectedClient, (clientCount) => {
        span_client_count.text(clientCount);
        console.log("connected client count: ", clientCount);
    })



    //subscribe mesaji client olarak tuket..
    connection.on(receiveMessageForAllClientMethodCall, (message) => {
        console.log("gelen mesaj", message);
    })

    connection.on(receiveMessageForCallerClient, (message) => {
        console.log("(Caller) gelen mesaj", message);
    })

    connection.on(receiveMessageForOtherClient, (message) => {
        console.log("(Others) gelen mesaj", message);
    })

    connection.on(receiveMessageForSpecificClient, (message) => {
        console.log("(Specific) gelen mesaj", message);
    })


    $("#btn-send-message-all-client").click(function () {
        const message = "hello world";
        connection.invoke(broadcastMessageToAllClientHubMethodCall, message).catch(err => console.error("hata", err))
        console.log("mesaj gonderildi");
    })
    $("#btn-send-message-caller-client").click(function () {
        const message = "hello world";
        connection.invoke(broadcastMessageToCallerClientMethodCall, message).catch(err => console.error("hata", err))
        console.log("mesaj gonderildi");
    })
    $("#btn-send-message-others-client").click(function () {
        const message = "hello world";
        connection.invoke(broadcastMessageToOtherClientMethodCall, message).catch(err => console.error("hata", err))
        console.log("mesaj gonderildi");
    })
    $("#btn-send-message-specific-client").click(function () {
        const message = "hello world";
        const connectionId = $("#text-connectionId").val();
        connection.invoke(broadcastMessageToSpecificClientMethodCall, message, connectionId).catch(err => console.error("hata", err))
        console.log("mesaj gonderildi");
    })
})