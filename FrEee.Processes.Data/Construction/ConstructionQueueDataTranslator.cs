using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrEee.Data;
using FrEee.Extensions;
using Microsoft.Scripting.Utils;

namespace FrEee.Processes.Construction;

[Export(typeof(IDataTranslator))]
public class ConstructionQueueDataTranslator
	: DataTranslator<ConstructionQueue, ConstructionQueueData>
{
	public override ConstructionQueue FromData(ConstructionQueueData data)
	{
		ConstructionQueue obj = new(data.Container.Value)
		{
			ID = data.ID,
			IsConstructionDelayed = data.IsConstructionDelayed,
			IsDisposed = data.IsDisposed,
			IsOnHold = data.IsOnHold,
			IsOnRepeat = data.IsOnRepeat,
			Timestamp = data.Timestamp,
			UnspentRate = data.UnspentRate,
		};
		obj.Orders.AddRange(data.Orders);
		return obj;
	}

	public override ConstructionQueueData ToData(ConstructionQueue obj)
	{
		return new()
		{
			Container = obj.Container.ReferViaGalaxy(),
			ID = obj.ID,
			IsConstructionDelayed = obj.IsConstructionDelayed,
			IsDisposed = obj.IsDisposed,
			IsOnHold = obj.IsOnHold,
			IsOnRepeat = obj.IsOnRepeat,
			Orders = obj.Orders,
			Timestamp = obj.Timestamp,
			UnspentRate = obj.UnspentRate,
		};
	}
}
