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
	/// Displays a star system map.
	/// </summary>
	public partial class StarSystemView : UserControl
	{
		public StarSystemView()
		{
			InitializeComponent();
		}

		StarSystem starSystem;
		/// <summary>
		/// The star system to display.
		/// </summary>
		public StarSystem StarSystem
		{
			get { return starSystem; }
			set
			{
				bool sizeChanged = starSystem == null || value == null || starSystem.Radius != value.Radius;
				starSystem = value;
				Bind(sizeChanged);
			}
		}

		/// <summary>
		/// Updates the control display to match any changes in the sector.
		/// </summary>
		/// <param name="sizeChanged">Did the system size change?</param>
		public void Bind(bool sizeChanged = false)
		{
			SuspendLayout();
			if (StarSystem != null)
			{
				if (sizeChanged)
				{
					// rebuild all controls
					tblSectors.Controls.Clear(); // delete all controls
					tblSectors.RowCount = StarSystem.Diameter;
					tblSectors.ColumnCount = StarSystem.Diameter;
					tblSectors.RowStyles.Clear();
					for (var i = 0; i < StarSystem.Diameter; i++)
						tblSectors.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / StarSystem.Diameter));
					tblSectors.ColumnStyles.Clear();
					for (var i = 0; i < StarSystem.Diameter; i++)
						tblSectors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / StarSystem.Diameter));
					for (int x = -StarSystem.Radius; x <= StarSystem.Radius; x++)
					{
						for (int y = -StarSystem.Radius; y <= StarSystem.Radius; y++)
						{
							var sectorView = new SectorView { Sector = StarSystem.GetSector(x, y) };
							tblSectors.Controls.Add(sectorView, x + StarSystem.Radius, y + StarSystem.Radius);
						}
					}
				}
				else
				{
					// reuse old controls
					for (int x = -StarSystem.Radius; x <= StarSystem.Radius; x++)
					{
						for (int y = -StarSystem.Radius; y <= StarSystem.Radius; y++)
						{
							var sectorView = (SectorView)tblSectors.GetControlFromPosition(x + StarSystem.Radius, y + StarSystem.Radius);
							sectorView.Sector = StarSystem.GetSector(x, y);
						}
					}
				}
			}
			else
				tblSectors.Controls.Clear(); // delete all controls
			ResumeLayout();
		}
	}
}
