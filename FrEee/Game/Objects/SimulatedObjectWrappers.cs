using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects
{
    public class SimulatedEmpire : IDisposable
    {
        #region Public Constructors

        public SimulatedEmpire(Empire emp)
        {
            Empire = emp.CopyAndAssignNewID();
            SpaceObjects = new HashSet<SimulatedSpaceObject>();
        }

        #endregion Public Constructors

        #region Public Properties

        public Empire Empire { get; private set; }

        public ISet<SimulatedSpaceObject> SpaceObjects { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            Empire.Dispose();
            foreach (var sobj in SpaceObjects)
                sobj.Dispose();
        }

        #endregion Public Methods
    }

    public class SimulatedSpaceObject : IDisposable
    {
        #region Public Constructors

        public SimulatedSpaceObject(ICombatSpaceObject sobj)
        {
            SpaceObject = sobj;
            Units = new HashSet<SimulatedUnit>();
        }

        #endregion Public Constructors

        #region Public Properties

        public ICombatSpaceObject SpaceObject { get; private set; }

        public ISet<SimulatedUnit> Units { get; private set; }

        #endregion Public Properties

        // TODO - population in cargo?

        // TODO - enemy troops in cargo? or can those go under Units?

        #region Public Methods

        public void Dispose()
        {
            SpaceObject.Dispose();
            foreach (var u in Units)
                u.Dispose();
        }

        #endregion Public Methods
    }

    public class SimulatedUnit : IDisposable
    {
        #region Public Constructors

        public SimulatedUnit(IUnit u)
        {
            Unit = u;
        }

        #endregion Public Constructors

        #region Public Properties

        public IUnit Unit { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            Unit.Dispose();
        }

        #endregion Public Methods
    }
}
