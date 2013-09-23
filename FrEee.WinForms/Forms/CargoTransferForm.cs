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
    public partial class CargoTransferForm : Form
    {
        public CargoTransferForm(FrEee.Game.Objects.Vehicles.AutonomousSpaceVehicle selected_Vehicle = null, FrEee.Game.Objects.Space.Planet selected_Planet = null)
        {
            InitializeComponent();
            FrEee.WinForms.Controls.Cargolist clistFrom = new FrEee.WinForms.Controls.Cargolist(selected_Vehicle, selected_Planet);

            tableLayoutPanel1.Controls.Add(clistFrom, 1, 2);
            /* sudocode
             * foreach (var obj in sector)
             * { 
             *      if (obj is type planet)
             *      {
             *          FrEee.Winforms.Controls.Cargolist clistTo = new FrEee.WinForms.Controls.Cargolist(null, obj);
             *      }
             *      else if (obj is type Vehicle)
             *      {
             *          FrEee.Winforms.Controls.Cargolist clistTo = new FrEee.WinForms.Controls.Cargolist(obj, null);
             *      }
             *      tableLayoutPanel1.Controls.Add(clistTo, 2, 2);
             * }     
             *      
             */
        }
    }
}
