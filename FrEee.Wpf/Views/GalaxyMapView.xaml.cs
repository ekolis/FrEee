using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using FrEee.Wpf.Views.GalaxyMapViewRenderers;
using Gmvrs = FrEee.Wpf.Views.GalaxyMapViewRenderers.GalaxyMapViewRenderers;

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

		private void View_Loaded(object sender, RoutedEventArgs e)
		{
			if (Galaxy != null)
				Galaxy.PropertyChanged += Galaxy_PropertyChanged;
		}

		private void Galaxy_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			InvalidateVisual();
		}

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);

			if (Galaxy != null)
			{
				// draw background image
				if (Galaxy.BackgroundImage != null)
				{
					var desiredAspect = ActualWidth / ActualHeight;
					if (Galaxy.UsedWidth > 0 && Galaxy.UsedHeight > 0)
					{
						var actualAspect = Galaxy.Width / Galaxy.Height;
						double x, y, w, h;
						if (actualAspect > desiredAspect)
						{
							x = -(int)(ActualWidth / actualAspect / 2) + ActualWidth / 2;
							w = (int)(ActualWidth / actualAspect);
							h = ActualHeight;
							y = 0;
						}
						else
						{
							y = -(int)(ActualHeight * actualAspect / 2) + ActualHeight / 2;
							h = (int)(ActualHeight * actualAspect);
							w = ActualWidth;
							x = 0;
						}
						dc.DrawImage(Galaxy.BackgroundImage, new Rect(x, y, w, h));
					}
				}

				var whitePen = new Pen(Brushes.White, 1);

				// draw star systems
				if (Galaxy.StarSystemLocations.Any())
				{
					var minx = Galaxy.StarSystemLocations.Min(l => l.Location.X);
					var maxx = Galaxy.StarSystemLocations.Max(l => l.Location.X);
					var miny = Galaxy.StarSystemLocations.Min(l => l.Location.Y);
					var maxy = Galaxy.StarSystemLocations.Max(l => l.Location.Y);
					var avgx = (minx + maxx) / 2d;
					var avgy = (miny + maxy) / 2d;

					var drawsize = Math.Min(ActualWidth / Galaxy.UsedWidth, ActualHeight / Galaxy.UsedHeight);

					/*Debug.WriteLine($"drawsize: {drawsize}");
					Debug.WriteLine($"frame size: {ActualWidth}:{ActualHeight}");
					Debug.WriteLine($"galaxy bounds: {minx}:{miny} to {maxx}:{maxy}");
					Debug.WriteLine($"avg pos: {avgx}:{avgy} which is at {0 * drawsize + ActualWidth / 2f}:{0 * drawsize + ActualHeight / 2f}");*/

					var usedWidth = (maxx - minx + 1) * drawsize;
					var usedHeight = (maxy - miny + 1) * drawsize;
					var xpadding = (ActualWidth - usedWidth) / 2d;
					var ypadding = (ActualHeight - usedHeight) / 2d;

					if (Galaxy.IsGridEnabled)
					{
						for (var x = minx; x <= maxx; x++)
						{
							for (var y = miny; y <= maxy; y++)
							{
								dc.DrawRectangle(null, new Pen(Application.Current.Resources["GameTransparentBrush"] as Brush, 1), new Rect(
									(x - 0.5 - avgx) * drawsize + usedWidth / 2f + xpadding,
									(y - 0.5 - avgy) * drawsize + usedHeight / 2f + ypadding,
									drawsize, drawsize));
/*#if DEBUG
								if (x == 0 || y == 0)
									dc.DrawText(new FormattedText($"{x}:{y}", CultureInfo.CurrentCulture, CultureInfo.CurrentCulture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight, new Typeface("Sans Serif"), 10, App.Current.Resources["GameBrightBrightBrush"] as Brush), new Point((x - 0.5 - avgx) * drawsize + usedWidth / 2f + xpadding, (y - 0.5 - avgy) * drawsize + usedHeight / 2f + ypadding));
#endif*/
							}
						}
					}

					foreach (var ssl in Galaxy.StarSystemLocations)
					{
						// where will we draw the star system?
						var x = ssl.Location.X;// - minx;
						var y = ssl.Location.Y;// - miny;
											   //var x = (int)Math.Round(((float)p.X - Width / 2f - drawsize / 2f) / drawsize);
						var drawx = (x - avgx) * drawsize + ActualWidth / 2f;
						var drawy = (y - avgy) * drawsize + ActualHeight / 2f;

						// find star system
						var sys = ssl.Item;

						// draw it if possible
						if (Galaxy.Renderer != null)
							Galaxy.Renderer.Render(sys, dc, new Point(drawx, drawy), drawsize / 2d);

						// draw selection reticule
						if (sys == Galaxy.SelectedStarSystem)
							dc.DrawRectangle(null, new Pen(App.BrightBrightBrush, 2), new Rect(drawx - drawsize / 2f - 1, drawy - drawsize / 2f - 1, drawsize + 2, drawsize + 2));
					}

					// draw warp points
					// TODO - draw warp points using draw mode? maybe draw blockaded warp points in red?
					foreach (var ssl in Galaxy.WarpGraph)
					{
						var startPos = new Point
						(
							(ssl.Location.X - avgx) * drawsize + ActualWidth / 2f,
							(ssl.Location.Y - avgy) * drawsize + ActualHeight / 2f
						);
						foreach (var target in Galaxy.WarpGraph.GetExits(ssl))
						{
							if (target == null)
								continue; // TODO - draw short line guessing where warp point might lead based on its location in the system? if in center, make something up?

							var endPos = new Point
							(
								(target.Location.X - avgx) * drawsize + ActualWidth / 2f,
								(target.Location.Y - avgy) * drawsize + ActualHeight / 2f
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
							dc.DrawLine(new Pen(App.BrightBrightBrush, 1), realStartPos, realEndPos);

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
		}

		public GalaxyMapViewModel Galaxy
		{
			get
			{
				return DataContext as GalaxyMapViewModel;
			}
			set
			{
				DataContext = value;
			}
		}

		private Point TransformViewPointToGalaxyPoint(Point p)
		{
			var minx = Galaxy.StarSystemLocations.Min(l => l.Location.X);
			var maxx = Galaxy.StarSystemLocations.Max(l => l.Location.X);
			var miny = Galaxy.StarSystemLocations.Min(l => l.Location.Y);
			var maxy = Galaxy.StarSystemLocations.Max(l => l.Location.Y);
			var avgx = (minx + maxx) / 2d;
			var avgy = (miny + maxy) / 2d;

			var drawsize = Math.Min(ActualWidth / Galaxy.UsedWidth, ActualHeight / Galaxy.UsedHeight);

			var usedWidth = (maxx - minx + 1) * drawsize;
			var usedHeight = (maxy - miny + 1) * drawsize;
			var xpadding = (ActualWidth - usedWidth) / 2d;
			var ypadding = (ActualHeight - usedHeight) / 2d;

			var x = (int)Math.Round((p.X - xpadding - usedWidth / 2f) / drawsize + avgx);
			var y = (int)Math.Round((p.Y - ypadding - usedHeight / 2f) / drawsize + avgy);

			return new Point(x, y);
		}

		private void View_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// select clicked star system (or none if empty space selected)
			var p = e.GetPosition(this);
			var gp = TransformViewPointToGalaxyPoint(p);
			Galaxy.SelectedStarSystem = Galaxy.StarSystemLocations.SingleOrDefault(l => l.Location.X == Math.Round(gp.X) && l.Location.Y == Math.Round(gp.Y))?.Item;
		}

		private void View_MouseMove(object sender, MouseEventArgs e)
		{
			// set tooltip to name of hovered system
			var p = e.GetPosition(this);
			var gp = TransformViewPointToGalaxyPoint(p);
			try
			{
				var sys = Galaxy.StarSystemLocations.SingleOrDefault(l => l.Location.X == Math.Round(gp.X) && l.Location.Y == Math.Round(gp.Y))?.Item;
				if (sys != null)
				{
					var tt = (ToolTip)ToolTip;
					if (tt != null)
						tt.IsOpen = false;
					// TODO - how to dispose of it?
					tt = new ToolTip();
					tt.IsOpen = true;
					tt.Content = sys.Name;
					ToolTip = tt;
				}
				else if (ToolTip != null)
				{
					var tt = (ToolTip)ToolTip;
					tt.IsOpen = false;
					// TODO - how to dispose of it?
					ToolTip = null;
				}
			}
			catch (InvalidOperationException ex)
			{
				Console.Error.WriteLine($"Multiple star systems found at coordinates {Math.Round(gp.X)}, {Math.Round(gp.Y)}");
			}
		}
	}
}
