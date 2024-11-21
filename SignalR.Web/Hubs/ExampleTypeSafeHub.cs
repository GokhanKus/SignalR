using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Hubs;
public class ExampleTypeSafeHub : Hub<IExampleTypeSafeHub>
{
	private static int ConnectedClientCount = 0;
	public async Task BroadcastMessageToAllClient(string message)
	{
		await Clients.All.ReceiveMessageForAllClient(message);
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