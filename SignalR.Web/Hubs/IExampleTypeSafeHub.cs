﻿namespace SignalR.Web.Hubs;
public interface IExampleTypeSafeHub
{
	Task ReceiveMessageForAllClient(string message);
	Task ReceiveMessageForCallerClient(string message);
	Task ReceiveMessageForOtherClient(string message);
	Task ReceiveCountOfAllConnectedClient(int clientCount);
}