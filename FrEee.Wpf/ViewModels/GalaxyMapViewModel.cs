using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Wpf.Utility;
using FrEee.Wpf.Views.GalaxyMapViewRenderers;
using static FrEee.Wpf.Utility.Pictures;

namespace FrEee.Wpf.ViewModels
{
	public class GalaxyMapViewModel : INotifyPropertyChanged
	{
		/// <summary>
		/// For XAML design time binding.
		/// </summary>
		public GalaxyMapViewModel()
			: this(null)
		{

		}

		public GalaxyMapViewModel(Galaxy galaxy)
		{
			Galaxy = galaxy;
			ComputeWarpPointConnectivity();
			Renderer = GalaxyMapViewRenderers.All.FirstOrDefault();
			IsGridEnabled = true;
		}

		#region Model
		private Galaxy Galaxy { get; set; }
		#endregion

		#region Properties
		public IEnumerable<ObjectLocation<StarSystem>> StarSystemLocations
		{
			get
			{
				if (Galaxy == null)
					return Enumerable.Empty<ObjectLocation<StarSystem>>();
				return Galaxy.StarSystemLocations;
			}
		}

		public ImageSource BackgroundImage
		{
			get
			{
				// TODO - galaxy view background image can depend on galaxy template?
				return GetModImageSource(Path.Combine(App.RootDirectory, "Pictures", "UI", "Map", "quadrant"));
			}
		}

		public int Width { get { return Galaxy?.Width ?? 0; } }

		public int Height { get { return Galaxy?.Height ?? 0; } }

		public int UsedWidth { get { return Galaxy?.UsedWidth ?? 0; } }

		public int UsedHeight { get { return Galaxy?.UsedHeight ?? 0; } }

		private StarSystem _SelectedStarSystem;

		public StarSystem SelectedStarSystem
		{
			get { return _SelectedStarSystem; }
			set
			{
				_SelectedStarSystem = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedStarSystem)));
			}
		}

		private ConnectivityGraph<ObjectLocation<StarSystem>> warpGraph;

		public ConnectivityGraph<ObjectLocation<StarSystem>> WarpGraph
		{
			get
			{
				if (warpGraph == null)
					ComputeWarpPointConnectivity();
				return warpGraph;
			}
			private set { warpGraph = value; }
		}

		private bool _IsGridEnabled;

		public bool IsGridEnabled
		{
			get { return _IsGridEnabled; }
			set
			{
				_IsGridEnabled = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsGridEnabled)));
			}
		}

		private IGalaxyMapViewRenderer _Renderer;

		public IGalaxyMapViewRenderer Renderer
		{
			get { return _Renderer; }
			set
			{
				_Renderer = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Renderer)));
			}
		}
		#endregion

		#region Methods
		public void ComputeWarpPointConnectivity()
		{
			if (Galaxy == null)
				WarpGraph = new ConnectivityGraph<ObjectLocation<StarSystem>>();
			else
			{
				WarpGraph = new ConnectivityGraph<ObjectLocation<StarSystem>>(Galaxy.StarSystemLocations);

				foreach (var ssl in WarpGraph)
				{
					foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>())
					{
						if (wp.TargetStarSystemLocation == null)
							continue; // can't make connection if we don't know where warp point ends!

						WarpGraph.Connect(ssl, wp.TargetStarSystemLocation);
					}
				}
			}
		}
		#endregion

		#region Sub View Models
		#endregion

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
