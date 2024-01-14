using FrEee.Objects.Civilization;
using FrEee.WinForms.Interfaces;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls;

public partial class EmpireReport : UserControl, IBindable<Empire>
{
	public EmpireReport()
	{
		InitializeComponent();
	}

	public Empire Empire
	{
		get
		{
			return empire;
		}
		set
		{
			empire = value;
			Bind();
		}
	}

	private Empire empire;

	public void Bind(Empire data)
	{
		Empire = data;
	}

	public void Bind()
	{
		SuspendLayout();
		if (Empire == null)
			gameTabControl1.Visible = false;
		else
		{
			gameTabControl1.Visible = true;
			picPortrait.Image = Empire.Portrait;
			picInsignia.Image = Empire.Icon;
			txtName.Text = Empire.Name;
			txtLeader.Text = Empire.LeaderName;
			txtPortrait.Text = Empire.LeaderPortraitName;
			txtShipset.Text = Empire.ShipsetPath;
			txtCulture.Text = Empire.Culture.Name;
			// TODO - race report
			// TODO - known technology
		}
		ResumeLayout();
	}
}