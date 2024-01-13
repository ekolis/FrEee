using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Objects.Vehicles;
using FrEee.Processes;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Extensions;
using FrEee.WinForms.Forms;
using FrEee.WinForms.Utility;
using FrEee.WinForms.Utility.Extensions;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FrEee.WinForms
{
	internal static class Program
	{
		private static int DisplaySyntax()
		{
			MessageBox.Show(
@"Syntax:

FrEee: display main menu

FrEee --help: display this help

FrEee --host gamename_turnnumber.gam: load host console
	Shortcut: FrEee gamename_turnnumber.gam
FrEee --process gamename_turnnumber.gam: process turn
FrEee --process-safe gamename_turnnumber.gam: process turn, halting if any plr files are missing

FrEee --play gamename_turnnumber_playernumber.gam: play a turn, resuming from where you left off if a plr file is present
	Shortcut: FrEee gamename_turnnumber_playernumber.gam
FrEee --play gamename_turnnumber_playernumber.plr: play a turn, resuming from where you left off
	Shortcut: FrEee gamename_turnnumber_playernumber.plr
FrEee --load gamename_turnnumber_playernumber.gam: play a turn, prompting to resume if plr file is present
FrEee --restart gamename_turnnumber_playernumber.gam: play a turn, restarting from the beginning of the turn");
			return 1;
		}

		private static readonly HttpClient http = new HttpClient();

		/// <summary>
		/// The main entry point for the application.
		/// Return values:
		/// 0 for success
		/// 1 for syntax error in command line
		/// 2 for missing GAM or PLR file specified to load
		/// 3 for crash
		/// 1xx for missing PLR file for player xx when running in "safe processing" mode
		/// </summary>
		[STAThread]
		private static int Main(string[] args)
		{
			// enable Microsoft App Center integration
			AppCenter.Start("f433569d-4dcf-416c-9c6e-e6c11f284cd5",
				   typeof(Analytics), typeof(Crashes));

			// log exceptions online
			Application.ThreadException += (sender, e) =>
			{
				try
				{
					void Log(Exception ex)
					{
						if (ex.InnerException != null)
							Log(ex.InnerException);
						var values = new Dictionary<string, string>
						{
							{ "app", "FrEee (WinForms)" },
							{ "version", Application.ProductVersion },
							{ "type", ex.GetType().Name },
							{ "message", ex.Message },
							{ "stackTrace", ex.StackTrace },
						};
						var content = new FormUrlEncodedContent(values);
						var response = http.PostAsync("http://edkolis.com/errorlog", content).Result;
						var responseString = response.Content.ReadAsStringAsync().Result;
					}
					var exception = e.Exception;
					exception.LogFatal();
					Log(exception);
					var inner = exception;
					while (inner is TargetInvocationException)
						inner = inner.InnerException;
					MessageBox.Show(inner.GetType().Name + ": " + inner.Message + "\n\nSee errorlog.txt for more details.");
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error logging exception: " + ex.Message);
				}
			};

			// HACK - so many things are based on the working directory...
			Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				var stdout = new FileStream("stdout.txt", FileMode.Create);
				var swOut = new StreamWriter(stdout);
				swOut.AutoFlush = true;
				Console.SetOut(swOut);
			}
			catch (IOException)
			{
				Console.Error.WriteLine("Cannot open stdout.txt.");
			}
			try
			{
				var stderr = new FileStream("stderr.txt", FileMode.Create);
				var swErr = new StreamWriter(stderr);
				swErr.AutoFlush = true;
				Console.SetError(swErr);
			}
			catch (IOException)
			{
				Console.Error.WriteLine("Cannot open stderr.txt.");
			}
			if (args.Length == 0)
			{
				Application.Run(MainMenuForm.GetInstance());
				return 0;
			}
			else if (args.Length == 1)
			{
				if (args[0].TrimStart('-').ToLower() == "help")
				{
					DisplaySyntax();
					return 0;
				}
				else
				{
					var file = args[0];
					return ProcessArgs(file);
				}
			}
			else if (args.Length == 2)
			{
				var operation = args[0];
				var file = args[1];
				return ProcessArgs(file, operation.TrimStart('-').ToLower());
			}
			else if (args.Length == 3)
			{
				var operation = args[0];
				var file = args[1];
				var extra = args[2];
				return ProcessArgs(file, operation.TrimStart('-').ToLower(), extra);
			}
			else
			{
				return DisplaySyntax();
			}
		}

		private static int PlayTurn(string plrfile = null)
		{
			if (plrfile != null)
			{
				if (File.Exists(plrfile))
				{
					try
					{
						Galaxy.Current.LoadCommands();
					}
					catch
					{
						MessageBox.Show("An error occurred while loading your commands. You will need to restart your turn from the beginning.");
						Galaxy.Load(Galaxy.Current.GameFileName); // in case some commands got loaded
					}
				}
				else
					MessageBox.Show(plrfile + " does not exist. You will need to start your turn from the beginning.");
			}

			Design.ImportFromLibrary();

			var form = new MainGameForm(false, true);
			form.KeyPreview = true;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.KeyDown += GuiExtensions.childForm_KeyDown_forDebugConsole;
			Application.Run(form);
			return 0;
		}

		private static int ProcessArgs(string file, string operation = null, string extraArg = null)
		{
			string gamfile = null, plrfile = null;

			// guess operation from filename
			// TODO - move these regexes to the Galaxy class?
			var playerRegex = @"(?i).*_.*_.*\.gam";
			var hostRegex = @"(?i).*_.*\.gam";
			var cmdRegex = @"(?i).*_.*_.*\.plr";

			if (operation == null)
			{
				if (Regex.IsMatch(file, playerRegex))
					operation = "play";
				else if (Regex.IsMatch(file, hostRegex))
					operation = "host";
				else if (Regex.IsMatch(file, cmdRegex))
					operation = "play";
				else
					return DisplaySyntax();
			}
			gamfile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", Path.GetFileNameWithoutExtension(file) + ".gam");
			plrfile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Savegame", Path.GetFileNameWithoutExtension(file) + ".plr");

			if (!File.Exists(gamfile))
			{
				MessageBox.Show(gamfile + " does not exist.");
				return 2;
			}

			// load GAM file, see if it's a host or player view
			Galaxy.Load(gamfile);
			if (Empire.Current == null)
			{
				// host view
				if (operation == "host")
				{
					// display host console
					var form = new HostConsoleForm();
					form.StartPosition = FormStartPosition.CenterScreen;
					Application.Run(form);
					return 0;
				}
				else if (operation == "process")
					return ProcessTurn(false);
				else if (operation == "process-safe")
					return ProcessTurn(true);
				else
					return DisplaySyntax();
			}
			else
			{
				// player view
				if (operation == "play")
				{
					if (File.Exists(plrfile))
						return PlayTurn(plrfile);
					else
						return PlayTurn();
				}
				else if (operation == "load")
				{
					// load turn and prompt player to resume if PLR file exists
					if (File.Exists(plrfile))
					{
						var result = MessageBox.Show("Player commands file exists for this turn. Resume turn from where you left off?", "Resume Turn", MessageBoxButtons.YesNo);
						if (result == DialogResult.Yes)
							return PlayTurn(plrfile);
						else
							return PlayTurn();
					}
					else
						return PlayTurn();
				}
				else if (operation == "restart")
					return PlayTurn();
				else
					return DisplaySyntax();
			}
		}

		private static int ProcessTurn(bool safe)
		{
			try
			{
				var status = new Status();
				Action status_Changed = () =>
				{
					Console.WriteLine(status.Progress.ToString("p0") + ": " + status.Message);
					if (status.Exception != null)
						Console.Error.WriteLine(status.Exception);
				};
				status.Changed += new Status.ChangedDelegate(status_Changed);

				Console.WriteLine("Processing turn...");
				var processor = new TurnProcessor();
				var emps = processor.ProcessTurn(Galaxy.Current, false, status);
				foreach (var emp in emps)
					Console.WriteLine(emp + " did not submit a PLR file.");
				if (safe && emps.Any())
				{
					Console.Error.WriteLine("Halting turn processing due to missing PLR file(s).");
					return 101 + Galaxy.Current.Empires.IndexOf(emps.First());
				}
				Galaxy.SaveAll();
				Console.WriteLine("Turn processed successfully. It is now turn " + Galaxy.Current.TurnNumber + " (stardate " + Galaxy.Current.Stardate + ").");
				Gui.Exit();
				return 0;
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Exception occurred (" + ex.Message + "): check errorlog.txt for details.");
				ex.LogFatal();
				return 3;
			}
		}

		/// <summary>
		/// The path of the executable for the game.
		/// </summary>
		public static string ExecutablePath
			=> Assembly.GetEntryAssembly()?.Location ?? throw new NotSupportedException("Can't retrieve executable path from unmanaged code.");

		/// <summary>
		/// The root directory of the game.
		/// </summary>
		public static string RootDirectory
			=> Path.GetDirectoryName(ExecutablePath) ?? throw new ArgumentNullException(nameof(ExecutablePath));

		/// <summary>
		/// Gets a path starting with the root directory of the game.
		/// </summary>
		/// <param name="dirs">Directories in order of hierarchy, e.g. to retrieve the path of Pictures/Races/Neutral001 you would pass in "Pictures", then "Races", then "Neutral001".</param>
		/// <returns></returns>
		public static string GetPath(params string[] dirs)
			=> Path.Combine(new string[] { RootDirectory }.Concat(dirs).ToArray());
	}
}
