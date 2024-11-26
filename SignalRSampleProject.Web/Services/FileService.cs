using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SignalRSampleProject.Web.Models;
using System.Threading.Channels;

namespace SignalRSampleProject.Web.Services;
public class FileService(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager,Channel<(string,List<Product>)> channel)
{
	public async Task<bool> AddMessageToQueue()
	{
		var userId = userManager.GetUserId(httpContextAccessor.HttpContext!.User);

		var products = await context.Products.Where(p=>p.UserId == userId).ToListAsync();

		return channel.Writer.TryWrite((userId, products)); 
	}
}
