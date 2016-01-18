using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Performance
{
	class Program
	{
		static void Main(string[] args)
		{
			PrintReport(EqualsPerf.WhichEqualsIsFaster());
			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static void PrintReport(IDictionary<string, TimeSpan> data)
		{
			Console.WriteLine(GottaGoFast.CreateReport(data));
		}
	}
}
