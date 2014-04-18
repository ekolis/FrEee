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
        public List<MountedComponentTemplate> weapons { get; set; }
        public StrategyComObj tgt { get; set; }

        public ListBox lb = new ListBox();



        public UCFireControlTarget(StratMainForm ParentForm, Canvasdata canvasdata)
            : base("FireControl", ParentForm, canvasdata)
        {
            InitializethisComponent();
            
            weapons = new List<MountedComponentTemplate>();
            lb.DataSource = weapons.ToList();

            lb.AllowDrop = true;

            tgt = new StrategyComObj();

            UCLinkObj linkTgt = new UCLinkObj(ParentForm, this, tgt, true, 0);
            linkTgt.CheckAlign = ContentAlignment.MiddleLeft;
            linkTgt.Text = "Target Object";


            this.GameTableLayoutPanel1.RowCount = 1;
            while (GameTableLayoutPanel1.RowStyles.Count > 1)
                GameTableLayoutPanel1.RowStyles.RemoveAt(1);

            RowStyle style0 = new RowStyle(SizeType.Absolute, 20);
            this.GameTableLayoutPanel1.RowStyles[0] = style0;

            this.GameTableLayoutPanel1.ColumnCount = 2;

            this.GameTableLayoutPanel1.RowCount += 1;
            this.GameTableLayoutPanel1.SetRow(linkTgt, 1);
            this.GameTableLayoutPanel1.SetColumn(linkTgt, 0);
            this.GameTableLayoutPanel1.SetColumnSpan(linkTgt, 2);
            this.GameTableLayoutPanel1.Controls.Add(linkTgt);
            RowStyle style_linkTgt = new RowStyle(SizeType.Absolute, 20);
            this.GameTableLayoutPanel1.RowStyles[0] = style_linkTgt;

            this.GameTableLayoutPanel1.RowCount += 1;
            this.GameTableLayoutPanel1.SetRow(lb, 2);
            this.GameTableLayoutPanel1.SetColumn(lb, 0);
            this.GameTableLayoutPanel1.SetColumnSpan(lb, 2);
            this.GameTableLayoutPanel1.Controls.Add(lb);
            lb.Dock = DockStyle.Fill;
            RowStyle style_lb = new RowStyle(SizeType.Percent, 100);
            this.GameTableLayoutPanel1.RowStyles.Add(style_lb);

            this.Dock = DockStyle.Fill;

        }

        private void InitializethisComponent()
        {
            this.lb.DragDrop += new System.Windows.Forms.DragEventHandler(this.lb_DragDrop);
            this.lb.DragEnter += new System.Windows.Forms.DragEventHandler(this.lb_DragEnter);
            this.lb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lb_MouseDown);
            InitializeComponent();
        }

        private void lb_MouseDown(object sender, MouseEventArgs e)
        {
            this.DoDragDrop(this, DragDropEffects.All);
        }

        private void lb_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }


        private void lb_DragDrop(object sender, DragEventArgs e)
        {
            ListBox from = (ListBox)e.Data.GetData(typeof(ListBox));
            //CombatWeapon wpndrop = (CombatWeapon)e.Data.GetData(typeof(CombatWeapon));
        }
    }
}
