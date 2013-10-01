using FrEee.Utility; using FrEee.Utility.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// An object which can be "promoted" from the client side to the server side.
	/// </summary>
	public interface IPromotable
	{
		/// <summary>
		/// Replaces client-side object IDs with the newly generated server side IDs.
		/// </summary>
		/// <param name="idmap"></param>
		void ReplaceClientIDs(IDictionary<long, long> idmap);
	}
}
