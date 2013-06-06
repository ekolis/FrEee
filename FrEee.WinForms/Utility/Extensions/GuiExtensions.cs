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
		/// Creates image lists for a list view and clears the items.
		/// </summary>
		public static void Initialize(this ListView lv, int largeImageSize, int smallImageSize)
		{
			lv.Items.Clear();
			lv.LargeImageList = new ImageList { ImageSize = new Size(largeImageSize, largeImageSize), ColorDepth = ColorDepth.Depth32Bit };
			lv.SmallImageList = new ImageList { ImageSize = new Size(smallImageSize, smallImageSize), ColorDepth = ColorDepth.Depth32Bit };
		}

		/// <summary>
		/// Adds an item with an image to a list view.
		/// </summary>
		/// <param name="lv"></param>
		/// <param name="group"></param>
		/// <param name="text"></param>
		/// <param name="image"></param>
		public static void AddItemWithImage(this ListView lv, string groupName, string text, object tag, Image image, params string[] subitems)
		{
			int imageNum = lv.Items.Count;
			lv.LargeImageList.Images.Add(image ?? new Bitmap(lv.LargeImageList.ImageSize.Width, lv.LargeImageList.ImageSize.Height));
			lv.SmallImageList.Images.Add(image ?? new Bitmap(lv.SmallImageList.ImageSize.Width, lv.SmallImageList.ImageSize.Height));
			var group = lv.Groups.Cast<ListViewGroup>().SingleOrDefault(g => g.Header == groupName);
			if (group == null)
			{
				group = new ListViewGroup(groupName);
				lv.Groups.Add(group);
			}
			var item = new ListViewItem(text, group);
			foreach (var sub in subitems)
			{
				var lvsub = new ListViewItem.ListViewSubItem(item, sub);
				item.SubItems.Add(lvsub);
			}
			item.Tag = tag;
			item.ImageIndex = imageNum;
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
