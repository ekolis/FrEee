using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to manipulate an object's order queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TOrder"></typeparam>
    [Serializable]
    public abstract class OrderCommand<T> : Command<T>, IOrderCommand<T>
        where T : IOrderable
    {
        #region Protected Constructors

        protected OrderCommand(T target, IOrder<T> order)
            : base(target)
        {
            Order = order;
        }

        #endregion Protected Constructors

        #region Public Properties

        [DoNotSerialize]
        public virtual IOrder<T> Order
        {
            get
            {
                return order.Value;
            }
            set
            {
                order = value.ReferViaGalaxy();
            }
        }

        #endregion Public Properties

        #region Private Properties

        private GalaxyReference<IOrder<T>> order { get; set; }

        #endregion Private Properties

        #region Public Methods

        public override void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
        {
            if (done == null)
                done = new HashSet<IPromotable>();
            if (!done.Contains(this))
            {
                done.Add(this);
                base.ReplaceClientIDs(idmap, done);
                order.ReplaceClientIDs(idmap, done);
            }
        }

        #endregion Public Methods
    }
}
