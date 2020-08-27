using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using konusarakogren.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace konusarakogren.DAL
{
	public class QuestionDAL
	{
		private string QuestionsTable = "QuizTable";
		DBControl dbControl = new DBControl();

		// DB' ye Quiz ekranında girdiğimiz parametreler geliyor quizId' si aynı kalacak şekilde 4 soruda ekleniyor
		public bool Add(string title, string description, string question, string a, string b, string c, string d, int quizId, string dogruCevap)
		{
			int result = 0;
			using (var connection = dbControl.GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"INSERT INTO {QuestionsTable}(title,description,question, a, b, c, d, quizId, publishdate,dogrucevap) " +
					$"values('{title.Replace("'", "")}', '{description.Replace("'", "")}', '{question.Replace("'", "")}', '{a.Replace("'", "")}', '{b.Replace("'", "")}', '{c.Replace("'", "")}', '{d.Replace("'", "")}', {quizId}, '{DateTime.Now}', '{dogruCevap}')";
				result = tableCmd.ExecuteNonQuery();
			}
			if(result > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		// QuizId' sini bulup 4 soruyu birden siliyor
		public bool Delete(int quizId)
		{
			int result = 0;
			using (var connection = dbControl.GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"DELETE FROM {QuestionsTable} WHERE quizId = {quizId}";
				result = tableCmd.ExecuteNonQuery();
			}
			if (result > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		// QuizListe' sinde göstermek için sqlden gelen elemanları class list' inde toplayıp geri döndürüyoruz
		public List<Entity.QuizList> QuizListRead()
		{
			List<QuizList> questionLists = new List<QuizList>();

			using (var connection = dbControl.GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"SELECT title, publishdate, quizId FROM {QuestionsTable}";
				SqliteDataReader dataReader = tableCmd.ExecuteReader();
				int quizId = 0;

				while (dataReader.Read())
				{

					if (quizId != Convert.ToInt32(dataReader["quizId"]) || quizId == 0)
					{
						QuizList questionList = new QuizList();
						questionList.Title = dataReader["title"].ToString();
						questionList.PublishDate = dataReader["publishdate"].ToString();
						questionList.QuizId = Convert.ToInt32(dataReader["quizId"]);
						questionLists.Add(questionList);
						quizId = Convert.ToInt32(dataReader["quizId"]);
					}
				}
				dataReader.Close();
			}
			return questionLists;
		}

		// Quiz içerisindeki soruların detaylarını çekiyoruz
		public Quiz QuizRead(int quizId)
		{
			Quiz quiz = new Quiz();
			quiz.Question = new List<string>();
			quiz.Id = new List<int>();
			quiz.A = new List<string>();
			quiz.B = new List<string>();
			quiz.C = new List<string>();
			quiz.D = new List<string>();

			int result = 0;
			using (var connection = dbControl.GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"select * from {QuestionsTable} where quizId='{quizId}'";
				SqliteDataReader dataReader = tableCmd.ExecuteReader();

				while (dataReader.Read())
				{
					quiz.Id.Add(Convert.ToInt32(dataReader["id"]));
					quiz.Title = dataReader["title"].ToString();
					quiz.Description = dataReader["description"].ToString();
					string a = dataReader["question"].ToString();
					quiz.Question.Add(dataReader["question"].ToString());
					quiz.A.Add(dataReader["a"].ToString());
					quiz.B.Add(dataReader["b"].ToString());
					quiz.C.Add(dataReader["c"].ToString());
					quiz.D.Add(dataReader["d"].ToString());
				}
			}
			return quiz;
		}

		// Burada tabloya quizleri eklerken sonId'yi çekiyoruzki bir sonraki gelecek quiz için bir ayrıştırma oluşturalım
		public int GetLastQuizId()
		{
			int lastId = 0;

			using (var connection = dbControl.GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"SELECT MAX(QuizId) FROM {QuestionsTable}";
				try
				{
					lastId = Convert.ToInt32(tableCmd.ExecuteScalar());
				}
				catch
				{
				}
			}

			if(lastId <= 0)
			{
				return 1000;
			}
			
			return lastId;
		}

		// Sorunun dogru cevabını çekiyoruz
		public List<string> CheckQuiz(int quizId)
		{
			List<string> dogruCevap = new List<string>();

			using (var connection = dbControl.GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"SELECT dogrucevap FROM {QuestionsTable} where quizId={quizId}";
				SqliteDataReader dataReader = tableCmd.ExecuteReader();
				while (dataReader.Read())
				{
					dogruCevap.Add(dataReader["dogrucevap"].ToString());
				}

				dataReader.Close();
			}

			return dogruCevap;
		}
		
	}
}
