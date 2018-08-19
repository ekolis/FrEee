using FrEee.Game.Objects.Space;

namespace FrEee.Game.Objects.Civilization
{
    /// <summary>
    /// A waypoint which is fixed at a specific location in space.
    /// </summary>
    public class SectorWaypoint : Waypoint
    {
        #region Public Constructors

        public SectorWaypoint(Sector sector)
            : base()
        {
            Sector = sector;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Name
        {
            get { return "Waypoint at " + Sector.Name; }
        }

        public override Sector Sector
        {
            get;
            set;
        }

        public override StarSystem StarSystem
        {
            get { return Sector.StarSystem; }
        }

        #endregion Public Properties
    }
}
