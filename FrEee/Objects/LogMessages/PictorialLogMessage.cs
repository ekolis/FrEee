using FrEee.Interfaces;
using System;
using System.Drawing;

namespace FrEee.Objects.LogMessages;

/// <summary>
/// A log message which displays a message including a picture from some object.
/// </summary>
/// <typeparam name="T"></typeparam>
[Serializable]
public class PictorialLogMessage<T> : LogMessage, IPictorialLogMessage<T>
{
	public PictorialLogMessage(string text, T context, LogMessageType logMessageType = LogMessageType.Generic)
		: base(text, logMessageType)
	{
		Context = context;
	}

	public PictorialLogMessage(string text, int turn, T context, LogMessageType logMessageType = LogMessageType.Generic)
		: base(text, turn, logMessageType)
	{
		Context = context;
	}

	/// <summary>
	/// The context for the log message.
	/// </summary>
	public T Context { get; set; }

	public override Image Picture
	{
		get
		{
			if (Context is IPictorial p)
				return p.Portrait;
			else
				return null;
		}
	}
}