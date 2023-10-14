using System.Collections.Generic;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads population modifiers from a record.
	/// </summary>
	public static class PopulationModifierLoader
	{
		/// <summary>
		/// Loads population modifiers from a record.
		/// </summary>
		/// <param name="rec"></param>
		public static IEnumerable<PopulationModifier> Load(Record rec)
		{
			int count = 0;
			int start = 0;
			while (true)
			{
				count++;
				var pfield = rec.FindField(new string[] { "Pop Modifier " + count + " Population Amount", "Pop Modifier Population Amount" }, ref start, false, start, true);
				if (pfield == null)
					break; // no more pop modifiers
				var popmod = new PopulationModifier();
				popmod.PopulationAmount = pfield.CreateFormula<long>(null).Value * The.Mod.Settings.PopulationFactor;
				popmod.ProductionRate = rec.Get<int>(new string[] { "Pop Modifier " + count + " Production Modifier Percent", "Pop Modifier Production Modifier Percent" }, popmod);
				popmod.ConstructionRate = rec.Get<int>(new string[] { "Pop Modifier " + count + " SY Rate Modifier Percent", "Pop Modifier SY Rate Modifier Percent" }, popmod); // this actually affects all queues, not just SY queues
				yield return popmod;
			}
		}
	}
}