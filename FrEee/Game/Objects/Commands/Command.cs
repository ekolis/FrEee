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
		where T : IReferrable
	{
		protected Command(T target)
		{
			Issuer = Empire.Current;
			Executor = target;
		}

		[DoNotSerialize]
		public Empire Issuer { get { return issuer; } set { issuer = value; } }

		private Reference<Empire> issuer { get; set; }

		[DoNotSerialize]
		public T Executor { get { return executor; } set { executor = value; } }

		private Reference<T> executor { get; set; }

		public abstract void Execute();

		public virtual void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			issuer.ReplaceClientIDs(idmap);
			executor.ReplaceClientIDs(idmap);
		}

		public virtual IEnumerable<IReferrable> NewReferrables
		{
			get
			{
				yield break;
			}
		}
	}
}
