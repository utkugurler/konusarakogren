using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace konusarakogren.DAL
{
	public class LoginDAL
	{
		SqliteConnectionStringBuilder sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder();
		private string UserTable = "UserTable";

		public bool IsUserValid(string userName, string password)
		{
			sqliteConnectionStringBuilder.DataSource = @"C:\Users\utkug\Documents\konusarakogren\konusarakogren\bin\Debug\netcoreapp3.1\db\konusarakOgrenDB.db";

			// TODO: quizzesden gelen verileri jsona çevir
			int result = 0;
			using (var connection = new SqliteConnection(sqliteConnectionStringBuilder.ConnectionString))
			{
				connection.Open();

				var tableCmd = connection.CreateCommand();
				tableCmd.CommandText = $"select * from {UserTable} where Username='{userName}' AND Password='{password}'";
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
	}
}
