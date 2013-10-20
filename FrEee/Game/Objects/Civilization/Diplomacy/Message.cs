using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A diplomatic message.
	/// </summary>
	public abstract class Message<TReply> : IMessage<TReply>
		where TReply : IMessage
	{
		protected Message(Empire recipient)
		{
			Owner = Empire.Current;
			Recipient = recipient;
		}

		public long ID
		{
			get;
			set;
		}

		/// <summary>
		/// The text of the message.
		/// </summary>
		public string Text { get; set; }

		public void Dispose()
		{
			Galaxy.Current.UnassignID(this);
			Owner.OutgoingMessages.Remove(this);
		}

		/// <summary>
		/// The empire sending this message.
		/// </summary>
		public Empire Owner
		{
			get;
			set;
		}

		/// <summary>
		/// The empire receiving the message.
		/// </summary>
		public Empire Recipient
		{
			get; set;
		}
	
		public Visibility CheckVisibility(Empire emp)
		{
			// TODO - intel to spy on foreign messages or disrupt communications
 			if (emp == Owner)
				return Visibility.Owned;
			if (emp == Recipient)
				return Visibility.Scanned;
			return Visibility.Unknown;
		}

		public void Redact(Empire emp)
		{
			// TODO - partial espionage of messages?
			// e.g. you could tell that a type of message was sent but not the text or parameters
 			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose();
		}

		/// <summary>
		/// Messages cannot be memories, as they do not change over time.
		/// </summary>
		public bool IsMemory
		{
			  get; set;
		}

		public double Timestamp
		{
			get; set;
		}

		/// <summary>
		/// Messages cannot be memories, as they do not change over time.
		/// </summary>
		public bool IsObsoleteMemory(Empire emp)
		{
 			return false;
		}

		/// <summary>
		/// Creates a reply to this message.
		/// </summary>
		/// <returns></returns>
		public abstract TReply CreateReply();

		public Image Icon
		{
			get
			{
				return Owner == Empire.Current ? Recipient.Icon : Owner.Icon;
			}
		}

		public Image Portrait
		{
			get
			{
				return Owner == Empire.Current ? Recipient.Portrait : Owner.Portrait;
			}
		}

		public abstract void ReplaceClientIDs(IDictionary<long, long> idmap);
	}
}
