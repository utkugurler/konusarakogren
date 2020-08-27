using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using konusarakogren.Models;
using konusarakogren.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace konusarakogren.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		static List<Entity.Wired> wiredPosts;
		public IActionResult Index()
		{
			WiredDAL wiredDAL = new WiredDAL();
			wiredPosts = wiredDAL.GetRSS();

			ViewBag.wiredPosts = new SelectList(wiredPosts, "Id", "Title");

			ViewBag.id = Request.Query["id"];

			if (Request.Query["id"].Count > 0)
			{
				ViewBag.Title = wiredPosts[Convert.ToInt32(Request.Query["id"])].Title;
				ViewBag.Description = wiredPosts[Convert.ToInt32(Request.Query["id"])].Description;
			}

			return View();
		}

		[Microsoft.AspNetCore.Authorization.AllowAnonymous]
		public IActionResult Login(Entity.User model)
		{
			if (ModelState.IsValid)
			{
				DAL.LoginDAL loginDAL = new LoginDAL();
				bool isValid = loginDAL.IsUserValid(model.Username, model.Password);
				if(isValid)
				{
					// SetAuthCookie(model.Username, true);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					if(model.Username != null && model.Password != null)
						ModelState.AddModelError("", "EMail veya şifre hatalı!");
				}
			}

			return View();
		}

		public IActionResult AddQuiz(Entity.Quiz model)
		{
			if (ModelState.IsValid)
			{
				QuestionDAL questionDAL = new QuestionDAL();
				int getLastQuizId = questionDAL.GetLastQuizId();

				for (int i = 0; i < model.Question.Count; i++)
				{
					bool result = questionDAL.Add(wiredPosts[Convert.ToInt32(Request.Query["id"])].Title, 
													wiredPosts[Convert.ToInt32(Request.Query["id"])].Description, model.Question[i], 
														model.A[i], model.B[i], model.C[i], model.D[i], 
															getLastQuizId+1);
				}
				return RedirectToAction("QuizList");
			}
			return View();
		}
		// TODO: silme eksik
		public IActionResult QuizList()
		{
			QuestionDAL questionDAL = new QuestionDAL();
			List<Entity.QuizList> quizLists = new List<Entity.QuizList>();
			quizLists = questionDAL.Read();

			List<string> titles = new List<string>();
			List<string> publishdates = new List<string>();
			List<int> quizIds = new List<int>();
			
			for (int i = 0; i < quizLists.Count; i++)
			{
				titles.Add(quizLists[i].Title);
				publishdates.Add(quizLists[i].PublishDate);
				quizIds.Add(quizLists[i].QuizId);
			}

			ViewBag.Question = titles;
			ViewBag.PublishDate = publishdates;
			ViewBag.QuizID = quizIds;
			ViewBag.Count = titles.Count;

			return View();
		}

		public IActionResult QuizDelete(string quizId)
		{
			
			if (ModelState.IsValid)
			{
				
				QuestionDAL questionDAL = new QuestionDAL();
				if (Convert.ToInt32(quizId) != 0)
				{
					questionDAL.Delete(Convert.ToInt32(quizId));
				}
				return RedirectToAction("QuizList");
			}
			return View();
		}

		public ActionResult UserLandingView()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
