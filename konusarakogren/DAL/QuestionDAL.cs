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
		SqliteConnectionStringBuilder sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder();
		private string QuestionsTable = "QuizTable";

		public void ConnectDB()
		{
			sqliteConnectionStringBuilder.DataSource = "./konusarakOgrenDB.db";

			using(var connection = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString))
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
			}
		}

		public bool Add(string title, string description, string question, string a, string b, string c, string d, int quizId)
		{
			sqliteConnectionStringBuilder.DataSource = @"C:\Users\utkug\Documents\konusarakogren\konusarakogren\bin\Debug\netcoreapp3.1\db\konusarakOgrenDB.db";

			// TODO: quizzesden gelen verileri jsona çevir
			int result = 0;
			using (var connection = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString))
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"INSERT INTO {QuestionsTable}(title,description,question, a, b, c, d, quizId, publishdate) values('{title.Replace("'", "")}', '{description}', '{question}', '{a}', '{b}', '{c}', '{d}', {quizId}, '{DateTime.Now.Date}')";
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

		public bool Delete(int quizId)
		{
			sqliteConnectionStringBuilder.DataSource = @"C:\Users\utkug\Documents\konusarakogren\konusarakogren\bin\Debug\netcoreapp3.1\db\konusarakOgrenDB.db";

			int result = 0;
			using (var connection = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString))
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

		public List<Entity.QuizList> Read()
		{
			List<QuizList> questionLists = new List<QuizList>();
			sqliteConnectionStringBuilder.DataSource = @"C:\Users\utkug\Documents\konusarakogren\konusarakogren\bin\Debug\netcoreapp3.1\db\konusarakOgrenDB.db";

			// TODO: quizzesden gelen verileri jsona çevir
			using (var connection = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString))
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

		public int GetLastQuizId()
		{
			int lastId = 0;
			sqliteConnectionStringBuilder.DataSource = @"C:\Users\utkug\Documents\konusarakogren\konusarakogren\bin\Debug\netcoreapp3.1\db\konusarakOgrenDB.db";

			// TODO: quizzesden gelen verileri jsona çevir
			using (var connection = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString))
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
		
	}
}
