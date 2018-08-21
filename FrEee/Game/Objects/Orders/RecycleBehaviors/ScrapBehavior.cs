﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.LogMessages;
using FrEee.Game.Objects.Space;
using FrEee.Game.Objects.Vehicles;
using FrEee.Utility.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Orders.RecycleBehaviors
{
	/// <summary>
	/// Scraps a recyclable object, returning resources equal to its scrap value.
	/// If there is anything in cargo, it will be scrapped as well.
	/// </summary>
	public class ScrapBehavior : IRecycleBehavior
	{
		public string Verb { get { return "Scrap"; } }

		public void Execute(IRecyclable target, bool didRecycle = false)
		{
			// don't scrap stuff that's already been scrapped due to it being in cargo of something else being scrapped!
			if (!target.IsDisposed)
			{
				var val = target.ScrapValue;
				if (target.Owner != null) // if not, it's already scrapped?
				{
					target.Owner.StoredResources += val;
					target.Owner.Log.Add(target.CreateLogMessage("We have scrapped " + target + " and reclaimed " + val + "."));
				}
				target.Dispose();

				if (!didRecycle)
					target.Recycle(this, true);
			}
		}

		public IEnumerable<LogMessage> GetErrors(IMobileSpaceObject executor, IRecyclable target)
		{
			if (target.IsDisposed)
				yield return target.CreateLogMessage($"{target} cannot be scrapped because it is already destroyed.");
			if (target.RecycleContainer != executor)
				yield return target.CreateLogMessage(target + " cannot be scrapped by " + executor + " because " + target + " does not belong to " + executor + ".");
			if ((target is Ship || target is Base) && !executor.Sector.SpaceObjects.Any(sobj => sobj.Owner == executor.Owner && sobj.HasAbility("Space Yard")))
				yield return target.CreateLogMessage(target + " cannot be scrapped at " + executor.Sector + " because there is no space yard present in that sector.");
			if ((target is IUnit) && !executor.Sector.SpaceObjects.Any(sobj => sobj.Owner == executor.Owner && (sobj is Planet || sobj.HasAbility("Space Yard"))))
				yield return target.CreateLogMessage(target + " cannot be scrapped at " + executor.Sector + " because there is no space yard or colony present in that sector.");
		}
	}
}