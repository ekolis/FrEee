using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
    /// <summary>
    /// An action that rejects a proposal.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RejectProposalAction : Action
    {
        #region Public Constructors

        public RejectProposalAction(Proposal proposal)
            : base(proposal.Executor)
        {
            Proposal = proposal;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Description
        {
            get { return "Reject " + Proposal; }
        }

        /// <summary>
        /// The proposal in question.
        /// </summary>
        [DoNotSerialize]
        public Proposal Proposal { get { return proposal; } set { proposal = value; } }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<Proposal> proposal { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void Execute()
        {
            if (Proposal.IsResolved)
                Executor.Log.Add(Target.CreateLogMessage("The proposal \"" + Proposal + "\" has already been resolved and cannot be rejected now."));
            else
                Target.Log.Add(Executor.CreateLogMessage("The " + Executor + " has rejected our proposal (" + Proposal + ")."));
        }

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            base.ReplaceClientIDs(idmap, done);
            if (!done.Contains(this))
            {
                done.Add(this);
                proposal.ReplaceClientIDs(idmap, done);
            }
        }

        #endregion Public Methods
    }
}
