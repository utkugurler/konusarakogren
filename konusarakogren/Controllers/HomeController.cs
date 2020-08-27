﻿using System;
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
using Newtonsoft.Json.Linq;

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
			List<string> dogru = new List<string>();
			dogru.Add("a");
			dogru.Add("b");
			dogru.Add("c");
			dogru.Add("d");
			ViewBag.DogruCevaplar = new SelectList(dogru);

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
															getLastQuizId+1, model.DogruCevap[i]);
				}
				return RedirectToAction("QuizList");
			}
			return View();
		}
		// TODO: silme eksik
		public IActionResult QuizList(string quizId, string submit)
		{
			if (Request.Method == "POST")
			{
				if(submit == "Delete")
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
				}
				else if(submit == "View")
				{
					return RedirectToAction($"QuizView", new { quizId = quizId });
				}
			}
			else
			{
				QuestionDAL questionDAL = new QuestionDAL();
				List<Entity.QuizList> quizLists = new List<Entity.QuizList>();
				quizLists = questionDAL.QuizListRead();

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
			}
			

			return View();
		}

		public IActionResult QuizView(Entity.Quiz model)
		{
			int quizId = Convert.ToInt32(Request.Query["quizId"]);
			Entity.Quiz quiz = new Entity.Quiz();
			QuestionDAL questionDAL = new QuestionDAL();
			quiz = questionDAL.QuizRead(quizId);

			ViewBag.posts = quiz.Title;
			ViewBag.description = quiz.Description;
			ViewBag.questions = quiz.Question;
			ViewBag.a = quiz.A;
			ViewBag.b = quiz.B;
			ViewBag.c = quiz.C;
			ViewBag.d = quiz.D;
			ViewBag.count = quiz.Question.Count;
			ViewBag.quizid = quizId;

			return View();
		}
		public IActionResult QuizCheck(string soru1, string soru2, string soru3, string soru4, int quizId)
		{
			QuestionDAL questionDAL = new QuestionDAL();
			List<string> dogruCevaplar = questionDAL.CheckQuiz(quizId);
			Entity.Answers answers = new Entity.Answers();

			if (soru1 == dogruCevaplar[0])
			{
				answers.Soru1 = true;
				answers.Soru1cevap = 'a';
			}
			else
			{
				answers.Soru1 = false;
				answers.Soru1cevap = 'a';
			}
			if (soru2 == dogruCevaplar[1])
			{
				answers.Soru2 = true;
				answers.Soru2cevap = 'b';
			}
			else
			{
				answers.Soru2 = false;
				answers.Soru2cevap = 'b';

			}
			if (soru3 == dogruCevaplar[2])
			{
				answers.Soru3 =true;
				answers.Soru3cevap = 'c';
			}
			else
			{
				answers.Soru3 = false;
				answers.Soru3cevap = 'c';
			}
			if (soru4 == dogruCevaplar[3])
			{
				answers.Soru4 = true;
				answers.Soru4cevap = 'd';
			}
			else
			{
				answers.Soru4 = false;
				answers.Soru4cevap = 'd';
			}

			var json = Newtonsoft.Json.JsonConvert.SerializeObject(answers);


			// var data = new { status = "ok", result = result };
			json = json.Replace("'\'", "");
			return Json(json);
		}

		public IActionResult QuizDelete(string quizId)
		{
			
			
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
