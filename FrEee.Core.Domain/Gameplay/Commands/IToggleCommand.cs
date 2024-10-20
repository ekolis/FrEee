using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;

namespace FrEee.Gameplay.Commands;

/// <summary>
/// A command which enables or disables something.
/// </summary>
public interface IToggleCommand
	: ICommand
{
	/// <summary>
	/// Is the toggle enabled or disabled?
	/// </summary>
	bool IsToggleEnabled { get; set; }
}
