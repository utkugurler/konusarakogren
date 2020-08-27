using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.Entity
{
	public class QuizList
	{
		private string title;
		private string publishDate;
		private int quizId;

		public string Title { get => title; set => title = value; }
		public string PublishDate { get => publishDate; set => publishDate = value; }
		public int QuizId { get => quizId; set => quizId = value; }
	}
}
