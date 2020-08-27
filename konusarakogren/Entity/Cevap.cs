using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.Entity
{
	public class Cevap
	{
		private int id;
		private string dogrucevap;
		public int Id { get => id; set => id = value; }
		public string Dogrucevap { get => dogrucevap; set => dogrucevap = value; }
	}
}
