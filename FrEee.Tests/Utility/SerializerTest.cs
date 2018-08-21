﻿using FrEee.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Utility
{
	[TestClass]
	public class SerializerTest
	{
		[ClassInitialize]
		public static void ClassInit(TestContext ctx)
		{
			// silly unit tests can't find their own assembly
			SafeType.ForceLoadType(typeof(SerializerTest));
			SafeType.ForceLoadType(typeof(Car));
			SafeType.ForceLoadType(typeof(Company));
		}

		[TestMethod]
		public void CircularReferences()
		{
			var george = new Person("George");
			var brad = new Person("Brad");
			george.Partner = brad;
			brad.Partner = george;
			var s = Serializer.SerializeToString(george);
			var george2 = Serializer.DeserializeFromString<Person>(s);
			Assert.AreEqual(george.ToString(), george2.ToString());
		}

		[TestMethod]
		public void MagicTypes()
		{
			var p = new System.Drawing.Point(6, 9);
			var s = Serializer.SerializeToString(p);
			var p2 = Serializer.DeserializeFromString(s);
			Assert.AreEqual(p, p2);
		}

		[TestMethod]
		public void Roundtrip()
		{
			var chevy = new Company("Chevrolet");
			var car = new Car(chevy, "HHR", 2007);
			var s = Serializer.SerializeToString(car);
			var car2 = Serializer.DeserializeFromString<Car>(s);
			Assert.AreEqual(car.ToString(), car2.ToString());
		}

		[TestMethod]
		public void Scalar()
		{
			var answer = 42;
			var s = Serializer.SerializeToString(answer);
			var answer2 = Serializer.DeserializeFromString(s);
			Assert.AreEqual(answer, answer2);
		}

		private class Car
		{
			public Car(Company manufacturer, string model, int year)
			{
				Manufacturer = manufacturer;
				Model = model;
				Year = year;
			}

			public Company Manufacturer { get; private set; }

			public string Model { get; private set; }

			public int Year { get; private set; }

			public override string ToString() => $"{Year} {Manufacturer} {Model}";
		}

		private class Company
		{
			public Company(string name)
			{
				Name = name;
			}

			public string Name { get; set; }

			public override string ToString()
			{
				return Name;
			}
		}

		private class Person
		{
			public Person(string name)
			{
				Name = name;
			}

			public string Name { get; set; }

			public Person Partner { get; set; }

			public override string ToString() => $"{Name ?? "Nobody"}, whose partner is {Partner?.Name ?? "nobody"}";
		}
	}
}