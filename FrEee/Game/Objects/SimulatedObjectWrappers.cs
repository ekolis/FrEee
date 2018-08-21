﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects
{
	public class SimulatedEmpire : IDisposable
	{
		public SimulatedEmpire(Empire emp)
		{
			Empire = emp.CopyAndAssignNewID();
			SpaceObjects = new HashSet<SimulatedSpaceObject>();
		}

		public Empire Empire { get; private set; }

		public ISet<SimulatedSpaceObject> SpaceObjects { get; private set; }

		public void Dispose()
		{
			Empire.Dispose();
			foreach (var sobj in SpaceObjects)
				sobj.Dispose();
		}
	}

	public class SimulatedSpaceObject : IDisposable
	{
		public SimulatedSpaceObject(ICombatSpaceObject sobj)
		{
			SpaceObject = sobj;
			Units = new HashSet<SimulatedUnit>();
		}

		public ICombatSpaceObject SpaceObject { get; private set; }

		public ISet<SimulatedUnit> Units { get; private set; }

		// TODO - population in cargo?

		// TODO - enemy troops in cargo? or can those go under Units?

		public void Dispose()
		{
			SpaceObject.Dispose();
			foreach (var u in Units)
				u.Dispose();
		}
	}

	public class SimulatedUnit : IDisposable
	{
		public SimulatedUnit(IUnit u)
		{
			Unit = u;
		}

		public IUnit Unit { get; private set; }

		public void Dispose()
		{
			Unit.Dispose();
		}
	}
}