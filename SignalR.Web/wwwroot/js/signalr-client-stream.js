$(document).ready(function () {

    const connection = new signalR.HubConnectionBuilder().withUrl("/exampleTypeSafehub").configureLogging(signalR.LogLevel.Information).build();
    async function start() {
        try {
            await connection.start().then(() => {
                console.log("hub ile baglanti kuruldu");
                $("#connectionId").html(`Connection Id : ${connection.connectionId}`);
            });
        }
        catch (err) {
            console.error("hub ile baglanti kurulamadi", err)
            setTimeout(() => start(), 3000)
        }
    }

    connection.onclose(async () => {
        await start(); //baglanti koparsa tekrar baglansin
    })

    start();

    const broadcastStreamDataToAllClient = "BroadcastStreamDataToAllClient";
    const receiveMessageAsStreamForAllClient = "ReceiveMessageAsStreamForAllClient";

    connection.on(receiveMessageAsStreamForAllClient, (name) => {
        $("#streamBox").append(`<p>${name}</p>`);
    })

    $("#btn_FromClient_ToHub").click(function () {

        const names = $("#txt_stream").val();
        const namesAsChunk = names.split(";");

        const subject = new signalR.Subject();

        connection.send(broadcastStreamDataToAllClient, subject).catch(err => console.error(err))

        namesAsChunk.forEach((name) => {
            subject.next(name)
        });

        subject.complete();
    })



})