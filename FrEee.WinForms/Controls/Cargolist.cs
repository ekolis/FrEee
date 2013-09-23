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


namespace FrEee.WinForms.Controls
{
    public partial class Cargolist : UserControl
    {
        public Cargolist(FrEee.Game.Objects.Vehicles.AutonomousSpaceVehicle selected_Vehicle = null, FrEee.Game.Objects.Space.Planet selected_Planet = null)
        {
            InitializeComponent();
            int used;
            int free;
            int total;
            lstCargoDetail.Clear();
            if (selected_Planet != null)
            {
                used = selected_Planet.Cargo.Size;
                total = selected_Planet.CargoStorage;
                free = used - total;
                foreach (var ug in selected_Planet.Cargo.Units.GroupBy(u => u.Design))
                    lstCargoDetail.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
                foreach (var pop in selected_Planet.Cargo.Population)
                    lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
            }
            else if (selected_Vehicle != null)
            {
                used = selected_Vehicle.Cargo.Size;
                total = selected_Vehicle.CargoStorage;
                free = used - total;
                foreach (var ug in selected_Vehicle.Cargo.Units.GroupBy(u => u.Design))
					lstCargoDetail.AddItemWithImage(ug.Key.VehicleTypeName, ug.Count() + "x " + ug.Key.Name, ug, ug.First().Icon);
				foreach (var pop in selected_Vehicle.Cargo.Population)
					lstCargoDetail.AddItemWithImage("Population", pop.Value.ToUnitString(true) + " " + pop.Key.Name, pop, pop.Key.Icon);
            }
        }
    }
}
