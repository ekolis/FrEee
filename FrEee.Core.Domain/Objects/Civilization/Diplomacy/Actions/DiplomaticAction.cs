using FrEee.Gameplay.Commands;
using FrEee.Objects.GameState;
using FrEee.Serialization;

namespace FrEee.Objects.Civilization.Diplomacy.Actions;

/// <summary>
/// A unilateral diplomatic action.
/// </summary>
public abstract class DiplomaticAction : Command<Empire>
{
    protected DiplomaticAction(Empire target)
        : base(Empire.Current)
    {
        Target = target;
    }

    public abstract string Description { get; }

    /// <summary>
    /// The empire that is the target of this action.
    /// </summary>
    [DoNotSerialize]
    public Empire Target { get { return target; } set { target = value; } }

    private GameReference<Empire> target { get; set; }
}