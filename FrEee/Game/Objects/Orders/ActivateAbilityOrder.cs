using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Abilities;
using FrEee.Game.Objects.Civilization;
using FrEee.Game.Objects.LogMessages;
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
	/// An order to activate some sort of ability, like self-destruct or create a star.
	/// </summary>
	public class ActivateAbilityOrder : IOrder<IMobileSpaceObject>
	{
		public ActivateAbilityOrder(Ability ability)
		{
			Owner = Empire.Current;
			Ability = ability;
		}

		private Reference<Ability> ability { get; set; }

		/// <summary>
		/// What ability to activate?
		/// </summary>
		public Ability Ability { get { return ability.Value; } set { ability = value.Reference(); } }

		public void Execute(IMobileSpaceObject executor)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor)
		{
			throw new NotImplementedException();
		}

		public bool CheckCompletion(IMobileSpaceObject executor)
		{
			return IsComplete;
		}

		public bool IsComplete
		{
			get;
			private set;
		}

		public bool ConsumesMovement
		{
			get { return false; }
		}

		public long ID
		{
			get;
			set;
		}

		public bool IsDisposed
		{
			get;
			set;
		}

		public void Dispose()
		{
			if (IsDisposed)
				return;
			foreach (var sobj in Galaxy.Current.Referrables.OfType<IMobileSpaceObject>())
				sobj.RemoveOrder(this);
			Galaxy.Current.UnassignID(this);
		}

		private Reference<Empire> owner { get; set; }

		/// <summary>
		/// The empire which issued the order.
		/// </summary>
		[DoNotSerialize]
		public Empire Owner { get { return owner; } set { owner = value; } }


		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			// no new client objects here, nothing to do
		}
	}
}
