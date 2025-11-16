using System;
using System.Windows.Forms;

namespace FrEee.UI.WinForms;

internal static class Program
{
	/// <summary>
	/// The main entry point for the application.
	/// Return values:
	/// 0 for success
	/// 1 for syntax error in command line
	/// 2 for missing GAM or PLR file specified to load
	/// 3 for crash
	/// 4 for unsupported feature
	/// 1xx for missing PLR file for player xx when running in "safe processing" mode
	/// </summary>
	[STAThread]
	private static int Main(string[] args)
	{
		WinFormsCommandLine commandLine = new();
	
		Application.ThreadException += (sender, e) =>
		{
			commandLine.Log(e.Exception);
		};

		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		return (int)commandLine.Run(args);
	}
}