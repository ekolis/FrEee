﻿using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
    /// <summary>
    /// A diplomatic proposal.
    /// </summary>
    public class Proposal : Command<Empire>, IFoggable
    {
        #region Public Constructors

        public Proposal(Empire recipient)
            : base(Empire.Current)
        {
            Timestamp = Galaxy.Current.TurnNumber;
            Recipient = recipient;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Description
        {
            get
            {
                if (IsNullOrEmpty(GivePackage) && IsNullOrEmpty(ReceivePackage))
                    return "No Proposal";
                if (IsNullOrEmpty(GivePackage))
                    return "Request " + ReceivePackage;
                if (IsNullOrEmpty(ReceivePackage))
                    return "Give " + GivePackage;
                return "Trade " + GivePackage + " for " + ReceivePackage;
            }
        }

        /// <summary>
        /// The package being given.
        /// </summary>
        public Package GivePackage { get; set; }

        public long ID
        {
            get;
            set;
        }

        public bool IsMemory
        {
            get;
            set;
        }

        /// <summary>
        /// No fair accepting a gift twice!
        /// </summary>
        public bool IsResolved { get; set; }

        /// <summary>
        /// Is this a tentative offer?
        /// Tentative offers cannot be accepted; instead they must be countered.
        /// </summary>
        public bool IsTentative
        {
            get;
            set;
        }

        public Empire Owner
        {
            get { return Executor; }
        }

        /// <summary>
        /// The package being received in return.
        /// </summary>
        public Package ReceivePackage { get; set; }

        /// <summary>
        /// The empire that the proposal is being sent to.
        /// </summary>
        [DoNotSerialize]
        public Empire Recipient { get { return recipient; } set { recipient = value; } }

        public double Timestamp
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<Empire> recipient { get; set; }

        #endregion Private Properties

        #region Public Methods

        public Visibility CheckVisibility(Empire emp)
        {
            // TODO - intel that can spy on or disrupt comms
            if (emp == Owner)
                return Visibility.Owned;
            if (emp == Recipient)
                return Visibility.Scanned;
            return Visibility.Unknown;
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;
            Galaxy.Current.UnassignID(this);
        }

        public override void Execute()
        {
            var errors = GivePackage.Errors.Concat(ReceivePackage.Errors);
            if (errors.Any())
            {
                Executor.Log.Add(Recipient.CreateLogMessage("We could not execute a trade with the " + Recipient + " because: " + errors.First()));
                Recipient.Log.Add(Executor.CreateLogMessage("We could not execute a trade with the " + Executor + " because: " + errors.First()));
            }
            else
            {
                if (GivePackage != null)
                    GivePackage.Transfer(Recipient);
                if (ReceivePackage != null)
                    ReceivePackage.Transfer(Executor);
            }
        }

        public bool IsObsoleteMemory(Empire emp)
        {
            return false;
        }

        public void Redact(Empire emp)
        {
            if (CheckVisibility(emp) < Visibility.Fogged)
                Dispose();
        }

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            base.ReplaceClientIDs(idmap, done);
            if (!done.Contains(this))
            {
                done.Add(this);
                recipient.ReplaceClientIDs(idmap, done);
            }
        }

        public override string ToString()
        {
            return Description;
        }

        #endregion Public Methods

        #region Private Methods

        private bool IsNullOrEmpty(Package package)
        {
            return package == null || package.IsEmpty;
        }

        #endregion Private Methods
    }
}
