using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls
{
	public partial class GameButton : Button
	{
		public GameButton()
		{
			InitializeComponent();
			UseVisualStyleBackColor = false;
			BackColor = Color.Black;
			ForeColor = Color.CornflowerBlue;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}
	}
}
