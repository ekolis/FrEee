# Default empire AI script

# import necessary classes and extension methods
clr.AddReference("System");
clr.AddReference("System.Core");
import System;
from System.Collections.Generic import List;
clr.ImportExtensions(System.Linq);
import FrEee;
import FrEee.Utility;
from FrEee.Objects.Commands import *;
clr.ImportExtensions(FrEee.Utility.Extensions);
from System import Console;

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
		cmd = ResearchCommand();
		cmd.Spending[Mod.Current.Technologies.FindByName("Propulsion")] = 100;
		empire.ResearchCommand = cmd;
	# TODO - check for more ministers and execute their code
# TODO - Vehicle Managment ministers

# test AI data storage
if (not empire.AINotes.HasProperty("Log")):
	empire.AINotes.Log = [];
empire.AINotes.Log.Add("{0} has played its turn for stardate {1}.\n".format(empire.Name, galaxy.Stardate));
for msg in empire.AINotes.Log:
	Console.WriteLine(msg);