using FrEee.Game.Interfaces;
using FrEee.Utility;
using FrEee.Utility.Extensions;
using FrEee.WinForms.Interfaces;
using FrEee.WinForms.Utility.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrEee.WinForms.Forms
{
    public partial class EditorForm : Form, IBindable<object>
    {
        #region Private Fields

        private Func<bool> save, cancel;

        #endregion Private Fields

        #region Public Constructors

        public EditorForm(object root, Func<bool> save, Func<bool> cancel)
        {
            InitializeComponent();
            try { this.Icon = new Icon(FrEee.WinForms.Properties.Resources.FrEeeIcon); }
            catch { }
            Bind(root);
            this.save = save;
            this.cancel = cancel;
        }

        #endregion Public Constructors

        #region Public Properties

        public object Root { get; private set; }

        #endregion Public Properties

        #region Public Methods

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

        #endregion Public Methods

        #region Private Methods

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
                        yield return new KeyValuePair<string, object>(prop.Key, prop.Value.GetValue(obj, null));
                }
            }
        }

        private void PopulateSubNodes(object obj, TreeNode node)
        {
            node.Nodes.Clear();
            foreach (var kvp in GetSubItems(obj))
                AddNode(node, kvp.Key, kvp.Value);
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                propertyGrid.Bind(e.Node.Tag);
                PopulateSubNodes(e.Node.Tag, e.Node);
            }
        }

        #endregion Private Methods
    }
}
