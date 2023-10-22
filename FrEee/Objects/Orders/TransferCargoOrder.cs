using FrEee.Extensions;
using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Objects.LogMessages;
using FrEee.Objects.Space;
using FrEee.Serialization;
using FrEee.Utility;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Objects.Orders
{
	/// <summary>
	/// An order to transfer cargo from one object to another.
	/// </summary>
	public class TransferCargoOrder : IOrder
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

		public bool ConsumesMovement
		{
			get { return false; }
		}

		public long ID { get; set; }

		public bool IsComplete
		{
			get;
			set;
		}

		public bool IsDisposed { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[GameReference]
		public Empire Owner { get; set; }

		/// <summary>
		/// The cargo transferrer to which the cargo will be transferred, or null to launch/recover to/from space.
		/// </summary>
		[GameReference]
		public ICargoTransferrer Target { get; set; }

		/// <summary>
		/// True if this is a load order, false if it is a drop order.
		/// </summary>
		private bool IsLoadOrder { get; set; }

		public bool CheckCompletion(IOrderable v)
		{
			return IsComplete;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var v in The.Game.Referrables.OfType<ICargoTransferrer>())
				v.RemoveOrder(this);
			The.ReferrableRepository.Remove(this);
		}

		public void Execute(IOrderable ord)
		{
			if (ord is ICargoTransferrer executor)
			{
				var errors = GetErrors(executor);
				if (executor.Owner != null)
				{
					foreach (var error in errors)
						executor.Owner.Log.Add(error);
				}

				if (!errors.Any())
				{
					if (IsLoadOrder)
						Target.TransferCargo(CargoDelta, executor, executor.Owner);
					else
						executor.TransferCargo(CargoDelta, Target, executor.Owner);
				}
				IsComplete = true;
			}
		}

		public IEnumerable<LogMessage> GetErrors(IOrderable executor)
		{
			if (executor is ICargoTransferrer t)
			{
				if (Target != null && t.Sector != Target.Sector)
					yield return t.CreateLogMessage(executor + " cannot transfer cargo to " + Target + " because they are not in the same sector.", LogMessageType.Warning);
			}
			else
				yield return executor.CreateLogMessage($"{executor} cannot transfer cargo.", LogMessageType.Warning);
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
			}
		}

		public override string ToString()
		{
			if (Target == null)
			{
				if (IsLoadOrder)
					return "Recover " + CargoDelta;
				else
					return "Launch " + CargoDelta;
			}
			else
			{
				if (IsLoadOrder)
					return "Load " + CargoDelta + " from " + Target;
				else
					return "Drop " + CargoDelta + " at " + Target;
			}
		}
	}
}
