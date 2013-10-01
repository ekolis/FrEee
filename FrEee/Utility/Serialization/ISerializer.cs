using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility.Serialization
{
	public interface ISerializer
	{
		string Stringify(IList<object> known, object obj, int indent = 0);

		object Parse(IList<object> known, string text, Type t);

		string Serialize(object obj, int indent = 0, IList<object> known = null);
	}
}
