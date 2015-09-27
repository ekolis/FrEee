using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using static FrEee.Wpf.Utility.Pictures;

namespace FrEee.Wpf.ViewModels
{
	public class SectorViewModel : INotifyPropertyChanged
	{
		#region Constructors
		/// <summary>
		/// For XAML design time binding.
		/// </summary>
		public SectorViewModel()
			: this(null)
		{

		}

		public SectorViewModel(Sector s)
		{
			Sector = s;
			spaceObjects = new Lazy<IEnumerable<SpaceObjectViewModel>>(() =>
			{
				if (Sector == null)
					return Enumerable.Empty<SpaceObjectViewModel>();
				return Sector.SpaceObjects.Select(sobj => new SpaceObjectViewModel(sobj));
			});
			largestSpaceObject = new Lazy<SpaceObjectViewModel>(() => new SpaceObjectViewModel(Sector?.SpaceObjects.Largest()));
		}
		#endregion

		#region Model
		private Sector Sector;
		#endregion

		#region Properties
		public int X { get { return Sector.Coordinates.X; } }

		public int Y { get { return Sector.Coordinates.Y; } }
		#endregion

		#region Sub View Models
		private Lazy<IEnumerable<SpaceObjectViewModel>> spaceObjects;
		public IEnumerable<SpaceObjectViewModel> SpaceObjects
		{
			get
			{
				return spaceObjects.Value;
			}
		}

		private Lazy<SpaceObjectViewModel> largestSpaceObject;
		public SpaceObjectViewModel LargestSpaceObject
		{
			get
			{
				return largestSpaceObject.Value;
			}
		}
		#endregion

		#region Methods
		#endregion

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
