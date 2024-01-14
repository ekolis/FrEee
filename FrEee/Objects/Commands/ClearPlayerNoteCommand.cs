using FrEee.Interfaces;
using FrEee.Objects.Civilization;
using FrEee.Utility; using FrEee.Serialization;
using FrEee.Extensions;
using System.Collections.Generic;

namespace FrEee.Objects.Commands;

/// <summary>
/// Toggles AI ministers.
/// </summary>
public class MinisterToggleCommand : Command<Empire>
{
	public MinisterToggleCommand()
		: base(Empire.Current)
	{
	}

	public SafeDictionary<string, ICollection<string>> EnabledMinisters { get; set; }

	public override void Execute()
	{
		if (Executor.AI == null)
			Executor.RecordLog(Executor, $"Could not toggle AI ministers for {Executor} because there is no AI for this empire.", LogMessages.LogMessageType.Error);
		else
			Executor.EnabledMinisters = EnabledMinisters;
	}
}