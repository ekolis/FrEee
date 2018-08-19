using FrEee.Game.Objects.Civilization;
using System.Linq;

namespace FrEee.Modding
{
    /// <summary>
    /// Requirement that an empire's primary race have or not have some trait.
    /// </summary>
    public class EmpireTraitRequirement : Requirement<Empire>
    {
        #region Public Constructors

        public EmpireTraitRequirement(Trait trait, bool requiredOrForbidden)
            : base(requiredOrForbidden ? ("Requires " + trait + ".") : ("Empire cannot have " + trait + "."))
        {
            Trait = trait;
            RequiredOrForbidden = requiredOrForbidden;
        }

        #endregion Public Constructors

        #region Public Properties

        public override bool IsStrict
        {
            get { return RequiredOrForbidden; }
        }

        /// <summary>
        /// Is this a required trait (true) or a forbidden one (false)?
        /// </summary>
        public bool RequiredOrForbidden { get; set; }

        /// <summary>
        /// The trait in question
        /// </summary>
        public Trait Trait { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override bool IsMetBy(Empire obj)
        {
            return obj.PrimaryRace.Traits.Contains(Trait) == RequiredOrForbidden;
        }

        #endregion Public Methods
    }
}
