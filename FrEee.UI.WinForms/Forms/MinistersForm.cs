using FrEee.Gameplay.Commands.Messages;
using FrEee.Gameplay.Commands.Ministers;
using FrEee.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace FrEee.UI.WinForms.Forms;

public partial class MinistersForm : GameForm
{
	public MinistersForm()
	{
		InitializeComponent();
		try { base.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
	}

	private void MinistersForm_Load(object sender, EventArgs e)
	{
		foreach (var kvp in Empire.Current.AI.MinisterNames)
		{
			var groupname = kvp.Key;
			foreach (var minister in kvp.Value)
			{
				var index = lstMinisters.Items.Add($"{groupname}: {minister}");
				if (Empire.Current.EnabledMinisters != null && Empire.Current.EnabledMinisters[groupname] != null && Empire.Current.EnabledMinisters[groupname].Contains(minister))
					lstMinisters.SetItemChecked(index, true);
			}
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		var cmd = Empire.Current.Commands.OfType<IToggleMinistersCommand>().SingleOrDefault();
		if (cmd == null)
		{
			cmd = DIRoot.MinisterCommands.ToggleMinisters();
			Empire.Current.Commands.Add(cmd);
		}
		var dict = new SafeDictionary<string, ICollection<string>>();
		foreach (var item in lstMinisters.CheckedItems)
		{
			var spl = item.ToString().Split(':').Select(q => q.Trim());
			if (spl.Count() != 2)
				throw new Exception("AI minister tag should contain only one colon character. Does the minister category or minister name contain a colon? It shouldn't.");
			var key = spl.First();
			var val = spl.Last();
			if (dict[key] == null)
				dict[key] = new List<string>();
			dict[key].Add(val);
		}
		cmd.EnabledMinisters = dict;
		cmd.Execute();
	}

	private void btnEnableAll_Click(object sender, EventArgs e)
	{
		for (var i = 0; i < lstMinisters.Items.Count; i++)
			lstMinisters.SetItemChecked(i, true);
	}

	private void btnDisableAll_Click(object sender, EventArgs e)
	{
		for (var i = 0; i < lstMinisters.Items.Count; i++)
			lstMinisters.SetItemChecked(i, false);
	}
}