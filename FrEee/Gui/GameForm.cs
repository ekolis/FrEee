using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrEee.Gui.Controls;

namespace FrEee
{
	public partial class GameForm : Form
	{
		public GameForm()
		{
			InitializeComponent();
			var pnlResources = new FlowLayoutPanel();
			pnlResources.FlowDirection = FlowDirection.LeftToRight;
			pnlResources.WrapContents = false;
			pnlResources.Controls.Add(new GameResourceDisplay { ResourceColor = Color.Blue, Amount = 500000, Change = -25000});
			pnlResources.Controls.Add(new GameResourceDisplay { ResourceColor = Color.Green, Amount = 250000, Change = +10000});
			pnlResources.Controls.Add(new GameResourceDisplay { ResourceColor = Color.Red, Amount = 0, Change = -5000});
			var pnlResIntel = new FlowLayoutPanel();
			pnlResIntel.FlowDirection = FlowDirection.LeftToRight;
			pnlResIntel.WrapContents = false;
			pnlResIntel.Controls.Add(new GameResourceDisplay { ResourceColor = Color.Magenta, Amount = 50000 });
			pnlResIntel.Controls.Add(new GameResourceDisplay { ResourceColor = Color.White, Amount = 10000 });
			pagResources.Content = new List<Control>();
			pagResources.Content.Add(pnlResources);
			pagResources.Content.Add(pnlResIntel);
			pagResources.CurrentPage = 0;
		}
	}
}
