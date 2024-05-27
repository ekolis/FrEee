using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Models
{

	public class ReferrableDataModel(long id)
		: IDataModel
	{
		/// <summary>
		/// The ID of the referrable.
		/// </summary>
		public long ID { get; set; } = id;
	}
}
