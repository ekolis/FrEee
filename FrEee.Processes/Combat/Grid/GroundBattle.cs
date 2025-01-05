using FrEee.Objects.Civilization;
using FrEee.Objects.Space;
using FrEee.Modding;
using FrEee.Utility;
using FrEee.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Objects.GameState;
using FrEee.Vehicles;

namespace FrEee.Processes.Combat.Grid;

/// <summary>
/// A battle which takes place on a planet's surface.
/// </summary>
/// <seealso cref="Battle" />
public class GroundBattle : Battle
{
    public GroundBattle(Planet location)
        : base()
    {
        Planet = location;
        OriginalPlanetOwner = Planet.Owner;
        Sector = location.Sector ?? throw new Exception("Ground battles require a sector location.");

        // TODO - should weapon platforms participate in ground combat like in SE5?
        Empires = Planet.Cargo.Units.Where(q => q.CanInvadeAndPoliceColonies).Select(t => t.Owner).Distinct();
        var combatants = new HashSet<ICombatant>(Planet.Cargo.Units.Where(q => q.CanInvadeAndPoliceColonies));
        for (var i = 0; i < Planet.PopulationFill.Value / Mod.Current.Settings.PopulationFactor / (Mod.Current.Settings.PopulationPerMilitia == 0 ? 20 : Mod.Current.Settings.PopulationPerMilitia); i++)
        {
            var militia = DIRoot.Designs.MilitiaDesign.Instantiate();
            militia.Owner = Planet.Owner;
            combatants.Add(militia);
        }

        Initialize(combatants);
    }

    public override int DamagePercentage => Mod.Current.Settings.GroundCombatDamagePercent;

    public Planet Planet { get; private set; }

    public Empire OriginalPlanetOwner { get; private set; }

    public override void Initialize(IEnumerable<ICombatant> combatants)
    {
        base.Initialize(combatants);

        // TODO: should weapon platforms take part in ground combat like in SE5?
        Empires = Planet.Cargo.Units.Where(q => q.CanInvadeAndPoliceColonies).Select(t => t.Owner).Distinct();

        int moduloID = (int)(Planet.ID % 100000);
        Dice = new PRNG((int)(moduloID / Game.Current.Timestamp * 10));
    }

    public override void PlaceCombatants(SafeDictionary<ICombatant, Vector2<int>> locations)
    {
        // in ground combat, for now everyone is right on top of each other
        foreach (var c in Combatants)
            locations.Add(c, new Vector2<int>());
    }

    public override int MaxRounds => Mod.Current.Settings.GroundCombatTurns;

    public override void ModifyHappiness()
    {
        foreach (var e in Empires)
        {
            switch (this.ResultFor(e))
            {
                case "victory":
                    if (OriginalPlanetOwner != e)
                        e.TriggerHappinessChange(hm => hm.EnemyPlanetCaptured);
                    break;
                case "defeat":
                    if (OriginalPlanetOwner == e)
                    {
                        e.TriggerHappinessChange(hm => hm.OurPlanetCaptured);
                        e.TriggerHappinessChange(hm => hm.OurPlanetLost);
                        if (Planet.Colony.IsHomeworld)
                            e.TriggerHappinessChange(hm => hm.OurHomeworldLost);
                    }
                    break;
            }

        }
    }

    public override string Name => $"Ground Battle at {Planet}";
}
