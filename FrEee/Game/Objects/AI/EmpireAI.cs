using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.AI
{
	/// <summary>
	/// An AI that controls an empire.
	/// </summary>
	public class EmpireAI
	{
		/// <summary>
		/// Lets the AI play a turn in the current galaxy and saves commands to a PLR file.
		/// </summary>
		public void PlayTurn()
		{
			// TODO - implement AI
			Galaxy.Current.SaveCommands();
		}
	}
}
