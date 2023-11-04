using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Serialization; using FrEee.Serialization.Attributes;
using FrEee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.Commands
{
	/// <summary>
	/// A generic command.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public abstract class Command<T> : ICommand<T>
		where T : IReferrable
	{
		protected Command(T target)
		{
			Issuer = Empire.Current;
			Executor = target;
		}

		[GameReference]
		public T Executor { get; set; }

		IReferrable ICommand.Executor => Executor;

		public long ExecutorID => Executor.ID;

		public bool IsDisposed { get; set; }

		[GameReference]
		public Empire Issuer { get; set; }

		public virtual IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield break;
			}
		}

		public abstract void Execute();

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				foreach (var r in NewReferrables.OfType<IPromotable>())
					r.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
