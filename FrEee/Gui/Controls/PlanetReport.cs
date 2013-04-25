using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game;

namespace FrEee.Gui.Controls
{
	public partial class PlanetReport : UserControl
	{
		public PlanetReport()
		{
			InitializeComponent();
		}

		public PlanetReport(Planet planet)
			: this()
		{
			Planet = planet;
		}

		private Planet planet;

		/// <summary>
		/// The planet for which to display a report.
		/// </summary>
		public Planet Planet
		{
			get { return planet; }
			set
			{
				planet = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (Planet == null)
				Visible = false;
			else
			{
				Visible = true;
				// TODO - planet stuff
			}

			base.OnPaint(e);
		}
	}
}
