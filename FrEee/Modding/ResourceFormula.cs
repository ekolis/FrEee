using FrEee.Game.Interfaces;
using FrEee.Modding.Interfaces;
using FrEee.Utility.Extensions;
using FrEee.Utility; using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding
{
	public class ResourceFormula : SafeDictionary<string, Formula<int>>
	{
		/// <summary>
		/// For serialization
		/// </summary>
		private ResourceFormula()
		{
		}

		public ResourceFormula(object context)
		{
			Context = context;
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

		public ResourceQuantity Evaluate(object host)
		{
			var q = new ResourceQuantity();
			foreach (var kvp in this)
				q.Add(kvp.Key, kvp.Value.Evaluate(host));
			return q;
		}

		public static ResourceFormula operator +(ResourceFormula r1, ResourceFormula r2)
		{
			var result = new ResourceFormula(r1.Context ?? r2.Context);
			foreach (var key in r1.Keys.Union(r2.Keys))
				result.Add(key, r1[key] + r2[key]);
			return result;
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
	
		public static implicit operator ResourceQuantity(ResourceFormula f)
		{
			return f.Value;
		}

		/// <summary>
		/// The object which owns this quantity.
		/// </summary>
		public object Context
		{
			get;
			private set;
		}

		public Formula<int> this[Resource r]
		{
			get { return this[r.Name]; }
			set { this[r.Name] = value; }
		}
	}		
}
