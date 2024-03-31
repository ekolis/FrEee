using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Extensions;
using FrEee.Properties;
using FrEee.Utility;
using Microsoft.AspNetCore.Components;

namespace FrEee.UI.Blazor.Views
{
	public class ImageDisplayViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public Image Image { get; set; } = new Bitmap(1, 1);

		public string ImageSource
		{
			get
			{
				if (Image is null)
				{
					return null;
				}

				// TODO: cache image source until image changes
				ImageConverter converter = new();
				var bytes = (byte[])converter.ConvertTo(Image, typeof(byte[]));
				var dataString = Convert.ToBase64String(bytes);
				var format = Image.RawFormat.ToString().ToLower();
				return $"data:image/{format};base64,{dataString}";
			}
		}

		public double AspectRatio =>
			Image == null ? 1 : ((double)Image.Width / (double)Image.Height);

		public Action OnClick { get; set; } = () => { };
	}
}
