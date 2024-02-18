using System.Windows.Forms;

namespace FrEee.UI.WinForms.Controls;

public partial class GameTableLayoutPanel : TableLayoutPanel
{
	public GameTableLayoutPanel()
	{
		InitializeComponent();
		DoubleBuffered = true;
	}
}