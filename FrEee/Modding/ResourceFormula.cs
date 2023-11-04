using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;

using System.Linq;

namespace FrEee.Modding
{
	// TODO - split ResourceFormula into LiteralResourceFormula and ComputedResourceFormula?
	// TODO - equals method and IEquatable<ResourceFormula>
	public class ResourceFormula : SafeDictionary<Resource, Formula<int>>
	{
		public ResourceFormula(object context)
		{
			Context = context;
		}

		/// <summary>
		/// For serialization
		/// </summary>
		private ResourceFormula()
		{
		}

		/// <summary>
		/// The object which owns this quantity.
		/// </summary>
		[DoNotCopy]
		public object Context
		{
			get;
			private set;
		}

		public ResourceQuantity Value
		{
			get
			{
				var q = new ResourceQuantity();
				foreach (var kvp in this)
					q.Add(kvp.Key, kvp.Value);
				return q;
			}
		}

		public static implicit operator ResourceQuantity(ResourceFormula f)
		{
			return f.Value;
		}

		public static ResourceFormula operator -(ResourceFormula r1, ResourceFormula r2)
		{
			var result = new ResourceFormula(r1.Context ?? r2.Context);
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] - r2[key]);
			return result;
		}

		public static ResourceFormula operator *(ResourceFormula r, double scalar)
		{
			var result = new ResourceFormula(r.Context);
			foreach (var key in r.Keys)
				result.Add(key, r[key] * scalar);
			return result;
		}

		public static ResourceFormula operator /(ResourceFormula r, double scalar)
		{
			var result = new ResourceFormula(r.Context);
			foreach (var key in r.Keys)
				result.Add(key, r[key] / scalar);
			return result;
		}

		public static ResourceFormula operator +(ResourceFormula r1, ResourceFormula r2)
		{
			var result = new ResourceFormula(r1.Context ?? r2.Context);
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] + r2[key]);
			return result;
		}

		public ResourceQuantity Evaluate(object host)
		{
			var q = new ResourceQuantity();
			foreach (var kvp in this)
				q.Add(kvp.Key, kvp.Value.Evaluate(host));
			return q;
		}
	}
}
