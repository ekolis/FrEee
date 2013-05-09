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
	/// <summary>
	/// Displays a galaxy map.
	/// </summary>
	public partial class GalaxyView : Control
	{
		public GalaxyView()
		{
			InitializeComponent();
			BackColor = Color.Black;
			this.SizeChanged += GalaxyView_SizeChanged;
			this.MouseClick += GalaxyView_MouseClick;
		}

		/// <summary>
		/// Delegate for events related to star system selection.
		/// </summary>
		/// <param name="sender">The galaxy view triggering the event.</param>
		/// <param name="sector">The star system selected/deselected/etc.</param>
		public delegate void StarSystemSelectionDelegate(GalaxyView sender, StarSystem starSystem);

		/// <summary>
		/// Occurs when the user clicks with the left mouse button on a star system or on empty space.
		/// </summary>
		public event StarSystemSelectionDelegate StarSystemClicked;

		/// <summary>
		/// Occurs when the selected star system changes.
		/// </summary>
		public event StarSystemSelectionDelegate StarSystemSelected;

		void GalaxyView_MouseClick(object sender, MouseEventArgs e)
		{
			if (StarSystemClicked != null)
				StarSystemClicked(this, GetStarSystemAtPoint(e.Location));
		}

		/// <summary>
		/// Gets the star system at specific screen coordinates.
		/// </summary>
		/// <param name="p">The screen coordinates.</param>
		/// <returns></returns>
		public StarSystem GetStarSystemAtPoint(Point p)
		{
			if (Galaxy == null)
				return null; // no such sector
			var drawsize = StarSystemDrawSize;
			// TODO - don't cut off the systems on the edges
			var x = (int)Math.Round((p.X - Width / 2f) / drawsize);
			var y = (int)Math.Round((p.Y - Height / 2f) / drawsize);
			var p2 = new Point(x, y);
			var ssloc = Galaxy.StarSystemLocations.FirstOrDefault(ssl => ssl.Location == p2);
			if (ssloc == null)
				return null;
			return ssloc.Item;
		}

		/// <summary>
		/// The size at which each star system will be drawn, in pixels.
		/// </summary>
		public float StarSystemDrawSize
		{
			get
			{
				if (Galaxy == null)
					return 0;
				return (float)Math.Min(Width, Height) / ((float)Math.Max(Galaxy.Width, Galaxy.Height));
			}
		}

		void GalaxyView_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		private Galaxy galaxy;

		/// <summary>
		/// The galaxy to display.
		/// </summary>
		public Galaxy Galaxy
		{
			get { return galaxy; }
			set
			{
				galaxy = value;
				Invalidate();
			}
		}

		private StarSystem selectedStarSystem;

		public StarSystem SelectedStarSystem
		{
			get { return selectedStarSystem; }
			set
			{
				selectedStarSystem = value;
				if (StarSystemSelected != null)
					StarSystemSelected(this, value);
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			pe.Graphics.Clear(BackColor);

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			if (Galaxy != null)
			{
				foreach (var ssl in Galaxy.StarSystemLocations)
				{
					// where and how big will we draw the star system?
					// TODO - don't cut off the systems on the edges
					var drawsize = StarSystemDrawSize;
					var x = ssl.Location.X;
					var y = ssl.Location.Y;
					var drawx = x * drawsize + Width / 2f;
					var drawy = y * drawsize + Height / 2f;

					// find star system
					var sys = ssl.Item;

					// draw circle for star system
					// do SE3-style split circles for contested systems because they are AWESOME!
					var owners = sys.FindSpaceObjects<ISpaceObject>().SelectMany(g => g).Select(g => g.Owner).Distinct().Where(o => o != null);
					if (owners.Count() == 0)
						pe.Graphics.DrawEllipse(new Pen(Color.Gray), drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
					else
					{
						var arcSize = 360f / owners.Count();
						int i = 0;
						foreach (var owner in owners)
						{
							pe.Graphics.DrawArc(new Pen(owner.Color), drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize, i * arcSize, arcSize);
							i++;
						}
					}

					// TODO - draw star system name

					// draw selection reticule (just a square for now)
					if (sys == SelectedStarSystem)
					{
						// TOOD - cache pen asset
						pe.Graphics.DrawRectangle(new Pen(Color.White), drawx - drawsize / 2f - 1, drawy - drawsize / 2f - 1, drawsize + 2, drawsize + 2);
					}
				}
			}
		}
	}
}
