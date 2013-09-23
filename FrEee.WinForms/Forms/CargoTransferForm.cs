using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
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
        public CargoTransferForm(ICargoContainer fromContainer, Sector targetSector)
        {
            InitializeComponent();

			clFrom.CargoContainer = fromContainer;
			txtFromContainer.Text = fromContainer.Name;
			ddlToContainer.Items.Add(targetSector);
			foreach (var cc in targetSector.SpaceObjects.OfType<ICargoTransferrer>().Where(cc => cc != fromContainer && cc.Owner == Empire.Current).OrderBy(cc => cc.Name))
				ddlToContainer.Items.Add(cc);
			ddlToContainer.SelectedIndex = 0;
        }

		private void ddlToContainer_SelectedIndexChanged(object sender, EventArgs e)
		{
			clTo.CargoContainer = (ICargoContainer)ddlToContainer.SelectedItem;
		}
    }
}
