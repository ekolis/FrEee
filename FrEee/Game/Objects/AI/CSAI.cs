using System;
using System.Collections.Generic;

using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;

using Microsoft.CodeAnalysis.Scripting;

#nullable enable

namespace FrEee.Game.Objects.AI
{
	[Serializable]
    public class CSAI<TDomain, TContext> : AI<TDomain, TContext> where TDomain : Empire where TContext : Galaxy
    {
        public CSAI(string name, CSScript script, SafeDictionary<string, ICollection<string>> ministerNames) 
            : base(name, script, ministerNames)
        {
            ScriptRunner = script.CreateScriptObject<Globals, TDomain>(); 
        }

        /// <summary>
        /// The object that holds a cs script within memory. 
        /// </summary>
        public ScriptRunner<TDomain> ScriptRunner;

        public override void Act(TDomain domain, TContext context, SafeDictionary<string, ICollection<string>> EnabledMinisters)
        {
            try
            {
                //NOTE: this really should be run as its own seperate program, passing information back and forth. 
                // Reason being, there is nothing to stop a script from doing things like creating files. 
                //This means it is a security risk to run scripts as mods are usually done so. 
                // By running in a seperate process, it can be sandboxed by the system, limiting its rights. 
                //however, such is a lot more intensive than this. 
                var global = new Globals() { Context = context, Domain = domain, EnabledMinisters = EnabledMinisters };

                var task = ScriptRunner.Invoke(globals: global);

                task.Wait();
                var result = task.Result;
                if (result != null)
                    domain = result;

            }
            catch (Exception)
            {
                throw; 
                //error log? 
            }
        }
    }


    /// <summary>
    /// Local class containing the tdomain and tcontext. 
    /// </summary>
    public class Globals
    {
        public Empire? Domain;

        public Galaxy? Context;

        public SafeDictionary<string, ICollection<string>>? EnabledMinisters;
    }
}
