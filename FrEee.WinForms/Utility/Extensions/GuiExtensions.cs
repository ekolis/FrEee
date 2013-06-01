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

		/// <summary>
		/// Shows a form as a dialog in the center of its parent form with a wait cursor while the form loads.
		/// </summary>
		/// <param name="parent"></param>
		public static void ShowChildForm(this Form parent, Form form)
		{
			parent.Cursor = Cursors.WaitCursor;
			form.StartPosition = FormStartPosition.CenterParent;
			form.ShowDialog();
			parent.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Finds the form that contains a control.
		/// </summary>
		/// <param name="control"></param>
		public static Form FindForm(this Control control)
		{
			if (control is Form)
				return (Form)control;
			return control.Parent.FindForm();
		}
	}
}
