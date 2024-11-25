using SignalR.Web.Models;

namespace SignalR.Web.Hubs;
public interface IExampleTypeSafeHub
{
	Task ReceiveMessageForAllClient(string message);
	Task ReceiveMessageAsStreamForAllClient(string name);
	Task ReceiveComplexMessageForAllClient(Product product); //string yerine class, record gibi complex typelari gondermek..
	Task ReceiveMessageForCallerClient(string message);
	Task ReceiveMessageForOtherClient(string message);
	Task ReceiveMessageForSpecificClient(string message);
	Task ReceiveMessageForGroupClient(string message);
	Task ReceiveCountOfAllConnectedClient(int clientCount);
}