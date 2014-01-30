﻿using FrEee.Game.Objects.Civilization;
using FrEee.Utility; using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IConstructionOrder : IOrder<ConstructionQueue>
	{
		/// <summary>
		/// The template.
		/// </summary>
		IConstructionTemplate Template { get; }

		/// <summary>
		/// The item being constructed.
		/// </summary>
		IConstructable Item { get; }

		/// <summary>
		/// The cost of the construction.
		/// </summary>
		ResourceQuantity Cost { get; }
	}
}
