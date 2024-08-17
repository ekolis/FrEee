using System;
using System.IO;

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
}
