using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Enumerations;
using FrEee.Modding.Interfaces;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads components from Components.txt.
	/// </summary>
	public class ComponentLoader : DataFileLoader
	{
		public ComponentLoader(string modPath)
					: base(modPath, Filename, DataFile.Load(modPath, Filename))
		{
		}

		public const string Filename = "Components.txt";

		private static List<string> numbers = new List<string>()
		{
			"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"
		};

		public override IEnumerable<IModObject> Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var c = new ComponentTemplate();
				c.TemplateParameters = rec.Parameters;
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

						w.MinRange = rec.Get<int>(new string[] { "Min Range", "Minimum Range", "Weapon Min Range", "Weapon Minimum Range" }, c) ?? 0;
						w.MaxRange = rec.Get<int>(new string[] { "Max Range", "Maximum Range", "Weapon Max Range", "Weapon Maximum Range" }, c) ?? 20;
						var dmgfield = rec.FindField(new string[] { "Damage", "Weapon Damage", "Damage At Rng", "Weapon Damage At Rng", "Damage At Range", "Weapon Damage At Range" }, ref index);
						if (dmgfield.Value.StartsWith("="))
							w.Damage = dmgfield.CreateFormula<int>(c);
						else
						{
							string dmgstr = null;
							try
							{
								var dict = new SafeDictionary<int, int>();
								var split = dmgfield.Value.Split(' ');
								for (var i = 0; i < split.Length; i++)
								{
									if (split[i].ToInt() == 0)
										continue;
									dict[i + 1] = split[i].ToInt();
								}

								// HACK - SE4 doesn't explicitly specify damage at range zero so copy the damage at range one value
								if (dict[1] != 0)
								{
									dict[0] = dict[1];
									w.MinRange = 0;
								}

								w.MinRange = dict.Keys.Min();
								w.MaxRange = dict.Keys.Max();

								w.Damage = dict.BuildMultiConditionalLessThanOrEqual(c, "range", 0);
							}
							catch (Exception ex)
							{
								Mod.Errors.Add(new DataParsingException("Can't parse \"" + dmgstr + "\" as a damage string: " + ex.Message, Mod.CurrentFileName, rec));
							}
						}

						var damTypeName = rec.Get<string>("Weapon Damage Type", c);
						w.DamageType = Mod.Current.DamageTypes.FindByName(damTypeName);

						if (w.DamageType == null)
						{
							// no valid damage type? then make it normal damage and log a warning
							w.DamageType = DamageType.Normal;
							Mod.Errors.Add(new DataParsingException("Unknown damage type \"" + damTypeName + "\"; setting " + c + "'s damage type to Normal.", Mod.CurrentFileName, rec));
						}

						w.ReloadRate = rec.Get<double>("Weapon Reload Rate", c);

						var wdisptype = rec.Get<string>("Weapon Display Type", c);
						var wdispname = rec.Get<string>("Weapon Display", c);
						if (wdisptype == "Beam")
							w.DisplayEffect = new BeamWeaponDisplayEffect(wdispname);
						else if (wdisptype == "Torp" || wdisptype == "Torpedo" || wdisptype == "Projectile")
							w.DisplayEffect = new ProjectileWeaponDisplayEffect(wdispname);
						else if (wdisptype == "Seeker")
							w.DisplayEffect = new SeekerWeaponDisplayEffect(wdispname);
						else
							Mod.Errors.Add(new DataParsingException("Invalid weapon display effect type \"" + wdisptype + "\".", Mod.CurrentFileName, rec));

						// sanity check
						if (wdisptype == "Beam" && w is SeekingWeaponInfo)
							Mod.Errors.Add(new DataParsingException("A seeking weapon cannot use a beam display effect.", Mod.CurrentFileName, rec));

						w.Sound = rec.Get<string>("Weapon Sound", c);
						w.Family = rec.Get<string>("Weapon Family", c);
					}
					c.WeaponInfo = w;

					yield return c;
				}
			}
		}

		private VehicleTypes ParseVehicleTypes(string s, string separator, Record rec)
		{
			if (s == null)
				return VehicleTypes.All;
			return s.ParseEnum<VehicleTypes>();
		}

		private WeaponTargets ParseWeaponTargets(string s, string separator, Record rec)
		{
			if (s == null)
				return WeaponTargets.All;
			return s.ParseEnum<WeaponTargets>();
		}
	}
}