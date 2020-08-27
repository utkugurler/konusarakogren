using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace konusarakogren.DAL
{
	public class LoginDAL
	{
		private string UserTable = "UserTable";
		DBControl dbControl = new DBControl();

		// Kullanıcı var mı yok mu kontrolu yapılıyor
		public bool IsUserValid(string userName, string password)
		{
			// TODO: quizzesden gelen verileri jsona çevir
			int result = 0;
			using (var connection = dbControl.GetDB())
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
