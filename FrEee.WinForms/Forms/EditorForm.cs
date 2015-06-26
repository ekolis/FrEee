using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;

namespace FrEee.WinForms.Forms
{
	public partial class EditorForm : Form, IBindable<object>
	{
		public EditorForm(object root, Func<bool> save, Func<bool> cancel)
		{
			InitializeComponent();
			try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
			catch { }
			Bind(root);
			this.save = save;
			this.cancel = cancel;
		}

		public object Root { get; private set; }

		private Func<bool> save, cancel;

		public void Bind(object data)
		{
			Root = data;
			Bind();
		}

		public void Bind()
		{
			tree.Nodes.Clear();
			if (Root != null)
			{
				PopulateSubNodes(Root, AddNode(null, Root));
			}
			else
			{
				tree.Nodes.Add("<null>");
			}
			propertyGrid.Bind(Root);
		}

		private void PopulateSubNodes(object obj, TreeNode node)
		{
			node.Nodes.Clear();
			foreach (var kvp in GetSubItems(obj))
				AddNode(node, kvp.Key, kvp.Value);
		}

		private IEnumerable<KeyValuePair<string, object>> GetSubItems(object obj)
		{
			// not recursive, that would take forever and probably cause an infinite loop
			if (obj != null)
			{
				if (obj is IEnumerable)
				{
					// list, dictionary, etc.
					foreach (var item in obj as IEnumerable)
						yield return new KeyValuePair<string, object>(item.ToString(), item);
				}
				else
				{
					// regular object
					var ctx = new ObjectGraphContext();
					var props = ctx.GetProperties(obj.GetType());
					foreach (var prop in props)
						yield return new KeyValuePair<string, object>(prop.Name, prop.GetValue(obj, null));
				}
			}	
		}

		private TreeNode AddNode(string propname, object obj)
		{
			if (obj == null)
				return tree.AddItemWithImage(propname ?? "<null>", obj, null);
			return tree.AddItemWithImage(propname ?? obj.ToSpacedString().Capitalize(), obj, obj is IPictorial ? (obj as IPictorial).Icon : null);
		}

		private TreeNode AddNode(TreeNode parent, string propname, object obj)
		{
			if (obj == null)
				return parent.AddItemWithImage(propname ?? "<null>", obj, null);
			return parent.AddItemWithImage(propname ?? obj.ToSpacedString().Capitalize(), obj, obj is IPictorial ? (obj as IPictorial).Icon : null);
		}

		private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				propertyGrid.Bind(e.Node.Tag);
				PopulateSubNodes(e.Node.Tag, e.Node);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (cancel())
				Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (save())
				Close();
		}
	}
}
