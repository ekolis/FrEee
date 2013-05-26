using System;
using System.Drawing;
using System.Windows.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
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
		public float SectorDrawSize
		{
			get
			{
				if (StarSystem == null)
					return 0;
				return (float)Math.Min(Width, Height) / (float)StarSystem.Diameter;
			}
		}

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
						var drawx = x * drawsize + Width / 2f;
						var drawy = y * drawsize + Height / 2f;

						// find sector
						var sector = StarSystem.GetSector(x, y);

						// draw image and owner flag of largest space object (if any)
						var largest = sector.SpaceObjects.Largest();
						if (largest != null)
						{
							var portrait = largest.Portrait.GetThumbnailImage((int)drawsize, (int)drawsize, () => false, IntPtr.Zero);
							if (largest is Planet)
								((Planet)largest).DrawPopulationBars(portrait);
							pe.Graphics.DrawImage(portrait, drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
							
							// TODO - draw owner flag
						}
						
						// draw number to indicate how many space objects are present if >1
						if (sector.SpaceObjects.Count > 1)
						{
							// TODO - cache font and brush assets
							var font = new Font("Sans Serif", 8);
							var sf = new StringFormat();
							sf.Alignment = StringAlignment.Far; // right align our number
							sf.LineAlignment = StringAlignment.Far; // bottom align our number
							pe.Graphics.DrawString(sector.SpaceObjects.Count.ToString(), font, new SolidBrush(Color.White), drawx + drawsize / 2f, drawy + drawsize / 2f, sf);
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
