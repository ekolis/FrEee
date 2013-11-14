using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
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
	public abstract class Message : IMessage
	{
		protected Message(Empire recipient)
		{
			Owner = Empire.Current;
			Recipient = recipient;
			TurnNumber = Galaxy.Current.TurnNumber;
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
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
			var cmd = Owner.Commands.OfType<SendMessageCommand>().SingleOrDefault(c => c.Message == this);
			if (cmd != null)
				Owner.Commands.Remove(cmd);
		}

		private Reference<Empire> owner {get; set;}

		/// <summary>
		/// The empire sending this message.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner {get { return owner;}set { owner = value;}}

		private Reference<Empire> recipient {get; set;}

		/// <summary>
		/// The empire receiving the message.
		/// </summary>
		[DoNotSerialize]
		public Empire Recipient { get { return recipient; } set { recipient = value; } }
	
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

		public int TurnNumber { get; set; }

		public abstract void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null);

		public IMessage InReplyTo { get; set; }

		public bool IsDisposed { get; set; }
	}
}
