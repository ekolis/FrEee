using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Vehicles;

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
			this.MouseClick += StarSystemView_MouseClicked;
			DoubleBuffered = true;
		}

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

		void StarSystemView_MouseClicked(object sender, MouseEventArgs e)
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

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.Clear(BackColor);

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

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
						var drawsize = SectorDrawSize;
						var drawx = x * (drawsize + SectorBorderSize) + Width / 2f + SectorBorderSize;
						var drawy = y * (drawsize + SectorBorderSize) + Height / 2f + SectorBorderSize;

						// find sector
						var sector = StarSystem.GetSector(x, y);

						// draw image, owner flag, and name of largest space object (if any)
						var largest = sector.SpaceObjects.Largest();
						if (largest != null)
						{
							Image pic;
							if (largest is AutonomousSpaceVehicle)
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
							var font = new Font("Sans Serif", 8);
							var sf = new StringFormat();
							sf.Alignment = StringAlignment.Center; // center align our name
							sf.LineAlignment = StringAlignment.Far; // bottom align our name
							pe.Graphics.DrawString(largest.Name, font, new SolidBrush(Color.White), drawx, drawy + drawsize / 2f, sf);
						}

						// draw number to indicate how many stellar objects are present if >1
						if (sector.SpaceObjects.OfType<StellarObject>().Count() > 1)
						{
							// TODO - cache font and brush assets
							var font = new Font("Sans Serif", 8);
							var sf = new StringFormat();
							sf.Alignment = StringAlignment.Far; // right align our number
							sf.LineAlignment = StringAlignment.Far; // bottom align our number
							pe.Graphics.DrawString(sector.SpaceObjects.OfType<StellarObject>().Count().ToString(), font, new SolidBrush(Color.White), drawx + drawsize / 2f, drawy + drawsize / 2f - 12, sf);
						}

						var availForFlagsAndNums = Math.Min(drawsize - 21, 12);
						var cornerx = drawx - drawsize / 2;
						var cornery = drawy - drawsize / 2;
						if (sector.SpaceObjects.Count > 1)
						{
							int top = 0;
							int insigniaSize = availForFlagsAndNums / 2;
							foreach (var g in sector.SpaceObjects.Except(sector.SpaceObjects.OfType<StellarObject>()).GroupBy(sobj => sobj.Owner))
							{
								// draw empire insignia and space object count
								var owner = g.Key;
								var count = g.Count();
								var aspect = owner.Icon;
								if (owner.Icon != null)
									pe.Graphics.DrawImage(owner.Icon, cornerx, cornery + top, insigniaSize, insigniaSize);
								// TODO - cache font and brush assets
								pe.Graphics.DrawString(count.ToString(), new Font("Sans Serif", insigniaSize), new SolidBrush(owner.Color), cornerx + insigniaSize, cornery + top);
								top += insigniaSize;
							}
						}

						// draw selection reticule
						if (sector == SelectedSector)
						{
							// TOOD - cache pen asset
							pe.Graphics.DrawRectangle(new Pen(Color.White), drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
						}
					}
				}
			}
		}
	}
}
