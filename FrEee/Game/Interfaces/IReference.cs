using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A lightweight reference to some object in some context (e.g. the current mod or galaxy).
	/// Can be passed around on the network as a surrogate for said object.
	/// </summary>
	/// <typeparam name="TID"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	public interface IReference<out TID, out TValue>
	{
		TID ID { get; }
		TValue Value { get; }
		bool HasValue { get; }
	}

	[Serializable]
	public class ReferenceException<TID, TValue> : Exception, ISerializable
	{
		protected ReferenceException(SerializationInfo info, StreamingContext ctx)
			: base(info, ctx)
		{
			ID = (TID)info.GetValue("ID", typeof(TID));
		}

		public ReferenceException(string message, TID id = default(TID))
			: base(message)
		{
			ID = id;
		}

		public TID ID { get; private set; }

		public Type Type { get { return typeof(TValue); } }

		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ID", ID);
		}
	}
}
