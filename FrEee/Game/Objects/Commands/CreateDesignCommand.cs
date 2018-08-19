﻿using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Game.Objects.Commands
{
    /// <summary>
    /// A command to create a new vehicle design.
    /// </summary>
    [Serializable]
    public class CreateDesignCommand<T> : Command<Empire>, ICreateDesignCommand where T : IVehicle
    {
        #region Public Constructors

        public CreateDesignCommand(IDesign<T> design)
            : base(Empire.Current)
        {
            Design = design;
        }

        #endregion Public Constructors

        #region Public Properties

        IDesign ICreateDesignCommand.Design { get { return Design; } }

        public IDesign<T> Design { get; set; }

        public override IEnumerable<IReferrable> NewReferrables
        {
            get
            {
                yield return Design;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override void Execute()
        {
            Design.VehiclesBuilt = 0; // in case it was tested in the simulator
            if (Design.Warnings.Any())
                Issuer.Log.Add(Design.CreateLogMessage("The " + Design.Name + " " + Design.VehicleTypeName + " design cannot be saved because it has warnings."));
            Issuer.KnownDesigns.Add(Design);
        }

        #endregion Public Methods
    }
}
