using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.Entity
{
	public class Answers
	{
		private bool soru1;
		private bool soru2;
		private bool soru3;
		private bool soru4;

		public bool Soru1 { get => soru1; set => soru1 = value; }
		public bool Soru2 { get => soru2; set => soru2 = value; }
		public bool Soru3 { get => soru3; set => soru3 = value; }
		public bool Soru4 { get => soru4; set => soru4 = value; }
	}
}
