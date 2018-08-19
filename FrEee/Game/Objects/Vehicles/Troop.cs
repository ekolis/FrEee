﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Vehicles
{
    [Serializable]
    public class Troop : Vehicle, IUnit
    {
        #region Public Properties

        public override AbilityTargets AbilityTarget
        {
            get { return AbilityTargets.Troop; }
        }

        public ICargoContainer Container
        {
            get { return Galaxy.Current.FindSpaceObjects<ICargoTransferrer>().SingleOrDefault(cc => cc.Cargo.Units.Contains(this)); }
        }

        public override int MaxTargets => int.MaxValue;

        public override IEnumerable<IAbilityObject> Parents
        {
            get
            {
                if (Owner != null)
                    yield return Owner;
                if (Container != null && Container is IAbilityObject)
                    yield return (IAbilityObject)Container;
            }
        }

        /// <summary>
        /// Troops participate in ground combat.
        /// </summary>
        public override bool ParticipatesInGroundCombat
        {
            get { return true; }
        }

        public override IMobileSpaceObject RecycleContainer
        {
            get { return (this as IUnit).Container as IMobileSpaceObject; }
        }

        public override bool RequiresSpaceYardQueue
        {
            get { return false; }
        }

        [DoNotSerialize]
        public override Sector Sector
        {
            get { return Container == null ? null : Container.Sector; }
            set
            {
                //throw new NotSupportedException("Cannot set the sector of a troop.");
            }
        }

        public override StarSystem StarSystem
        {
            get { return Container.StarSystem; }
        }

        public override WeaponTargets WeaponTargetType
        {
            // troops cannot be targeted in space combat
            get { return Enumerations.WeaponTargets.Invalid; }
        }

        #endregion Public Properties

        #region Public Methods

        public override Visibility CheckVisibility(Empire emp)
        {
            if (Owner == emp)
                return Visibility.Owned;
            var sobj = Container as ISpaceObject;
            if (sobj != null && sobj.CheckVisibility(emp) >= Visibility.Scanned)
                return Visibility.Scanned;
            return Visibility.Unknown;
        }

        public override bool IsObsoleteMemory(Empire emp)
        {
            return Container.StarSystem.CheckVisibility(emp) >= Visibility.Visible && Timestamp < Galaxy.Current.Timestamp - 1;
        }

        public override void Place(ISpaceObject target)
        {
            if (target is ICargoContainer)
            {
                var cc = (ICargoContainer)target;
                if (cc.AddUnit(this))
                    return;
            }
            // cargo was full? then try other space objects
            foreach (var cc in target.Sector.SpaceObjects.Where(sobj => sobj.Owner == target.Owner).OfType<ICargoContainer>())
            {
                if (cc.AddUnit(this))
                    return;
            }
            target.Owner.Log.Add(this.CreateLogMessage(this + " could not be placed in cargo at " + target + " because there is not enough cargo space available."));
        }

        #endregion Public Methods
    }
}
