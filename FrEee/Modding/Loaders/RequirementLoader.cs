using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Technology;
using FrEee.Game.Interfaces;
using FrEee.Modding.Enumerations;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads technology requirements from a record.
	/// </summary>
	public static class RequirementLoader
	{
		/// <summary>
		/// Loads requirements from a record.
		/// </summary>
		/// <param name="rec"></param>
		public static IEnumerable<Requirement> Load(Record rec, IResearchable r, RequirementType rtype)
		{
			int count = 0;
			int start = 0;
			if (rtype == RequirementType.Unlock)
			{
				while (true)
				{
					count++;
					var nfield = rec.FindField(new string[] { "Tech Area Req " + count, "Tech Area Req" }, ref start, false, start, true);
					if (nfield == null)
						break; // no more tech requirements
					var lfield = rec.FindField(new string[] { "Tech Level Req " + count, "Tech Level Req" }, ref start, false, start, true);
					if (lfield == null)
					{
						Mod.Errors.Add(new DataParsingException("Could not find Tech Level Req field to match Tech Area Req field.", Mod.CurrentFileName, rec));
						break;
					}
					var techname = nfield.CreateFormula<string>(r).Value;
					var levelFormula = lfield.CreateFormula<int>(r);
					yield return new Requirement(
							new Formula<bool>(r,
								"empire.ResearchedTechnologies[\"" + techname + "\"] >= " + levelFormula, FormulaType.Dynamic),
							new Formula<string>(r,
								"Requires level " + levelFormula.Value + " " + techname + ".", FormulaType.Dynamic));
				}
			}
			start = 0;
			while (true)
			{
				var reqfield = rec.FindField(rtype + " Requirement", ref start, false, start);
				if (reqfield == null)
					break;
				var descfield = rec.FindField(rtype + " Requirement Description", ref start, false, start);
				var desc = descfield == null ? (Formula<string>)reqfield.Value : descfield.CreateFormula<string>(r);
				yield return new Requirement(reqfield.CreateFormula<bool>(r), desc);
			}
		}
	}
}
