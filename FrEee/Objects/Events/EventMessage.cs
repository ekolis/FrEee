using FrEee.Modding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrEee.Objects.Events;

/// <summary>
/// A message sent to a player when an event is triggered.
/// </summary>
public class EventMessage
{
	public IFormula<string> Title { get; set; }
	public IFormula<string> Text { get; set; }
}
