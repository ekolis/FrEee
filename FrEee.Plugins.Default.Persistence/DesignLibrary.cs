﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Designs;
using FrEee.Persistence;
using FrEee.Vehicles;

namespace FrEee.Plugins.Default.Persistence
{
	/// <summary>
	/// <see cref="ILibrary{T}"> which stores <see cref="IDesign"/>s.
	/// </summary>
	[Export(typeof(IPlugin))]
	public class DesignLibrary
		: Library<IDesign>
	{
		public override string Name { get; } = "DesignLibrary";

		public override string FilePath { get; } = Path.Combine(RootPath, "DesignLibrary.dat");

		public override void Clean(IDesign design)
		{
			design.Owner = null;
			design.TurnNumber = 0;
			design.Iteration = 0;
		}
	}
}
