using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Combat2;
using NewtMath.f16;

namespace FrEee.Tests.Game.Objects.Combat2
{
	class MockCombatObject : CombatObject
	{
		public MockCombatObject(ITargetable workingObject, PointXd position, PointXd vector, long ID, string IDprefix)
			: base(workingObject, position, vector, ID, IDprefix)
		{

		}

		public override void TakeSpecialDamage(Battle_Space battle, FrEee.Game.Objects.Combat.Hit hit, Utility.PRNG dice)
		{
			
		}
	}
}
