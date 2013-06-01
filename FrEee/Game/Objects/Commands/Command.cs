using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
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
		where T : ICommandable<T>
	{
		protected Command(Empire issuer, T target)
		{
			Issuer = issuer;
			Target = target;
		}

		public int IssuerID { get; set; }

		public Empire Issuer
		{
			get
			{
				return Galaxy.Current.Empires[IssuerID - 1];
			}
			set
			{
				IssuerID = Galaxy.Current.Empires.IndexOf(value) + 1;
			}
		}

		public int TargetID
		{
			get;
			set;
		}

		public T Target
		{
			get
			{
				return (T)Galaxy.Current.Referrables[TargetID - 1];
			}
			set
			{
				TargetID = Galaxy.Current.Referrables.IndexOf((IReferrable<object>)value) + 1;
			}
		}

		public abstract void Execute();
	}
}
