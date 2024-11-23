using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClientWorkerApp
{
	public class Worker(ILogger<Worker> logger, IConfiguration _configuration) : BackgroundService
	{
		private HubConnection? _connection;
		public override Task StartAsync(CancellationToken cancellationToken)
		{
			var hubConnection = _configuration.GetSection("SignalR")["Hub"];
			_connection = new HubConnectionBuilder().WithUrl(hubConnection!).Build();

			_connection?.StartAsync().ContinueWith(result =>
			{
				logger.LogInformation(result.IsCompletedSuccessfully ? "Connected" : "Connection Failed");
			});

			return base.StartAsync(cancellationToken);
		}
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_connection!.On<Product>("ReceiveComplexMessageForAllClient", product =>
			{
				logger.LogInformation($"Received Message: {product.Id} - {product.Name} - {product.Price}");
			});

			return Task.CompletedTask;
		}
		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			await _connection!.StopAsync(cancellationToken);
			await _connection.DisposeAsync();
			await base.StopAsync(cancellationToken);
		}
	}
}
