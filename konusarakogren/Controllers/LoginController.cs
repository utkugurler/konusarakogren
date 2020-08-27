using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konusarakogren.DAL;
using Microsoft.AspNetCore.Mvc;

namespace konusarakogren.Controllers
{
	public class LoginController : Controller
	{
		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public IActionResult Index(Entity.User model)
		{
			if(ModelState.IsValid)
			{
				DAL.LoginDAL loginDAL = new LoginDAL();
				bool isValid = loginDAL.IsUserValid(model.Username, model.Password);
				if (isValid)
				{
					// Giriş yapıldıysa Soru ekleme bölümüne yollayacak
					// SetAuthCookie(model.Username, true);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					// Yanlışsa ekrana bilgi basacak
					if (model.Username != null && model.Password != null)
						ModelState.AddModelError("", "EMail veya şifre hatalı!");
				}
			}

			return View();
		}
	}
}
