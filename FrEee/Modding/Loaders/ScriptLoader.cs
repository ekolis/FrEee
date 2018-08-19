using FrEee.Modding.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FrEee.Modding.Loaders
{
    /// <summary>
    /// Loads the mod's general purpose scripts (not AI scripts).
    /// </summary>
    public class ScriptLoader : ILoader
    {
        #region Public Constructors

        public ScriptLoader(string modPath)
        {
            ModPath = modPath;
            FileName = "*.py";
        }

        #endregion Public Constructors

        #region Public Properties

        public string FileName
        {
            get;
            set;
        }

        public string ModPath
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        public IEnumerable<IModObject> Load(Mod mod)
        {
            // TODO - should scripts be mod objects?
            {
                var name = "Global";
                string filename;
                string stockFilename = filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts", name);
                if (ModPath == null)
                    filename = stockFilename;
                else
                    filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Mods", ModPath, "Scripts", name);
                mod.GlobalScript = Script.Load(filename) ?? Script.Load(stockFilename) ?? new Script(name, "");
            }
            {
                var name = "GameInit";
                string filename;
                string stockFilename = filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts", name);
                if (ModPath == null)
                    filename = stockFilename;
                else
                    filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Mods", ModPath, "Scripts", name);
                mod.GameInitScript = Script.Load(filename) ?? Script.Load(stockFilename) ?? new Script(name, "");
            }
            {
                var name = "EndTurn";
                string filename;
                string stockFilename = filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Scripts", name);
                if (ModPath == null)
                    filename = stockFilename;
                else
                    filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Mods", ModPath, "Scripts", name);
                mod.EndTurnScript = Script.Load(filename) ?? Script.Load(stockFilename) ?? new Script(name, "");
            }

            yield break;
        }

        #endregion Public Methods
    }
}
