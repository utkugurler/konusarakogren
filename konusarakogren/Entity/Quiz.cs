using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.Entity
{
	public class Quiz
	{
		private string title;
		private string description;
		private List<int> id;
		private List<string> question;
		private List<string> a;
		private List<string> b;
		private List<string> c;
		private List<string> d;

		public List<string> Question { get => question; set => question = value; }
		public List<string> A { get => a; set => a = value; }
		public List<string> B { get => b; set => b = value; }
		public List<string> C { get => c; set => c = value; }
		public List<string> D { get => d; set => d = value; }
		public string Title { get => title; set => title = value; }
		public string Description { get => description; set => description = value; }
		public List<int> Id { get => id; set => id = value; }
	}

	
}
