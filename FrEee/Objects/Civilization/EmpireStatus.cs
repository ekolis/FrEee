using FrEee.Objects.Space;
using System.Drawing;
using System.IO;

namespace FrEee.Objects.Civilization;

/// <summary>
/// Status of an empire (has the player uploaded a PLR file, etc.)
/// </summary>
public class EmpireStatus
{
	public EmpireStatus(Empire emp)
	{
		Empire = emp;
	}

	public Empire Empire { get; set; }

	public Image Insignia { get { return Empire.Icon; } }
	public bool IsDefeated { get { return Empire.IsDefeated; } }
	public bool IsPlayerEmpire { get { return Empire.IsPlayerEmpire; } }
	public string Name { get { return Empire.Name; } }

	public PlrUploadStatus PlrUploadStatus
	{
		get
		{
			if (IsDefeated)
				return PlrUploadStatus.Defeated;
			if (!IsPlayerEmpire)
				return PlrUploadStatus.AI;
			// TODO - check for PLR files in same folder as GAM?
			if (File.Exists(Galaxy.Current.GetEmpireCommandsSavePath(Empire)))
				return PlrUploadStatus.Uploaded;
			return PlrUploadStatus.NotUploaded;
		}
	}
}

public enum PlrUploadStatus
{
	/// <summary>
	/// PLR file has not been uploaded.
	/// </summary>
	NotUploaded,

	/// <summary>
	/// PLR file has been uploaded.
	/// </summary>
	Uploaded,

	/// <summary>
	/// Empire is defeated. No PLR file needed.
	/// </summary>
	Defeated,

	/// <summary>
	/// Empire is an AI. No PLR file needed.
	/// </summary>
	AI,
}