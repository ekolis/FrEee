using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.WinForms.Interfaces;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using Component = FrEee.Game.Objects.Technology.Component;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.WinForms.Controls
{
	public partial class ComponentReport : UserControl, IBindable<Component>, IBindable<MountedComponentTemplate>, IBindable<ComponentTemplate>
	{
		public ComponentReport()
		{
			InitializeComponent();
		}

		public ComponentReport(Component comp)
		{
			InitializeComponent();
			Bind(comp);
		}

		public ComponentReport(MountedComponentTemplate mct)
		{
			InitializeComponent();
			Bind(mct);
		}

		public ComponentReport(ComponentTemplate ct)
		{
			InitializeComponent();
			Bind(ct);
		}

		private Component component;

		public Component Component
		{
			get
			{
				return component;
			}
			set
			{
				component = value;
				Bind();
			}
		}

		public void Bind(Component data)
		{
			Component = data;
			Bind();
		}

		public void Bind()
		{
			if (Component == null)
				Visible = false;
			else
			{
				Visible = true;
				picPortrait.Image = Component.Portrait;
				txtName.Text = Component.Name;
				txtDescription.Text = Component.Template.ComponentTemplate.Description;
				resMin.Amount = Component.Template.Cost[Resource.Minerals];
				resOrg.Amount = Component.Template.Cost[Resource.Organics];
				resRad.Amount = Component.Template.Cost[Resource.Radioactives];
				txtSize.Text = Component.Template.Size.Kilotons();
				if (Component.Hitpoints < Component.MaxHitpoints)
					txtDurability.Text = Component.Hitpoints + " / " + Component.MaxHitpoints + " HP";
				else
					txtDurability.Text = Component.MaxHitpoints + " HP";
				txtSupplyUsage.Text = Component.Template.SupplyUsage + " supplies";
				txtVehicleTypes.Text = Component.Template.ComponentTemplate.VehicleTypes.ToString();
				txtWeaponType.Text = Component.Template.ComponentTemplate.WeaponType.ToSpacedString();
				var weapon = Component.Template.ComponentTemplate.WeaponInfo;
				if (Component.Template.ComponentTemplate.WeaponInfo != null)
				{
					ShowRow(0);
					ShowRow(3);
					txtWeaponType.Text = Component.Template.ComponentTemplate.WeaponType.ToSpacedString();
					txtTargets.Text = weapon.Targets.ToSpacedString();
					txtReload.Text = weapon.ReloadRate.ToString() + " rounds";
					txtDamageType.Text = weapon.DamageType.Name;
					if (weapon.WeaponType.IsDirectFire())
					{
						ShowRow(1);
						txtAccuracy.Text = Component.Template.WeaponAccuracy.ToString("0:+#;-#;0") + "%"; // show plus sign on positive numbers
					}
					else
						HideRow(1);
					if (weapon.WeaponType.IsSeeking())
					{
						ShowRow(2);
						var swpn = (SeekingWeaponInfo)weapon;
						txtSeekerSpeed.Text = swpn.SeekerSpeed + " squares / round";
						txtSeekerDurability.Text = swpn.SeekerDurability + " HP";
					}
					else
						HideRow(2);
					int? maxRange = 0;
					var dmg = Component.Template.WeaponDamage;
					if (dmg[0] > 0)
						maxRange = null; // infinite range
					else
					{
						for (int r = 1; r < dmg.Count(); r++)
						{
							if (dmg[r] > 0)
								maxRange = r;
						}
					}
					damageGraph.Title = "Max Range: " + (maxRange == null ? "Infinite" : maxRange.ToString());
					damageGraph.DataPoints = dmg.Skip(1).Take(1).Concat(dmg.Skip(1)).Concat(dmg.Take(1)).Select(i => (double)i); // put infinite range value at index 21; put range 1 value at range 0
				}
				else
				{
					HideRow(0);
					HideRow(1);
					HideRow(2);
					HideRow(3);
				}
				abilityTree.IntrinsicAbilities = Component.Abilities;
				abilityTree.Abilities = Component.Abilities.StackToTree(Component);
			}
		}

		public void Bind(MountedComponentTemplate data)
		{
			Bind(new Component(data));
		}

		public void Bind(ComponentTemplate data)
		{
			Bind(new Component(new MountedComponentTemplate(data, null)));
		}

		private void picPortrait_Click(object sender, EventArgs e)
		{
			if (Component != null)
				picPortrait.ShowFullSize(Component.Name);
		}

		private void HideRow(int n)
		{
			var r = table.RowStyles[n];
			r.SizeType = SizeType.Absolute;
			r.Height = 0;
		}

		private void ShowRow(int n)
		{
			var r = table.RowStyles[n];
			r.SizeType = SizeType.AutoSize;
		}
	}
}
