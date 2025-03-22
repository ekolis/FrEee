using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Vehicles;

namespace FrEee.Persistence
{
	/// <summary>
	/// <see cref="ILibrary{T}"> which stores <see cref="IDesign"/>s.
	/// </summary>
	public class DesignLibrary
		: Library<IDesign>
	{
		public override string FilePath { get; } = Path.Combine(RootPath, "DesignLibrary.dat");

		public override void Clean(IDesign design)
		{
			design.Owner = null;
			design.TurnNumber = 0;
			design.Iteration = 0;
		}
	}
}
