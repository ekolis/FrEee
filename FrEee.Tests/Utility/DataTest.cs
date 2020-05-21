using FrEee.Game.Objects.Space;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace FrEee.Tests.Utility
{
	/// <summary>
	/// Tests data operations.
	/// </summary>
	public class DataTest
	{
		private Person barack, michelle, malia, sasha;

		[OneTimeSetUp]
		public static void ClassInit()
		{
			// silly unit tests can't find their own assembly
			SafeType.ForceLoadType(typeof(DataTest));
			SafeType.ForceLoadType(typeof(Person));
		}

		/// <summary>
		/// Tests simple data on an object that is not an IDataObject.
		/// </summary>
		[Test]
		public void AnyObjectSimpleData()
		{
			var timmy = new Person("Timmy", null, null);
			var lassie = new Dog("Lassie", timmy);

			var simple = new SimpleDataObject(lassie, null);
			simple.InitializeData();
			Assert.AreEqual(lassie.Name, simple.Data[nameof(lassie.Name)]);
			Assert.AreEqual(lassie.Owner, simple.Data[nameof(lassie.Owner)]);
			var clone = (Dog)simple.Value;
			Assert.AreEqual(lassie.Name, clone.Name);
			Assert.AreEqual(lassie.Owner, clone.Owner);
		}

		///// <summary>
		///// Tests sending simple data over app domain boundaries.
		///// </summary>
		//[Test]
		//public void AppDomains()
		//{
		//	// make a sandbox
		//	var sandbox = BuildSandbox();

		//	// can we send Barack over?
		//	sandbox.SetData("data", new SimpleDataObject(barack));

		//	// can we make a new person (well, person data) over there and poke him?
		//	var data = (SimpleDataObject)sandbox.CreateInstanceAndUnwrap(Assembly.GetAssembly(typeof(SimpleDataObject)).FullName, typeof(SimpleDataObject).FullName);
		//	var nobody = new Person(null, null, null);
		//	data.Data = nobody.Data;
		//	nobody.Data = data.Data;
		//	Assert.AreEqual("Hi, I'm nobody!", nobody.SayHi());
		//}

		/// <summary>
		/// Tests full-fledged (object oriented) data operations.
		/// </summary>
		[Test]
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
		[Test]
		public void SimpleData()
		{
			var simple = new SimpleDataObject(barack, null);
			simple.InitializeData();
			Assert.AreEqual(barack.Name, simple.Data[nameof(barack.Name)]);
			Assert.AreEqual(barack.Children, simple.Data[nameof(barack.Children)]);
			var clone = (Person)simple.Value;
			Assert.AreEqual(barack.Name, clone.Name);
			Assert.AreEqual(barack.Children, clone.Children); // well, the DNA test would say they're the clone's as well ;)
		}

		///// <summary>
		///// Tests serializing game state over the simple data protocol.
		///// </summary>
		//[Test]
		//public void SimpleDataGameState()
		//{
		//	Galaxy.Load("freeefurball_1.gam");
		//	var gal = Galaxy.Current;
		//	var simple = new SimpleDataObject(gal);
		//	var sandbox = BuildSandbox();
		//	simple.Context.KnownObjects.Clear();
		//	sandbox.SetData("galaxy", simple);
		//	// can we make a new galaxy over there and get some data out?
		//	var data = (SimpleDataObject)sandbox.CreateInstanceAndUnwrap(Assembly.GetAssembly(typeof(SimpleDataObject)).FullName, typeof(SimpleDataObject).FullName);
		//	data.SimpleData = simple.SimpleData;
		//	var data2 = new SimpleDataObject();
		//	data2.SimpleData = data.SimpleData;
		//	var galcopy = data2.Reconstitute<Galaxy>();
		//	Assert.AreEqual(gal.Empires[0].Name, galcopy.Empires[0].Name);
		//}

		[SetUp]
		public void TestInit()
		{
			barack = new Person("Barack", null, null);
			michelle = new Person("Michelle", null, null);
			malia = new Person("Malia", barack, michelle);
			sasha = new Person("Sasha", barack, michelle);
		}

		//private AppDomain BuildSandbox()
		//{
		//	//Setting the AppDomainSetup. It is very important to set the ApplicationBase to a folder
		//	//other than the one in which the sandboxer resides.
		//	AppDomainSetup adSetup = new AppDomainSetup();
		//	adSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
		//	adSetup.ApplicationName = "FrEee";
		//	adSetup.DynamicBase = "ScriptEngine";

		//	//Setting the permissions for the AppDomain. We give the permission to execute and to
		//	//read/discover the location where the untrusted code is loaded.
		//	var evidence = new Evidence();
		//	evidence.AddHostEvidence(new Zone(SecurityZone.MyComputer));
		//	var permissions = SecurityManager.GetStandardSandbox(evidence);
		//	var reflection = new ReflectionPermission(PermissionState.Unrestricted);
		//	permissions.AddPermission(reflection);

		//	//Now we have everything we need to create the AppDomain, so let's create it.
		//	return AppDomain.CreateDomain("Test", null, adSetup, permissions, AppDomain.CurrentDomain.GetAssemblies().Select(a => a.Evidence.GetHostEvidence<StrongName>()).Where(sn => sn != null).ToArray());
		//}

		private class Dog
		{
			public Dog(string name, Person owner)
			{
				Name = Name;
				Owner = owner;
			}

			public string Name { get; set; }
			public Person Owner { get; private set; }
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

			public Person Father { get; private set; }
			public Person Mother { get; private set; }
			public string Name { get; set; }

			public string SayHi()
			{
				return $"Hi, I'm {Name ?? "nobody"}!";
			}
		}
	}
}