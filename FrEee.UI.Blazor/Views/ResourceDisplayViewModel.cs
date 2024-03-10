using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Properties;
using FrEee.Utility;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Views
{
	public class ResourceDisplayViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public int Amount { get; set; } = 0;

		public int? Change { get; set; } = null;

		public string Text
		{
			get
			{
				if (Change is null || Change == 0)
				{
					return Amount.ToUnitString();
				}
				else if (Change > 0)
				{
					return $"{Amount.ToUnitString()} (+{Change.ToUnitString()})";
				}
				else if (Change < 0)
				{
					return $"{Amount.ToUnitString()} ({Change.ToUnitString()})";
				}
				else
				{
					// shouldn't happen
					return Amount.ToUnitString();
				}
			}
		}

		public Resource? Resource { get; set; } = null;

		public Color Color => Resource?.Color ?? Color.White;

		public Image? Icon
		{
			get
			{
				try
				{
					return Resource?.Icon;
				}
				catch (NullReferenceException)
				{
					// HACK - stupid forms designer thinks it's null and not null at the same time, WTF?!
					var icon = new Bitmap(1, 1);
					var color = Color.Gray;
					if (Resource is not null)
					{
						color = Color;
					}
					var g = Graphics.FromImage(icon);
					g.Clear(color);
					return icon;
				}
			}
		}

		// TODO: generalize IconSource, rewrite Pictures class, allow other images besides PNG
		public string? IconSource => Resource?.IconPaths
			.Select(it => Path.Combine("Pictures", it + ".png"))
			.FirstOrDefault(File.Exists);

		public string? Name
		{
			get => Resource?.Name;
			set => Resource = Resource.Find(value ?? "");
		}

		public string? Abbreviation => Name?.Substring(0, 3);
	}
}
