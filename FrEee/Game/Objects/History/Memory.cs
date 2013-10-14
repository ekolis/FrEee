using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FrEee.Game.Objects.History
{
	/// <summary>
	/// A memory of some object that has been seen by an empire.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Memory
	{
		public Type Type { get; private set; }

		public ICollection<IKeyframe> Keyframes { get; private set; }

		public Memory(Empire emp, IFoggable obj)
		{
			Type = obj.GetType();
			Keyframes = new List<IKeyframe>(emp.TakeSnapshot(true));
		}

		public IFoggable CreateObject()
		{
			var obj = (IFoggable)Type.Instantiate();
			foreach (var k in Keyframes)
				k.Apply(obj);
			obj.IsMemory = true;
			return obj;
		}
	}
}
