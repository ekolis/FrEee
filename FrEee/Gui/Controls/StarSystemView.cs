using FrEee.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.Gui.Controls
{
	public partial class StarSystemView : Control
	{
		public StarSystemView()
		{
			InitializeComponent();
			BackColor = Color.Black;
			this.SizeChanged += StarSystemView_SizeChanged;
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

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.Clear(BackColor);

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			if (StarSystem != null)
			{
				// TODO - draw star system background

				for (var x = -StarSystem.Radius; x <= StarSystem.Radius; x++)
				{
					for (var y = -StarSystem.Radius; y <= StarSystem.Radius; y++)
					{
						// where and how big will we draw the sector?
						var drawsize = (float)Math.Min(Width, Height) / (float)StarSystem.Diameter;
						var drawx = x * drawsize + Width / 2f;
						var drawy = y * drawsize + Height / 2f;

						// find sector
						var sector = StarSystem.GetSector(x, y);

						// draw icon and owner flag of largest space object (if any)
						var largest = sector.SpaceObjects.Largest();
						if (largest != null)
						{
							pe.Graphics.DrawImage(largest.Icon, drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
							
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
					}
				}
			}
		}
	}
}
