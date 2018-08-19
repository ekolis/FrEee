﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Orders
{
    public class RecycleVehicleInSpaceOrder : IOrder<SpaceVehicle>
    {
        #region Public Constructors

        public RecycleVehicleInSpaceOrder(IRecycleBehavior behavior)
        {
            Behavior = behavior;
        }

        #endregion Public Constructors

        #region Public Properties

        public IRecycleBehavior Behavior { get; private set; }

        public bool ConsumesMovement
        {
            get { return false; }
        }

        public long ID
        {
            get;
            set;
        }

        public bool IsComplete
        {
            get;
            private set;
        }

        public bool IsDisposed
        {
            get;
            set;
        }

        /// <summary>
        /// The empire which issued the order.
        /// </summary>
        [DoNotSerialize]
        public Empire Owner { get { return owner; } set { owner = value; } }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<Empire> owner { get; set; }

        #endregion Private Properties

        #region Public Methods

        public bool CheckCompletion(SpaceVehicle executor)
        {
            return IsComplete;
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;
            foreach (var v in Galaxy.Current.Referrables.OfType<SpaceVehicle>())
                v.Orders.Remove(this);
            Galaxy.Current.UnassignID(this);
        }

        public void Execute(SpaceVehicle executor)
        {
            var errors = GetErrors(executor);
            if (errors.Any() && Owner != null)
            {
                foreach (var e in errors)
                    Owner.Log.Add(e);
                return;
            }

            Behavior.Execute(executor);
            IsComplete = true;
        }

        public IEnumerable<LogMessage> GetErrors(SpaceVehicle executor)
        {
            return Behavior.GetErrors(executor, executor);
        }

        public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            // This type does not use client objects, so nothing to do here.
        }

        public override string ToString()
        {
            return Behavior.Verb;
        }

        #endregion Public Methods
    }
}
