using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Combat2;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads components from Components.txt.
	/// </summary>
	public class ComponentLoader : DataFileLoader
	{
		public const string Filename = "Components.txt";

		public ComponentLoader(string modPath)
			: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var c = new ComponentTemplate();
				mod.ComponentTemplates.Add(c);

				int index = -1;

				c.ModID = rec.Get<string>("ID", c);
				c.Name = rec.Get<string>("Name", c);
				c.Description = rec.Get<string>("Description", c);

				var picfield = rec.FindField("Pic", ref index, false, 0, true);
				if (picfield != null)
					c.PictureName = picfield.CreateFormula<string>(c);
				else
					c.PictureName = "Comp_" + rec.Get<int>("Pic Num", c).Value.ToString("000"); // for compatibility with SE4

				c.Size = rec.Get<int>("Tonnage Space Taken", c);
				c.Durability = rec.Get<int>("Tonnage Structure", c);

				foreach (var costfield in rec.Fields.Where(cf => cf.Name.StartsWith("Cost ")))
					c.Cost[Resource.Find(costfield.Name.Substring("Cost ".Length))] = costfield.CreateFormula<int>(c);

				var vtoverridefield = rec.FindField(new string[] { "Vehicle List Type Override", "Vechicle List Type Override" }, ref index, false, 0, true); // silly Aaron can't spell "vehicle"
				if (vtoverridefield != null)
					c.VehicleTypes = ParseVehicleTypes(vtoverridefield.Value, ",", rec);
				else
					c.VehicleTypes = ParseVehicleTypes(rec.Get<string>("Vehicle Type", c), @"\", rec);

				c.SupplyUsage = rec.Get<int>("Supply Amount Used", c);

				var restrictions = rec.Get<string>("Restrictions", c);
				if (!string.IsNullOrEmpty(restrictions) && restrictions != "None")
				{
					var word = restrictions.Value.Split(' ').First();
					int num;
					if (numbers.Contains(word))
						c.MaxPerVehicle = numbers.IndexOf(word);
					else if (int.TryParse(word, out num))
						c.MaxPerVehicle = num;
					else
						Mod.Errors.Add(new DataParsingException("Can't parse \"" + word + "\" as a max-per-vehicle restriction.", Mod.CurrentFileName, rec));
				}

				c.Group = rec.Get<string>("General Group", c);
				c.Family = rec.Get<string>("Family", c);
				c.RomanNumeral = rec.Get<int>("Roman Numeral", c);
				c.StellarConstructionGroup = rec.Get<string>("Custom Group", c);

				foreach (var tr in RequirementLoader.LoadEmpireRequirements(rec, c, RequirementType.Unlock))
					c.UnlockRequirements.Add(tr);

				// TODO - build and use requirements

				foreach (var abil in AbilityLoader.Load(Filename, rec, c))
					c.Abilities.Add(abil);

				var wfield = rec.FindField("Weapon Type", ref index, false, 0, true);
				if (wfield != null)
				{
					WeaponInfo w = null;
					if (wfield.Value == "Seeking" || wfield.Value == "Seeking Point-Defense")
					{
						var sw = new SeekingWeaponInfo();
						sw.SeekerSpeed = rec.Get<int>("Weapon Seeker Speed", c);
						sw.SeekerDurability = rec.Get<int>("Weapon Seeker Dmg Res", c);
						w = sw;
					}
					else if (wfield.Value == "Direct Fire" || wfield.Value == "Point-Defense")
					{
						var dfw = new DirectFireWeaponInfo();
						dfw.AccuracyModifier = rec.Get<int>("Weapon Modifier", c);
						w = dfw;
					}
					else if (wfield.Value == "Warhead" || wfield.Value == "Warhead Point-Defense")
					{
						var ww = new WarheadWeaponInfo();
						w = ww;
					}
					else if (string.IsNullOrEmpty(wfield.Value) || wfield.Value == "None")
						w = null;
					else
						Mod.Errors.Add(new DataParsingException("Invalid weapon type \"" + wfield.Value + "\".", Mod.CurrentFileName, rec, wfield));
					if (w != null)
					{
						if (wfield.Value.EndsWith("Point-Defense"))
							w.IsPointDefense = true;

						var wtoverridefield = rec.FindField("Weapon List Target Override", ref index, false, 0, true);
						if (wtoverridefield != null)
							w.Targets = ParseWeaponTargets(wtoverridefield.Value, ",", rec);
						else
							w.Targets = ParseWeaponTargets(rec.Get<string>("Weapon Target", c), @"\", rec);

						w.MinRange = rec.Get<int>(new string[] { "Min Range", "Minimum Range" }, c) ?? 1;
						w.MaxRange = rec.Get<int>(new string[] { "Max Range", "Maximum Range" }, c) ?? 20;
						var dmgfield = rec.FindField(new string[] { "Damage", "Weapon Damage", "Damage At Rng", "Weapon Damage At Rng" }, ref index);
						if (dmgfield.Value.StartsWith("="))
							w.Damage = dmgfield.CreateFormula<int>(c);
						else
						{
							string dmgstr = null;
							try
							{
								dmgstr = dmgfield.Value;
								var dmg = dmgstr.Split(' ').Select(s => int.Parse(s)).ToList();
								int firstNonzero = w.MinRange;
								while (dmg.Count > 0 && firstNonzero < dmg.Count && dmg[firstNonzero] == 0)									firstNonzero++;
								int lastNonzero = w.MaxRange - w.MinRange;
								while (dmg.Count > lastNonzero && dmg[lastNonzero] == 0)
									lastNonzero--;
								if (dmg.Count > lastNonzero && dmg[lastNonzero] > 0)
									lastNonzero++;
								w.MaxRange = w.MinRange + lastNonzero - 1;
								var dict = new Dictionary<int, int>();
								for (int i = firstNonzero; i <= lastNonzero - firstNonzero; i++)
									dict.Add(i + w.MinRange.Value, dmg[i]);
								if (firstNonzero > lastNonzero)
									w.Damage = 0;
								else
									w.Damage = dict.BuildMultiConditionalLessThanOrEqual(c, "range", 0);
							}
							catch (Exception ex)
							{
								Mod.Errors.Add(new DataParsingException("Can't parse \"" + dmgstr + "\" as a damage string: " + ex.Message, Mod.CurrentFileName, rec));
							}
						}

						// TODO - populate damage types once we implement them
						w.DamageType = new DamageType { Name = rec.Get<string>("Weapon Damage Type", c) };

						w.ReloadRate = rec.Get<double>("Weapon Reload Rate", c);

						var wdisptype = rec.Get<string>("Weapon Display Type", c);
						var wdispname = rec.Get<string>("Weapon Display", c);
						if (wdisptype == "Beam")
							w.DisplayEffect = new BeamWeaponDisplayEffect { Name = wdispname };
						else if (wdisptype == "Torp" || wdisptype == "Torpedo" || wdisptype == "Projectile")
							w.DisplayEffect = new ProjectileWeaponDisplayEffect { Name = wdispname };
						else if (wdisptype == "Seeker")
							w.DisplayEffect = new SeekerWeaponDisplayEffect { Name = wdispname };
						else
							Mod.Errors.Add(new DataParsingException("Invalid weapon display effect type \"" + wdisptype + "\".", Mod.CurrentFileName, rec));

						// sanity check
						if (wdisptype == "Beam" && w is SeekingWeaponInfo)
							Mod.Errors.Add(new DataParsingException("A seeking weapon cannot use a beam display effect.", Mod.CurrentFileName, rec));

						w.Sound = rec.Get<string>("Weapon Sound", c);
						w.Family = rec.Get<string>("Weapon Family", c);
					}
					c.WeaponInfo = w;
				}
			}
		}

		private VehicleTypes ParseVehicleTypes(string s, string separator, Record rec)
		{
			if (s == null)
				return VehicleTypes.All;
			var splitstr = s.Split(new string[] { separator }, StringSplitOptions.None).Select(sub => sub.Trim());
			var vt = VehicleTypes.None;
			foreach (var item in splitstr)
			{
				VehicleTypes subvt;
				if (Enum.TryParse<VehicleTypes>(item, out subvt))
					vt |= subvt;
				else
					Mod.Errors.Add(new DataParsingException("Can't parse \"" + item + "\" as a vehicle type.", Mod.CurrentFileName, rec));
			}
			return vt;
		}

		private static List<string> numbers = new List<string>()
		{
			"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"
		};

		private WeaponTargets ParseWeaponTargets(string s, string separator, Record rec)
		{
			if (s == null)
				return WeaponTargets.All;
			var splitstr = s.Split(new string[] { separator }, StringSplitOptions.None).Select(sub => sub.Trim());
			var wt = WeaponTargets.None;
			foreach (var item in splitstr)
			{
				WeaponTargets subwt;
				if (Enum.TryParse<WeaponTargets>(item, out subwt))
					wt |= subwt;
				else
					Mod.Errors.Add(new DataParsingException("Can't parse \"" + item + "\" as a weapon target.", Mod.CurrentFileName, rec));
			}
			return wt;
		}
	}
}
