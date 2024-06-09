using FrEee.Utility;
using FrEee.Utility;
namespace FrEee.Processes.Setup;

public enum EmpirePlacement
{
    [CanonicalName("Can Start In Same System")]
    CanStartInSameSystem,

    [CanonicalName("Different Systems")]
    DifferentSystems,

    Equidistant
}