using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game.Objects.Abilities;

namespace FrEee.WinForms.Controls
{
	public partial class AbilityTreeView : UserControl
	{
		public AbilityTreeView()
		{
			InitializeComponent();
		}

		private ILookup<Ability, Ability> abilities;
		public ILookup<Ability, Ability> Abilities
		{
			get { return abilities; }
			set { abilities = value; Bind(); }
		}

		private IEnumerable<Ability> intrinsicAbilities;
		public IEnumerable<Ability> IntrinsicAbilities
		{
			get { return intrinsicAbilities; }
			set { intrinsicAbilities = value; Bind(); }
		}

		public void Bind()
		{
			treeAbilities.Nodes.Clear();
			if (Abilities != null)
			{
				foreach (var group in Abilities)
				{
					var branch = new TreeNode(group.Key.ToString());
					if (group.Any(abil => IntrinsicAbilities == null || !IntrinsicAbilities.Contains(abil)))
						branch.NodeFont = new Font(Font, FontStyle.Italic);
					treeAbilities.Nodes.Add(branch);
					if (group.Count() > 1)
					{
						foreach (var sub in group)
						{
							var twig = new TreeNode(sub.Description);
							if (IntrinsicAbilities == null || !IntrinsicAbilities.Contains(sub))
								twig.NodeFont = new Font(Font, FontStyle.Italic);
							branch.Nodes.Add(twig);
						}
					}
				}
			}
		}
	}
}
