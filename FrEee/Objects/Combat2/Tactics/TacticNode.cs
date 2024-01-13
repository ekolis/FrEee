using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Enumerations;
using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Civilization;
using FrEee.Modding;
using FrEee.Modding.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;

namespace FrEee.Game.Objects.Combat2.Tactics
{
	/// <summary>
	/// An input or output node of a tactic block.
	/// </summary>
	public interface ITacticNode : IErrorProne
	{
		/// <summary>
		/// The value of the node.
		/// </summary>
		object Value { get; }

		/// <summary>
		/// The desired data type of the node.
		/// </summary>
		Type Type { get; }
	}

	/// <summary>
	/// An input node providing data to a tactic block.
	/// </summary>
	public abstract class TacticInput : ITacticNode
	{
		public TacticInput(TacticBlock block, Type type = null, TacticOutput connection = null)
		{
			Block = block;
			Type = type ?? typeof(object);
			ConnectedOutput = connection;
		}

		public TacticBlock Block { get; private set; }

		public Type Type { get; private set; }

		private TacticOutput connectedOutput;

		public TacticOutput ConnectedOutput
		{
			get
			{
				return connectedOutput;
			}
			set
			{
				CheckType(value.Type);
				connectedOutput = value;
			}
		}

		public abstract object Value { get; }

		private void CheckType(object val)
		{
			if (Type.IsValueType && val == null)
				throw new Exception("Cannot set value of null to " + Type + ".");
			if (val != null)
				CheckType(val.GetType());
		}

		private void CheckType(Type type)
		{
			if (!Type.IsAssignableFrom(type))
				throw new Exception("Cannot set object of type " + type + " to " + Type + ".");
		}

		public virtual IEnumerable<string> Errors
		{
			get
			{
				if (!Type.IsAssignableFrom(ConnectedOutput.Type))
					yield return "Cannot connect output of type {0} to input of type {1}.".F(ConnectedOutput.Type, Type);
			}
		}
	}

	/// <summary>
	/// A tactic input which uses a formula for its default value.
	/// </summary>
	public class TacticFormulaInput : TacticInput
	{
		public TacticFormulaInput(TacticBlock block, Type type = null, IFormula defaultValue = null, TacticOutput connection = null)
			: base(block, type, connection)
		{
			DefaultValue = defaultValue;
		}

		public IFormula DefaultValue { get; private set; }

		public override object Value
		{
			get
			{
				if (ConnectedOutput == null)
					return DefaultValue.Value;
				else
					return ConnectedOutput.Value;
			}
		}

		// TODO - check if formula compiles?
	}

	/// <summary>
	/// A tactic input which just uses an object for its default value.
	/// </summary>
	public class TacticObjectInput : TacticInput
	{
		public TacticObjectInput(TacticBlock block, Type type = null, object defaultValue = null, TacticOutput connection = null)
			: base(block, type, connection)
		{
			DefaultValue = defaultValue;
		}

		public object DefaultValue { get; private set; }

		public override object Value
		{
			get
			{
				if (ConnectedOutput == null)
					return DefaultValue;
				return ConnectedOutput.Value;
			}
		}
	}

	/// <summary>
	/// An output node of a tactic block, providing data to other blocks or to the game itself.
	/// </summary>
	public abstract class TacticOutput : ITacticNode
	{
		public TacticOutput(TacticBlock block, Type type = null)
		{
			Block = block;
			Type = type ?? typeof(object);
		}

		public TacticBlock Block { get; private set; }

		public Type Type { get; private set; }

		public IEnumerable<TacticInput> ConnectedInputs
		{
			get
			{
				return Block.Parent.Children.SelectMany(b => b.Inputs).Select(kvp => kvp.Value).Where(ti => ti.ConnectedOutput == this);
			}
		}

		public abstract object Value { get; }

		public virtual IEnumerable<string> Errors
		{
			get { yield break; }
		}
	}

	/// <summary>
	/// A tactic output which commputes a value based on a formula using the block's inputs as variables.
	/// </summary>
	public class TacticFormulaOutput : TacticOutput
	{
		public TacticFormulaOutput(TacticBlock block, Type type = null, IFormula formula = null)
			: base(block, type)
		{
			Formula = formula;
		}

		/// <summary>
		/// The formula to use to compute the output value based on the inputs.
		/// </summary>
		public IFormula Formula { get; set; }

		public override object Value
		{
			get
			{
				var variables = new SafeDictionary<string, object>();
				foreach (var input in Block.Inputs)
					variables.Add(input.Key, input.Value.Value);
				return Formula.Evaluate(variables);
			}
		}

		// TODO - check if formula compiles?
	}

	/// <summary>
	/// A tactic output which selects a property value from an input.
	/// </summary>
	public class TacticPropertyOutput : TacticOutput
	{
		public TacticPropertyOutput(TacticBlock block, string inputName, string propertyName, Type type = null)
			: base(block, type)
		{
			InputName = inputName;
			PropertyName = propertyName;
		}

		/// <summary>
		/// The name of the input to select the property value from.
		/// </summary>
		public string InputName { get; set; }

		/// <summary>
		/// The name of the property to select.
		/// </summary>
		public string PropertyName { get; set; }

		public override object Value
		{
			get
			{
				var inputObj = Block.Inputs[InputName].Value;
				return inputObj.GetPropertyValue(PropertyName);
			}
		}

		public override IEnumerable<string> Errors
		{
			get
			{
				foreach (var e in base.Errors)
					yield return e;

				if (Block.Inputs[InputName] == null)
				{
					yield return "Block {0} has no input named {1}.".F(Block.Name, InputName);
					yield break;
				}
				if (!Block.Inputs[InputName].Type.GetProperties().Any(p => p.Name == PropertyName))
				{
					yield return "Type {0} has no property named {1}.".F(Block.Inputs[InputName].Type, PropertyName);
					yield break;
				}
				if (!Block.Inputs[InputName].Type.GetProperties().Any(p => p.Name == PropertyName && Type.IsAssignableFrom(p.PropertyType)))
				{
					yield return "Type {0}'s {1} property is not compatible with output type {3}.".F(Block.Inputs[InputName].Type, PropertyName, Type);
					yield break;
				}
			}
		}
	}

	/// <summary>
	/// A tactic output which accepts a connection from another tactic node within the same block.
	/// Either an input node, or an output node of a sub-block.
	/// </summary>
	public class TacticConnectionOutput : TacticOutput
	{
		public TacticConnectionOutput(TacticBlock block, bool mandatory, Type type = null, ITacticNode connectedNode = null)
			: base(block, type)
		{
		}

		public ITacticNode ConnectedNode { get; set; }

		/// <summary>
		/// Needs to be connected?
		/// </summary>
		public bool IsMandatory { get; set; }

		public override object Value
		{
			get { return ConnectedNode.Value; }
		}

		public override IEnumerable<string> Errors
		{
			get
			{
				foreach (var e in base.Errors)
					yield return e;

				if (IsMandatory && ConnectedNode == null)
					yield return "Output is not connected.";
			}
		}
	}
}
