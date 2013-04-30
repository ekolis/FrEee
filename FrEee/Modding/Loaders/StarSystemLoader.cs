using FrEee.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Loaders
{
	/// <summary>
	/// Loads star system templates from SystemTypes.txt.
	/// </summary>
	public class StarSystemLoader : ILoader
	{
		public void Load(DataFile df, Mod mod)
		{
			foreach (var rec in df.Records)
			{
				var sst = new StarSystemTemplate();
				string temp;
				int index = -1;

				rec.TryFindFieldValue("Name", out temp, ref index, mod.Errors, 0, true);
				mod.StarSystemTemplates.Add(temp, sst);

				rec.TryFindFieldValue("Description", out temp, ref index, mod.Errors, 0, true);
				sst.Description = temp;

				rec.TryFindFieldValue("Background Bitmap", out temp, ref index, mod.Errors, 0, true);
				sst.BackgroundImagePath = temp;

				rec.TryFindFieldValue("Empires Can Start In", out temp, ref index, mod.Errors, 0, true);
				sst.EmpiresCanStartIn = bool.Parse(temp);

				rec.TryFindFieldValue("Non-Tiled Center Pic", out temp, ref index, mod.Errors, 0, true);
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
						mod.Errors.Add(new DataParsingException("Could not find \"Obj Position\" field.", Mod.CurrentFileName, rec));
						continue; // skip this stellar object
					}
					else
						pos = temp;
					start++;

					ITemplate<ISpaceObject> sobjTemplate;
					if (sobjtype == "Star")
					{
						var template = new StarTemplate();

						// TODO - load star abilities from StellarAbilityTypes.txt

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for star.", Mod.CurrentFileName, rec));
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
									template.Size = (Game.Size)Enum.Parse(typeof(Game.Size), temp);
								}
								catch (ArgumentException ex)
								{
									mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Age", "Obj Age" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Age\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Age = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Color", "Obj Color" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Color\" field for star.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Color = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Luminosity", "Obj Luminosity" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Luminosity\" field for star.", Mod.CurrentFileName, rec));
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

						// TODO - load planet abilities from StellarAbilityTypes.txt

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for planet.", Mod.CurrentFileName, rec));
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
									template.Size = (Game.Size)Enum.Parse(typeof(Game.Size), temp);
								}
								catch (ArgumentException ex)
								{
									mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Atmosphere", "Obj Atmosphere" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Atmosphere\" field for planet.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Atmosphere = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Composition", "Obj Composition" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Composition\" field for planet.", Mod.CurrentFileName, rec));
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

						// TODO - load asteroid field abilities from StellarAbilityTypes.txt

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for asteroid field.", Mod.CurrentFileName, rec));
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
									template.Size = (Game.Size)Enum.Parse(typeof(Game.Size), temp);
								}
								catch (ArgumentException ex)
								{
									mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Atmosphere", "Obj Atmosphere" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Atmosphere\" field for asteroid field.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							template.Atmosphere = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Composition", "Obj Composition" }, out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Composition\" field for asteroid field.", Mod.CurrentFileName, rec));
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

						// TODO - load storm abilities from StellarAbilityTypes.txt

						if (!rec.TryFindFieldValue(new string[] { "Obj " + count + " Size", "Obj Size" }, out temp, ref start, null, start, true))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Size\" field for storm.", Mod.CurrentFileName, rec));
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
									template.Size = (Game.Size)Enum.Parse(typeof(Game.Size), temp);
								}
								catch (ArgumentException ex)
								{
									mod.Errors.Add(new DataParsingException("Invalid stellar object size \"" + temp + "\". Must be Tiny, Small, Medium, Large, Huge, or Any.", ex, Mod.CurrentFileName, rec));
									continue; // skip this stellar object
								}
							}
						}
						start++;

						sobjTemplate = template;
					}
					else
					{
						mod.Errors.Add(new DataParsingException("Invalid stellar object type \"" + sobjtype + "\". Must be Star, Planet, Asteroids, or Storm.", Mod.CurrentFileName, rec));
						continue;
					}

					if (pos.StartsWith("Ring "))
					{
						int ring;
						if (!int.TryParse(pos.Substring("Ring ".Length), out ring))
						{
							mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected integer after Ring.", Mod.CurrentFileName, rec));
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
							mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected two comma separated integers after Coord.", Mod.CurrentFileName, rec));
							continue;
						}
						if (!int.TryParse(splitData[0].Trim(), out x))
						{
							mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected two comma separated integers after Coord.", Mod.CurrentFileName, rec));
							continue;
						}
						if (!int.TryParse(splitData[1].Trim(), out y))
						{
							mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected two comma separated integers after Coord.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new CoordStellarObjectLocation { Coordinates = new Point(x, y), UseCenteredCoordinates = isCentered, StellarObjectTemplate = sobjTemplate });
					}
					else if (pos.StartsWith("Same As"))
					{
						int idx;
						if (!int.TryParse(pos.Substring("Same As ".Length), out idx))
						{
							mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected integer after Same As.", Mod.CurrentFileName, rec));
							continue;
						}
						if (idx > sst.StellarObjectLocations.Count)
						{
							mod.Errors.Add(new DataParsingException("A \"Same As\" stellar object location can reference only previously defined locations.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new SameAsStellarObjectLocation { StarSystemTemplate = sst, TargetIndex = idx, StellarObjectTemplate = sobjTemplate });
					}
					else if (pos.StartsWith("Circle Radius "))
					{
						int radius;
						if (!int.TryParse(pos.Substring("Circle Radius ".Length), out radius))
						{
							mod.Errors.Add(new DataParsingException("Could not parse stellar object location \"" + pos + "\". Expected integer after Circle Radius.", Mod.CurrentFileName, rec));
							continue;
						}
						sst.StellarObjectLocations.Add(new CircleRadiusStellarObjectLocation { Radius = radius, StellarObjectTemplate = sobjTemplate });
					}
				}
			}
		}
	}
}
