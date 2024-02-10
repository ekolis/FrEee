using FrEee.Modding;
using FrEee.Serialization;
using FrEee.Extensions;
using FrEee.WinForms.Controls;
using FrEee.WinForms.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FrEee.Objects.GameState;

namespace FrEee.WinForms;

/// <summary>
/// Displays a grid of properties for an object.
/// </summary>
public partial class GamePropertyGrid : UserControl, IBindable<object>
{
	public GamePropertyGrid()
	{
		InitializeComponent();
	}

	public object Data { get; private set; }

	public void Bind(object data)
	{
		Data = data;
		Bind();
	}

	public void Bind()
	{
		table.RowStyles.Clear();
		table.Controls.Clear();
		if (Data != null)
		{
			if (Data is IEnumerable)
			{
				// list or dictionary or whatever
				var type = Data.GetType();
				var list = Data as IEnumerable;
				Type itemType = null;
				bool isDict;
				if (type.GetGenericArguments().Length == 2)
				{
					// HACK - assume it's a dictionary, no real way to test
					itemType = typeof(KeyValuePair<,>).MakeGenericType(type.GetGenericArguments());
					isDict = true;
				}
				else if (type.BaseType.GetGenericArguments().Length == 2)
				{
					// HACK - Resources inherits from a dictionary type
					itemType = typeof(KeyValuePair<,>).MakeGenericType(type.BaseType.GetGenericArguments());
					isDict = true;
				}
				else if (type.GetGenericArguments().Length == 1)
				{
					// HACK - assume it's a collection, no real way to test
					itemType = type.GetGenericArguments()[0];
					isDict = false;
				}
				else
				{
					// no generic type? probably a list of objects?
					itemType = typeof(object);
					isDict = false;
				}

				int row = 0;
				foreach (var item in list)
				{
					table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
					var lbl = new Label();
					if (isDict)
						lbl.Text = item.GetPropertyValue("Key").ToString();
					else
						lbl.Text = "Item " + row;
					table.Controls.Add(lbl, 0, row);
					var editor = MakeEditor(itemType, item);
					table.Controls.Add(editor, 1, row);
					row++;
				}
			}
			else
			{
				// regular object
				var ctx = new ObjectGraphContext();
				var props = ctx.GetProperties(Data.GetType());
				int row = 0;
				foreach (var prop in props.Values)
				{
					table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
					var lbl = new Label();
					lbl.Text = prop.Name.ToSpacedString().Capitalize();
					table.Controls.Add(lbl, 0, row);
					var editor = MakeEditor(prop.PropertyType, prop.GetValue(Data, null));
					table.Controls.Add(editor, 1, row);
					row++;
				}
			}
		}
	}

	private void BindDropdownPanel<T>(Panel p, IEnumerable<T> options, T selected)
	{
		var ddl = p.Controls.OfType<ComboBox>().Single();
		ddl.Items.Clear();
		foreach (var option in options)
			ddl.Items.Add(new Option<T>(option));
		ddl.SelectedItem = selected;
	}

	private T FindControl<T>(Control ctl)
	{
		if (ctl is T)
			return (T)(object)ctl;
		foreach (Control c2 in ctl.Controls)
		{
			if (c2 is T)
				return (T)(object)c2;
		}
		foreach (Control c2 in ctl.Controls)
		{
			var c3 = FindControl<T>(c2);
			if (c3 != null)
				return c3;
		}
		return default(T);
	}

	private T GetEditorValue<T>(Control ctl)
	{
		return default(T); // TODO
	}

	private Panel MakeDropdownPanel<T>(IEnumerable<T> options, T selected, EventHandler newItemHandler)
	{
		var pnl = new Panel();

		var ddl = new ComboBox();
		ddl.DropDownStyle = ComboBoxStyle.DropDownList;
		ddl.Dock = DockStyle.Fill;
		pnl.Controls.Add(ddl);

		if (newItemHandler != null)
		{
			var btn = new GameButton();
			btn.Text = "...";
			btn.Dock = DockStyle.Right;
			btn.Click += newItemHandler;
			pnl.Controls.Add(btn);
		}

		BindDropdownPanel(pnl, options, selected);

		return pnl;
	}

