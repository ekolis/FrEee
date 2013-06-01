using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A command to some object.
	/// </summary>
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
	public interface ICommand<T> : ICommand where T : ICommandable<T>
	{
		/// <summary>
		/// The ID of the target.
		/// </summary>
		int TargetID { get; set; }

		/// <summary>
		/// The object whose queue is being manipulated.
		/// </summary>
		T Target { get; set; }

		/// <summary>
		/// The index of the empire issuing the command (the first empire is number 1).
		/// </summary>
		int IssuerID { get; set; }
	}
}
