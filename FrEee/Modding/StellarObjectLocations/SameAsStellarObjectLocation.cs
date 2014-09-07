﻿using FrEee.Game;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Modding.Interfaces;
using FrEee.Modding.Templates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Modding.StellarObjectLocations
{
	/// <summary>
	/// Places one stellar object at the same location as another.
	/// </summary>
	 [Serializable] public class SameAsStellarObjectLocation : IStellarObjectLocation
	{
		public ITemplate<StellarObject> StellarObjectTemplate { get; set; }

		public StarSystemTemplate StarSystemTemplate { get; set; }

		public int TargetIndex { get; set; }

		public Point? LastResult { get; private set; }

		public Point Resolve(StarSystem sys)
		{
			if (TargetIndex < 1 || TargetIndex > StarSystemTemplate.StellarObjectLocations.Count)
				throw new Exception("Invalid location \"Same As " + TargetIndex + "\" specified for system with " + StarSystemTemplate.StellarObjectLocations.Count + " stellar objects.");
			var target = StarSystemTemplate.StellarObjectLocations[TargetIndex - 1];
			if (target is SameAsStellarObjectLocation)
				throw new Exception("A \"Same As\" stellar object location cannot target another \"Same As\" location.");
			LastResult = target.LastResult;
			return LastResult.Value;
		}
	}
}
