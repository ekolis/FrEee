using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Extensions;
using FrEee.UI.WinForms.Interfaces;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.Civilization.CargoStorage;

namespace FrEee.UI.WinForms.Controls;

public partial class StarSystemReport : UserControl, IBindable<StarSystem>
{
	public StarSystemReport()
	{
		InitializeComponent();
	}

	public StarSystemReport(StarSystem starSystem)
		: this()
	{
		StarSystem = starSystem;
	}

	/// <summary>
	/// The star system for which to display a report.
	/// </summary>
	public StarSystem StarSystem
	{
		get { return starSystem; }
		set
		{
			starSystem = value;
			Bind();
		}
	}

	private StarSystem starSystem;

	public void Bind()
	{
		SuspendLayout();
		if (StarSystem == null)
			Visible = false;
		else
		{
			Visible = true;

			picPortrait.Image = StarSystem.Portrait;

			txtName.Text = StarSystem.Name;
			txtDescription.Text = StarSystem.Description;

			var planets = StarSystem.FindSpaceObjects<Planet>();
			var colonies = planets.Select(p => p.Colony).Where(c => c != null);
			txtOurFacilities.Text = colonies.Where(c => c.Owner == Empire.Current).Sum(c => c.Facilities.Count).ToString();
			txtAllyFacilities.Text = colonies.Where(c => Empire.Current.IsAllyOf(c.Owner, StarSystem)).Sum(c => c.Facilities.Count).ToString();
			txtNeutralFacilities.Text = colonies.Where(c => Empire.Current.IsNeutralTo(c.Owner, StarSystem)).Sum(c => c.Facilities.Count).ToString();
			txtEnemyFacilities.Text = colonies.Where(c => Empire.Current.IsEnemyOf(c.Owner, StarSystem)).Sum(c => c.Facilities.Count).ToString();

			var vehicles = StarSystem.FindSpaceObjects<SpaceVehicle>();
			txtOurVehicles.Text = vehicles.Where(v => v.Owner == Empire.Current).Sum(v => v.Design.Hull.Size).Kilotons();
			txtAllyVehicles.Text = vehicles.Where(v => Empire.Current.IsAllyOf(v.Owner, StarSystem)).Sum(v => v.Design.Hull.Size).Kilotons();
			txtNeutralVehicles.Text = vehicles.Where(v => Empire.Current.IsNeutralTo(v.Owner, StarSystem)).Sum(v => v.Design.Hull.Size).Kilotons();
			txtEnemyVehicles.Text = vehicles.Where(v => Empire.Current.IsEnemyOf(v.Owner, StarSystem)).Sum(v => v.Design.Hull.Size).Kilotons();

			var uncolonized = planets.Where(p => p.Colony == null && Empire.Current.CanColonize(p));
			var uncolonizedCount = uncolonized.Count();
			txtUncolonizedPlanets.Text = uncolonizedCount.ToString();
			var breathableUsCount = uncolonized.Where(p => p.Atmosphere == Empire.Current.PrimaryRace.NativeAtmosphere).Count();
			txtBreathableUs.Text = breathableUsCount.ToString();
			var otherAtmospheres = Empire.Current.ColonizedPlanets.SelectMany(p => p.Colony.Population).Select(kvp => kvp.Key.NativeAtmosphere)
				.Union(Empire.Current.OwnedSpaceObjects.OfType<ICargoContainer>().SelectMany(sobj => sobj.Cargo.Population.Keys.Select(r => r.NativeAtmosphere))
				.Distinct()).Where(a => a != Empire.Current.PrimaryRace.NativeAtmosphere);
			var breathableOthersCount = uncolonized.Where(p => otherAtmospheres.Contains(p.Atmosphere)).Count();
			txtBreathableOthers.Text = breathableOthersCount.ToString();
			txtNonbreathable.Text = (uncolonizedCount - breathableUsCount - breathableOthersCount).ToString();

			abilityTreeView.Abilities = StarSystem.Abilities.Union(StarSystem.EmpireAbilities(Empire.Current)).StackToTree(StarSystem);
			abilityTreeView.IntrinsicAbilities = StarSystem.Abilities;
		}
		ResumeLayout();
	}

	public void Bind(StarSystem data)
	{
		StarSystem = data;
		Bind();
	}

	private void picPortrait_Click(object sender, System.EventArgs e)
	{
		picPortrait.ShowFullSize(StarSystem.Name);
	}
}