using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Commands;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A diplomatic message.
	/// </summary>
	public abstract class Message : IMessage
	{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		// initialized via property
		protected Message(Empire recipient)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
		{
			Owner = Empire.Current;
			Recipient = recipient;
			TurnNumber = Galaxy.Current.TurnNumber;
		}

		public Image Icon => Owner == Empire.Current ? Recipient.Icon : Owner.Icon;

		public Image Icon32 => Icon.Resize(32);

		public abstract IEnumerable<string> IconPaths { get; }

		public long ID { get; set; }

		public IMessage? InReplyTo { get; set; }

		public bool IsDisposed { get; set; }

		/// <summary>
		/// Messages cannot be memories, as they do not change over time.
		/// </summary>
		public bool IsMemory { get; set; }

		/// <summary>
		/// The empire sending this message.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get => owner; set => owner = value; }

		public Image? Portrait => Owner == Empire.Current ? Recipient.Portrait : Owner?.Portrait;

		public abstract IEnumerable<string> PortraitPaths { get; }

		/// <summary>
		/// The empire receiving the message.
		/// </summary>
		[DoNotSerialize]
		public Empire Recipient { get => recipient; set => recipient = value; }

		/// <summary>
		/// The text of the message.
		/// </summary>
		public string? Text { get; set; }

		public double Timestamp { get; set; }

		public int TurnNumber { get; set; }

		private GalaxyReference<Empire> owner { get; set; }

		private GalaxyReference<Empire> recipient { get; set; }

		public Visibility CheckVisibility(Empire emp)
		{
			// TODO - intel to spy on foreign messages or disrupt communications
			if (emp == Owner)
				return Visibility.Owned;
			if (emp == Recipient)
				return Visibility.Scanned;
			return Visibility.Unknown;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			Galaxy.Current.UnassignID(this);
			if (Owner != null)
			{
				// HACK - how could a diplomatic message have no owner?
				var cmd = Owner.Commands.OfType<SendMessageCommand>().SingleOrDefault(c => c.Message == this);
				if (cmd != null)
					Owner.Commands.Remove(cmd);
			}
		}

		/// <summary>
		/// Messages cannot be memories, as they do not change over time.
		/// </summary>
		public bool IsObsoleteMemory(Empire emp) => false;

		public void Redact(Empire emp)
		{
			// TODO - partial espionage of messages?
			// e.g. you could tell that a type of message was sent but not the text or parameters
			if (CheckVisibility(emp) < Visibility.Fogged)
				Dispose();
		}

		public abstract void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null);
	}
}
