using FrEee.Game.Interfaces;
using System;
using System.Drawing;

namespace FrEee.Game.Objects.LogMessages
{
	/// <summary>
	/// A log message which displays a message including a picture from some object.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class PictorialLogMessage<T> : LogMessage, IPictorialLogMessage<T> where T : IPictorial
	{
		public PictorialLogMessage(string text, T context)
			: base(text)
		{
			Context = context;
		}

		public PictorialLogMessage(string text, int turn, T context)
			: base(text, turn)
		{
			Context = context;
		}

		/// <summary>
		/// The context for the log message.
		/// </summary>
		public T Context { get; set; }

		public override Image Picture
		{
			get { return Context?.Portrait; }
		}
	}
}