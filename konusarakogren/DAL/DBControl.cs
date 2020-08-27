using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.DAL
{
	public class DBControl
	{
		private const string UserTable = "UserTable";
		private const string QuizTable = "QuizTable";
		private const string DB = "konusarakOgrenDB";

		// DB' yi çağırdığımız yer db yaratılmadıysa derleyip çalıştırdığında otomatik çalışır.
		public SqliteConnection GetDB()
		{
			SqliteConnectionStringBuilder sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder();
			sqliteConnectionStringBuilder.DataSource = $"{DB}.db";
			SqliteConnection con = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString);
			return con;
		}

		// Login tablosu yaratıldı mı kontrolu yapıyor
		public void CheckLoginTable()
		{
			using (var connection = GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"CREATE TABLE IF NOT EXISTS {UserTable}(\"id\" INTEGER,\"Username\"  TEXT,\"Password\"  TEXT,PRIMARY KEY(\"id\" AUTOINCREMENT))";
				tableCmd.ExecuteNonQuery();
			}
		}

		// Quiz tablosu yaratıldı mı kontrolu yapıyor
		public void CheckQuizTable()
		{
			using (var connection = GetDB())
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"CREATE TABLE IF NOT EXISTS {QuizTable}(\"id\"    INTEGER,\"title\" TEXT,\"description\"   TEXT,\"question\"  TEXT,\"a\" TEXT,\"b\" TEXT,\"c\" TEXT,\"d\" TEXT,\"quizId\"    INTEGER,\"publishdate\"   TEXT, \"dogrucevap\" TEXT,PRIMARY KEY(\"id\" AUTOINCREMENT))";
				tableCmd.ExecuteNonQuery();
			}


		}
	}
}
