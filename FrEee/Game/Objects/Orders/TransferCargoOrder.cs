using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Orders
{
	/// <summary>
	/// An order to transfer cargo from one object to another.
	/// </summary>
	public class TransferCargoOrder : IOrder<ICargoTransferrer>
	{
		public TransferCargoOrder(bool isLoadOrder, CargoDelta cargoDelta, ICargoTransferrer target)
		{
			Owner = Empire.Current;
			IsLoadOrder = isLoadOrder;
			CargoDelta = cargoDelta;
			Target = target;
		}

		/// <summary>
		/// The cargo delta, which specifies what cargo is to be transferred.
		/// </summary>
		public CargoDelta CargoDelta { get; set; }

		private IReference<ICargoTransferrer> destination { get; set; }

		/// <summary>
		/// The cargo transferrer to which the cargo will be transferred.
		/// </summary>
		[DoNotSerialize]
		public ICargoTransferrer Target { get { return destination.Value; } set { destination = value.Reference(); } }

		/// <summary>
		/// True if this is a load order, false if it is a drop order.
		/// </summary>
		private bool IsLoadOrder { get; set; }

		public void Execute(ICargoTransferrer executor)
		{
			if (executor.FindSector() == Target.Sector)
			{
				if (IsLoadOrder)
					Target.TransferCargo(CargoDelta, executor, executor.Owner);
				else
					executor.TransferCargo(CargoDelta, Target, executor.Owner);
			}
			else
				executor.Owner.Log.Add(executor.CreateLogMessage(executor + " cannot transfer cargo to " + Target + " because they are not in the same sector."));
			IsComplete = true;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public long ID { get; set; }

		public void Dispose()
		{
			// TODO - remove from queue, but we don't know which object we're on...
			Galaxy.Current.UnassignID(this);
		}

		private Reference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }

		public void ReplaceClientIDs(IDictionary<long, long> idmap)
		{
			destination.ReplaceClientIDs(idmap);
			owner.ReplaceClientIDs(idmap);
		}

		public override string ToString()
		{
			if (IsLoadOrder)
				return "Load " + CargoDelta + " from " + Target;
			else
				return "Drop " + CargoDelta + " at " + Target;
		}
	}
}
