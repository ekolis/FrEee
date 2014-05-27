using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FrEee.Game.Objects.Technology;
using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    public partial class UCFireControlTarget : UCStratBlock
    {
        Dictionary<int, MountedComponentTemplate> weapons;
        public StrategyComObj tgt { get { return (StrategyComObj)base.stratblock; } set { base.stratblock = value; } }
        //public UCLinkObj linkTgt { get; set; }
        public ListBox lbx_wpns = new ListBox();

        public UCFireControlTarget(StratMainForm ParentForm, Canvasdata canvasdata)
            : base(new StrategyComObj(), ParentForm, canvasdata, false)
        {
            InitializethisComponent();
            
            weapons = new Dictionary<int, MountedComponentTemplate>();
            //lbx_wpns.DataSource = weapons;

            lbx_wpns.AllowDrop = true;

            //tgt = (StrategyComObj);



            this.Dock = DockStyle.Fill;
        }

        protected override void createInputs(StratMainForm parentForm)
        {
            //base.createInputs(parentForm);
            inputlinks.Add(new UCLinkObj(parentForm, this, tgt, true, 0));
            inputlinks[0].CheckAlign = ContentAlignment.MiddleLeft;
            inputlinks[0].Text = "Target Object";

            this.GameTableLayoutPanel1.RowCount = 3;
            while (GameTableLayoutPanel1.RowStyles.Count < 3)
                GameTableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));

            this.GameTableLayoutPanel1.AutoSize = true;
            this.GameTableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;


            this.GameTableLayoutPanel1.ColumnCount = 1;
            this.GameTableLayoutPanel1.SetRow(lbl_FunctName, 0);
            this.GameTableLayoutPanel1.SetColumn(lbl_FunctName, 0);
            this.GameTableLayoutPanel1.SetColumnSpan(lbl_FunctName, 1);

            this.GameTableLayoutPanel1.RowCount += 1;
            this.GameTableLayoutPanel1.SetRow(inputlinks[0], 1);
            this.GameTableLayoutPanel1.SetColumn(inputlinks[0], 0);
            this.GameTableLayoutPanel1.SetColumnSpan(inputlinks[0], 1);
            this.GameTableLayoutPanel1.Controls.Add(inputlinks[0]);
            RowStyle style_linkTgt = new RowStyle(SizeType.Absolute, 24);
            this.GameTableLayoutPanel1.RowStyles[1] = style_linkTgt;

            this.GameTableLayoutPanel1.RowCount += 1;
            this.GameTableLayoutPanel1.SetRow(lbx_wpns, 2);
            this.GameTableLayoutPanel1.SetColumn(lbx_wpns, 0);
            this.GameTableLayoutPanel1.SetColumnSpan(lbx_wpns, 1);
            this.GameTableLayoutPanel1.Controls.Add(lbx_wpns);
            lbx_wpns.Dock = DockStyle.Fill;
            RowStyle style_lb = new RowStyle(SizeType.Percent, 100);
            this.GameTableLayoutPanel1.RowStyles[2] = style_lb;
        }

        private void InitializethisComponent()
        {
            this.lbx_wpns.DragDrop += new System.Windows.Forms.DragEventHandler(this.lb_DragDrop);
            this.lbx_wpns.DragEnter += new System.Windows.Forms.DragEventHandler(this.lb_DragEnter);
            this.lbx_wpns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_MouseDown);
            InitializeComponent();
        }

        public Dictionary<int, MountedComponentTemplate> Weapons
        {
            get { return this.weapons; }
            set
            {
                this.weapons = value;
                updateWpnLst();
            }
        }

        public void updateWpnLst()
        {
            this.lbx_wpns.DataSource = null;
            if (this.weapons.Count > 0)
            {
                this.lbx_wpns.DataSource = new BindingSource(this.weapons, null);
                this.lbx_wpns.DisplayMember = "Value";
                this.lbx_wpns.ValueMember = "Key";
            }
        }

        private void lb_MouseDown(object sender, MouseEventArgs e)
        {
            int itemindex = lbx_wpns.IndexFromPoint(e.X, e.Y);
            if( itemindex >=0 && itemindex < lbx_wpns.Items.Count)
            {
                var kvp = (KeyValuePair<int, MountedComponentTemplate>)lbx_wpns.Items[itemindex];
                object[] data = new object[]{this, kvp};
                this.DoDragDrop(data, DragDropEffects.Move);
            }
        }

        private void lb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lb_DragDrop(object sender, DragEventArgs e)
        {
            object[] data = (object[])e.Data.GetData(typeof(object[]));
            UCFireControlTarget from = (UCFireControlTarget)data[0];
            KeyValuePair<int, MountedComponentTemplate> wpnkvp = (KeyValuePair<int, MountedComponentTemplate>)data[1];

            this.weapons.Add(wpnkvp.Key, wpnkvp.Value);
            this.updateWpnLst();
            from.weapons.Remove(wpnkvp.Key);
            from.updateWpnLst();                
            
        }
    }
}
