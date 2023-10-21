using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Utility.Serialization
{
	/// <summary>
	/// Something that performs an action after being deserialized.
	/// </summary>
	public interface IAfterDeserialize
	{
		void AfterDeserialize();
	}
}
