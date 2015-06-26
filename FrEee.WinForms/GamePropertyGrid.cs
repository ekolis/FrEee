using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using FrEee.Utility;
using System.Collections;

namespace FrEee.WinForms
{
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
					foreach (var prop in props)
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

		private Control MakeEditor(Type objectType, object obj)
		{
			Control result;

			// TODO - make proper editor
			var lbl = new Label();
			lbl.Text = obj == null ? "<null>" : obj.ToString();
			result = lbl;

			return result;
		}
	}
}
