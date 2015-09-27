using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Wpf.Rendering;
using FrEee.Wpf.Utility;
using static FrEee.Wpf.Utility.Pictures;

namespace FrEee.Wpf.ViewModels
{
	public class SpaceObjectViewModel : INotifyPropertyChanged
	{
		#region Constructors
		/// <summary>
		/// For XAML design time binding.
		/// </summary>
		public SpaceObjectViewModel()
			: this(null)
		{

		}

		public SpaceObjectViewModel(ISpaceObject s)
		{
			SpaceObject = s;
		}
		#endregion

		#region Model
		private ISpaceObject SpaceObject;
		#endregion

		#region Properties
		public string Name { get { return SpaceObject?.Name; } }

		public ImageSource Icon
		{
			get
			{
				return Renderers.RenderIcon(SpaceObject);
			}
		}

		public ImageSource Portrait
		{
			get
			{
				return Renderers.RenderPortrait(SpaceObject);
			}
		}
		#endregion

		#region Sub View Models
		#endregion

		#region Methods
		#endregion

		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
