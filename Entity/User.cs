using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.Entity
{
	public class User
	{
		private string username;
		private string password;

		public string Username { get => username; set => username = value; }
		public string Password { get => password; set => password = value; }
	}
}
