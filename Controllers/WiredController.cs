using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace konusarakogren.Controllers
{
	public class WiredController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
