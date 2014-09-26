using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;

namespace FrEee.WinForms.Controls
{
	/// <summary>
	/// Displays a map of a star system.
	/// </summary>
	public partial class StarSystemView : Control
	{
		public StarSystemView()
		{
			InitializeComponent();
			BackColor = Color.Black;
			this.SizeChanged += StarSystemView_SizeChanged;
			this.MouseDown += StarSystemView_MouseDowned;
			DoubleBuffered = true;
			DrawText = true;
		}

		/// <summary>
		/// Should we draw text for space object names and counts on the map?
		/// </summary>
		public bool DrawText { get; set; }

		/// <summary>
		/// Delegate for events related to sector selection.
		/// </summary>
		/// <param name="sender">The star system view triggering the event.</param>
		/// <param name="sector">The sector selected/deselected/etc.</param>
		public delegate void SectorSelectionDelegate(StarSystemView sender, Sector sector);

		/// <summary>
		/// Occurs when the user clicks with the left mouse button on a sector.
		/// </summary>
		public event SectorSelectionDelegate SectorClicked;

		/// <summary>
		/// Occurs when the selected sector changes.
		/// </summary>
		public event SectorSelectionDelegate SectorSelected;

		void StarSystemView_MouseDowned(object sender, MouseEventArgs e)
		{
			if (SectorClicked != null)
				SectorClicked(this, GetSectorAtPoint(e.Location));
		}

		/// <summary>
		/// Gets the sector at specific screen coordinates.
		/// </summary>
		/// <param name="p">The screen coordinates.</param>
		/// <returns></returns>
		public Sector GetSectorAtPoint(Point p)
		{
			if (StarSystem == null)
				return null; // no such sector
			var drawsize = SectorDrawSize;
			var x = (int)Math.Round((p.X - Width / 2f) / drawsize);
			var y = (int)Math.Round((p.Y - Height / 2f) / drawsize);
			if (!StarSystem.AreCoordsInBounds(x, y))
				return null;
			return StarSystem.GetSector(x, y);
		}

		/// <summary>
		/// The size at which each sector will be drawn, in pixels.
		/// </summary>
		public int SectorDrawSize
		{
			get
			{
				if (StarSystem == null)
					return 0;
				return Math.Min(Width - SectorBorderSize, Height - SectorBorderSize) / StarSystem.Diameter - SectorBorderSize;
			}
		}

		/// <summary>
		/// Border in pixels between sectors and around the entire map.
		/// </summary>
		private const int SectorBorderSize = 1;

		void StarSystemView_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		private StarSystem starSystem;

		/// <summary>
		/// The star system to display.
		/// </summary>
		public StarSystem StarSystem
		{
			get { return starSystem; }
			set
			{
				starSystem = value;
				Invalidate();
			}
		}

		private Sector selectedSector;

		public Sector SelectedSector
		{
			get { return selectedSector; }
			set
			{
				selectedSector = value;
				if (SectorSelected != null)
					SectorSelected(this, value);
				Invalidate();
			}
		}

		private ISpaceObject selectedSpaceObject;

