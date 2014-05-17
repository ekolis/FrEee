using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IStellarObject : ISpaceObject, IReferrable, IModObject
	{
		/// <summary>
		/// The stellar size of this object.
		/// </summary>
		StellarSize StellarSize { get; }

		/// <summary>
		/// Used for naming.
		/// </summary>
		int Index { get; set; }

		/// <summary>
		/// Used for naming.
		/// </summary>
		bool IsUnique { get; set; }

		/// <summary>
		/// The name of this stellar object.
		/// </summary>
		new string Name { get; set; }

		/// <summary>
		/// A description of this stellar object.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Name of the picture used to represent this stellar object, excluding the file extension.
		/// PNG files will be searched first, then BMP.
		/// </summary>
		string PictureName { get; set; }

		void Redact(Empire emp);
	}
}
