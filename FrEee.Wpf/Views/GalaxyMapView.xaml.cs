using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrEee.Wpf.Utility;
using FrEee.Wpf.ViewModels;

namespace FrEee.Wpf.Views
{
	/// <summary>
	/// Displays a map of the galaxy.
	/// </summary>
	public partial class GalaxyMapView
	{
		public GalaxyMapView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The size at which each star system will be drawn, in pixels.
		/// </summary>
		public int StarSystemDrawSize
		{
			get
			{
				if (Galaxy == null)
					return 0;
				return (int)Math.Min((float)Width / (float)Galaxy.UsedWidth, (float)Height / Galaxy.UsedHeight);
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);

			if (Galaxy != null)
			{
				// draw background image
				if (Galaxy.BackgroundImage != null)
				{
					var desiredAspect = Width / Height;
					var actualAspect = Galaxy.Width / Galaxy.Height;
					double x, y, w, h;
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
					dc.DrawImage(Galaxy.BackgroundImage, new Rect(x, y, w, h));
				}

				var drawsize = StarSystemDrawSize;
				var whitePen = new Pen(Brushes.White, 1);

				// draw star systems
				var avgx = (Galaxy.StarSystemLocations.Min(l => l.Location.X) + Galaxy.StarSystemLocations.Max(l => l.Location.X)) / 2f;
				var avgy = (Galaxy.StarSystemLocations.Min(l => l.Location.Y) + Galaxy.StarSystemLocations.Max(l => l.Location.Y)) / 2f;
				foreach (var ssl in Galaxy.StarSystemLocations)
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
					if (Renderer != null)
						Renderer.Render(sys, dc, new Point(drawx, drawy), drawsize);

					// draw selection reticule
					if (sys == Galaxy.SelectedStarSystem)
						dc.DrawRectangle(null, whitePen, new Rect(drawx - drawsize / 2f - 1, drawy - drawsize / 2f - 1, drawsize + 2, drawsize + 2));
				}

				// draw warp points
				// TODO - draw warp points using draw mode? maybe draw blockaded warp points in red?
				foreach (var ssl in Galaxy.WarpGraph)
				{
					var startPos = new Point
					(
						(ssl.Location.X - avgx) * drawsize + Width / 2f,
						(ssl.Location.Y - avgy) * drawsize + Height / 2f
					);
					foreach (var target in Galaxy.WarpGraph.GetExits(ssl))
					{
						if (target == null)
							continue; // TODO - draw short line guessing where warp point might lead based on its location in the system? if in center, make something up?

						var endPos = new Point
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
						var realStartPos = new Point(startPos.X + ndx, startPos.Y + ndy);
						var realEndPos = new Point(endPos.X - ndx, endPos.Y - ndy);

						// draw line
						dc.DrawLine(new Pen(Brushes.Gray, 1), realStartPos, realEndPos);

						if (!Galaxy.WarpGraph.GetExits(target).Contains(ssl))
						{
							// one way warp point, so draw an arrow
							var angle = startPos.AngleTo(endPos);
							var radians = Math.PI * angle / 180d;
							var realMidPos = new Point((realStartPos.X + realEndPos.X) / 2f, (realStartPos.Y + realEndPos.Y) / 2f);
							var adx1 = -(float)Math.Cos(radians + Math.PI / 6d) * drawsize / 2f;
							var ady1 = -(float)Math.Sin(radians + Math.PI / 6d) * drawsize / 2f;
							var arrowEndPos1 = new Point(realMidPos.X + adx1, realMidPos.Y + ady1);
							var adx2 = -(float)Math.Cos(radians - Math.PI / 6d) * drawsize / 2f;
							var ady2 = -(float)Math.Sin(radians - Math.PI / 6d) * drawsize / 2f;
							var arrowEndPos2 = new Point(realMidPos.X + adx2, realMidPos.Y + ady2);
							dc.DrawLine(whitePen, realMidPos, arrowEndPos1);
							dc.DrawLine(whitePen, realMidPos, arrowEndPos2);
						}
					}
				}
			}
		}

		public GalaxyMapViewModel Galaxy
		{
			get
			{
				return (GalaxyMapViewModel)ViewModel;
			}
			set
			{
				ViewModel = value;
			}
		}

		public GalaxyMapViewRenderer Renderer
		{
			get { return (GalaxyMapViewRenderer)GetValue(RendererProperty); }
			set { SetValue(RendererProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Renderer.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty RendererProperty =
			DependencyProperty.Register("Renderer", typeof(GalaxyMapViewRenderer), typeof(GalaxyMapView), new PropertyMetadata(null));


	}
}
