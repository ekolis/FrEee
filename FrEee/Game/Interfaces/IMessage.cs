using FrEee.Game.Objects.Civilization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Interfaces
{
	/// <summary>
	/// A diplomatic message.
	/// </summary>
	public interface IMessage : IFoggable, IPictorial, IPromotable
	{
		string Text { get; set; }

		Empire Recipient { get; set; }

		IMessage InReplyTo { get; set; }
	}
}
