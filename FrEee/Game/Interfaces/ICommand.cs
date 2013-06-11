using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command to some object.
	/// </summary>
	[ClientSafe]
	public interface ICommand
	{
		/// <summary>
		/// The empire issuing the command.
		/// </summary>
		Empire Issuer { get; }

		/// <summary>
		/// Executes the command.
		/// </summary>
		void Execute();
	}

	/// <summary>
	/// A command to some object.
	/// </summary>
	public interface ICommand<T> : ICommand where T : ICommandable
	{
		/// <summary>
		/// The object whose queue is being manipulated.
		/// </summary>
		T Target { get; set; }
	}
}
