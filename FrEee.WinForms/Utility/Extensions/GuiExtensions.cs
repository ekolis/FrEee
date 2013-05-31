using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Utility.Extensions
{
	/// <summary>
	/// Extension methods for GUIs.
	/// </summary>
	public static class GuiExtensions
	{
		/// <summary>
		/// Creates image lists for a list view.
		/// </summary>
		public static void InitializeImageLists(this ListView lv, int largeImageSize, int smallImageSize)
		{
			lv.LargeImageList = new ImageList { ImageSize = new Size(largeImageSize, largeImageSize) };
			lv.SmallImageList = new ImageList { ImageSize = new Size(smallImageSize, smallImageSize) };
		}

		/// <summary>
		/// Adds an item with an image to a list view.
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="group"></param>
		/// <param name="text"></param>
		/// <param name="image"></param>
		/// <param name="imageKey">Needs to be unique for the image lists!</param>
		public static void AddItemWithImage(this ListView lv, string groupName, string text, Image image, string imageKey)
		{
			lv.LargeImageList.Images.Add(imageKey, image);
			lv.SmallImageList.Images.Add(imageKey, image);
			var group = lv.Groups.Cast<ListViewGroup>().SingleOrDefault(g => g.Header == groupName);
			if (group == null)
			{
				group = new ListViewGroup(groupName);
				lv.Groups.Add(group);
			}
			var item = new ListViewItem(text, group);
			item.ImageKey = imageKey;
			lv.Items.Add(item);
		}
	}
}
