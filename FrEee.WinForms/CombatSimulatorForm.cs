using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms
{
	public partial class CombatSimulatorForm : Form
	{
		public CombatSimulatorForm(bool groundCombat)
		{
			InitializeComponent();

			IsGroundCombat = groundCombat;

			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
		}

		/// <summary>
		/// Is this a ground combat sim, or a space combat sim?
		/// </summary>
		public bool IsGroundCombat { get; private set; }

		private void clTo_Load(object sender, EventArgs e)
		{

		}
	}
}
