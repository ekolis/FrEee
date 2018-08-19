using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
    /// <summary>
    /// An action that accepts a proposal.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AcceptProposalAction : Action
    {
        #region Public Constructors

        public AcceptProposalAction(Proposal proposal)
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
                Executor.Log.Add(Target.CreateLogMessage("The proposal \"" + Proposal + "\" has already been resolved and cannot be accepted now."));
            else
            {
                Target.Log.Add(Executor.CreateLogMessage("The " + Executor + " has accepted our proposal (" + Proposal + ")."));
                Proposal.Execute();
            }
        }

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                base.ReplaceClientIDs(idmap, done);
                proposal.ReplaceClientIDs(idmap, done);
            }
        }

        #endregion Public Methods
    }
}
