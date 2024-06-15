using System;
using FrEee.Serialization.Stringifiers;
using FrEee.Serialization.Stringifiers;

namespace FrEee.Serialization.Stringifiers;

public abstract class Stringifier<T> : IStringifier<T>
{
	public Type SupportedType => typeof(T);

	public abstract T Destringify(string s);

	public abstract string Stringify(T t);

	public string Stringify(object o)
	{
		if (o == null)
			return null;
		if (typeof(T).IsAssignableFrom(o.GetType()))
			return Stringify((T)o);
		throw new ArgumentException($"Stringifier of type {typeof(T)} can't stringify objects of type {o.GetType()}");
	}

	object IStringifier.Destringify(string s)
	{
		return Destringify(s);
	}
}
