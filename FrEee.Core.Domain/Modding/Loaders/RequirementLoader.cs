using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Extensions;
using System.Collections.Generic;

namespace FrEee.Modding.Loaders;

/// <summary>
/// Loads technology requirements from a record.
/// </summary>
public static class RequirementLoader
{
	/// <summary>
	/// Loads requirements from a record.
	/// </summary>
	/// <param name="rec"></param>
	public static IEnumerable<Requirement<Empire>> LoadEmpireRequirements(Record rec, IResearchable r, RequirementType rtype)
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
				var techname = nfield.CreateFormula<string>(r).Value;
				var levelFormula = lfield?.CreateFormula<int>(r) ?? 1;
				var tech = Mod.Current.Technologies.FindByName(techname);
				if (tech == null)
					Mod.Errors.Add(new DataParsingException("Could not find a technology named " + techname + ".", Mod.CurrentFileName, rec));
				else
					yield return new TechnologyRequirement(r, tech, levelFormula);
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
			yield return new ScriptRequirement<Empire>(reqfield.CreateFormula<bool>(r), desc);
		}
	}
}