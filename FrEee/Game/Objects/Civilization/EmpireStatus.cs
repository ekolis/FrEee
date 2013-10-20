using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization
{
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

		public string Name { get { return Empire.Name; } }

		public Image Insignia { get { return Empire.Icon; } }

		public bool IsPlayerEmpire { get { return Empire.IsPlayerEmpire; } }

		public bool IsDefeated { get { return Empire.IsDefeated; } }

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
}
