﻿using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrEee.Game.Objects.Commands
{
	/// <summary>
	/// A command to enable or disable the obsolete flag on a design.
	/// </summary>
	public class SetObsoleteFlagCommand : Command<IDesign>
	{
		public SetObsoleteFlagCommand(IDesign design, bool isObsolete)
			: base(design)
		{
			Design = design;
			IsObsolete = isObsolete;
		}

		private Reference<IDesign> design { get; set; }

		/// <summary>
		/// The design to set the flag on.
		/// </summary>
		[DoNotSerialize]
		public IDesign Design { get { return design.Value; } set { design = value.Reference(); } }

		/// <summary>
		/// The flag state to set.
		/// </summary>
		public bool IsObsolete { get; set; }

		public override void Execute()
		{
			Design.IsObsolete = IsObsolete;
		}

		public void ReplaceClientIDs(IDictionary<long, long> idmap, ISet<IPromotable> done = null)
		{
			if (done == null)
				done = new HashSet<IPromotable>();
			if (!done.Contains(this))
			{
				done.Add(this);
				base.ReplaceClientIDs(idmap, done);
				design.ReplaceClientIDs(idmap, done);
			}
		}
	}
}
