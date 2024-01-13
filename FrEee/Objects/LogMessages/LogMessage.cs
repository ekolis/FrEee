using FrEee.Objects.Space;
using System;
using System.Drawing;

namespace FrEee.Objects.LogMessages
{
	[Serializable]
	public abstract class LogMessage
	{
		protected LogMessage(string text, LogMessageType logMessageType)
		{
			Text = text;
			TurnNumber = Galaxy.Current.TurnNumber;
			LogMessageType = logMessageType;
		}

		protected LogMessage(string text, int turn, LogMessageType logMessageType)
		{
			Text = text;
			TurnNumber = turn;
			LogMessageType =logMessageType; 
		}

		/// <summary>
		/// A picture to display with the log message.
		/// </summary>
		public abstract Image Picture { get; }

		/// <summary>
		/// The text of the log message.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// The turn number on which the log message was generated.
		/// </summary>
		public int TurnNumber { get; set; }

		/// <summary>
		/// The type of this message. 
		/// </summary>
		public LogMessageType LogMessageType { get; set; }

		
	}
	public enum LogMessageType
	{
		Generic,
		Error,
		Warning,
		ResearchComplete,
		ConstructionComplete, 
		PlanetColonised, 
		Battle, 
		Victory,
		Diplomacy,
	}
}