using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Modding;
using FrEee.Objects.Civilization;
using FrEee.Objects.Technology;

namespace FrEee.Gameplay.Commands.Projects;

/// <summary>
/// Command which assigns research spending for an empire.
/// </summary>
public interface IResearchCommand
	: ICommand<Empire>
{
	/// <summary>
	/// List of technologies to research in order.
	/// If percentage spending weights total to less than 100, the queue will get the remaining points.
	/// </summary>
	ModReferenceList<Technology> Queue { get; }

	/// <summary>
	/// Percentage spending weights for technologies.
	/// </summary>
	ModReferenceKeyedDictionary<Technology, int> Spending { get; }
}
