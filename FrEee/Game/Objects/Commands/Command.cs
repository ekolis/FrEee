using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A generic command.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class Command<T> : ICommand<T>
		where T : ICommandable
	{
		protected Command(Empire issuer, T target)
		{
			Issuer = issuer;
			Target = target;
			NewReferrables = new Dictionary<long, IReferrable>();
		}

		[DoNotSerialize]
		public Empire Issuer { get { return issuer; } set { issuer = value; } }

		private Reference<Empire> issuer { get; set; }

		[DoNotSerialize]
		public T Target { get { return target; } set { target = value; } }

		private Reference<T> target { get; set; }

		public abstract void Execute();

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			issuer.ReplaceClientIDs(idmap);
			target.ReplaceClientIDs(idmap);
		}

		public IDictionary<long, IReferrable> NewReferrables
		{
			get;
			private set;
		}
	}
}
