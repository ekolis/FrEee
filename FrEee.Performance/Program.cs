using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Game.Objects.Space;
using FrEee.Utility;

namespace FrEee.Performance
{
	class Program
	{
		static void Main(string[] args)
		{
			char yn;

			Console.WriteLine("Which implementation of Equals is faster?");
			PrintReport(EqualsPerf.WhichEqualsIsFaster());

			do
			{
				Console.WriteLine("Run savegame analysis? [yn] This might take a while...");
				yn = Console.ReadKey(true).KeyChar.ToString().ToLower()[0];
			} while (yn != 'y' && yn != 'n');

			if (yn == 'y')
			{
				Console.WriteLine("Running analysis...");
				Galaxy.Load("Quickstart_1.gam");
				var dict = new SafeDictionary<Type, Tuple<int, int>>(true);
				var p = new ObjectGraphParser();
				Action<object> startobj = o =>
				{
					var t = o.GetType();
					var old = dict[t];
					int propcount = old.Item1;
					if (propcount <= 0)
					{
						ObjectGraphContext.AddProperties(t);
						propcount = ObjectGraphContext.GetKnownProperties(t).Count;
					}
					int objcount = old.Item2;
					objcount++;
					dict[t] = Tuple.Create(propcount, objcount);
				};
				p.StartObject += new ObjectGraphParser.ObjectDelegate(startobj);
				p.Parse(Galaxy.Current);
				var list = new List<Tuple<string, int, int, int>>();
				foreach (var x in dict)
				{
					var typename = x.Key.Name;
					var propcount = x.Value.Item1;
					var objcount = x.Value.Item2;
					var cost = propcount * objcount;
					list.Add(Tuple.Create(typename, propcount, objcount, cost));
				}
				var sorted = list.OrderByDescending(x => x.Item4); // sort by total cost descending
				var totalcost = list.Sum(x => x.Item4);

				Console.WriteLine($"{list.Sum(x => x.Item3)} total objects found in savegame with a total of {totalcost} property accesses.");
				WriteTabbed("Type", "Props", "Objs", "Cost", "%Cost");
				foreach (var x in sorted)
					WriteTabbed(x.Item1, x.Item2, x.Item3, x.Item4, (100d * x.Item4 / totalcost) + "%");
			}
			else
				Console.WriteLine("OK, skipping it then.");

			Console.WriteLine("Press any key to exit...");
			Console.ReadKey();
		}

		private static void PrintReport(IDictionary<string, TimeSpan> data)
		{
			Console.WriteLine(GottaGoFast.CreateReport(data));
		}

		private static void WriteTabbed(params object[] ss)
		{
			Console.WriteLine(string.Join("\t", ss));
		}
	}
}
