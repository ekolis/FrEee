using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.Extensions;
using FrEee.UI.WinForms.Controls;
using FrEee.UI.WinForms.Interfaces;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.GameState;
using FrEee.Extensions;

namespace FrEee.UI.WinForms.Forms;

public partial class TechTreeForm : GameForm, IBindable<IUnlockable>
{
	public TechTreeForm()
	{
		InitializeComponent();
		BindTypes();
		BindItems();
		Bind();

		try { this.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); }
		catch { }
	}

	public TechTreeForm(IUnlockable u)
	{
		InitializeComponent();
		BindTypes();
		BindItems();
		Bind(u);
	}

	/// <summary>
	/// The current contextual item.
	/// </summary>
	public IUnlockable Context { get; private set; }

	private IEnumerable<IUnlockable> AllItems
	{
		get
		{
			return
				Mod.Current.Traits.Cast<IUnlockable>().Concat(
				Mod.Current.Technologies.Cast<IUnlockable>()).Concat(
				Mod.Current.Hulls.Cast<IUnlockable>()).Concat(
				Mod.Current.ComponentTemplates.Cast<IUnlockable>()).Concat(
				Mod.Current.Mounts.Cast<IUnlockable>()).Concat(
				Mod.Current.FacilityTemplates.Cast<IUnlockable>());
		}
	}

	public void Bind(IUnlockable data)
	{
		Context = data;
		Bind();
	}

	public void Bind()
	{
		pnlDetails.Controls.Clear();
		lstRequired.Initialize(16, 16);
		lstUnlocks.Initialize(32, 32);

		Cursor = Cursors.WaitCursor;

		if (Context == null)
		{
			// display all items with no strict prereqs
			lblPrereqs.Text = "Root Items";

			var items = AllItems.Where(i => !i.UnlockRequirements.Any(r => r.IsStrict));
			foreach (var item in items)
				lstRequired.AddItemWithImage(item.ResearchGroup, item.Name, item, item.Icon);
		}
		else
		{
			lblPrereqs.Text = "Prerequisites";

			if (Context is Trait)
			{
				// display trait
				var trait = (Trait)Context;
				lblDetailsHeader.Text = trait.Name;
				var lblInfo = new Label();
				lblInfo.Text = "Cost: " + trait.Cost.Value.ToUnitString(true) + " racial points\n" + trait.Description;
				lblInfo.Dock = DockStyle.Fill;
				pnlDetails.Controls.Add(lblInfo);

				// display what's unlocked
				// note: this won't catch scripted unlocks!
				var unlocks = AllItems.Where(u => u.UnlockRequirements.OfType<EmpireTraitRequirement>().Any(r => r.RequiredOrForbidden && r.Trait == trait));
				foreach (var unlock in unlocks.OrderBy(u => u.UnlockRequirements.Count()))
					lstUnlocks.AddItemWithImage(unlock.ResearchGroup, unlock.Name, unlock, unlock.Icon);
				// HACK - racial tech requirements are special
				foreach (var a in trait.Abilities.Where(a => a.Rule.Name == "Tech Area"))
				{
					foreach (var tech in Mod.Current.Technologies.Where(t => t.RacialTechID == a.Value1))
					{
						if (tech.UnlockRequirements.Count() == 1)
						{
							var req = tech.UnlockRequirements.Single();
							if (req is TechnologyRequirement)
							{
								var tr = (TechnologyRequirement)req;
								lstUnlocks.AddItemWithImage(tech.ResearchGroup, tech.Name + " (with " + tr.Technology + " L" + tr.Level + ")", tech, tech.Icon);
							}
							else if (req is EmpireTraitRequirement)
							{
								var tr = (EmpireTraitRequirement)req;
								lstUnlocks.AddItemWithImage(tech.ResearchGroup, tech.Name + " (with " + tr.Trait + ")", tech, tech.Icon);
							}
							else
								lstUnlocks.AddItemWithImage(tech.ResearchGroup, tech.Name + " (with 1 other requirement)", tech, tech.Icon);
						}
						else if (tech.UnlockRequirements.Count() > 1)
							lstUnlocks.AddItemWithImage(tech.ResearchGroup, tech.Name + " (with " + tech.UnlockRequirements.Count() + " other requirements)", tech, tech.Icon);
						else // no other prereqs
							lstUnlocks.AddItemWithImage(tech.ResearchGroup, tech.Name, tech, tech.Icon);
					}
				}
			}
			else
			{
				foreach (var req in Context.UnlockRequirements)
				{
					if (req is TechnologyRequirement)
					{
						var tr = (TechnologyRequirement)req;
						lstRequired.AddItemWithImage(tr.Technology.ResearchGroup, tr.Technology.Name + " L" + tr.Level, tr.Technology, tr.Technology.Icon);
					}
					else if (req is EmpireTraitRequirement)
					{
						var tr = (EmpireTraitRequirement)req;
						lstRequired.AddItemWithImage(tr.Trait.ResearchGroup, tr.Trait.Name, tr.Trait, tr.Trait.Icon);
					}
					else
						lstRequired.AddItemWithImage("Miscellaneous", req.Description, req, null);
				}

				if (Context is Technology)
				{
					// display technology
					var tech = (Technology)Context;
					lblDetailsHeader.Text = tech.Name;
					var lblInfo = new Label();
					lblInfo.Text = "First Level: " + tech.LevelCost.ToUnitString(true) + " research points\n" + tech.Description;
					lblInfo.Dock = DockStyle.Fill;
					pnlDetails.Controls.Add(lblInfo);

					// display racial trait prereqs
					if (tech.RacialTechID != null && tech.RacialTechID.Trim() != "0")
					{
						var traits = Mod.Current.Traits.Where(t => t.Abilities.Any(a => a.Rule != null && a.Rule.Name == "Tech Area" && a.Value1 == tech.RacialTechID));
						if (traits.Count() == 1)
						{
							var trait = traits.Single();
							lstRequired.AddItemWithImage(trait.ResearchGroup, trait.Name, trait, trait.Icon);
						}
						else if (traits.Count() > 1)
							lstRequired.AddItemWithImage(traits.First().ResearchGroup, string.Join(" or ", traits.Select(t => t.Name).ToArray()), traits, null);
					}

					// display what's unlocked
					// note: this won't catch scripted unlocks!
					var unlocks = AllItems.Select(u =>
					{
						var req = u.UnlockRequirements.OfType<TechnologyRequirement>().Where(r => r.Technology == tech).WithMax(r => r.Level).FirstOrDefault();
						if (req != null)
						{
							IEnumerable<Trait> traits;
							if (u is Technology)
								traits = Mod.Current.Traits.Where(t => t.Abilities.Any(a => a.Rule != null && a.Rule.Name == "Tech Area" && a.Value1 == tech.RacialTechID));
							else
								traits = Enumerable.Empty<Trait>();
							return new
							{
								Item = u,
								Level = req.Level,
								Others = u.UnlockRequirements.ExceptSingle(req),
								Traits = traits,
							};
						}
						else
							return null;
					}).Where(u => u != null);
					foreach (var unlock in unlocks.OrderBy(u => u.Level).ThenBy(u => u.Others.Count()))
					{
						string suffix;
						if (unlock.Others.Count() == 0)
						{
							if (unlock.Traits.Count() == 0)
								suffix = "";
							else
								suffix = " (with " + string.Join(" or ", unlock.Traits.Select(t => t.Name).ToArray());
						}
						else if (unlock.Others.Count() == 1 && unlock.Others.Single() is TechnologyRequirement)
						{
							var other = (TechnologyRequirement)unlock.Others.Single();
							if (unlock.Traits.Count() == 0)
								suffix = " (with " + other.Technology + " L" + other.Level + ")";
							else
								suffix = " (with " + string.Join(" or ", unlock.Traits.Select(t => t.Name).ToArray() + ", and " + other.Technology + " L" + other.Level);
						}
						else if (unlock.Others.Count() == 1 && unlock.Others.Single() is EmpireTraitRequirement)
						{
							var other = (EmpireTraitRequirement)unlock.Others.Single();
							if (unlock.Traits.Count() == 0)
								suffix = " (with " + other.Trait + ")";
							else
								suffix = " (with " + other.Trait + ", and " + unlock.Traits.Select(t => t.Name).ToArray();
						}
						else
							suffix = " (with " + unlock.Others.Count() + " other requirements";
						lstUnlocks.AddItemWithImage("L" + unlock.Level, unlock.Item.Name + suffix, unlock.Item, unlock.Item.Icon);
					}
				}
				else if (Context is IHull)
				{
					// display hull
					var hull = (IHull)Context;
					lblDetailsHeader.Text = hull.Name;
					// TODO - hull report
					var lblInfo = new Label();
					lblInfo.Text = hull.Size.Kilotons() + " " + hull.VehicleTypeName + " hull\n" + hull.Description;
					lblInfo.Dock = DockStyle.Fill;
					pnlDetails.Controls.Add(lblInfo);
				}
				else if (Context is ComponentTemplate)
				{
					// display component
					var ct = (ComponentTemplate)Context;
					lblDetailsHeader.Text = ct.Name;
					var report = new ComponentReport(ct);
					report.Dock = DockStyle.Fill;
					pnlDetails.Controls.Add(report);
				}
				else if (Context is Mount)
				{
					// display mount
					var m = (Mount)Context;
					lblDetailsHeader.Text = m.Name;
					var report = new MountReport(m);
					report.Dock = DockStyle.Fill;
					pnlDetails.Controls.Add(report);
				}
				else if (Context is FacilityTemplate)
				{
					// display facility
					var ft = (FacilityTemplate)Context;
					lblDetailsHeader.Text = ft.Name;
					var report = new FacilityReport(ft);
					report.Dock = DockStyle.Fill;
					pnlDetails.Controls.Add(report);
				}
			}
		}

		Cursor = Cursors.Default;
	}

	private void BindItems()
	{
		dynamic sel = ddlType.SelectedItem;
		ddlItems.Items.Clear();
		if (sel == null)
			return;
		foreach (var item in AllItems.Where(r => ((Type)sel.Type).IsAssignableFrom(r.GetType())))
			ddlItems.Items.Add(item);
	}

	private void BindTypes()
	{
		ddlType.Items.Clear();
		ddlType.Items.Add(new { Name = "Traits", Type = typeof(Trait) });
		ddlType.Items.Add(new { Name = "Technologies", Type = typeof(Technology) });
		ddlType.Items.Add(new { Name = "Hulls", Type = typeof(IHull) });
		ddlType.Items.Add(new { Name = "Components", Type = typeof(ComponentTemplate) });
		ddlType.Items.Add(new { Name = "Mounts", Type = typeof(Mount) });
		ddlType.Items.Add(new { Name = "Facilities", Type = typeof(FacilityTemplate) });
	}

	private void btlReset_Click(object sender, EventArgs e)
	{
		Bind(null);
	}

	private void btnGo_Click(object sender, EventArgs e)
	{
		var item = (IUnlockable)ddlItems.SelectedItem;
		Bind(item);
	}

	private void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
	{
		Bind((IUnlockable)ddlItems.SelectedItem);
	}

	private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindItems();
	}

	private void lstRequired_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		var item = lstRequired.GetItemAt(e.X, e.Y);
		if (item != null)
		{
			var u = (IUnlockable)item.Tag;
			if (u != null)
				Bind(u);
		}
	}

	private void lstUnlocks_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		var item = lstUnlocks.GetItemAt(e.X, e.Y);
		if (item != null)
		{
			var u = (IUnlockable)item.Tag;
			Bind(u);
		}
	}
}