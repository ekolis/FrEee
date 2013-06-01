using FrEee.Game.Interfaces;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.LogMessages
{
	public abstract class LogMessage
	{
		protected LogMessage(string text, int turn)
		{
			Text = text;
			TurnNumber = turn;
		}

		/// <summary>
		/// The text of the log message.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// The turn number on which the log message was generated.
		/// </summary>
		int TurnNumber { get; set; }

		/// <summary>
		/// A picture to display with the log message.
		/// </summary>
		public abstract Image Picture { get; }
	}
}
