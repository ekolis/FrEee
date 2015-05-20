﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	public interface IErrorProne
	{
		IEnumerable<string> Errors { get; }
	}
}