		/// <summary>
		/// Path lines will be drawn for this space object.
		/// </summary>
		public ISpaceObject SelectedSpaceObject
		{
			get { return selectedSpaceObject; }
			set
			{
				selectedSpaceObject = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.Clear(BackColor);

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var drawsize = SectorDrawSize;
			var bigFontSize = Math.Max(SectorDrawSize / 6, 1);
			var bigFont = new Font("Sans Serif", bigFontSize);
			var littleFontSize = Math.Max(SectorDrawSize / 8, 1);
			var littleFont = new Font("Sans Serif", littleFontSize);

			if (StarSystem != null)
			{
				if (StarSystem.BackgroundImage != null)
				{
					// draw star system background
					var size = Math.Min(Width, Height);
					if (Width >= Height)
						pe.Graphics.DrawImage(StarSystem.BackgroundImage, (Width - Height) / 2, 0, size, size);
					else
						pe.Graphics.DrawImage(StarSystem.BackgroundImage, 0, (Height - Width) / 2, size, size);
				}

				// draw sectors
				for (var x = -StarSystem.Radius; x <= StarSystem.Radius; x++)
				{
					for (var y = -StarSystem.Radius; y <= StarSystem.Radius; y++)
					{
						// where and how big will we draw the sector?
						var drawPoint = GetDrawPoint(x, y);
						var drawx = drawPoint.X;
						var drawy = drawPoint.Y;

						// find sector
						var sector = StarSystem.GetSector(x, y);

						// draw image, owner flag, and name of largest space object (if any)
						var largest = sector.SpaceObjects.Largest();
						if (largest != null)
						{
							Image pic;
							if (largest is SpaceVehicle)
								pic = largest.Icon.Resize((int)drawsize); // spacecraft get the icon, not the portrait, drawn, since the icon is topdown
							else
								pic = largest.Portrait.Resize((int)drawsize);
							if (largest is Planet)
							{
								var p = (Planet)largest;
								if (p.Colony != null)
									p.DrawPopulationBars(pic);
								else
									p.DrawStatusIcons(pic);
							}
							pe.Graphics.DrawImage(pic, drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);

							// TODO - draw owner flag

							// TODO - cache font and brush assets
							if (DrawText)
							{
								var sf = new StringFormat();
								sf.Alignment = StringAlignment.Center; // center align our name
								sf.LineAlignment = StringAlignment.Far; // bottom align our name
								var name = largest.Name;
								Brush nameBrush;
								if (largest.Timestamp < Galaxy.Current.Timestamp)
									nameBrush = Brushes.Gray;
								else
									nameBrush = Brushes.White;
								pe.Graphics.DrawString(name, bigFont, nameBrush, drawx, drawy + drawsize / 2f, sf);
							}
						}

						// draw number to indicate how many stellar objects are present if >1
						if (DrawText && sector.SpaceObjects.OfType<StellarObject>().Count() > 1)
						{
							// TODO - cache brush assets
							var sf = new StringFormat();
							sf.Alignment = StringAlignment.Near; // left align our number
							sf.LineAlignment = StringAlignment.Far; // bottom align our number
							pe.Graphics.DrawString(sector.SpaceObjects.OfType<StellarObject>().Count().ToString(), bigFont, new SolidBrush(Color.White), drawx - drawsize / 2f, drawy + drawsize / 2f - bigFontSize, sf);
						}

						var availForFlagsAndNums = Math.Min(drawsize - 21, 24);
						var top = 0;
						if (availForFlagsAndNums > 0)
						{
							var cornerx = drawx - drawsize / 2;
							var cornery = drawy - drawsize / 2;
							if (sector.SpaceObjects.Count() > 1)
							{
								foreach (var g in sector.SpaceObjects.Except(sector.SpaceObjects.OfType<StellarObject>()).Where(sobj => sobj.Owner != null).GroupBy(sobj => sobj.Owner))
								{
									// draw empire insignia and space object count
									var owner = g.Key;
									var fleetedCount = g.OfType<Fleet>().Sum(f => f.LeafVehicles.Count());
									var unfleetedCount = g.Except(g.OfType<Fleet>()).Count();
									var count = fleetedCount + unfleetedCount;
									var aspect = owner.Icon;
									if (owner.Icon != null)
									{
										var mode = pe.Graphics.InterpolationMode;
										if (owner.Icon.Width == 1 && owner.Icon.Height == 1)
										{
											pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
											pe.Graphics.DrawImage(owner.Icon, cornerx, cornery + top, bigFontSize * 2, bigFontSize * 2);
											pe.Graphics.InterpolationMode = mode;
										}
										else
											pe.Graphics.DrawImage(owner.Icon, cornerx, cornery + top, bigFontSize, bigFontSize);

									}
									if (DrawText)
									{
										// TODO - cache brush assets
										var sf = new StringFormat();
										sf.Alignment = StringAlignment.Near; // left align our number
										sf.LineAlignment = StringAlignment.Near; // top align our number
										pe.Graphics.DrawString(count.ToString(), bigFont, new SolidBrush(owner.Color), cornerx + bigFontSize, cornery + top, sf);
									}
									top += bigFontSize;
								}
							}
						}

						// draw ability text
						// TODO - make ability text moddable via AbilityRules.txt
						var abilText = "";
						var sobjs = sector.SpaceObjects.Where(sobj => sobj.Owner == Empire.Current);
						if (sobjs.Any(o => o.HasAbility("Space Yard")))
							abilText += "Sy";
						if (sobjs.Any(o => o.HasAbility("Component Repair")))
							abilText += "Rp";
						if (sobjs.Any(o => o.HasAbility("Supply Generation")))
							abilText += "Rd";
						// TODO - make quantum reactor code same as resupply depot if we decide to make it auto-resupply like a resupply depot
						if (sobjs.Any(o => o.HasAbility("Quantum Reactor")))
							abilText += "Qr";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Resource Gen Modifier Planet - Minerals").ToInt() > 0))
							abilText += "Mi+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Resource Gen Modifier Planet - Minerals").ToInt() < 0))
							abilText += "Mi-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Resource Gen Modifier Planet - Organics").ToInt() > 0))
							abilText += "Or+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Resource Gen Modifier Planet - Organics").ToInt() < 0))
							abilText += "Or-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Resource Gen Modifier Planet - Radioactives").ToInt() > 0))
							abilText += "Ra+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Resource Gen Modifier Planet - Radioactives").ToInt() < 0))
							abilText += "Ra-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Point Gen Modifier Planet - Research").ToInt() > 0))
							abilText += "Re+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Point Gen Modifier Planet - Research").ToInt() < 0))
							abilText += "Re-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Point Gen Modifier Planet - Intelligence").ToInt() > 0))
							abilText += "In+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Point Gen Modifier Planet - Intelligence").ToInt() < 0))
							abilText += "In-";
						if (sobjs.Any(o => o.HasAbility("Resource Conversion")))
							abilText += "Rc";
						if (sobjs.Any(o => o.HasAbility("Resource Reclamation")))
							abilText += "Rr";
						if (sobjs.Any(o => o.HasAbility("Ship Training")))
							abilText += "Ts";
						if (sobjs.Any(o => o.HasAbility("Fleet Training")))
							abilText += "Tf";
						if (sobjs.OfType<Planet>().Any(o => o.HasAbility("Stop Planet Destroyer")))
							abilText += "Sh";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Conditions").ToInt() > 0))
							abilText += "Co+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Conditions").ToInt() < 0))
							abilText += "Co-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Minerals Value").ToInt() > 0))
							abilText += "Vm+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Minerals Value").ToInt() < 0))
							abilText += "Vm-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Organics Value").ToInt() > 0))
							abilText += "Vo+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Organics Value").ToInt() < 0))
							abilText += "Vo-";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Radioactives Value").ToInt() > 0))
							abilText += "Vr+";
						if (sobjs.OfType<Planet>().Any(o => o.GetAbilityValue("Planet - Change Radioactives Value").ToInt() < 0))
							abilText += "Vr-";
						if (sobjs.OfType<Planet>().Any(o => o.HasAbility("Planet - Change Atmosphere")))
							abilText += "At";
						abilText = abilText.ToSpacedString();
						// TODO - cache brush assets
						var sfAbil = new StringFormat();
						sfAbil.Alignment = StringAlignment.Far;
						sfAbil.LineAlignment = StringAlignment.Far;
						var rectAbil = new RectangleF(drawx - drawsize / 2f + bigFontSize, drawy - drawsize / 2f, drawsize - bigFontSize, drawsize - bigFontSize - 1);
						pe.Graphics.DrawString(abilText, littleFont, Brushes.Black, rectAbil, sfAbil); // drop shadow
						rectAbil = new RectangleF(new PointF(rectAbil.X + 1, rectAbil.Y + 1), rectAbil.Size);
						pe.Graphics.DrawString(abilText, littleFont, new SolidBrush(Empire.Current.Color), rectAbil, sfAbil); // real text

						// draw waypoint reticule
						var sfwp = new StringFormat();
						sfwp.Alignment = StringAlignment.Near;
						sfwp.LineAlignment = StringAlignment.Far;
						var box = new Rectangle((int)(drawx - drawsize / 2f), (int)(drawy - drawsize / 2f), drawsize, drawsize);
						foreach (var wp in Empire.Current.Waypoints.Except(Empire.Current.NumberedWaypoints))
						{
							if (wp.Sector == sector)
							{
								// waypoints with no hotkey are drawn in gray
								pe.Graphics.DrawRectangle(Pens.Gray, box);
								pe.Graphics.DrawString(wp.Name, littleFont, Brushes.Gray, box);
							}
						}
						for (int i = 0; i < Empire.Current.NumberedWaypoints.Length; i++)
						{
							// waypoints with a hotkey are drawn in silver
							var wp = Empire.Current.NumberedWaypoints[i];
							if (wp != null && wp.Sector == sector)
							{
								pe.Graphics.DrawRectangle(Pens.Silver, box);
								pe.Graphics.DrawString("WP" + i + ": " + wp.Name, littleFont, Brushes.Silver, box);
							}
						}

						// draw selection reticule
						if (sector == SelectedSector)
						{
							// TODO - cache pen asset
							pe.Graphics.DrawRectangle(new Pen(Color.White), box);
						}
					}
				}

				if (SelectedSpaceObject is IMobileSpaceObject)
				{
					// draw path lines
					var sobj = (IMobileSpaceObject)SelectedSpaceObject;
					Sector last = SelectedSpaceObject.FindSector();
					PointF? lastPoint = null;
					if (last.StarSystem == StarSystem)
					{
						var lastCoords = last.Coordinates;
						lastPoint = GetDrawPoint(lastCoords.X, lastCoords.Y);
					}
					int moves = 0;
					int turns = 0;
					foreach (var cur in sobj.Path())
					{
						moves += 1;
						if (moves >= sobj.Speed)
						{
							moves = 0;
							turns++;
						}

						PointF? curPoint = null;
						if (cur != null && cur.StarSystem == StarSystem)
						{
							var curCoords = cur.Coordinates;
							curPoint = GetDrawPoint(curCoords.X, curCoords.Y);
						}

						if (lastPoint == null && curPoint == null)
						{
							// do nothing, we're not in this system
						}
						else if (lastPoint == null)
						{
							// entering the system, draw a circle
							pe.Graphics.DrawEllipse(Pens.White, curPoint.Value.X - 5, curPoint.Value.Y - 5, 10, 10);
						}
						else if (curPoint == null)
						{
							// leaving the system, draw a square
							pe.Graphics.DrawRectangle(Pens.White, lastPoint.Value.X - 5, lastPoint.Value.Y - 5, 10, 10);
						}
						else
						{
							// regular movement, draw a line
							pe.Graphics.DrawLine(Pens.White, lastPoint.Value, curPoint.Value);
						}

						if (moves == 0 && curPoint != null)
						{
							// draw turn number
							var sf = new StringFormat();
							sf.Alignment = StringAlignment.Center;
							sf.LineAlignment = StringAlignment.Center;
							pe.Graphics.DrawString(turns.ToString(), bigFont, Brushes.White, curPoint.Value.X, curPoint.Value.Y, sf); 
						}

						last = cur;
						lastPoint = curPoint;
					}
				}
			}
		}

		private PointF GetDrawPoint(int x, int y)
		{
			var drawx = x * (SectorDrawSize + SectorBorderSize) + Width / 2f + SectorBorderSize;
			var drawy = y * (SectorDrawSize + SectorBorderSize) + Height / 2f + SectorBorderSize;
			return new PointF(drawx, drawy);
		}
	}
}
