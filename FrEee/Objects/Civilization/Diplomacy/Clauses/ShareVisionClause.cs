using FrEee.Utility.Extensions;

namespace FrEee.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A treaty clause which shares vision between empires.
	/// </summary>
	public class ShareVisionClause : Clause
	{
		public ShareVisionClause(Empire giver, Empire receiver)
			: base(giver, receiver)
		{
		}

		public override string BriefDescription
		{
			get { return "Share Vision"; }
		}

		public override string FullDescription
		{
			get
			{
				return Giver.WeOrName() + " will share all vision memory with " + Receiver.UsOrName();
			}
		}

		public override void PerformAction()
		{
			foreach (var kvp in Giver.Memory)
			{
				var existing = Receiver.Memory[kvp.Key];
				if (existing == null || existing.Timestamp < kvp.Value.Timestamp)
				{
					// copy memory
					var copy = kvp.Value.CopyAndAssignNewID();
					Receiver.Memory[kvp.Key] = copy;
				}
			}
		}
	}
}