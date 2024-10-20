using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Gameplay.Commands.Notes;
using FrEee.Objects.Civilization;
using FrEee.Objects.Civilization.Orders;
using FrEee.Objects.GameState;
using FrEee.Objects.Vehicles;

namespace FrEee.Gameplay.Commands.Projects;

public class ProjectCommandFactory
	: IProjectCommandFactory
{
	public IResearchCommand Research()
	{
		return new ResearchCommand();
	}
}
