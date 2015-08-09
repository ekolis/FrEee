using System;
using System.Collections.Generic;
using System.Linq;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FrEee.Tests.Utility.Extensions
{
	/// <summary>
	/// Tests extension methods.
	/// </summary>
	[TestClass]
	public class DataTest
	{
		[TestInitialize]
		public void TestInit()
		{
			barack = new Person("Barack", null, null);
			michelle = new Person("Michelle", null, null);
			malia = new Person("Malia", barack, michelle);
			sasha = new Person("Sasha", barack, michelle);
		}

		private Person barack, michelle, malia, sasha;

		/// <summary>
		/// Tests full-fledged (object oriented) data operations.
		/// </summary>
		[TestMethod]
		public void Data()
		{
			var data = barack.Data;
			Assert.AreEqual(barack.Name, data[nameof(barack.Name)]);
			Assert.AreEqual(barack.Children, data[nameof(barack.Children)]);
			var clone = new Person(null, null, null);
			clone.Data = data;
			Assert.AreEqual(barack.Name, clone.Name);
			Assert.AreEqual(barack.Children, clone.Children); // well, the DNA test would say they're the clone's as well ;)
		}

		/// <summary>
		/// Tests simple (string-only) data operations.
		/// </summary>
		[TestMethod]
		public void SimpleData()
		{
			var simple = new SimpleDataObject<Person>(barack, barack);
			Assert.AreEqual(barack.Name, simple.Data[nameof(barack.Name)]);
			Assert.AreEqual(barack.Children, simple.Data[nameof(barack.Children)]);
			var clone = simple.Value;
			Assert.AreEqual(barack.Name, clone.Name);
			Assert.AreEqual(barack.Children, clone.Children); // well, the DNA test would say they're the clone's as well ;)
		}

		private class Person : IDataObject
		{
			public Person(string name, Person father, Person mother)
			{
				Name = name;
				Mother = mother;
				Father = father;
				if (Mother != null)
					Mother.Children.Add(this);
				if (Father != null)
					Father.Children.Add(this);
			}

			public string Name { get; private set; }

			public Person Mother { get; private set; }

			public Person Father { get; private set; }

			public ISet<Person> Children { get; private set; } = new HashSet<Person>();

			public SafeDictionary<string, object> Data
			{
				get
				{
					var dict = new SafeDictionary<string, object>();
					dict[nameof(Name)] = Name;
					dict[nameof(Mother)] = Mother;
					dict[nameof(Father)] = Father;
					dict[nameof(Children)] = Children;
					return dict;
				}

				set
				{
					Name = value[nameof(Name)].Default<string>();
					Mother = value[nameof(Mother)].Default<Person>();
					Father = value[nameof(Father)].Default<Person>();
					Children = value[nameof(Children)].Default(new HashSet<Person>());
				}
			}
		}
	}
}
