using System.Collections.Generic;

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
		/// <param name="done">Any promoted objects that are already done replacing IDs.</param>
		void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null);
	}
}
