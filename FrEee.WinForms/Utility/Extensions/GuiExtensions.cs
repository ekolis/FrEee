using FrEee.Utility;
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
		public static ListViewItem AddItemWithImage(this ListView lv, string groupName, string text, object tag, Image image, params string[] subitems)
		{
			int imageNum = lv.Items.Count;
			if (lv.LargeImageList != null)
				lv.LargeImageList.Images.Add(image ?? new Bitmap(lv.LargeImageList.ImageSize.Width, lv.LargeImageList.ImageSize.Height));
			if (lv.SmallImageList != null)
				lv.SmallImageList.Images.Add(image ?? new Bitmap(lv.SmallImageList.ImageSize.Width, lv.SmallImageList.ImageSize.Height));
			ListViewItem item;
			if (groupName != null)
			{
				var group = lv.Groups.Cast<ListViewGroup>().SingleOrDefault(g => g.Header == groupName);
				if (group == null)
				{
					group = new ListViewGroup(groupName);
					lv.Groups.Add(group);
				}
				item = new ListViewItem(text, group);
			}
			else
				item = new ListViewItem(text);
			foreach (var sub in subitems)
			{
				var lvsub = new ListViewItem.ListViewSubItem(item, sub);
				item.SubItems.Add(lvsub);
			}
			item.Tag = tag;
			item.ImageIndex = imageNum;
			lv.Items.Add(item);
			return item;
		}

		/// <summary>
		/// Is a bound item selected in a listview?
		/// </summary>
		/// <param name="lv">The list view.</param>
		/// <param name="item">A data bound item (NOT a ListViewItem, but its Tag value).</param>
		/// <returns>true if the item is selected, otherwise false</returns>
		public static bool HasItemSelected(this ListView lv, object item)
		{
			return lv.SelectedItems.Cast<ListViewItem>().Any(i => i.Tag == item);
		}

		/// <summary>
		/// Creates image lists for a tree view and clears the items.
		/// </summary>
		public static void Initialize(this TreeView tv, int imageSize)
		{
			tv.Nodes.Clear();
			tv.ImageList = new ImageList { ImageSize = new Size(imageSize, imageSize), ColorDepth = ColorDepth.Depth32Bit };
		}

		/// <summary>
		/// Adds an item with an image to a tree view.
		/// </summary>
		public static TreeNode AddItemWithImage(this TreeView tv, string text, object tag, Image image)
		{
			int imageNum = tv.GetNodeCount(true);
			if (tv.ImageList != null)
				tv.ImageList.Images.Add(image ?? Pictures.GetSolidColorImage(Color.Transparent, Math.Min(tv.ImageList.ImageSize.Width, tv.ImageList.ImageSize.Height)));
			var node = new TreeNode(text, imageNum, imageNum);
			node.Tag = tag;
			tv.Nodes.Add(node);
			return node;
		}

		/// <summary>
		/// Adds an item with an image to a tree view as a child of another node.
		/// </summary>
		public static TreeNode AddItemWithImage(this TreeNode n, string text, object tag, Image image)
		{
			int imageNum = n.TreeView.GetNodeCount(true);
			if (n.TreeView.ImageList != null)
				n.TreeView.ImageList.Images.Add(image ?? Pictures.GetSolidColorImage(Color.Transparent, Math.Min(n.TreeView.ImageList.ImageSize.Width, n.TreeView.ImageList.ImageSize.Height)));
			var node = new TreeNode(text, imageNum, imageNum);
			node.Tag = tag;
			n.Nodes.Add(node);
			return node;
		}

		/// <summary>
		/// Gets a list of all nodes and child nodes, etc. recursively on a tree view.
		/// </summary>
		/// <param name="tv"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		public static IEnumerable<TreeNode> GetAllNodes(this TreeView tv)
		{
			foreach (TreeNode n in tv.Nodes)
			{
				yield return n;
				foreach (var n2 in n.GetAllNodes())
					yield return n2;
			}
		}

		/// <summary>
		/// Gets a list of all nodes and child nodes, etc. recursively on a tree node.
		/// </summary>
		/// <param name="tv"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		public static IEnumerable<TreeNode> GetAllNodes(this TreeNode tn)
		{
			foreach (TreeNode n in tn.Nodes)
			{
				yield return n;
				foreach (var n2 in n.GetAllNodes())
					yield return n2;
			}
		}

		/// <summary>
		/// Shows a form as a dialog in the center of its parent form with a wait cursor while the form loads.
		/// </summary>
		/// <param name="parent"></param>
		public static DialogResult ShowChildForm(this Form parent, Form form)
		{
			parent.BeginInvoke(new Action(() => parent.Cursor = Cursors.WaitCursor));
			if (form.StartPosition != FormStartPosition.CenterScreen)
				form.StartPosition = FormStartPosition.CenterParent;
			var result = form.ShowDialog();
			parent.Cursor = Cursors.Default;
			return result;
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

		/// <summary>
		/// Creates a "popup form" containing a control, which can be dismissed using Escape.
		/// </summary>
		/// <param name="control">The control to embed.</param>
		/// <param name="text">The text for the form's title bar.</param>
		/// <returns></returns>
		public static Form CreatePopupForm(this Control control, string text = "")
		{
			var form = new Form();
			form.Text = text;
			form.MaximizeBox = false;
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.ClientSize = control.Size;
			// TODO - deal with multiple screens
			if (form.Width > Screen.PrimaryScreen.WorkingArea.Width)
				form.Width = Screen.PrimaryScreen.WorkingArea.Width;
			if (form.Height > Screen.PrimaryScreen.WorkingArea.Height)
				form.Height = Screen.PrimaryScreen.WorkingArea.Height;
			form.StartPosition = FormStartPosition.CenterParent;
			form.Controls.Add(control);
			return form;
		}

		/// <summary>
		/// http://stackoverflow.com/questions/435433/what-is-the-preferred-way-to-find-focused-control-in-winforms-app
		/// </summary>
		/// <param name="control"></param>
		/// <returns></returns>
		public static Control FindFocusedControl(this Control control)
		{
			var container = control as ContainerControl;
			while (container != null)
			{
				control = container.ActiveControl;
				container = control as ContainerControl;
			}
			return control;
		}
	}
}
