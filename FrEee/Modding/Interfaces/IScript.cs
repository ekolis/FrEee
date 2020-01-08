using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Modding
{
	/// <summary>
	/// A type of script. 
	/// </summary>
    public interface IScript
    {
		/// <summary>
		/// The name of this script module. This should be a valid Python module name.
		/// </summary>
		string ModuleName { get; set; }

		/// <summary>
		/// The script text.
		/// </summary>
		string Text { get; set; }


		/// <summary>
		/// Any external scripts directly referenced by this script.
		/// </summary>
		//ISet<IScript> ExternalScripts { get; set; }
	}
}
