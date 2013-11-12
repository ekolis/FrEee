using FrEee.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Modding.Interfaces
{
	/// <summary>
	/// An object which can be stored in mod files.
	/// </summary>
	public interface IModObject : INamed, IDisposable
	{
		/// <summary>
		/// An ID used to represent this mod object when patching a mod.
		/// </summary>
		string ModID { get; set; }
	}
}
