using FrEee.Modding;
using FrEee.Objects.GameState;
using FrEee.Serialization;
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
	public T Context
	{
		get
		{
			return (T)gameContext?.Value ?? (T)modContext?.Value ?? context;
        }
		set
		{
			if (value is null)
			{
				context = default;
				gameContext = null;
				modContext = null;
			}
			else if (value is IReferrable r)
			{
				context = default;
				gameContext = new GameReference<IReferrable>(r);
				modContext = null;
			}
			else if (value is IModObject m)
			{
				context = default;
				gameContext = null;
				modContext = new ModReference<IModObject>(m);
			}
			else
			{
				context = value;
				gameContext = null;
				modContext = null;
			}
		}

	}


	// TODO: a discriminated union would be nice about now...
	[DoNotSerialize]
	public T? context { get; set; }

    [DoNotSerialize]
    public GameReference<IReferrable>? gameContext { get; set; }

    [DoNotSerialize]
    public ModReference<IModObject>? modContext { get; set; }

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
	
	public override string ToString() => $"{GetType().Name}<{typeof(T).Name}>: {Text} ({Context})";
}