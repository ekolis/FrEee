using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Objects.GameState;
using FrEee.Objects.Space;
using FrEee.Properties;
using FrEee.UI.Blazor.Views.GalaxyMapModes;
using FrEee.Utility;
using Microsoft.AspNetCore.Components;
using static IronPython.Modules._ast;

namespace FrEee.UI.Blazor.Views
{
	public class GalaxyMapViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// A star system has been clicked.
		/// </summary>
		public Action<StarSystem> StarSystemClicked { get; set; } = starSystem => { };

		/// <summary>
		/// The background has been clicked.
		/// </summary>
		public Action BackgroundClicked { get; set; } = () => { };

		/// <summary>
		/// A star system has been selected or deselected.
		/// </summary>
		public Action<StarSystem?> StarSystemSelected { get; set; } = starSystem => { };

		/// <summary>
		/// The current galaxy.
		/// </summary>
		private Galaxy Galaxy => Galaxy.Current;

		/// <summary>
		/// Any star systems in the galaxy, along with their locations.
		/// </summary>
		public IEnumerable<ObjectLocation<StarSystem>> StarSystemLocations => Galaxy?.StarSystemLocations ?? Enumerable.Empty<ObjectLocation<StarSystem>>();

		/// <summary>
		/// The image to display as the background of the map.
		/// </summary>
		public Image? BackgroundImage { get; set; } = null;

		public ImageDisplayViewModel BackgroundImageVM => new() { Image = BackgroundImage };

		/// <summary>
		/// The render mode for the map. Controls how star systems are displayed.
		/// </summary>
		public IGalaxyMapMode Mode { get; set; } = GalaxyMapModeLibrary.Find<PresenceMode>();

		private StarSystem? selectedStarSystem;

		/// <summary>
		/// The currently selected star system.
		/// </summary>
		public StarSystem? SelectedStarSystem
		{
			get => selectedStarSystem;
			set
			{
				selectedStarSystem = value;
				StarSystemSelected?.Invoke(selectedStarSystem);
			}
		}

		public int MinX => Galaxy?.MinX ?? 0;

		public int MaxX => Galaxy?.MaxX ?? 0;

		public int MinY => Galaxy?.MinY ?? 0;

		public int MaxY => Galaxy?.MaxY ?? 0;

		/// <summary>
		/// The number of star systems which can be lined up horizontally on the map.
		/// </summary>
		public int Width => Galaxy?.UsedWidth ?? 0;

		/// <summary>
		/// The number of star systems which can be lined up vertically on the map.
		/// </summary>
		public int Height => Galaxy?.UsedHeight ?? 0;

		public double AspectRatio => (double)Height / Width;

		/// <summary>
		/// Graph linking any star systems that are connected by warp points.
		/// </summary>
		public ConnectivityGraph<ObjectLocation<StarSystem>> WarpGraph { get; set; } = new();

		/// <summary>
		/// Computes connectivity of warp points in the galaxy.
		/// </summary>
		public void ComputeWarpPointConnectivity()
		{
			WarpGraph = new(Galaxy.StarSystemLocations);

			foreach (var ssl in WarpGraph)
			{
				foreach (var wp in ssl.Item.FindSpaceObjects<WarpPoint>())
				{
					// can't make connection if we don't know where warp point ends!
					if (wp.TargetStarSystemLocation is not null)
					{
						WarpGraph.Connect(ssl, wp.TargetStarSystemLocation);
					}
				}
			}
		}

		public PieChartViewModel<int>? GetStarSystemViewModel(StarSystem? starSystem)
		{
			/*if (starSystem is null)
			{
				return null;
			}*/
			return Mode.GetStarSystemViewModel(starSystem);
			// TODO: use map modes to get view model
			/*
			var entry = new PieChartViewModel<int>.Entry(starSystem.Name, Color.Blue, 1);
			return new PieChartViewModel<int>
			{
				Entries = new HashSet<PieChartViewModel<int>.Entry> { entry },
				OnClickEntry = entry => StarSystemClicked?.Invoke(starSystem),
			};
			*/
		}
	}
}
