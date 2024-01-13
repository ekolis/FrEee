using FrEee.Objects.Space;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.WinForms.Objects.GalaxyViewModes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
			this.MouseMove += GalaxyView_MouseMove;
			DoubleBuffered = true;
		}

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

		public IGalaxyViewMode Mode
		{
			get
			{
				return mode;
			}
			set
			{
				mode = value;
				Invalidate();
			}
		}

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

		private Image backgroundImage;

		private IGalaxyViewMode mode = GalaxyViewModes.All.First();

		private StarSystem selectedStarSystem;

		private ConnectivityGraph<ObjectLocation<StarSystem>> warpGraph;

		public void ComputeWarpPointConnectivity()
		{
			warpGraph = new ConnectivityGraph<ObjectLocation<StarSystem>>(Galaxy.Current.StarSystemLocations);

			foreach (var ssl in warpGraph)
			{
				foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>())
				{
					if (wp.TargetStarSystemLocation == null)
						continue; // can't make connection if we don't know where warp point ends!

					warpGraph.Connect(ssl, wp.TargetStarSystemLocation);
				}
			}
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
			var x = (int)Math.Round(((float)p.X - Width / 2f) / drawsize + avgx);
			var y = (int)Math.Round(((float)p.Y - Height / 2f) / drawsize + avgy);
			var p2 = new Point(x, y);
			var ssloc = Galaxy.Current.StarSystemLocations.FirstOrDefault(ssl => ssl.Location == p2);
			if (ssloc == null)
				return null;
			return ssloc.Item;
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
				switch (BackgroundImageLayout)
				{
					case ImageLayout.Stretch:
						if (actualAspect > desiredAspect)
						{
							x = -(int)(Width / actualAspect / 2) + Width / 2;
							w = (int)(Width / actualAspect);
							h = Height;
							y = 0;
						}
						else
						{
							y = -(int)(Height * actualAspect / 2) + Height / 2;
							h = (int)(Height * actualAspect);
							w = Width;
							x = 0;
						}
						break;

					case ImageLayout.Zoom:
						if (actualAspect > desiredAspect)
						{
							w = Width;
							h = (int)(Height * desiredAspect / actualAspect); ;
						}
						else
						{
							w = (int)(Width * actualAspect / desiredAspect);
							h = Height;
						}
						x = (Width - w) / 2;
						y = (Height - h) / 2;
						break;

					default: // TODO - more image layouts
						if (actualAspect > desiredAspect)
						{
							x = -(int)(Width / actualAspect / 2) + Width / 2;
							w = (int)(Width / actualAspect);
							h = Height;
							y = 0;
						}
						else
						{
							y = -(int)(Height * actualAspect / 2) + Height / 2;
							h = (int)(Height * actualAspect);
							w = Width;
							x = 0;
						}
						break;
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
					var drawx = (x - avgx) * drawsize + Width / 2f;
					var drawy = (y - avgy) * drawsize + Height / 2f;

					// find star system
					var sys = ssl.Item;

					// draw it if possible
					if (Mode != null)
						Mode.Draw(sys, pe.Graphics, new PointF(drawx, drawy), drawsize);

					// draw selection reticule
					if (sys == SelectedStarSystem)
						pe.Graphics.DrawRectangle(whitePen, drawx - drawsize / 2f - 1, drawy - drawsize / 2f - 1, drawsize + 2, drawsize + 2);
				}

				// draw warp points
				// TODO - draw warp points using draw mode? maybe draw blockaded warp points in red?
				if (warpGraph == null)
					ComputeWarpPointConnectivity();
				foreach (var ssl in warpGraph)
				{
					var startPos = new PointF
					(
						(ssl.Location.X - avgx) * drawsize + Width / 2f,
						(ssl.Location.Y - avgy) * drawsize + Height / 2f
					);
					foreach (var target in warpGraph.GetExits(ssl))
					{
						if (target == null)
							continue; // TODO - draw short line guessing where warp point might lead based on its location in the system? if in center, make something up?

						var endPos = new PointF
						(
							(target.Location.X - avgx) * drawsize + Width / 2f,
							(target.Location.Y - avgy) * drawsize + Height / 2f
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

		private void GalaxyView_MouseDown(object sender, MouseEventArgs e)
		{
			if (StarSystemClicked != null)
				StarSystemClicked(this, GetStarSystemAtPoint(e.Location));
		}

		private void GalaxyView_MouseMove(object sender, MouseEventArgs e)
		{
			var sys = GetStarSystemAtPoint(e.Location);
			if (sys == null)
				toolTip.SetToolTip(this, null);
			else if (toolTip.GetToolTip(this) != sys.Name)
				toolTip.SetToolTip(this, sys.Name);
		}

		private void GalaxyView_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		/// <summary>
		/// Occurs when the user clicks with the left mouse button on a star system or on empty space.
		/// </summary>
		public event StarSystemSelectionDelegate StarSystemClicked;

		/// <summary>
		/// Occurs when the selected star system changes.
		/// </summary>
		public event StarSystemSelectionDelegate StarSystemSelected;

		/// <summary>
		/// Delegate for events related to star system selection.
		/// </summary>
		/// <param name="sender">The galaxy view triggering the event.</param>
		/// <param name="sector">The star system selected/deselected/etc.</param>
		public delegate void StarSystemSelectionDelegate(GalaxyView sender, StarSystem starSystem);
	}
}