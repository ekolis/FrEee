using FrEee.Game.Objects.Combat;
using FrEee.Game.Objects.Technology;
using FrEee.Modding.Templates;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Component = FrEee.Game.Objects.Technology.Component;

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

		private Component component;

		public void Bind(Component data)
		{
			Component = data;
			Bind();
		}

		public void Bind()
		{
			SuspendLayout();
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
					txtReload.Text = weapon.ReloadRate.ToString() + " seconds";
					txtDamageType.Text = weapon.DamageType.Name;
					if (weapon.WeaponType.IsDirectFire())
					{
						ShowRow(1);
						txtAccuracy.Text = Component.Template.WeaponAccuracy.ToString("+#;-#;0") + "%"; // show plus sign on positive numbers
						if (weapon.DisplayEffect is BeamWeaponDisplayEffect)
							txtWeaponType.Text += " (Beam)";
						else if (weapon.DisplayEffect is ProjectileWeaponDisplayEffect)
							txtWeaponType.Text += " (Projectile)";
					}
					else
						HideRow(1);
					if (weapon.WeaponType.IsSeeking())
					{
						ShowRow(2);
						var swpn = (SeekingWeaponInfo)weapon;
						txtSeekerSpeed.Text = swpn.SeekerSpeed + " km/s²";
						txtSeekerDurability.Text = swpn.SeekerDurability + " HP";
					}
					else
						HideRow(2);
					var dmglist = new List<int>();
					for (int r = 0; r <= Math.Max(Component.Template.WeaponMaxRange, 20); r++)
					{
						var shot = new Shot(null, component, null, r);
						dmglist.Add(shot.FullDamage);
					}
					damageGraph.Title = "Range: " + Component.Template.WeaponMinRange + " - " + Component.Template.WeaponMaxRange;
					damageGraph.Series.Add(new LineGraph.GraphSeries
					{
						Name = "",
						Color = Color.White,
						DataPoints = dmglist.Select(d => (double)d)
					});
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
			ResumeLayout();
		}

		public void Bind(MountedComponentTemplate data)
		{
			Bind(new Component(null, data));
		}

		public void Bind(ComponentTemplate data)
		{
			Bind(new Component(null, new MountedComponentTemplate(null, data, null)));
		}

		private void HideRow(int n)
		{
			var r = table.RowStyles[n];
			r.SizeType = SizeType.Absolute;
			r.Height = 0;
		}

		private void picPortrait_Click(object sender, EventArgs e)
		{
			if (Component != null)
				picPortrait.ShowFullSize(Component.Name);
		}

		private void ShowRow(int n)
		{
			var r = table.RowStyles[n];
			r.SizeType = SizeType.AutoSize;
		}
	}
}