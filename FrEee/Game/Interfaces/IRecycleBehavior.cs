using FrEee.Game.Objects.LogMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A type of recycle behavior (scrap, analyze, etc.)
	/// </summary>
	public interface IRecycleBehavior
	{
		void Execute(IRecyclable target, bool didRecycle = false);

		IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor, IRecyclable target);

		string Verb { get; }
	}
}
