﻿using System;
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
		public IEnumerable<ObjectLocation<StarSystem>> StarSystemLocations => Galaxy.StarSystemLocations;

		/// <summary>
		/// The image to display as the background of the map.
		/// </summary>
		public Image? BackgroundImage { get; set; } = null;

		public ImageDisplayViewModel BackgroundImageVM => new() { Image = BackgroundImage };

		/// <summary>
		/// The render mode for the map. Controls how star systems are displayed.
		/// </summary>
		public IGalaxyMapMode Mode { get; set; } // TODO: specify default galaxy map mode

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

		public int MinX => Galaxy.MinX;

		public int MaxX => Galaxy.MaxX;

		public int MinY => Galaxy.MinY;

		public int MaxY => Galaxy.MaxY;

		/// <summary>
		/// The number of star systems which can be lined up horizontally on the map.
		/// </summary>
		public int Width => Galaxy.UsedWidth;

		/// <summary>
		/// The number of star systems which can be lined up vertically on the map.
		/// </summary>
		public int Height => Galaxy.UsedHeight;

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

		public PieChartViewModel<int> GetStarSystemViewModel(int x, int y)
		{
			var starSystem = StarSystemLocations
				.SingleOrDefault(q => q.Location.X == x && q.Location.Y == y)
				?.Item;
			if (starSystem == null)
			{
				return null;
			}
			// TODO: use map modes to get view model
			var entry = new PieChartViewModel<int>.Entry(starSystem.Name, Color.Blue, 1);
			return new PieChartViewModel<int>
			{
				Entries = new HashSet<PieChartViewModel<int>.Entry> { entry },
				OnClickEntry = entry => StarSystemClicked?.Invoke(starSystem),
			};
		}
	}
}