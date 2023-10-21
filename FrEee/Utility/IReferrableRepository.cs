using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Interfaces;

namespace FrEee.Utility
{
	public interface IReferrableRepository
	{
		/// <summary>
		/// The type of referrable stored by this repository.
		/// </summary>
		Type ReferrableType { get; }

		long Add(IReferrable r, long id);

		bool Remove(IReferrable r);

		bool Remove(long id);

		IReferrable this[long id] { get; }
	}
}
