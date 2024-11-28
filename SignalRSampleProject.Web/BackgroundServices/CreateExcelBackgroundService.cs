using ClosedXML.Excel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileProviders;
using SignalRSampleProject.Web.Hubs;
using SignalRSampleProject.Web.Models;
using System.Data;
using System.Threading.Channels;

namespace SignalRSampleProject.Web.BackgroundServices;
public class CreateExcelBackgroundService(Channel<(string userId, List<Product> products)> channel,
	IFileProvider fileProvider, IServiceProvider serviceProvider)
	: BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (await channel.Reader.WaitToReadAsync(stoppingToken)) //queue'da mesajlari oku bitince burada bekler mesaj gelince tekrar okumaya devam eder
		{

			await Task.Delay(4000);

			var (userId, products) = await channel.Reader.ReadAsync(stoppingToken);

			var wwwrootFolder = fileProvider.GetDirectoryContents("wwwroot");
			var files = wwwrootFolder.Single(x => x.Name == "files");

			var newExcelFileName = $"product-list-{Guid.NewGuid()}.xlsx";
			var newExcelFilePath = Path.Combine(files.PhysicalPath, newExcelFileName);

			var wb = new XLWorkbook();
			var ds = new DataSet();

			ds.Tables.Add(GetTable("Product List", products));

			wb.Worksheets.Add(ds);

			await using var excelFileStream = new FileStream(newExcelFilePath, FileMode.Create);
			wb.SaveAs(excelFileStream);

			using (var scope = serviceProvider.CreateScope())
			{
				var myHub = scope.ServiceProvider.GetRequiredService<IHubContext<AppHub>>();
				await myHub.Clients.User(userId).SendAsync("AlertCompleteFile", $"/files/{newExcelFileName}", stoppingToken);
			}

		}
	}
	private DataTable GetTable(string tableName, List<Product> products)
	{
		var table = new DataTable { TableName = tableName };

		foreach (var item in typeof(Product).GetProperties())
		{
			table.Columns.Add(item.Name, item.PropertyType);
		}
		products.ForEach(p => { table.Rows.Add(p.Id, p.Name, p.Price, p.Description, p.UserId); });

		return table;
	}
}
