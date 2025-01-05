using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Gameplay.Commands.Projects;

/// <summary>
/// Creates commands for empire wide projects such as research and espionage.
/// </summary>
public interface IProjectCommandService
{
    /// <summary>
    /// Creates an <see cref="IResearchCommand"/> to assign research spending.
    /// </summary>
    /// <returns></returns>
    IResearchCommand Research();
}