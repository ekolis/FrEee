using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads technology requirements from a record.
	/// </summary>
	public static class TechnologyRequirementLoader
	{
		/// <summary>
		/// Loads technology requirements from a record.
		/// </summary>
		/// <param name="rec"></param>
		public static IEnumerable<TechnologyRequirement> Load(Record rec)
		{
			int count = 0;
			int start = 0;
			while (true)
			{
				count++;
				var nfield = rec.FindField(new string[] { "Tech Area Req " + count, "Tech Area Req" }, ref start, false, start);
				if (nfield == null)
					break; // no more tech requirements
				var tech = Mod.Current.Technologies.SingleOrDefault(t => t.Name == nfield.Value);
				if (tech == null)
				{
					Mod.Errors.Add(new DataParsingException("Could not find technology named \"" + nfield.Value + "\".", Mod.CurrentFileName, rec));
					break;
				}
				var lfield = rec.FindField(new string[] { "Tech Level Req " + count, "Tech Level Req" }, ref start, false, start);
				if (lfield == null)
				{
					Mod.Errors.Add(new DataParsingException("Could not find Tech Level Req field to match Tech Area Req field.", Mod.CurrentFileName, rec));
					break;
				}
				var tr = new TechnologyRequirement
					(
						Mod.Current.Technologies.Single(t => t.Name == nfield.Value),
						lfield.IntValue(rec)
					);
				yield return tr;
			}
		}
	}
}
