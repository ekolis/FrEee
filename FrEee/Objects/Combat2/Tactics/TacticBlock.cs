using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Utility;

namespace FrEee.Game.Objects.Combat2.Tactics
{
	/// <summary>
	/// A logic block used in combat tactics.
	/// </summary>
	/// <remarks>
	/// </remarks>
	public abstract class TacticBlock : IErrorProne
	{
		protected TacticBlock(TacticBlock parent, string defaultName, string customName = null)
		{
			DefaultName = defaultName;
			CustomName = customName;
			Parent = parent;
			Children = new ObservableCollection<TacticBlock>();
			Children.CollectionChanged += Children_CollectionChanged;
			Inputs = new SafeDictionary<string, TacticInput>();
			Outputs = new SafeDictionary<string, TacticOutput>();
		}

		/// <summary>
		/// The default name of this block, based on what kind of block it is.
		/// </summary>
		public string DefaultName { get; private set; }

		/// <summary>
		/// The custom override name of this block.
		/// </summary>
		public string CustomName { get; set; }

		/// <summary>
		/// The name of this block.
		/// </summary>
		[DoNotSerialize(false)]
		public string Name { get { return CustomName ?? DefaultName; } set { CustomName = value; } }

		private TacticBlock parent;

		/// <summary>
		/// The block to which this block belongs.
		/// </summary>
		public TacticBlock Parent
		{
			get
			{
				return parent;
			}
			private set
			{
				if (parent != value)
				{
					parent.Children.Remove(this);
					parent = value;
					parent.Children.Add(this);
				}
			}
		}

		/// <summary>
		/// Any blocks belonging to this block.
		/// </summary>
		public ObservableCollection<TacticBlock> Children { get; private set; }

		void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			foreach (TacticBlock del in e.OldItems)
				del.Parent = null;
			foreach (TacticBlock add in e.NewItems)
				add.Parent = this;
		}

		/// <summary>
		/// Inputs to this tactic block.
		/// </summary>
		public SafeDictionary<string, TacticInput> Inputs { get; private set; }

		/// <summary>
		/// Outputs to this tactic block.
		/// </summary>
		public SafeDictionary<string, TacticOutput> Outputs { get; private set; }

		public virtual IEnumerable<string> Errors
		{
			get
			{ 
				foreach (var i in Inputs)
				{
					foreach (var e in i.Value.Errors)
					{
						yield return Name + "'s " + i.Key + " input: " + e;
					}
				}
				foreach (var o in Outputs)
				{
					foreach (var e in o.Value.Errors)
					{
						yield return Name + "'s " + o.Key + " output: " + e;
					}
				}
			}
		}
	}
}
