# Default empire AI script

# import necessary classes and extension methods
clr.AddReference("System.Core");
import System;
clr.ImportExtensions(System.Linq);
import FrEee;
import FrEee.Utility;
from FrEee.Game.Objects.Commands import *;
clr.ImportExtensions(FrEee.Utility.Extensions);

# alias the domain and context variables to avoid confusion
empire = domain;
galaxy = context;

# TODO - Design Managment ministers
# TODO - Colony Management ministers
if (enabledMinisters.ContainsKey("Empire Management")):
	category = enabledMinisters["Empire Management"];
	if (category.Contains("Research")):
		# choose what to research
		# TODO - actually choose sensibly, don't always research Propulsion
		cmd = ResearchCommand(empire);
		techs = galaxy.Mod.Technologies;
		tech = techs.Single(lambda t: t.Name == "Propulsion");
		# why isn't this line not working?!
		#cmd.SetSpending(galaxy.Mod.Technologies.Find("Propulsion"), 100);
		cmd.SetSpending(tech, 100);
		empire.ResearchCommand = cmd;
	# TODO - check for more ministers and execute their code
# TODO - Vehicle Managment ministers