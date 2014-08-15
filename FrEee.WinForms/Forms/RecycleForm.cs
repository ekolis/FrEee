using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
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
	/// <summary>
	/// Form where the player can choose recycling actions such as scrapping and mothballing.
	/// </summary>
	public partial class RecycleForm : Form
	{
		public RecycleForm(Sector sector)
		{
			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }

			InitializeComponent();

			Sector = sector;

			Bind();
		}

		/// <summary>
		/// Binds the controls on the form.
		/// </summary>
		private void Bind()
		{
			treeVehicles.Initialize(32);
			foreach (var p in Sector.SpaceObjects.OfType<Planet>().Where(p => p.Owner == Empire.Current))
			{
				// planets with our colonies
				var pnode = treeVehicles.AddItemWithImage(p.Name, p, p.Icon);
				foreach (var ft in p.Colony.Facilities.GroupBy(f => f.Template))
				{
					// facility templates
					var ftnode = pnode.AddItemWithImage(ft.Count() + "x " + ft.Key.Name, ft.Key, ft.Key.Icon);
					foreach (var f in ft)
					{
						// facilities
						var fnode = ftnode.AddItemWithImage(f.Name, f, f.Icon);
						// TODO - show orders to scrap/etc. facilities
					}
				}
				
				// cargo of planets
				BindUnitsIn(p, pnode);
			}
			foreach (var v in Sector.SpaceObjects.OfType<SpaceVehicle>().Where(v => v.Owner == Empire.Current))
			{
				// our space vehicles
				var vnode = treeVehicles.AddItemWithImage(v.Name, v, v.Icon);
				BindUnitsIn(v, vnode);
				// TODO - show orders to scrap/etc. space vehicles
			}
		}

		/// <summary>
		/// Binds the tree nodes for a planet's cargo or a ship's cargo.
		/// </summary>
		/// <param name="cc"></param>
		/// <param name="ccnode"></param>
		private void BindUnitsIn(ICargoContainer cc, TreeNode ccnode)
		{
			foreach (var ur in cc.Cargo.Units.GroupBy(u => u.Design.Role))
			{
				// unit roles
				var urnode = ccnode.AddItemWithImage(ur.Count() + "x " + ur.Key, ur.Key, ur.Majority(u => u.Icon));
				foreach (var ud in ur.GroupBy(u => u.Design))
				{
					// unit designs
					var udnode = urnode.AddItemWithImage(ud.Count() + "x " + ud.Key.Name, ud.Key, ud.Key.Icon);
					foreach (var u in ud)
					{
						// units
						udnode.AddItemWithImage(u.Name, u, u.Icon);
						// TODO - show orders to scrap/etc. units
					}
				}
			}
		}

		/// <summary>
		/// The sector in which we are recycling.
		/// </summary>
		public Sector Sector { get; private set; }
	}
}
