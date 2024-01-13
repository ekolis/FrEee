using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;

namespace FrEee.Objects.Technology
{
	/// <summary>
	/// A reference to a component on a vehicle.
	/// </summary>
	/// TODO - implement client side cache (see GalaxyReference)
	public class ComponentReference : IReference<(long, int), Component>
	{
		public ComponentReference(long vehicleID, int componentIndex)
		{
			vehicle = new GalaxyReference<IVehicle>(vehicleID);
			ComponentIndex = ComponentIndex;
		}

		public ComponentReference(Component c)
		{
			Vehicle = c.Container;
			ComponentIndex = Vehicle.Components.IndexOf(c);
		}

		/// <summary>
		/// First value is the vehicle ID, second value is the index of the component on the vehicle's component list.
		/// </summary>
		public (long, int) ID => (vehicle.ID, ComponentIndex);
		public bool HasValue => Value != null;
		public Component Value => Vehicle?.Components?[ComponentIndex];

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			
		}

		private GalaxyReference<IVehicle> vehicle { get; set; }

		[DoNotSerialize(false)]
		public IVehicle Vehicle
		{
			get => vehicle.Value;
			set => vehicle = value.ReferViaGalaxy();
		}

		public int ComponentIndex { get; set; }
	}
}
