using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to create a new fleet.
    /// </summary>
    public class CreateFleetCommand : Command<Empire>
    {
        #region Public Constructors

        public CreateFleetCommand(Fleet fleet, Sector sector)
            : base(Empire.Current)
        {
            Fleet = fleet;
            Sector = sector;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The fleet to create.
        /// </summary>
        public Fleet Fleet { get; set; }

        public override IEnumerable<IReferrable> NewReferrables
        {
            get
            {
                yield return Fleet;
            }
        }

        /// <summary>
        /// The sector to place the fleet in.
        /// </summary>
        public Sector Sector { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
            foreach (var v in Fleet.Vehicles)
                v.Container = null;
            Fleet.Vehicles.Clear(); // no cheating by spawning new vehicles!
            Fleet.Sector = Sector;
        }

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                base.ReplaceClientIDs(idmap, done);
                Fleet.ReplaceClientIDs(idmap, done);
            }
        }

        #endregion Public Methods
    }
}
