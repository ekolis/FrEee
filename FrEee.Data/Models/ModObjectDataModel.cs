using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Data.Models
{
	public class ModObjectDataModel(string modID)
		: IDataModel
	{
		/// <summary>
		/// The ID of the mod object.
		/// </summary>
		public string ModID { get; set; } = modID;
	}
}
