using System.Collections.Generic;

namespace FrEee.Interfaces
{
	public interface IErrorProne
	{
		IEnumerable<string> Errors { get; }
	}
}