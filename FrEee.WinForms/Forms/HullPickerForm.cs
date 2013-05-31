using FrEee.Game.Interfaces;
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
	public partial class HullPickerForm : Form
	{
		public HullPickerForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// The hull that the user selected.
		/// </summary>
		public IHull Hull { get; private set; }
	}
}
