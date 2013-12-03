using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Combat2
{
	[Serializable]
	public class DamageType : INamed
	{
		// TODO - implement damage type
		public string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Can this damage type damage a target?
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public bool CanDamage(IDamageable target)
		{
			// TODO - damage types being unable to affect targets
			return true;
		}

		/// <summary>
		/// Can this damage type target a target?
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public bool CanTarget(IDamageable target)
		{
			// TODO - damage types being unable to target targets
			return true;
		}
	}
}
