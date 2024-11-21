namespace SignalR.Web.Hubs;
public interface IExampleTypeSafeHub
{
	Task ReceiveMessageForAllClient(string message);
	Task ReceiveMessageForCallerClient(string message);
	Task ReceiveCountOfAllConnectedClient(int clientCount);
}