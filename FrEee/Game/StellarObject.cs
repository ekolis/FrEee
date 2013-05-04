using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game
{
	/// <summary>
	/// A (typically) naturally occurring, large, immobile space object.
	/// </summary>
	public class StellarObject : ISpaceObject, ITemplate<StellarObject>
	{
		public StellarObject()
		{
			IntrinsicAbilities = new List<Ability>();
		}

		/// <summary>
		/// The name of this stellar object.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Stellar objects cannot be obscured by fog of war.
		/// </summary>
		public bool CanBeFogged
		{
			get { return false; }
		}

		/// <summary>
		/// Name of the picture used to represent this stellar object, excluding the file extension.
		/// PNG files will be searched first, then BMP.
		/// </summary>
		public string PictureName { get; set; }

		public Image Icon
		{
			get { return Pictures.GetIcon(this); }
		}

		public Image Portrait
		{
			get { return Pictures.GetPortrait(this); }
		}

		/// <summary>
		/// Abilities intrinsic to this stellar object.
		/// </summary>
		public IList<Ability> IntrinsicAbilities { get; private set; }

		/// <summary>
		/// Typical stellar objects don't inherit any abilities, so this is usually just the intrinsic abilities.
		/// </summary>
		public virtual IEnumerable<Ability> Abilities
		{
			get { return IntrinsicAbilities; }
		}

		/// <summary>
		/// Just copy the data of this stellar object.
		/// </summary>
		/// <returns></returns>
		public StellarObject Instantiate()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typical stellar objects aren't owned by any empire, so this return null for most types.
		/// </summary>
		public virtual Empire Owner { get { return null; } }
	}
}
