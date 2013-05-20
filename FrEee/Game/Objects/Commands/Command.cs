using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	public abstract class Command<T> : ICommand<T> where T : IOrderable<T>
	{
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
				return (T)Galaxy.Current.OrderTargets[TargetID - 1];
			}
			set
			{
				TargetID = Galaxy.Current.OrderTargets.IndexOf(value) + 1;
			}
		}

		public int OrderID
		{
			get;
			set;
		}

		public IOrder<T> Order
		{
			get
			{
				return Target.Orders[OrderID - 1];
			}
			set
			{
				OrderID = Target.Orders.IndexOf(value) + 1;
			}
		}


		public abstract void Execute();
	}
}
