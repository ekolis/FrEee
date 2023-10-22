using FrEee.Extensions;
using FrEee.Objects.Civilization;

namespace FrEee.Objects.Civilization.Diplomacy.Clauses
{
	/// <summary>
	/// A treaty clause which shares knowledge of vehicle designs between empires.
	/// </summary>
	public class ShareDesignsClause : Clause
	{
		public ShareDesignsClause(Empire giver, Empire receiver)
			: base(giver, receiver)
		{
		}

		public override string BriefDescription
		{
			get { return "Share Designs"; }
		}

		public override string FullDescription
		{
			get
			{
				return Giver.WeOrName() + " will share all known vehicle designs with " + Receiver.UsOrName();
			}
		}

		public override void PerformAction()
		{
			foreach (var d in Giver.KnownDesigns)
				Receiver.KnownDesigns.Add(d);
		}
	}
}