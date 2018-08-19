using System;
using System.Runtime.Serialization;

namespace FrEee.Game.Interfaces
{
    /// <summary>
    /// A lightweight reference to some object in some context (e.g. the current mod or galaxy).
    /// Can be passed around on the network as a surrogate for said object.
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IReference<out TValue> : IPromotable
    {
        #region Public Properties

        bool HasValue { get; }
        TValue Value { get; }

        #endregion Public Properties
    }

    /// <summary>
    /// A lightweight reference to some object in some context (e.g. the current mod or galaxy).
    /// Can be passed around on the network as a surrogate for said object.
    /// </summary>
    /// <typeparam name="TID"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface IReference<out TID, out TValue> : IReference<TValue>
    {
        #region Public Properties

        TID ID { get; }

        #endregion Public Properties
    }

    [Serializable]
    public class ReferenceException<TID, TValue> : Exception, ISerializable
    {
        #region Public Constructors

        public ReferenceException(string message, TID id = default(TID))
            : base(message)
        {
            ID = id;
        }

        #endregion Public Constructors

        #region Protected Constructors

        protected ReferenceException(SerializationInfo info, StreamingContext ctx)
                    : base(info, ctx)
        {
            ID = (TID)info.GetValue("ID", typeof(TID));
        }

        #endregion Protected Constructors

        #region Public Properties

        public TID ID { get; private set; }

        public Type Type { get { return typeof(TValue); } }

        #endregion Public Properties

        #region Public Methods

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ID", ID);
        }

        #endregion Public Methods
    }
}
