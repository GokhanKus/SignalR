using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using NuGet.Protocol;
using SignalRSampleProject.Web.Models;
using SignalRSampleProject.Web.Models.ViewModels;
using System.Diagnostics;

namespace SignalRSampleProject.Web.Controllers
{
	public class HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context) : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var userToCreate = new IdentityUser
			{
				UserName = model.Email,
				Email = model.Email
			};
			var result = await userManager.CreateAsync(userToCreate, model.Password);

			if (!result.Succeeded)
			{
				foreach (var err in result.Errors)
				{
					ModelState.AddModelError("", err.Description);
				}
				return View();
			}
			return RedirectToAction(nameof(SignIn));
		}
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var hasUser = await userManager.FindByEmailAsync(model.Email);
			if (hasUser is null)
			{
				ModelState.AddModelError("", "no email found");
				return View();
			}
			var result = await signInManager.PasswordSignInAsync(hasUser, model.Password, true, true);

			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "email or password is wrong");
			}

			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> ProductList()
		{
			var user = await userManager.FindByEmailAsync("info@gokhankus.com");
			
			if (context.Products.Any(x => x.UserId == user!.Id)) //user'imiza ait product varsa
			{
				var products = context.Products.Where(x => x.UserId == user!.Id).ToList(); //o user'a ait product'lari getir
				return View(products);
			}

			var productList = new List<Product>
			{
				new Product{Name = "Product 1", Price = 250, Description = "Product 1 Description", UserId = user!.Id},
				new Product{Name = "Product 2", Price = 150, Description = "Product 2 Description", UserId = user!.Id},
				new Product{Name = "Product 3", Price = 200, Description = "Product 3 Description", UserId = user!.Id},
				new Product{Name = "Product 4", Price = 220, Description = "Product 4 Description", UserId = user!.Id},
				new Product{Name = "Product 5", Price = 250, Description = "Product 5 Description", UserId = user!.Id},
				new Product{Name = "Product 6", Price = 300, Description = "Product 6 Description", UserId = user!.Id}
			};
			await context.Products.AddRangeAsync(productList);
			await context.SaveChangesAsync();


			return View(productList);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
