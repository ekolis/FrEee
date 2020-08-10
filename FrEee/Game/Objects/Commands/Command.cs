using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A generic command.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class Command<T> : ICommand<T> where T : class, IReferrable
	{
		protected Command(T? target)
		{
			Issuer = Empire.Current;
			Executor = target;
		}

		[DoNotSerialize]
		public T? Executor { get => executor; set => executor = value; }

		IReferrable? ICommand.Executor => Executor;

		public long? ExecutorID => executor?.ID;

		public bool IsDisposed { get; set; }

		[DoNotSerialize]
		public Empire? Issuer { get => issuer; set => issuer = value; }

		public virtual IEnumerable<IReferrable?> NewReferrables
		{
			get
			{
				yield break;
			}
		}

		protected GalaxyReference<T?>? executor { get; set; }

		private GalaxyReference<Empire?>? issuer { get; set; }

		public abstract void Execute();

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable>? done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				issuer?.ReplaceClientIDs(idmap, done);
				executor?.ReplaceClientIDs(idmap, done);
				foreach (var r in NewReferrables.OfType<IPromotable>())
					r.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
