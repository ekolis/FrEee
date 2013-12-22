using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class GameTabControl : TabControl
	{
		public GameTabControl()
		{
			InitializeComponent();
			BackColor = Color.Black;
			TabBackColor = Color.Black;
			TabForeColor = Color.CornflowerBlue;
			SelectedTabBackColor = Color.CornflowerBlue;
			SelectedTabForeColor = Color.Black;
			TabBorderColor = Color.CornflowerBlue;
			this.DrawMode = TabDrawMode.OwnerDrawFixed;
		}

		private Color tabBackColor;
		public Color TabBackColor
		{
			get { return tabBackColor; }
			set
			{
				tabBackColor = value;
				Invalidate();
			}
		}

		private Color selectedTabBackColor;
		public Color SelectedTabBackColor
		{
			get { return selectedTabBackColor; }
			set
			{
				selectedTabBackColor = value;
				Invalidate();
			}
		}

		private Color tabForeColor;
		public Color TabForeColor
		{
			get { return tabForeColor; }
			set
			{
				tabForeColor = value;
				Invalidate();
			}
		}

		private Color selectedTabForeColor;
		public Color SelectedTabForeColor
		{
			get { return selectedTabForeColor; }
			set
			{
				selectedTabForeColor = value;
				Invalidate();
			}
		}

		private Color tabBorderColor;
		public Color TabBorderColor
		{
			get { return tabBorderColor; }
			set
			{
				tabBorderColor = value;
				Invalidate();
			}
		}

		// http://stackoverflow.com/questions/1849801/c-sharp-winform-how-to-set-the-base-color-of-a-tabcontrol-not-the-tabpage
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			// fill in the whole rect
			using (SolidBrush br = new SolidBrush(Color.Black))
			{
				e.Graphics.FillRectangle(br, ClientRectangle);
			}

			// draw the tabs
			for (int i = 0; i < TabPages.Count; ++i)
			{
				TabPage tab = TabPages[i];
				// Get the text area of the current tab
				RectangleF tabTextArea = (RectangleF)GetTabRect(i);
				tabTextArea.Width *= e.Graphics.DpiX / 96f;
				tabTextArea.Height *= e.Graphics.DpiY / 96f;

				// draw the background
				var tabColor = SelectedTab == tab ? SelectedTabBackColor : TabBackColor;
				using (SolidBrush br = new SolidBrush(tabColor))
				{
					e.Graphics.FillRectangle(br, tabTextArea);
				}

				// draw the border
				using (Pen p = new Pen(TabBorderColor))
				{
					e.Graphics.DrawRectangle(p, tabTextArea.X, tabTextArea.Y, tabTextArea.Width, tabTextArea.Height);
				}

				// draw the tab header text
				var textColor = SelectedTab == tab ? SelectedTabForeColor : TabForeColor;
				using (SolidBrush brush = new SolidBrush(textColor))
				{
					var sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					e.Graphics.DrawString(tab.Text, Font, brush, tabTextArea, sf);
				}
			}

			// draw the tab underline (same color as border)
			var bottom = TabPages.Cast<TabPage>().MaxOrDefault(t => GetTabRect(TabPages.IndexOf(t)).Bottom);
			using (Pen p = new Pen(TabBorderColor, 5))
			{
				e.Graphics.DrawLine(p, ClientRectangle.Left, bottom, ClientRectangle.Right, bottom);
			}
		}
	}
}