using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Gameplay.Commands.Ministers;

public interface IToggleMinistersCommand
	: ICommand<Empire>
{
	SafeDictionary<string, ICollection<string>> EnabledMinisters { get; set; }
}
