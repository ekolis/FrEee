using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility.Stringifiers
{
	public abstract class Stringifier<T> : IStringifier<T>
	{
		public Type SupportedType => typeof(T);

		public abstract T Destringify(string s);

		public abstract string Stringify(T t);

		public string Stringify(object o)
		{
			if (o == null)
				return null;
			if (o.GetType() == typeof(T))
				return Stringify((T)o);
			throw new ArgumentException($"Stringifier of type {typeof(T)} can't stringify objects of type {o.GetType()}");
		}

		object IStringifier.Destringify(string s)
		{
			return Destringify(s);
		}
	}
}
