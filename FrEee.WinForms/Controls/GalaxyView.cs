using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using FrEee.Utility;

namespace FrEee.WinForms.Controls
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
			this.MouseDown += GalaxyView_MouseDown;
			DoubleBuffered = true;
		}

		private Image backgroundImage;

		/// <summary>
		/// An image to display as the background for this galaxy view.
		/// </summary>
		public override Image BackgroundImage
		{
			get
			{
				return backgroundImage;
			}
			set
			{
				backgroundImage = value;
				Invalidate();
			}
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

		void GalaxyView_MouseDown(object sender, MouseEventArgs e)
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
			if (Galaxy.Current == null)
				return null; // no such sector
			var drawsize = StarSystemDrawSize;
			var avgx = (Galaxy.Current.StarSystemLocations.Min(l => l.Location.X) + Galaxy.Current.StarSystemLocations.Max(l => l.Location.X)) / 2f;
			var avgy = (Galaxy.Current.StarSystemLocations.Min(l => l.Location.Y) + Galaxy.Current.StarSystemLocations.Max(l => l.Location.Y)) / 2f;
			var x = (int)Math.Round(((float)p.X - Width / 2f - drawsize / 2f) / drawsize + avgx);
			var y = (int)Math.Round(((float)p.Y - Height / 2f - drawsize / 2f) / drawsize + avgy);
			var p2 = new Point(x, y);
			var ssloc = Galaxy.Current.StarSystemLocations.FirstOrDefault(ssl => ssl.Location == p2);
			if (ssloc == null)
				return null;
			return ssloc.Item;
		}

		/// <summary>
		/// The size at which each star system will be drawn, in pixels.
		/// </summary>
		public int StarSystemDrawSize
		{
			get
			{
				if (Galaxy.Current == null)
					return 0;
				return (int)Math.Min((float)Width / (float)Galaxy.Current.UsedWidth, (float)Height / Galaxy.Current.UsedHeight);
			}
		}

		void GalaxyView_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
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

			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			pe.Graphics.Clear(BackColor);

			if (BackgroundImage != null)
			{
				var desiredAspect = (double)Width / (double)Height;
				var actualAspect = (double)BackgroundImage.Width / (double)BackgroundImage.Height;
				int x, y, w, h;
				if (actualAspect > desiredAspect)
				{
					x = 0;
					w = Width;
					h = (int)(Width / actualAspect);
					y = (Height - h) / 2;
				}
				else
				{
					y = 0;
					h = Height;
					w = (int)(Height * actualAspect);
					x = (Width - w) / 2;
				}
				pe.Graphics.DrawImage(backgroundImage, x, y, w, h);
			}

			if (Galaxy.Current != null)
			{
				var drawsize = StarSystemDrawSize;
				var whitePen = new Pen(Color.White);

				// draw star systems
				var avgx = (Galaxy.Current.StarSystemLocations.Min(l => l.Location.X) + Galaxy.Current.StarSystemLocations.Max(l => l.Location.X)) / 2f;
				var avgy = (Galaxy.Current.StarSystemLocations.Min(l => l.Location.Y) + Galaxy.Current.StarSystemLocations.Max(l => l.Location.Y)) / 2f;
				foreach (var ssl in Galaxy.Current.StarSystemLocations)
				{
					// where will we draw the star system?
					var x = ssl.Location.X;// - minx;
					var y = ssl.Location.Y;// - miny;
					//var x = (int)Math.Round(((float)p.X - Width / 2f - drawsize / 2f) / drawsize);
					var drawx = (x - avgx) * drawsize + drawsize / 2f + Width / 2f;
					var drawy = (y - avgy) * drawsize + drawsize / 2f + Height / 2f;

					// find star system
					var sys = ssl.Item;

					// draw circle for star system
					// do SE3-style split circles for contested systems because they are AWESOME!
					var owners = sys.FindSpaceObjects<ISpaceObject>().SelectMany(g => g).Select(g => g.Owner).Distinct().Where(o => o != null);
					if (owners.Count() == 0)
						pe.Graphics.FillEllipse(Brushes.Gray, drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize);
					else
					{
						var arcSize = 360f / owners.Count();
						int i = 0;
						foreach (var owner in owners)
						{
							pe.Graphics.FillPie(new SolidBrush(owner.Color), drawx - drawsize / 2f, drawy - drawsize / 2f, drawsize, drawsize, i * arcSize, arcSize);
							i++;
						}
					}

					// draw selection reticule
					if (sys == SelectedStarSystem)
						pe.Graphics.DrawRectangle(whitePen, drawx - drawsize / 2f - 1, drawy - drawsize / 2f - 1, drawsize + 2, drawsize + 2);
				}

				// draw warp points
				if (warpGraph == null)
					ComputeWarpPointConnectivity();
				foreach (var ssl in warpGraph)
				{
					var startPos = new PointF
					(
						(ssl.Location.X - avgx) * drawsize + drawsize / 2f + Width / 2f,
						(ssl.Location.Y - avgy) * drawsize + drawsize / 2f + Height / 2f
					);
					foreach (var target in warpGraph.GetExits(ssl))
					{
						if (target == null)
							continue; // can't draw line if we don't know where warp point ends!

						var endPos = new PointF
						(
							(target.Location.X - avgx) * drawsize + drawsize / 2f + Width / 2f,
							(target.Location.Y - avgy) * drawsize + drawsize / 2f + Height / 2f
						);

						// overlapping systems or same system
						if (startPos == endPos)
							continue;

						// push the ends out past the system circles
						var dx = endPos.X - startPos.X;
						var dy = endPos.Y - startPos.Y;
						var length = Math.Max(Math.Abs(dx), Math.Abs(dy));
						var ndx = dx / length * drawsize / 2f;
						var ndy = dy / length * drawsize / 2f;
						var realStartPos = new PointF(startPos.X + ndx, startPos.Y + ndy);
						var realEndPos = new PointF(endPos.X - ndx, endPos.Y - ndy);

						// draw line
						pe.Graphics.DrawLine(Pens.Gray, realStartPos, realEndPos);

						if (!warpGraph.GetExits(target).Contains(ssl))
						{
							// one way warp point, so draw an arrow
							var angle = startPos.AngleTo(endPos);
							var radians = Math.PI * angle / 180d;
							var realMidPos = new PointF((realStartPos.X + realEndPos.X) / 2f, (realStartPos.Y + realEndPos.Y) / 2f);
							var adx1 = -(float)Math.Cos(radians + Math.PI / 6d) * drawsize / 2f;
							var ady1 = -(float)Math.Sin(radians + Math.PI / 6d) * drawsize / 2f;
							var arrowEndPos1 = new PointF(realMidPos.X + adx1, realMidPos.Y + ady1);
							var adx2 = -(float)Math.Cos(radians - Math.PI / 6d) * drawsize / 2f;
							var ady2 = -(float)Math.Sin(radians - Math.PI / 6d) * drawsize / 2f;
							var arrowEndPos2 = new PointF(realMidPos.X + adx2, realMidPos.Y + ady2);
							pe.Graphics.DrawLine(whitePen, realMidPos, arrowEndPos1);
							pe.Graphics.DrawLine(whitePen, realMidPos, arrowEndPos2);
						}
					}
				}
			}
		}

		private ConnectivityGraph<ObjectLocation<StarSystem>> warpGraph;

		public void ComputeWarpPointConnectivity()
		{
			warpGraph = new ConnectivityGraph<ObjectLocation<StarSystem>>(Galaxy.Current.StarSystemLocations);

			foreach (var ssl in warpGraph)
			{
				foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>().Flatten())
				{
					if (wp.TargetStarSystemLocation == null)
						continue; // can't make connection if we don't know where warp point ends!

					warpGraph.Connect(ssl, wp.TargetStarSystemLocation);
				}
			}
		}
	}
}
