using System;
using System.Threading;
using System.Windows.Forms;
using FrEee.WinForms.Forms;
using FrEee.Game.Objects.Space;
using FrEee.Utility.Extensions;
using System.IO;

namespace FrEee.WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			if (args.Length == 0)
			{
				Application.Run(MainMenuForm.GetInstance());
				return 0;
			}
			else
			{
				var gamfile = args[0];
				try
				{
					Galaxy.Load(gamfile);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Could not load game " + gamfile + ": " + ex.Message + "\nPlease check errorlog.txt for more details.");
					ex.Log();
					Application.Exit();
					return 1;
				}
				if (Galaxy.Current.CurrentEmpire == null)
				{
					// process turn
					// TODO - plrstop option like in SE4/SE5
					try
					{
						Galaxy.ProcessTurn();
						Galaxy.SaveAll();
						Console.WriteLine("Turn processed successfully. It is now turn " + Galaxy.Current.Settings.TurnNumber + " (stardate " + Galaxy.Current.Stardate + ").");
						Application.Exit();
						return 0;
					}
					catch (Exception ex)
					{
						MessageBox.Show("Could not process turn for game " + gamfile + ": " + ex.Message + "\nPlease check errorlog.txt for more details.");
						ex.Log();
						Application.Exit();
						return 1;
					}
				}
				else
				{
					// play turn
					Application.Run(new GameForm(false));
					return 0;
				}
			}
        }
    }
}
