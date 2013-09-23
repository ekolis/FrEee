using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game.Objects;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility.Extensions;
using FrEee.Game.Interfaces;
using FrEee.WinForms.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Game.Objects.Vehicles;
using FrEee.Modding;


namespace FrEee.WinForms.Controls
{
	public partial class CargoList : UserControl, IBindable<ICargoContainer>, IBindable<CargoDelta>
	{
		public CargoList()
		{
			InitializeComponent();
		}

		public CargoList(ICargoContainer cargoContainer)
		{
			InitializeComponent();
			CargoContainer = cargoContainer;
		}

		public CargoList(CargoDelta cargoDelta)
		{
			InitializeComponent();
			CargoDelta = cargoDelta;
		}

		private ICargoContainer cargoContainer;
		private CargoDelta cargoDelta;

		/// <summary>
		/// The cargo container whose cargo to display.
		/// </summary>
		public ICargoContainer CargoContainer
		{
			get { return cargoContainer; }
			set
			{
				cargoContainer = value;
				cargoDelta = null;
				Bind();
			}
		}

		/// <summary>
		/// The cargo delta whose cargo to display.
		/// </summary>
		public CargoDelta CargoDelta
		{
			get { return cargoDelta; }
			set
			{
				cargoDelta = value;
				cargoContainer = null;
				Bind();
			}
		}

		public void Bind(ICargoContainer data)
		{
			CargoContainer = data;
			Bind();
		}

		public void Bind(CargoDelta data)
		{
			CargoDelta = data;
			Bind();
		}

		public void Bind()
		{
			lstCargoDetail.Initialize(32, 32);
			if (CargoContainer != null)
			{
				var cargo = CargoContainer.Cargo;
				var used = cargo.Size;
				var total = CargoContainer.CargoStorage;
				var free = used - total;
				foreach (var ug in cargo.Units.GroupBy(u => u.Design))
					lstCargoDetail.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
				foreach (var pop in cargo.Population)
					lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
			}
			else if (CargoDelta != null)
			{
				foreach (var u in CargoDelta.Units)
					lstCargoDetail.AddItemWithImage(u.Design.VehicleTypeName, u.Name, u, u.Icon);
				foreach (var dt in CargoDelta.UnitDesignTonnage)
					lstCargoDetail.AddItemWithImage(dt.Key.VehicleTypeName, dt.Key.Name, dt.Key, dt.Key.Icon);
				foreach (var dt in CargoDelta.UnitRoleTonnage)
					lstCargoDetail.AddItemWithImage(dt.Key, dt.Key, dt.Key, Mod.Current.Hulls.OfType<Unit>().First().Icon);
				foreach (var dt in CargoDelta.UnitTypeTonnage)
					lstCargoDetail.AddItemWithImage(dt.Key.ToSpacedString(), dt.Key.ToSpacedString(), dt.Key, Mod.Current.Hulls.Where(h => h.VehicleType == dt.Key).First().Icon);
				foreach (var pop in CargoDelta.RacePopulation)
					lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
				if (CargoDelta.AnyPopulation > 0)
					lstCargoDetail.AddItemWithImage("Population", CargoDelta.AnyPopulation + " population", null, Pictures.GetGenericImage(typeof(Race)));
			}
		}
	}
}
