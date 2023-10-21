using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Serialization;
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

		[DoNotSerialize]
		public T Executor { get { return executor; } set { executor = value; } }

		IReferrable ICommand.Executor
		{
			get { return Executor; }
		}

		public long ExecutorID { get { return executor.ID; } }

		public bool IsDisposed { get; set; }

		[DoNotSerialize]
		public Empire Issuer { get { return issuer; } set { issuer = value; } }

		public virtual IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield break;
			}
		}

		protected GameReference<T> executor { get; set; }

		private GameReference<Empire> issuer { get; set; }

		public abstract void Execute();

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				issuer.ReplaceClientIDs(idmap, done);
				executor.ReplaceClientIDs(idmap, done);
				foreach (var r in NewReferrables.OfType<IPromotable>())
					r.ReplaceClientIDs(idmap, done);
			}
		}
	}
}