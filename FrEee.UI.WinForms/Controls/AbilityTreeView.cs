using FrEee.Objects.Abilities;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.UI.WinForms.Controls;

public partial class AbilityTreeView : UserControl
{
	public AbilityTreeView()
	{
		InitializeComponent();
	}

	public ILookup<Ability, Ability> Abilities
	{
		get { return abilities; }
		set { abilities = value; Bind(); }
	}

	public IEnumerable<Ability> IntrinsicAbilities
	{
		get { return intrinsicAbilities; }
		set { intrinsicAbilities = value; Bind(); }
	}

	private ILookup<Ability, Ability> abilities;

	private IEnumerable<Ability> intrinsicAbilities;

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

	private void treeAbilities_BeforeSelect(object sender, TreeViewCancelEventArgs e)
	{
		// don't allow selection
		e.Cancel = true;
	}
}