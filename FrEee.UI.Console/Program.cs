using FrEee.UI;
using FrEee.UI.Console;

ConsoleCommandLine commandLine = new();

try
{
	return (int)commandLine.Run(args, false);
}
catch (Exception ex)
{
	commandLine.Log(ex);
	return (int)CommandLine.ReturnValue.Crash;
}