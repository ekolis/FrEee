#r "../../../bin/Debug/FrEee.Core.dll"
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Commands;
using System.Linq;
/// <summary>
/// Class that will issue basic research commands. 
/// </summary>
public class BasicResearch
{
    public void Run(Empire empire, Galaxy Context)
    {
        if (empire.ResearchQueue.Count() < 3)
        {
            var cmd = new ResearchCommand();
            foreach (var current in empire.ResearchQueue)
            {
                cmd.Queue.Add(current);
            }

            var ordered = empire.AvailableTechnologies.OrderBy(x => x.NextLevelCost); 
            for(int idx = 0; idx < 3 - empire.ResearchQueue.Count(); idx++)
            {
                cmd.Queue.Add(ordered.ElementAt(idx)); 
            }
            empire.ResearchCommand = cmd;
        }
        
    }
}

