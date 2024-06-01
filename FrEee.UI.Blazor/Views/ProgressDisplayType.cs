using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.UI.Blazor.Views;

// TODO: put this in a FrEee.UI project?
[Flags]
public enum ProgressDisplayType
{
	None = 0,
	Percentage = 1,
	Numeric = 2,
	Both = 3,
}