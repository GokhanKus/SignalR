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
            console.error("hub ile baglanti kurulamadi",err)
            setTimeout(() => start(), 3000)
        }
    }

    connection.onclose(async () => {
        await start(); //baglanti koparsa tekrar baglansin
    })

    start();

})