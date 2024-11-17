using FrEee.Objects.Civilization;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.UI.WinForms.Utility.Extensions;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FrEee.Vehicles;
using FrEee.Vehicles.Types;

namespace FrEee.UI.WinForms.Forms;

public partial class HullPickerForm : GameForm
{
	public HullPickerForm()
	{
		InitializeComponent();
		ddlVehicleType.Items.Add(new { Name = "All", VehicleType = VehicleTypes.All });
		ddlVehicleType.Items.Add(new { Name = "Ships", VehicleType = VehicleTypes.Ship });
		ddlVehicleType.Items.Add(new { Name = "Bases", VehicleType = VehicleTypes.Base });
		ddlVehicleType.Items.Add(new { Name = "Fighters", VehicleType = VehicleTypes.Fighter });
		ddlVehicleType.Items.Add(new { Name = "Troops", VehicleType = VehicleTypes.Troop });
		ddlVehicleType.Items.Add(new { Name = "Satellites", VehicleType = VehicleTypes.Satellite });
		ddlVehicleType.Items.Add(new { Name = "Mines", VehicleType = VehicleTypes.Mine });
		ddlVehicleType.Items.Add(new { Name = "Drones", VehicleType = VehicleTypes.Drone });
		ddlVehicleType.Items.Add(new { Name = "Weapon Platforms", VehicleType = VehicleTypes.WeaponPlatform });
		ddlVehicleType.SelectedItem = ddlVehicleType.Items[0];

		try { base.Icon = new Icon(FrEee.UI.WinForms.Properties.Resources.FrEeeIcon); } catch { }
	}

	/// <summary>
	/// The hull that the user selected.
	/// </summary>
	public IHull Hull
	{
		get
		{
			return hull;
		}
		private set
		{
			hull = value;
			lstAbilities.Initialize(20, 20);
			if (hull != null)
			{
				foreach (var abil in hull.Abilities)
					lstAbilities.AddItemWithImage(null, abil.Description, abil, null);
			}
		}
	}

	public VehicleTypes SelectedVehicleType
	{
		get
		{
			if (ddlVehicleType.SelectedItem == null)
				return VehicleTypes.None;
			return ((dynamic)ddlVehicleType.SelectedItem).VehicleType;
		}
		set
		{
			ddlVehicleType.SelectedItem = ddlVehicleType.Items.Cast<dynamic>().SingleOrDefault(item => item.VehicleType == value);
			Bind();
		}
	}

	private IHull hull;

	private void Bind()
	{
		var vt = (VehicleTypes)((dynamic)ddlVehicleType.SelectedItem).VehicleType;
		var hulls = Empire.Current.UnlockedItems.OfType<IHull>().Where(h => vt.HasFlag(h.VehicleType));
		lstHulls.Initialize(32, 32);
		foreach (var hull in hulls)
		{
			var item = lstHulls.AddItemWithImage(ddlVehicleType.SelectedIndex > 0 ? hull.VehicleTypeName : null, hull.Name, hull, hull.Icon, null, hull.Size.Kilotons(), hull.Cost[Resource.Minerals].ToUnitString(), hull.Cost[Resource.Organics].ToUnitString(), hull.Cost[Resource.Radioactives].ToUnitString(), hull.Description);
			item.UseItemStyleForSubItems = false;
			item.SubItems[2].ForeColor = Resource.Minerals.Color;
			item.SubItems[3].ForeColor = Resource.Organics.Color;
			item.SubItems[4].ForeColor = Resource.Radioactives.Color;
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.Cancel;
		Close();
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		DialogResult = DialogResult.OK;
		Close();
	}

	private void ddlVehicleType_SelectedIndexChanged(object sender, EventArgs e)
	{
		Bind();
	}

	private void lstHulls_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		var item = lstHulls.GetItemAt(e.X, e.Y);
		if (item != null)
		{
			Hull = (IHull)item.Tag;
			DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void lstHulls_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lstHulls.SelectedItems.Count == 1)
		{
			var item = lstHulls.SelectedItems[0];
			Hull = (IHull)item.Tag;
		}
	}
}