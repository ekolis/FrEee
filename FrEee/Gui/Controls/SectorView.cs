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
	/// <summary>
	/// Displays a sector on the system map.
	/// </summary>
	public partial class SectorView : UserControl
	{
		public SectorView()
		{
			InitializeComponent();
		}

		Sector sector;
		/// <summary>
		/// The sector to display.
		/// </summary>
		public Sector Sector
		{
			get { return sector; }
			set
			{
				sector = value;
				Bind();
			}
		}

		/// <summary>
		/// Updates the control display to match any changes in the sector.
		/// </summary>
		public void Bind()
		{
			if (Sector == null)
			{
				picIcon.Image = null;
				picOwnerFlag.Image = null;
				lblObjectCount.Text = null;
			}
			else
			{
				var largest = Sector.SpaceObjects.Largest();
				if (largest == null)
				{
					picIcon.Image = null;
					picOwnerFlag.Image = null;
					lblObjectCount.Text = null;
				}
				else
				{
					picIcon.Image = largest.Icon;
					// TODO - show owner flag
					lblObjectCount.Text = Sector.SpaceObjects.Count > 1 ? Sector.SpaceObjects.Count.ToString() : "";
				}
			}
		}
	}
}
