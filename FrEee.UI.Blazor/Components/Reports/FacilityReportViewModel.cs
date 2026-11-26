using FrEee.Modding.Templates;
using FrEee.Objects.Technology;
using FrEee.UI.Blazor.Models;

namespace FrEee.UI.Blazor.Components.Reports;

public class FacilityReportViewModel : ViewModelBase
{
	public Facility? Facility { get; set; }
	public FacilityTemplate? FacilityTemplate { get; set; }
	public FacilityUpgrade? FacilityUpgrade { get; set; }

	public FacilityTemplate? EffectiveTemplate => FacilityUpgrade?.New ?? FacilityTemplate ?? Facility?.Template;
	public bool IsUpgrading => FacilityUpgrade is not null;
}
