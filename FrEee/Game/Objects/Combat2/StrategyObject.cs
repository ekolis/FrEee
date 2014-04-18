﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewtMath.f16;
using FixMath.NET;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Enumerations;
using FrEee.Game.Objects.Technology;

namespace FrEee.Game.Objects.Combat2
{
    public class StrategyObject : IPromotable, IFoggable
    {
        protected StrategyBaseBlock waypointObj { get; set; }
        protected StrategyBaseBlock[] targetObjs { get; set; }
        public StrategyBaseBlock[] blocks { get; set; }

        // TODO - weapons lists can't be combat weapons for a strategy, because strategies are shared between ships, and combat weapons belong to a single ship
        // maybe we need some sort of indexing, like "large mount depleted uranium cannon #1", a la Vectac?
        // or we could simplify a bit and assign weapons based on their template, so you have to assign ALL of your "large mount depleted uranium cannon"'s to the same firing group?
        public List<Dictionary<int, MountedComponentTemplate>> weaponslists { get; set; }

        public StrategyObject()
        { }
        public StrategyObject(StrategyBaseBlock waypointstratObj, StrategyBaseBlock[] targetstratObjs)
        {
            this.waypointObj = waypointstratObj;
            this.targetObjs = targetstratObjs;
        }

        public combatWaypoint calcWaypiont(CombatObject comObj)
        {
            combatWaypoint wp = (combatWaypoint)waypointObj.getOutput(comObj);
            return wp;
        }

        public CombatObject calcTarget(CombatObject comObj)
        {
            CombatObject tgt = (CombatObject)targetObjs[0].getOutput(comObj);
            return tgt;
        }
        public int numberOfTargetStrategies()
        {
            return this.targetObjs.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comObj">this comObj</param>
        /// <param name="index">the weapongroup index</param>
        /// <returns></returns>
        public CombatObject targetforgroup(CombatObject comObj, int index)
        {
            return (CombatObject)targetObjs[index].getOutput(comObj);
        }

        public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            // Strategies don't reference other client-side promotable objects, so nothing to do here.
            // Well there are the strategy blocks, but those aren't actually referrables to begin with...
        }

        public long ID
        {
            get;
            set;
        }

        public bool IsDisposed
        {
            get;
            set;
        }

        public void Dispose()
        {
            Galaxy.Current.UnassignID(this);
            IsDisposed = true;
        }

        public Empire Owner
        {
            get;
            set;
        }

        public Visibility CheckVisibility(Empire emp)
        {
            if (emp == Owner)
                return Visibility.Owned;
            else
            {
                var designsUsingThis = Galaxy.Current.Referrables.OfType<IDesign>().Where(d => d.Strategy == this && d.CheckVisibility(emp) >= Visibility.Scanned);
                if (!designsUsingThis.Any())
                    return Visibility.Unknown;
                var vehiclesUsingThis = Galaxy.Current.Referrables.OfType<IVehicle>().Join(designsUsingThis, v => v.Design, d => d, (v, d) => v).Where(v => v.CheckVisibility(emp) >= Visibility.Scanned);
                if (!vehiclesUsingThis.Any())
                    return Visibility.Unknown; // other player might have changed strategy of design
                return Visibility.Scanned;
            }
        }

        public void Redact(Empire emp)
        {
            var vis = CheckVisibility(emp);
            if (vis < Visibility.Fogged)
                Dispose();
        }

        public bool IsMemory
        {
            get;
            set;
        }

        public double Timestamp
        {
            get;
            set;
        }

        public bool IsObsoleteMemory(Empire emp)
        {
            // TODO - should strategies ever be obsolete memories?
            return false;
        }
    }

    public class StragegyObject_Default : StrategyObject
    {
        public StragegyObject_Default()
            : base()
        {
            StrategyWayPoint wpnt = new StrategyWayPoint();
            StrategyClosest closest1 = new StrategyClosest(null, null);
            StrategyThisObj thisobj = new StrategyThisObj();
            StrategyThisEnemys thisobjEnemys = new StrategyThisEnemys();
            StrategyLocdata enloc = new StrategyLocdata();
            StrategyVeldata envel = new StrategyVeldata();

            wpnt.inputLnks[0] = enloc;
            wpnt.inputLnks[1] = envel;

            closest1.inputLnks[0] = thisobj;
            closest1.inputLnks[1] = thisobjEnemys;
            closest1.outputLnks.Add(enloc);
            closest1.outputLnks.Add(envel);

            enloc.inputLnks[0] = closest1;
            enloc.outputLnks.Add(wpnt);

            envel.inputLnks[0] = closest1;
            envel.outputLnks.Add(wpnt);

            waypointObj = wpnt;
            blocks = new StrategyBaseBlock[3] { closest1, enloc, envel };

            targetObjs = new StrategyBaseBlock[1] { closest1 };
        }
    }
}
