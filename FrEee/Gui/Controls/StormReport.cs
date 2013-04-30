using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game;

namespace FrEee.Gui.Controls
{
	public partial class StormReport : UserControl
	{
		public StormReport()
		{
			InitializeComponent();
		}

		public StormReport(Storm storm)
			: this()
		{
			Storm = storm;
		}

		private Storm storm;

		/// <summary>
		/// The storm for which to display a report.
		/// </summary>
		public Storm Storm
		{
			get { return storm; }
			set
			{
				storm = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Storm == null)
				Visible = false;
			else
			{
				Visible = true;

				picOwnerFlag.Image = null; // TODO - load owner flag
				picPortrait.Image = Storm.Portrait;

				txtName.Text = Storm.Name;
				txtSize.Text = Storm.Size + " Storm";
				txtDescription.Text = Storm.Description;

				treeAbilities.Nodes.Clear();
				foreach (var group in Storm.Abilities.GroupBy(abil => abil.Name))
				{
					// TODO - deal with nonstacking abilities
					var branch = new TreeNode(group.Key + ": " + group.Sum(abil =>
						{
							double result = 0;
							double.TryParse(abil.Values.FirstOrDefault(), out result);
							return result;
						}));
					if (group.Any(abil => !Storm.IntrinsicAbilities.Contains(abil)))
						branch.NodeFont = new Font(Font, FontStyle.Italic);
					treeAbilities.Nodes.Add(branch);
					foreach (var abil in group)
					{
						var twig = new TreeNode(abil.Description);
						if (Storm.IntrinsicAbilities.Contains(abil))
							twig.NodeFont = new Font(Font, FontStyle.Italic);
						branch.Nodes.Add(twig);
					}
				}
			}

			base.OnPaint(e);
		}
	}
}
