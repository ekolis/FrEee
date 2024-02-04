using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;
using FrEee.Objects.Space;

namespace FrEee.WinForms.ViewModels;

public class ActivateAbilityFormViewModel(IMobileSpaceObject spaceObject)
{
	/// <summary>
	/// The star system that the space object is located in.
	/// </summary>
	public StarSystem StarSystem => spaceObject.StarSystem;

	/// <summary>
	/// Any space objects that are in the same sector as the space object (including it).
	/// </summary>
	public IEnumerable<ISpaceObject> SpaceObjectsInSector => spaceObject.Sector.SpaceObjects;

	/// <summary>
	/// The space object which can receive orders.
	/// </summary>
	public IOrderable Orderable => spaceObject;
}
