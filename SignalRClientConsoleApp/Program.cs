using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("SignalR console client");

var connection = new HubConnectionBuilder().WithUrl("https://localhost:7099/exampleTypeSafehub").Build();

connection.StartAsync().ContinueWith(result =>
{
	Console.WriteLine(result.IsCompletedSuccessfully ? "Connected" : "Connection Failed");
});

connection.On<Product>("ReceiveComplexMessageForAllClient", product =>
{
	Console.WriteLine($"Received Message: {product.Id} - {product.Name} - {product.Price}");
});

while (true)
{
	var key = Console.ReadLine();
	if (key == "exit") break;
	var product = new Product(50, "pen 50", 200);
	await connection.InvokeAsync("BroadcastComplexMessageToAllClient", product);
	//await connection.InvokeAsync("BroadcastMessageToAllClient", key);
}
