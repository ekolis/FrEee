using System.Drawing;
using System.Windows.Forms;

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
				
				// draw the background
				var tabColor = SelectedTab == tab ? SelectedTabBackColor : TabBackColor;
				using (SolidBrush br = new SolidBrush(tabColor))
				{
					e.Graphics.FillRectangle(br, tabTextArea);
				}

				// draw the tab header text
				var textColor = SelectedTab == tab ? SelectedTabForeColor : TabForeColor;
				using (SolidBrush brush = new SolidBrush(textColor))
				{
					e.Graphics.DrawString(tab.Text, Font, brush, tabTextArea);
				}
			}
		}
	}
}