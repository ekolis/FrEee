using System.Collections.Generic;

namespace FrEee.Game.Interfaces
{
	public interface IErrorProne
	{
		IEnumerable<string> Errors { get; }
	}
}