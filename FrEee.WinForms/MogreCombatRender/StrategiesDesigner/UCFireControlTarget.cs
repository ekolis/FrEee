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
    public partial class UCFireControlTarget : UserControlBaseObj
    {
        List<MountedComponentTemplate> weapons;
        public StrategyComObj tgt { get; set; }
        public UCLinkObj linkTgt { get; set; }
        public ListBox lbx_wpns = new ListBox();

        public UCFireControlTarget(StratMainForm ParentForm, Canvasdata canvasdata)
            : base("FireControl", ParentForm, canvasdata)
        {
            InitializethisComponent();
            
            weapons = new List<MountedComponentTemplate>();
            lbx_wpns.DataSource = weapons;

            lbx_wpns.AllowDrop = true;

            tgt = new StrategyComObj();

            linkTgt = new UCLinkObj(ParentForm, this, tgt, true, 0);
            linkTgt.CheckAlign = ContentAlignment.MiddleLeft;
            linkTgt.Text = "Target Object";
            
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
            this.GameTableLayoutPanel1.SetRow(linkTgt, 1);
            this.GameTableLayoutPanel1.SetColumn(linkTgt, 0);
            this.GameTableLayoutPanel1.SetColumnSpan(linkTgt, 1);
            this.GameTableLayoutPanel1.Controls.Add(linkTgt);
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


            this.Dock = DockStyle.Fill;

        }

        private void InitializethisComponent()
        {
            this.lbx_wpns.DragDrop += new System.Windows.Forms.DragEventHandler(this.lb_DragDrop);
            this.lbx_wpns.DragEnter += new System.Windows.Forms.DragEventHandler(this.lb_DragEnter);
            this.lbx_wpns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_MouseDown);
            InitializeComponent();
        }

        public List<MountedComponentTemplate> Weapons
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
            this.lbx_wpns.DataSource = this.weapons;
        }

        private void lb_MouseDown(object sender, MouseEventArgs e)
        {
            int itemindex = lbx_wpns.IndexFromPoint(e.X, e.Y);
            if( itemindex >=0 && itemindex < lbx_wpns.Items.Count)
            {
                object[] data = new object[]{this, lbx_wpns.Items[itemindex]};
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
            MountedComponentTemplate wpn = (MountedComponentTemplate)data[1];

            if (wpn != null)
            {
                this.weapons.Add(wpn);
                this.updateWpnLst();
                from.weapons.Remove(wpn);
                from.updateWpnLst();                
            }
        }
    }
}
