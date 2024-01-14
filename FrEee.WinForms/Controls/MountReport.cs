using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FrEee.WinForms.Controls;

public partial class MountReport : UserControl, IBindable<Mount>
{
	public MountReport()
	{
		InitializeComponent();
	}

	public MountReport(Mount mount)
	{
		InitializeComponent();
		Bind(mount);
	}

	public Mount Mount
	{
		get
		{
			return mount;
		}
		set
		{
			mount = value;
			Bind();
		}
	}

	private Mount mount;

	public void Bind(Mount data)
	{
		Mount = data;
		Bind();
	}

	public void Bind()
	{
		SuspendLayout();
		if (Mount == null)
			Visible = false;
		else
		{
			Visible = true;
			picPortrait.Image = Mount.Portrait;

			// basic info
			txtName.Text = Mount.Code + ": " + Mount.Name;
			txtDescription.Text = Mount.Description;

			// requirements
			txtVehicleTypes.Text = Mount.VehicleTypes.ToString();
			if (Mount.MinimumVehicleSize != null && Mount.MaximumVehicleSize != null)
				txtVehicleSizes.Text = Mount.MinimumVehicleSize.Value.Kilotons() + " - " + Mount.MaximumVehicleSize.Value.Kilotons();
			else if (Mount.MinimumVehicleSize != null)
				txtVehicleSizes.Text = "At least " + Mount.MinimumVehicleSize.Value.Kilotons();
			else if (Mount.MaximumVehicleSize != null)
				txtVehicleSizes.Text = "Up to " + Mount.MaximumVehicleSize.Value.Kilotons();
			else
				txtVehicleSizes.Text = "Any";
			if (Mount.RequiredComponentFamily == null)
				txtComponentFamily.Text = "Any";
			else
			{
				txtComponentFamily.Text = Mount.RequiredComponentFamily.Value;
				var comp = Mod.Current.ComponentTemplates.Where(ct => Mount.RequiredComponentFamily.Value.Split(',').Select(q => q.Trim()).Contains(ct.Family.Value)).FirstOrDefault();
				if (comp != null)
					txtComponentFamily.Text += " (" + comp + ", etc.)";
			}

			// basic modifiers
			Color red = Color.FromArgb(255, 128, 128);
			Color green = Color.FromArgb(128, 255, 128);
			Color white = Color.White;
			var cost = Mount.CostPercent.Value;
			txtCost.Text = cost + "%";
			if (cost > 100)
				txtCost.ForeColor = red;
			else if (cost < 100)
				txtCost.ForeColor = green;
			else
				txtCost.ForeColor = white;
			var size = Mount.SizePercent.Value;
			txtSize.Text = size + "%";
			if (size > 100)
				txtSize.ForeColor = red;
			else if (size < 100)
				txtSize.ForeColor = green;
			else
				txtSize.ForeColor = white;
			var hp = Mount.DurabilityPercent.Value;
			txtDurability.Text = hp + "%";
			if (hp < 100)
				txtDurability.ForeColor = red;
			else if (hp > 100)
				txtDurability.ForeColor = green;
			else
				txtDurability.ForeColor = white;
			var dmg = Mount.WeaponDamagePercent.Value;
			txtDamage.Text = dmg + "%";
			if (dmg < 100)
				txtDamage.ForeColor = red;
			else if (dmg > 100)
				txtDamage.ForeColor = green;
			else
				txtDamage.ForeColor = white;
			var range = Mount.WeaponRangeModifier.Value;
			txtRange.Text = range > 0 ? ("+" + range) : range.ToString();
			if (range < 0)
				txtRange.ForeColor = red;
			else if (range > 0)
				txtRange.ForeColor = green;
			else
				txtRange.ForeColor = white;
			var accy = Mount.WeaponAccuracyModifier.Value;
			txtAccuracy.Text = (accy > 0 ? ("+" + accy) : accy.ToString()) + "%";
			if (accy < 0)
				txtAccuracy.ForeColor = red;
			else if (accy > 0)
				txtAccuracy.ForeColor = green;
			else
				txtAccuracy.ForeColor = white;
			var supply = Mount.SupplyUsagePercent.Value;
			txtSupply.Text = supply + "%";
			if (supply > 100)
				txtSupply.ForeColor = red;
			else if (supply < 100)
				txtSupply.ForeColor = green;
			else
				txtSupply.ForeColor = white;
		}

		// ability modifiers
		// TODO - settings in AbilityRules.txt saying whether higher values are good or bad, so we can color the modifiers appropriately
		var abils = new SafeDictionary<Tuple<string, int>, string>();
		foreach (var kvp in Mount.AbilityPercentages)
		{
			var pcts = kvp.Value;
			foreach (var kvp2 in pcts)
			{
				var abilAndValNum = Tuple.Create(kvp.Key.Name, kvp2.Key);
				var modifier = kvp2.Value.Value.ToString() + "%";
				abils.Add(abilAndValNum, modifier);
			}
		}
		foreach (var kvp in Mount.AbilityModifiers)
		{
			var mods = kvp.Value;
			foreach (var kvp2 in mods)
			{
				var abilAndValNum = Tuple.Create(kvp.Key.Name, kvp2.Key);
				var modifier = kvp2.Value.Value.ToString("+0");
				if (abils.ContainsKey(abilAndValNum))
					abils[abilAndValNum] += ", " + modifier;
				else
					abils[abilAndValNum] = modifier;
			}
		}
		lstAbilities.Initialize(32, 32);
		foreach (var kvp in abils)
			lstAbilities.AddItemWithImage(null, kvp.Key.Item1 + " value " + kvp.Key.Item2, kvp, null, null, kvp.Value);
		ResumeLayout();
	}

	private void picPortrait_Click(object sender, EventArgs e)
	{
		if (Mount != null)
			picPortrait.ShowFullSize(Mount.Name);
	}
}