	private Control MakeEditor(Type objectType, object obj)
	{
		Control ctl;

		if (TypeMatch<ModReference<IModObject>>(objectType))
		{
			var objs = Mod.Current.Objects.Where(r => objectType.GetGenericArguments()[0].IsAssignableFrom(r.GetType())).ToList();
			objs.Insert(0, null);
			var pnl = MakeDropdownPanel(objs, obj, newModObjectHandler);
			ctl = pnl;
		}
		else if (TypeMatch<GalaxyReference<IReferrable>>(objectType))
		{
			var objs = Galaxy.Current.Referrables.Where(r => objectType.GetGenericArguments()[0].IsAssignableFrom(r.GetType())).ToList();
			objs.Insert(0, null);
			var pnl = MakeDropdownPanel(objs, obj, newReferrableHandler);
			ctl = pnl;
		}
		else if (TypeMatch<IModObject>(objectType))
		{
			var objs = Mod.Current.Objects.Where(r => objectType.IsAssignableFrom(r.GetType())).ToList();
			objs.Insert(0, null);
			var pnl = MakeDropdownPanel(objs, obj, newModObjectHandler);
			ctl = pnl;
		}
		else if (TypeMatch<IReferrable>(objectType))
		{
			var objs = Galaxy.Current.Referrables.Where(r => objectType.IsAssignableFrom(r.GetType())).ToList();
			objs.Insert(0, null);
			var pnl = MakeDropdownPanel(objs, obj, newReferrableHandler);
			ctl = pnl;
		}
		else if (objectType.IsEnum)
		{
			if (objectType.HasAttribute<FlagsAttribute>())
			{
				var chklist = new CheckedListBox();
				var objs = Enum.GetValues(objectType).Cast<object>().Distinct();
				foreach (var val in objs)
					chklist.Items.Add(val, ((int)obj & (int)val) != 0);
				ctl = chklist;
			}
			else
			{
				var objs = Enum.GetValues(objectType);
				var pnl = MakeDropdownPanel(objs.Cast<object>(), obj, null);
				ctl = pnl;
			}
		}
		else if (objectType.IsPrimitive || objectType == typeof(string) || TypeMatch<IFormula>(objectType))
		{
			// TODO - custom editors for each primitive, enum, and and formula type?
			// but most stuff is formulas or references anyway
			var txt = new TextBox();
			txt.Text = obj == null ? null : obj.ToString();
			ctl = txt;
		}
		else if (TypeMatch<IEnumerable<object>>(objectType))
		{
			var lbl = new Label();
			lbl.Text = obj == null ? "<null>" : "{0} items".F((obj as IEnumerable<object>).Count());
			ctl = lbl;
		}
		else
		{
			// unknown type, just tostring it
			var lbl = new Label();
			lbl.Text = obj == null ? "<null>" : obj.ToString();
			ctl = lbl;
		}

		return ctl;
	}

	private void newModObjectHandler(object sender, EventArgs e)
	{
	}

	private void newReferrableHandler(object sender, EventArgs e)
	{
	}

	private bool TypeMatch<T>(Type t)
	{
		return typeof(T).IsAssignableFrom(t);
	}

	private bool TypeMatch(Type tp, Type tc)
	{
		return tp.IsAssignableFrom(tc) || tp.IsAssignableFrom(tc.GetGenericTypeDefinition());
	}

	private class Option<T>
	{
		public Option(T value)
		{
			Value = value;
		}

		private T Value { get; set; }

		public override string ToString()
		{
			return Value == null ? "<null>" : Value.ToString();
		}
	}
}