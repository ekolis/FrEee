﻿using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace FrEee.Tests.Utility.Extensions
{
	/// <summary>
	/// Tests extension methods.
	/// </summary>
	[TestClass]
	public class CommonExtensionsTest
	{
		[TestMethod]
		public void PercentOfRounded()
		{
			Assert.AreEqual(2, 50.PercentOfRounded(4));
			Assert.AreEqual(4, 50.PercentOfRounded(7));
			Assert.AreEqual(3, 50.PercentOfRounded(5));
			Assert.AreEqual(-2, 50.PercentOfRounded(-4));
			Assert.AreEqual(-4, 50.PercentOfRounded(-7));
			Assert.AreEqual(-3, 50.PercentOfRounded(-5));
		}

		[TestMethod]
		public void SpawnTasksAsync()
		{
			var nums = new List<int>();
			for (var i = 0; i <= 5; i++)
				nums.Add(i);
			var squares = nums.SpawnTasksAsync(i => new Square(i)).Result;
			for (var i = 0; i <= 5; i++)
				Assert.AreEqual(i * i, squares.Single(s => s.Number == i).SquaredNumber);
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