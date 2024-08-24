using System;
using System.Runtime.Serialization;
using FrEee.Objects.GameState;

namespace FrEee.Utility;

/// <summary>
/// A lightweight reference to some object in some context (e.g. the current mod or galaxy).
/// Can be passed around on the network as a surrogate for said object.
/// </summary>
/// <typeparam name="TID"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IReference<out TValue> : IPromotable
{
	bool HasValue { get; }
	TValue Value { get; }
}

/// <summary>
/// A lightweight reference to some object in some context (e.g. the current mod or galaxy).
/// Can be passed around on the network as a surrogate for said object.
/// </summary>
/// <typeparam name="TID"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IReference<out TID, out TValue> : IReference<TValue>
{
	TID ID { get; }
}

[Serializable]
public class ReferenceException<TID, TValue> : Exception, ISerializable
{
	public ReferenceException(string message, TID id = default)
		: base(message)
	{
		ID = id;
	}

	public TID ID { get; private set; }

	public Type Type { get { return typeof(TValue); } }
}