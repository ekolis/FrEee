using System;

namespace FrEee.Game.Objects.LogMessages
{
	/// <summary>
	/// A generic text log message with no picture.
	/// </summary>
	[Serializable]
	public class GenericLogMessage : LogMessage
	{
		public GenericLogMessage(string text, LogMessageType logMessageType = LogMessageType.Generic)
			: base(text, logMessageType)
		{
		}

		public GenericLogMessage(string text, int turn, LogMessageType logMessageType = LogMessageType.Generic) : base(text, turn, logMessageType)
		{ }

		public override System.Drawing.Image Picture
		{
			get { return null; }
		}
	}
}