using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Utility.Extensions
{
	/// <summary>
	/// Tests extension methods.
	/// </summary>
	[TestClass]
	public class CommonExtensionsTest
	{
		[TestMethod]
		public void SpawnTasksAsync()
		{
			var nums = new List<int>();
			for (var i = 0; i <= 5; i++)
				nums.Add(i);
			var squares = nums.SpawnTasksAsync(i => new Square(i)).Result;
			for (var i = 0; i <= 5; i++)
				Assert.IsTrue(squares.Single(s => s.Number == i).SquaredNumber == i * i);
			Assert.IsTrue(!squares.Any(s => s.Number < 0));
			Assert.IsTrue(!squares.Any(s => s.Number > 5));
		}

		private class Square
		{
			public Square(int i)
			{
				Number = i;
			}

			public int Number { get; private set; }

			public int SquaredNumber { get { return Number * Number; } }
		}
	}
}
