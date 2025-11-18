using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FrEee.Utility;

public static class ClientUtilities
{
	/// <summary>
	/// The path to FrEee's user roaming application data folder.
	/// </summary>
	public static string ApplicationDataPath
	{
		get
		{
			return Path.Combine
				(
					Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
					"FrEee"
				);
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
		=> Path.Combine(new[] { RootDirectory }.Concat(dirs).ToArray());
}
