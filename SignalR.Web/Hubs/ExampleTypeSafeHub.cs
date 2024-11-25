using Microsoft.AspNetCore.SignalR;
using SignalR.Web.Models;

namespace SignalR.Web.Hubs;
public class ExampleTypeSafeHub : Hub<IExampleTypeSafeHub>
{
	private static int ConnectedClientCount = 0;
	public async Task BroadcastMessageToAllClient(string message)
	{
		await Clients.All.ReceiveMessageForAllClient(message);
	}
	public async Task BroadcastStreamDataToAllClient(IAsyncEnumerable<string> namesAsChunk)
	{
		await foreach (var nameAsChunk in namesAsChunk)
		{
			await Task.Delay(1000);
			await Clients.All.ReceiveMessageAsStreamForAllClient(nameAsChunk);
		}
	}
	public async Task BroadcastStreamProductToAllClient(IAsyncEnumerable<Product> productsAsChunk)
	{
		await foreach (var productAsChunk in productsAsChunk)
		{
			await Task.Delay(1000);
			await Clients.All.ReceiveProductAsStreamForAllClient(productAsChunk);
		}
	}
	public async IAsyncEnumerable<string> BroadcastFromHubToClient(int count)
	{
		foreach (var item in Enumerable.Range(1,count).ToList())
		{
			await Task.Delay(1000);
			yield return $"{item}.data";
		}
	}
	public async Task BroadcastComplexMessageToAllClient(Product product)
	{
		await Clients.All.ReceiveComplexMessageForAllClient(product);
	}
	public async Task BroadcastMessageToCallerClient(string message)
	{
		await Clients.Caller.ReceiveMessageForCallerClient(message);
	}
	public async Task BroadcastMessageToOtherClient(string message)
	{
		await Clients.Others.ReceiveMessageForOtherClient(message);
	}
	public async Task BroadcastMessageToSpecificClient(string message, string connectionId)
	{
		await Clients.Client(connectionId).ReceiveMessageForSpecificClient(message);
	}
	public async Task BroadcastMessageToGroupClient(string message, string groupName)
	{
		await Clients.Group(groupName).ReceiveMessageForGroupClient(message);
	}
	public async Task AddGroup(string groupName)
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
		await Clients.Caller.ReceiveMessageForCallerClient($"{groupName} grubuna dahil oldunuz");
		await Clients.Group(groupName).ReceiveMessageForGroupClient($"Kullanici({Context.ConnectionId}) {groupName} grubuna dahil oldu");

		//await Clients.Others.ReceiveMessageForOtherClient($"Kullanici({Context.ConnectionId}) {groupName} grubuna dahil oldu");

	}
	public async Task RemoveGroup(string groupName)
	{
		await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
		await Clients.Caller.ReceiveMessageForCallerClient($"{groupName} grubundan ciktiniz");
		await Clients.Group(groupName).ReceiveMessageForGroupClient($"Kullanici({Context.ConnectionId}) {groupName} grubuna dahil oldu");

		//await Clients.Others.ReceiveMessageForOtherClient($"Kullanici({Context.ConnectionId}) {groupName} grubundan cikti");
	}
	public override async Task OnConnectedAsync()
	{
		ConnectedClientCount++;
		await Clients.All.ReceiveCountOfAllConnectedClient(ConnectedClientCount);
		await base.OnConnectedAsync();
	}
	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		ConnectedClientCount--;
		await Clients.All.ReceiveCountOfAllConnectedClient(ConnectedClientCount);
		await base.OnDisconnectedAsync(exception);
	}
}