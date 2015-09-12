using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FrEee.Game.Objects.Space;
using static FrEee.Wpf.Utility.Pictures;

namespace FrEee.Wpf.ViewModels
{
	public class StarSystemMapViewModel : INotifyPropertyChanged
	{
		#region Constructors
		/// <summary>
		/// For XAML design time binding.
		/// </summary>
		public StarSystemMapViewModel()
			: this(null)
		{

		}

		public StarSystemMapViewModel(StarSystem sys)
		{
			StarSystem = sys;
            sectors = new Lazy<IEnumerable<SectorViewModel>>(() =>
			{
				if (StarSystem == null)
					return Enumerable.Empty<SectorViewModel>();
				return StarSystem.Sectors.Select(s => new SectorViewModel(s)).ToArray();
			});
		}
		#endregion

		#region Model
		private StarSystem StarSystem;
		#endregion

		#region Properties
		public int Radius
		{
			get
			{
				return StarSystem?.Radius ?? -1;
			}
		}

		public int Diameter
		{
			get
			{
				return StarSystem?.Diameter ?? 0;
			}
		}

		public ImageSource BackgroundImage
		{
			get
			{
				return GetModImageSource(Path.Combine(App.RootDirectory, "Pictures", "Systems", StarSystem.BackgroundImagePath));
			}
		}
		#endregion

		#region Sub View Models
		private Lazy<IEnumerable<SectorViewModel>> sectors;
		public IEnumerable<SectorViewModel> Sectors
		{
			get
			{
				return sectors.Value;
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
