using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Utility
{
	/// <summary>
	/// Status for an operation.
	/// </summary>
	public class Status
	{
		/// <summary>
		/// Progress to completion.
		/// 0 = just started.
		/// 1 = done.
		/// </summary>
		public double Progress { get; set; }

		/// <summary>
		/// Message indicating current sub-operation.
		/// </summary>
		public string Message { get; set; }
	}
}
