using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Models
{
	// TODO: ohh, we need a separate data model for the object itself and for a reference to the object...
	// we could store all the objects in a repository and then look them up by ID everywhere else
	// rather than storing them wherever they're encountered first
	// the object data model would contain a copy of the ID data model so it can identify itself

	public class ReferrableDataModel(long id)
		: IDataModel
	{
		/// <summary>
		/// The ID of the referrable.
		/// </summary>
		public long ID { get; set; } = id;
	}
}
