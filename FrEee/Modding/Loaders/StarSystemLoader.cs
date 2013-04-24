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

				rec.TryFindFieldValue("Name", out temp, ref index, mod.Errors);
				mod.StarSystemTemplates.Add(temp, sst);

				rec.TryFindFieldValue("Description", out temp, ref index, mod.Errors);
				sst.Description = temp;

				rec.TryFindFieldValue("Background Bitmap", out temp, ref index, mod.Errors);
				sst.BackgroundImagePath = temp;

				rec.TryFindFieldValue("Empires Can Start In", out temp, ref index, mod.Errors);
				sst.EmpiresCanStartIn = bool.Parse(temp);

				rec.TryFindFieldValue("Non-Tiled Center Pic", out temp, ref index, mod.Errors);
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

					if (!rec.TryFindFieldValue("Obj " + count + " Physical Type", out temp, ref start, null, start))
					{
						if (!rec.TryFindFieldValue("Obj Physical Type", out temp, ref start, null, start))
							break; // couldn't load next stellar object template
						else
							sobjtype = temp;
					}
					else
						sobjtype = temp;
					start++;
					if (!rec.TryFindFieldValue("Obj " + count + " Position", out temp, ref start, null, start))
					{
						if (!rec.TryFindFieldValue("Obj Position", out temp, ref start, null, start))
						{
							mod.Errors.Add(new DataParsingException("Could not find \"Obj Position\" field.", Mod.CurrentFileName, rec));
							continue; // skip this stellar object
						}
						else
							pos = temp;
					}
					else
						pos = temp;
					start++;

					ITemplate<ISpaceObject> sobjTemplate;
					if (sobjtype == "Star")
					{
						var template = new StarTemplate();

						// TODO - load star abilities from StellarAbilityTypes.txt

						if (!rec.TryFindFieldValue("Obj " + count + " Size", out temp, ref start, null, start))
						{
							if (!rec.TryFindFieldValue("Obj Size", out temp, ref start, null, start))
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

						if (!rec.TryFindFieldValue("Obj " + count + " Age", out temp, ref start, null, start))
						{
							if (!rec.TryFindFieldValue("Obj Age", out temp, ref start, null, start))
							{
								mod.Errors.Add(new DataParsingException("Could not find \"Obj Age\" field for star.", Mod.CurrentFileName, rec));
								continue; // skip this stellar object
							}
							else
								template.Age = temp == "Any" ? null : temp;
						}
						else
							template.Age = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue("Obj " + count + " Color", out temp, ref start, null, start))
						{
							if (!rec.TryFindFieldValue("Obj Color", out temp, ref start, null, start))
							{
								mod.Errors.Add(new DataParsingException("Could not find \"Obj Color\" field for star.", Mod.CurrentFileName, rec));
								continue; // skip this stellar object
							}
							else
								template.Color = temp == "Any" ? null : temp;
						}
						else
							template.Color = temp == "Any" ? null : temp;
						start++;

						if (!rec.TryFindFieldValue("Obj " + count + " Luminosity", out temp, ref start, null, start))
						{
							if (!rec.TryFindFieldValue("Obj Luminosity", out temp, ref start, null, start))
							{
								mod.Errors.Add(new DataParsingException("Could not find \"Obj Luminosity\" field.", Mod.CurrentFileName, rec));
								continue; // skip this stellar object
							}
							else
								template.Brightness = temp == "Any" ? null : temp;
						}
						else
							template.Brightness = temp == "Any" ? null : temp;
						start++;

						sobjTemplate = template;
					}
					else if (sobjtype == "Planet")
					{
						var template = new Planet();
						// TODO - set planet attributes
						sobjTemplate = template;
					}
					else if (sobjtype == "Asteroids")
					{
						var template = new AsteroidField();
						// TODO - set asteroids attributes
						sobjTemplate = template;
					}
					else if (sobjtype == "Storm")
					{
						var template = new Storm();
						// TODO - set storm attributes
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
