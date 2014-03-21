using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Technology;
using FrEee.Modding;
using FrEee.Modding.Templates;
using FrEee.WinForms.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
	public partial class TechTreeForm : Form, IBindable<IUnlockable>
	{
		public TechTreeForm()
		{
			InitializeComponent();
			BindTypes();
			BindItems();
			Bind();
		}

		/// <summary>
		/// The current contextual item.
		/// </summary>
		public IUnlockable Context { get; private set; }

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
					lblInfo.Text = trait.Description;
					lblInfo.Dock = DockStyle.Fill;
					pnlDetails.Controls.Add(lblInfo);

					// display what's unlocked
					// note: this won't catch scripted unlocks!
					var unlocks = AllItems.Where(u => u.UnlockRequirements.OfType<EmpireTraitRequirement>().Any(r => r.RequiredOrForbidden && r.Trait == trait));
					foreach (var unlock in unlocks)
						lstUnlocks.AddItemWithImage(unlock.ResearchGroup, unlock.Name, unlock, unlock.Icon);
					// HACK - racial tech requirements are special
					if (trait.GetAbilityValue("Tech Area") != null)
					{
						foreach (var tech in Mod.Current.Technologies.Where(t => t.RacialTechID == trait.GetAbilityValue("Tech Area")))
							lstUnlocks.AddItemWithImage(tech.ResearchGroup, tech.Name, tech, tech.Icon);
					}
				}
				else if (Context is Technology)
				{
					// display technology
				}
				else if (Context is IHull)
				{
					// display hull
				}
				else if (Context is ComponentTemplate)
				{
					// display component
				}
				else if (Context is Mount)
				{
					// display mount
				}
				else if (Context is FacilityTemplate)
				{
					// display facility
				}
			}
		}

		private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindItems();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			var item = (IUnlockable)ddlItems.SelectedItem;
			Bind(item);
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

		private void BindItems()
		{
			dynamic sel = ddlType.SelectedItem;
			ddlItems.Items.Clear();
			if (sel == null)
				return;
			foreach (var item in AllItems.Where(r => ((Type)sel.Type).IsAssignableFrom(r.GetType())))
				ddlItems.Items.Add(item);
		}

		private void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			Bind((IUnlockable)ddlItems.SelectedItem);
		}

		private IEnumerable<IUnlockable> AllItems
		{
			get
			{
				return
					Mod.Current.Traits.Cast<IUnlockable>().Union(
					Mod.Current.Technologies.Cast<IUnlockable>()).Union(
					Mod.Current.Hulls.Cast<IUnlockable>()).Union(
					Mod.Current.ComponentTemplates.Cast<IUnlockable>()).Union(
					Mod.Current.Mounts.Cast<IUnlockable>()).Union(
					Mod.Current.FacilityTemplates.Cast<IUnlockable>());
			}
		}

		private void lstRequired_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstRequired.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				var u = (IUnlockable)item.Tag;
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

		private void btlReset_Click(object sender, EventArgs e)
		{
			Bind(null);
		}
	}
}
