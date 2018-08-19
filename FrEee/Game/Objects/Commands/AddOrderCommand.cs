using FrEee.Game.Interfaces;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// Adds an order to the end of the queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class AddOrderCommand<T> : OrderCommand<T>
        where T : IOrderable
    {
        #region Public Constructors

        public AddOrderCommand(T target, IOrder<T> order)
            : base(target, order)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override IEnumerable<IReferrable> NewReferrables
        {
            get
            {
                yield return Order;
            }
        }

        public override IOrder<T> Order
        {
            get
            {
                return NewOrder;
            }
            set
            {
                base.Order = value;
                NewOrder = value;
            }
        }

        #endregion Public Properties

        #region Private Properties

        private IOrder<T> NewOrder
        {
            get;
            set;
        }

        #endregion Private Properties

        #region Public Methods

        public override void Execute()
        {
            if (Executor == null)
                Issuer.Log.Add(new GenericLogMessage("Attempted to add an order to nonexistent object with ID=" + executor.ID + ". This is probably a game bug."));
            else if (Issuer == Executor.Owner)
            {
                if (Order is IConstructionOrder && ((IConstructionOrder)Order).Item != null)
                    Issuer.Log.Add(new GenericLogMessage("You cannot add a construction order with a prefabricated construction item!"));
                else if (Order == null)
                    Issuer.Log.Add(new GenericLogMessage("Attempted to add a null order to " + Executor + ". This is probably a game bug."));
                else
                    Executor.AddOrder(Order);
            }
            else
                Issuer.Log.Add(new GenericLogMessage(Issuer + " cannot issue commands to " + Executor + " belonging to " + Executor.Owner + "!", Galaxy.Current.TurnNumber));
        }

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                base.ReplaceClientIDs(idmap, done);
                NewOrder.ReplaceClientIDs(idmap, done);
            }
        }

        #endregion Public Methods
    }
}
