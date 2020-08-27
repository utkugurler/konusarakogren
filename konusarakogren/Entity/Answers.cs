using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace konusarakogren.Entity
{
	public class Answers
	{
		private bool soru1;
		private char soru1cevap;
		private bool soru2;
		private char soru2cevap;
		private bool soru3;
		private char soru3cevap;
		private bool soru4;
		private char soru4cevap;

		public bool Soru1 { get => soru1; set => soru1 = value; }
		public bool Soru2 { get => soru2; set => soru2 = value; }
		public bool Soru3 { get => soru3; set => soru3 = value; }
		public bool Soru4 { get => soru4; set => soru4 = value; }
		public char Soru4cevap { get => soru4cevap; set => soru4cevap = value; }
		public char Soru3cevap { get => soru3cevap; set => soru3cevap = value; }
		public char Soru2cevap { get => soru2cevap; set => soru2cevap = value; }
		public char Soru1cevap { get => soru1cevap; set => soru1cevap = value; }
	}
}
