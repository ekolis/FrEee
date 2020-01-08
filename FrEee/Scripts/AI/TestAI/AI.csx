//place any extra usings here. 

//Calling Load will execute the script. However, in this case, it will also allow the classes in the script to be called. 
#load "BasicResearchModule.csx"

//Note, at present, classes within scripts cannot be stored in the AI Notes as classes. 
//Since they are not recognized as the same class the next time a script is run. 
var AI = new Runner();

return AI.Run(Domain, Context);


public class Runner
{
    Empire empire;

    Galaxy galaxy;
    BasicResearch BasicResearch; 

    public Runner()
    {
        BasicResearch = new BasicResearch(); 
    }

    public Empire Run(Empire Domain, Galaxy Context)
    {
        try
        {
            galaxy = Context;
            empire = Domain;
            BasicResearch.Run(empire, galaxy); 
        }
        catch (Exception e)
        {

        }
        return empire;  
        
    }


    //void SetResearch()
    //{
    //    if (empire.ResearchQueue.Count() < 3)
    //    {
    //        var cmd = new ResearchCommand();
    //        foreach (var current in empire.ResearchQueue)
    //        {
    //            cmd.Queue.Add(current);
    //        }
    //        cmd.Queue.Add(empire.AvailableTechnologies.ElementAt(0));
    //        empire.ResearchCommand = cmd;
    //    }
    //}
}

