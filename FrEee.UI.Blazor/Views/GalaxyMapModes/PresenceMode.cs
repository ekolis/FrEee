﻿using System.ComponentModel.Composition;
using System.Drawing;
using FrEee.Extensions;
using FrEee.Objects.Civilization;
using FrEee.Objects.Space;

namespace FrEee.UI.Blazor.Views.GalaxyMapModes
{
	[Export(typeof(IGalaxyMapMode))]
	public class PresenceMode : IGalaxyMapMode
	{
		public string Name => "Presence";

		public PieChartViewModel<int> GetStarSystemViewModel(StarSystem starSystem, Action<StarSystem> starSystemClicked)
		{
			var owners = FindOwners(starSystem);
			if (owners.Any())
			{
				return new()
				{
					// weight all empires equally
					Entries = owners.Select(empire =>
						new PieChartViewModel<int>.Entry(empire.Name, empire.Color, 1)),
					OnClick = () => starSystemClicked(starSystem)
				};
			}
			else
			{
				return new()
				{
					// put a dummy "no one" empire so something is rendered
					Entries = [new PieChartViewModel<int>.Entry("(no one)", Color.Black, 1)],
					OnClick = () => starSystemClicked(starSystem)
				};
			}
		}


		private IEnumerable<Empire> FindOwners(StarSystem sys) =>
			sys.FindSpaceObjects<ISpaceObject>()
				.Select(x => x.Owner)
				.ExceptNull()
				.Distinct();
	}
}
