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

Console.ReadKey();