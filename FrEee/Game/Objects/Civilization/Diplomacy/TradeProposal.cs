using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Civilization.Diplomacy
{
	/// <summary>
	/// A proposal to trade.
	/// </summary>
	public class GiveGiftProposal : Proposal
	{
		public GiveGiftProposal(Empire target)
			: base(target)
		{
		}

		public override string Description
		{
			get
			{
				if (IsNullOrEmpty(GivePackage) && IsNullOrEmpty(ReceivePackage))
					return "No Trade";
				if (IsNullOrEmpty(GivePackage))
					return "Request " + ReceivePackage;
				if (IsNullOrEmpty(ReceivePackage))
					return "Give " + GivePackage;
				return "Trade " + GivePackage + " for " + ReceivePackage;
			}
		}

		public override string ToString()
		{
			return Description;
		}

		public override void Execute()
		{
			var errors = GivePackage.Errors.Concat(ReceivePackage.Errors);
			if (errors.Any())
			{
				Executor.Log.Add(Target.CreateLogMessage("We could not execute a trade with the " + Target + " because: " + errors.First()));
				Target.Log.Add(Executor.CreateLogMessage("We could not execute a trade with the " + Executor + " because: " + errors.First()));
			}
			else
			{
				if (GivePackage != null)
					GivePackage.Transfer(Target);
				if (ReceivePackage != null)
					ReceivePackage.Transfer(Executor);
			}
		}

		/// <summary>
		/// The package being given.
		/// </summary>
		public Package GivePackage { get; set; }

		/// <summary>
		/// The package being received in return.
		/// </summary>
		public Package ReceivePackage { get; set; }

		private bool IsNullOrEmpty(Package package)
		{
			return package == null || package.IsEmpty;
		}
	}
}
