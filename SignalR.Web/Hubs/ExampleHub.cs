using Microsoft.AspNetCore.SignalR;

namespace SignalR.Web.Hubs;
public class ExampleHub:Hub
{
	public async Task BroadcastMessageToAllClient(string message)
	{
		//butun clientlere ReceiveMessageForAllClient adındaki javascript metodunu uyandirarak ilgili message'yi gonderiyoruz
		//ornegin 100 kisi bizim sayfamızı goruntuluyor ve birisi bir request attıgında diger clientlere da mesaj gidecektir orn chat uyg..
		await Clients.All.SendAsync("ReceiveMessageForAllClient", message);	
	}
}