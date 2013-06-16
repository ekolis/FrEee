using FrEee.Game;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Size = FrEee.Game.Enumerations.StellarSize;
using FrEee.Game.Enumerations;
using FrEee.Modding.StellarObjectLocations;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads star system templates from SystemTypes.txt.
	/// </summary>
	 [Serializable] public class StarSystemLoader : DataFileLoader
	{
		 public const string Filename = "SystemTypes.txt";

		 public StarSystemLoader(string modPath)
			 : base(Filename, DataFile.Load(modPath, Filename))
		{
		}

		public override void Load(Mod mod)
		{
			foreach (var rec in DataFile.Records)
			{
				var sst = new StarSystemTemplate();
				string temp;
				int index = -1;

				rec.TryFindFieldValue("Name", out temp, ref index, Mod.Errors, 0, true);
				sst.Name = temp;
				mod.StarSystemTemplates.Add(sst);

				rec.TryFindFieldValue("Description", out temp, ref index, Mod.Errors, 0, true);
				sst.Description = temp;

				rec.TryFindFieldValue("Background Bitmap", out temp, ref index, Mod.Errors, 0, true);
				sst.BackgroundImagePath = Path.GetFileNameWithoutExtension(temp); // so we can use PNG when SE4 specifies BMP files

				rec.TryFindFieldValue("Empires Can Start In", out temp, ref index, Mod.Errors, 0, true);
				sst.EmpiresCanStartIn = bool.Parse(temp);

				rec.TryFindFieldValue("Non-Tiled Center Pic", out temp, ref index, Mod.Errors, 0, true);
				sst.NonTiledCenterCombatImage = bool.Parse(temp);

				foreach (var abil in AbilityLoader.Load(rec))
					sst.Abilities.Add(abil);

				// TODO - load warp point stellar ability type

				int count = 0;
				int start = 0;
				while (true)
				{
					count++;
					string sobjtype;
					string pos;

					if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Physical Type", "Obj Physical Type" }, out temp, ref start, null, start, true))
						break; // couldn't load next stellar object template
					else
						sobjtype = temp;
					start++;

					if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Position", "Obj Position" }, out temp, ref start, null, start))
					{
						Mod.Errors.Add(new DataParsingException("Could not find \"Obj Position\" field.", Mod.CurrentFileName, rec));
						continue; // skip this stellar object
					}
					else
						pos = temp;
					start++;

					ITemplate<StellarObject> sobjTemplate;
					if (sobjtype == "Star" || sobjtype == "Destroyed Star")
					{
						var template = new StarTemplate();

						if (sobjtype == "DestroyedStar")
							template.IsDestroyed = true;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Stellar Abil Type", "Obj Stellar Abil Type" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Stellar Abil Type\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							template.Abilities = mod.StellarAbilityTemplates.Find(temp);
							if (template.Abilities == null)
							{
								template.Abilities = new RandomAbilityTemplate();
								Mod.Errors.Add(new DataParsingException("Could not find stellar ability type \"" + temp + "\" in StellarAbilityTypes.txt.", Mod.CurrentFileName, rec));
							}
						}

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							if (temp == "Any")
								template.StellarSize = null;
							else
							{
								try
								{
									template.StellarSize = (StellarSize)Enum.Parse(typeof(StellarSize), temp);
								}
								catch (ArgumentException ex)
								{
									Mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Age", "Obj Age" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Age\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Age = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Color", "Obj Color" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Color\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Color = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Luminosity", "Obj Luminosity" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Luminosity\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Brightness = temp == "Any" ? null : temp;
						start++;

						sobjTemplate = template;
					}
					else if (sobjtype == "Planet")
					{
						var template = new PlanetTemplate();

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Stellar Abil Type", "Obj Stellar Abil Type" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Stellar Abil Type\" field for planet.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							template.Abilities = mod.StellarAbilityTemplates.Find(temp);
							if (template.Abilities == null)
							{
								template.Abilities = new RandomAbilityTemplate();
								Mod.Errors.Add(new DataParsingException("Could not find stellar ability type \"" + temp + "\" in StellarAbilityTypes.txt.", Mod.CurrentFileName, rec));
							}
						}

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for planet.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							if (temp == "Any")
								template.StellarSize = null;
							else
							{
								try
								{
									template.StellarSize = (Size)Enum.Parse(typeof(Size), temp);
								}
								catch (ArgumentException ex)
								{
									Mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Atmosphere", "Obj Atmosphere" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Atmosphere\" field for planet.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Atmosphere = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Composition", "Obj Composition" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Composition\" field for planet.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Surface = temp == "Any" ? null : temp;
						start++;

						sobjTemplate = template;
					}
					else if (sobjtype == "Asteroids")
					{
						var template = new AsteroidFieldTemplate();

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Stellar Abil Type", "Obj Stellar Abil Type" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Stellar Abil Type\" field for asteroid field.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							template.Abilities = mod.StellarAbilityTemplates.Find(temp);
							if (template.Abilities == null)
							{
								template.Abilities = new RandomAbilityTemplate();
								Mod.Errors.Add(new DataParsingException("Could not find stellar ability type \"" + temp + "\" in StellarAbilityTypes.txt.", Mod.CurrentFileName, rec));
							}
						}

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for asteroid field.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							if (temp == "Any")
								template.StellarSize = null;
							else
							{
								try
								{
									template.StellarSize = (StellarSize)Enum.Parse(typeof(Size), temp);
								}
								catch (ArgumentException ex)
								{
									Mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Atmosphere", "Obj Atmosphere" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Atmosphere\" field for asteroid field.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Atmosphere = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Composition", "Obj Composition" }, out temp, ref start, null, start))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Composition\" field for asteroid field.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Surface = temp == "Any" ? null : temp;
						start++;

						sobjTemplate = template;
					}
					else if (sobjtype == "Storm")
					{
						var template = new StormTemplate();

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Stellar Abil Type", "Obj Stellar Abil Type" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Stellar Abil Type\" field for storm.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							template.Abilities = mod.StellarAbilityTemplates.Find(temp);
							if (template.Abilities == null)
							{
								template.Abilities = new RandomAbilityTemplate();
								Mod.Errors.Add(new DataParsingException("Could not find stellar ability type \"" + temp + "\" in StellarAbilityTypes.txt.", Mod.CurrentFileName, rec));
							}
						}

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							Mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for storm.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
						{
							if (temp == "Any")
								template.Size = null;
							else
							{
								try
								{
									template.Size = (Size)Enum.Parse(typeof(Size), temp);
								}
								catch (ArgumentException ex)
								{
									Mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						sobjTemplate = template;
					}
					else
					{
						Mod.Errors.Add(new DataParsingException("Invalid stellar object type \"" + sobjtype + "\". Must be Star, Planet, Asteroids, or Storm.", Mod.CurrentFileName, rec));
						continue;
					}

					if (pos.StartsWith("Ring "))
					{
						int ring;
						if (!int.TryParse(pos.Substring("Ring ".Length), out ring))
						{
							Mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected integer after Ring.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new RingStellarObjectLocation { Ring = ring, StellarObjectTemplate = sobjTemplate });
					}
					else if (pos.StartsWith("Coord ") || pos.StartsWith("Centered Coord "))
					{
						int x, y;
						string coordsData;
						bool isCentered = pos.StartsWith("Centered Coord ");
						if (isCentered)
							coordsData = pos.Substring("Centered Coord ".Length);
						else
							coordsData = pos.Substring("Coord ".Length);
						var splitData = coordsData.Split(',');
						if (splitData.Length < 2)
						{
							Mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected two comma separated integers after Coord.", Mod.CurrentFileName, rec));
							continue;
						}
						if (!int.TryParse(splitData[0].Trim(), out x))
						{
							Mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected two comma separated integers after Coord.", Mod.CurrentFileName, rec));
							continue;
						}
						if (!int.TryParse(splitData[1].Trim(), out y))
						{
							Mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected two comma separated integers after Coord.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new CoordStellarObjectLocation { Coordinates = new Point(x, y), UseCenteredCoordinates = isCentered, StellarObjectTemplate = sobjTemplate });
					}
					else if (pos.StartsWith("Same As"))
					{
						int idx;
						if (!int.TryParse(pos.Substring("Same As ".Length), out idx))
						{
							Mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected integer after Same As.", Mod.CurrentFileName, rec));
							continue;
						}
						if (idx > sst.StellarObjectLocations.Count)
						{
							Mod.Errors.Add(new DataParsingException("A \"Same As\" stellar object location can reference only previously defined locations.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new SameAsStellarObjectLocation { StarSystemTemplate = sst, TargetIndex = idx, StellarObjectTemplate = sobjTemplate });
					}
					else if (pos.StartsWith("Circle Radius "))
					{
						int radius;
						if (!int.TryParse(pos.Substring("Circle Radius ".Length), out radius))
						{
							Mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected integer after Circle Radius.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new CircleRadiusStellarObjectLocation { Radius = radius, StellarObjectTemplate = sobjTemplate });
					}
				}
			}
		}
	}
}
