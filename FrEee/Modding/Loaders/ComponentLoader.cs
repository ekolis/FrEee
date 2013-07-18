using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Utility;
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

				c.Name = rec.GetString("Name", ref index, true, 0, true);
				c.Description = rec.GetString("Description", ref index, true, 0, true);
				
				var picfield = rec.FindField("Pic", ref index, false, 0, true);
				if (picfield != null)
					c.PictureName = picfield.Value;
				else
					c.PictureName = "Comp_" + rec.GetInt("Pic Num", ref index, true, 0, true).ToString("000"); // for compatibility with SE4

				c.Size = rec.GetInt("Tonnage Space Taken", ref index, true, 0, true);
				c.Durability = rec.GetInt("Tonnage Structure", ref index, true, 0, true);

				foreach (var costfield in rec.Fields.Where(cf => cf.Name.StartsWith("Cost ")))
					c.Cost[Resource.Find(costfield.Name.Substring("Cost ".Length))] = costfield.IntValue(rec);

				var vtoverridefield = rec.FindField(new string[]{"Vehicle List Type Override", "Vechicle List Type Override"}, ref index, false, 0, true); // silly Aaron can't spell "vehicle"
				if (vtoverridefield != null)
					c.VehicleTypes = ParseVehicleTypes(vtoverridefield.Value, ",", rec);
				else
					c.VehicleTypes = ParseVehicleTypes(rec.GetString("Vehicle Type", ref index, true, 0, true), @"\", rec);

				c.SupplyUsage = rec.GetInt("Supply Amount Used", ref index, true, 0, true);

				var restrictions = rec.GetString("Restrictions", ref index, false, 0, true);
				if (!string.IsNullOrEmpty(restrictions) && restrictions != "None")
				{
					var word = restrictions.Split(' ').First();
					int num;
					if (numbers.Contains(word))
						c.MaxPerVehicle = numbers.IndexOf(word);
					else if (int.TryParse(word, out num))
						c.MaxPerVehicle = num;
					else
						Mod.Errors.Add(new DataParsingException("Can't parse \"" + word + "\" as a max-per-vehicle restriction.", Mod.CurrentFileName, rec));
				}

				c.Group = rec.GetString("General Group", ref index, true, 0, true);
				c.Family = rec.GetString("Family", ref index, true, 0, true);
				c.RomanNumeral = rec.GetInt("Roman Numeral", ref index, true, 0, true);
				c.StellarConstructionGroup = rec.GetString("Custom Group", ref index, true, 0, true);

				foreach (var tr in TechnologyRequirementLoader.Load(rec))
					c.TechnologyRequirements.Add(tr);

				foreach (var abil in AbilityLoader.Load(rec))
					c.Abilities.Add(abil);

				var wfield = rec.FindField("Weapon Type", ref index, false, 0, true);
				if (wfield != null)
				{
					WeaponInfo w = null;
					if (wfield.Value == "Seeking" || wfield.Value == "Seeking Point-Defense")
					{
						var sw = new SeekingWeaponInfo();
						sw.SeekerSpeed = rec.GetInt("Weapon Seeker Speed", ref index, true, 0, true);
						sw.SeekerDurability = rec.GetInt("Weapon Seeker Dmg Res", ref index, true, 0, true);
						w = sw;
					}
					else if (wfield.Value == "Direct Fire" || wfield.Value == "Point-Defense")
					{
						var dfw = new DirectFireWeaponInfo();
						dfw.AccuracyModifier = rec.GetInt("Weapon Modifier", ref index, true, 0, true);
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
							w.Targets = ParseWeaponTargets(rec.GetString("Weapon Target", ref index, true, 0, true), @"\", rec);

						var dmgstr = rec.GetString("Weapon Damage At Rng", ref index, true, 0, true);
						try
						{
							var dmg = dmgstr.Split(' ').Select(s => int.Parse(s)).ToList();
							if (w is SeekingWeaponInfo && dmg.Count >= 20)
							{
								// infinite range seekers!
								dmg.Insert(0, dmg.Last());
							}
							else
								dmg.Insert(0, 0);
							w.Damage = dmg.ToArray();
						}
						catch (Exception ex)
						{
							Mod.Errors.Add(new DataParsingException("Can't parse \"" + dmgstr + "\" as a damage string: " + ex.Message, Mod.CurrentFileName, rec));
						}

						// TODO - populate damage types once we implement them
						w.DamageType = new DamageType { Name = rec.GetString("Weapon Damage Type", ref index, true, 0, true)};

						w.ReloadRate = rec.GetInt("Weapon Reload Rate", ref index, true, 0, true);

						var wdisptype = rec.GetString("Weapon Display Type", ref index, true, 0, true);
						var wdispname = rec.GetString("Weapon Display", ref index, true, 0, true);
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

						w.Sound = rec.GetString("Weapon Sound", ref index, true, 0, true);
						w.Family = rec.GetString("Weapon Family", ref index, true, 0, true);
					}
					c.WeaponInfo = w;
				}
			}
		}

		private VehicleTypes ParseVehicleTypes(string s, string separator, Record rec)
		{
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
