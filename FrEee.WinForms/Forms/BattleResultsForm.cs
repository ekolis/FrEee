using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Combat2;
using FrEee.WinForms.DataGridView;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.MogreCombatRender;
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
	public partial class BattleResultsForm : Form, IBindable<Battle_Space>
	{
		public BattleResultsForm(Battle_Space battle)
		{
			InitializeComponent();

			Bind(battle);
		}

		/// <summary>
		/// The battle we are displaying results for.
		/// TODO - create an IBattle interface so we can have pluggable combat systems
		/// </summary>
		public Battle_Space Battle { get; private set; }

		public void Bind(Battle_Space data)
		{
			Battle = data;
			Bind();
		}

		public void Bind()
		{
			// create grid config
			var cfg = new GridConfig();
			cfg.Name = "Default";
			cfg.Columns.Add(new GridColumnConfig("EmpireName", "Empire", typeof(string), Color.White, Format.Raw, Sort.Ascending, 1));
			cfg.Columns.Add(new GridColumnConfig("HullName", "Hull", typeof(string), Color.White, Format.Raw));
			cfg.Columns.Add(new GridColumnConfig("HullSize", "Size", typeof(int), Color.White, Format.Kilotons, Sort.Descending, 2));
			cfg.Columns.Add(new GridColumnConfig("StartCount", "Start #", typeof(int), Color.White, Format.UnitsBForBillions));
			cfg.Columns.Add(new GridColumnConfig("StartHP", "Start HP", typeof(int), Color.White, Format.UnitsBForBillions));
			cfg.Columns.Add(new GridColumnConfig("Losses", "Losses", typeof(int), Color.White, Format.UnitsBForBillions));
			cfg.Columns.Add(new GridColumnConfig("Damage", "Damage", typeof(int), Color.White, Format.UnitsBForBillions));
			grid.CurrentGridConfig = cfg;

			// gather grid data
			var data = new List<object>();
			foreach (var group in Battle.StartCombatants.GroupBy(c => new { Empire = c.Owner, HullName = GetHullName(c), HullSize = GetHullSize(c) }))
			{
				var count = group.Count();
				var hp = group.Sum(c => c.ArmorHitpoints + c.HullHitpoints);
				var item = new
				{
					EmpireName = group.Key.Empire.Name,
					HullName = group.Key.HullName,
					HullSize = group.Key.HullSize,
					StartCount = count,
					StartHP = hp,
					Losses = group.Count(c => Battle.FindActualCombatant(c).IsDestroyed),
					Damage = hp - group.Sum(c =>
						{
							var ac = (ICombatant)Battle.FindActualCombatant(c);
							return ac.ArmorHitpoints + ac.HullHitpoints;
						})
				};
				data.Add(item);
			}
			grid.Data = data;
		}

		/// <summary>
		/// Gets the hull name of a vehicle/design, or the type name if it's not a vehicle/design (e.g. if it's a planet).
		/// I guess you could confuse people by naming your planet "Destroyer" or something...
		/// </summary>
		/// <param name="obj"></param>
		private string GetHullName(IOwnable obj)
		{
			if (obj is IVehicle)
				return ((IVehicle)obj).Design.Hull.Name;
			if (obj is IDesign)
				return ((IDesign)obj).Hull.Name;
			return obj.GetType().Name;
		}

		/// <summary>
		/// Gets the hull size of a vehicle/design, or int.MaxValue if it's not a vehicle/design (e.g. if it's a planet).
		/// </summary>
		/// <param name="obj"></param>
		private int GetHullSize(IOwnable obj)
		{
			if (obj is IVehicle)
				return ((IVehicle)obj).Design.Hull.Size;
			if (obj is IDesign)
				return ((IDesign)obj).Hull.Size;
			return int.MaxValue;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnReplay_Click(object sender, EventArgs e)
		{
			MogreFreeMain replay = new MogreFreeMain(Battle);
		}
	}
}
