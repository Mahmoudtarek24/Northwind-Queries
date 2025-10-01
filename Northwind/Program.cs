using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind
{
	internal class Program
	{
		static void Main(string[] args)
		{
	
			var date=DateTime.Now.ToString();

			var dadestring = DateTime.Parse(date);

			Console.WriteLine(dadestring);
		}
	}
}
