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
        StrategyComObj tgt { get; set; }

        public UCFireControlTarget(StratMainForm ParentForm, Canvasdata canvasdata)
            : base("FireControl", ParentForm, canvasdata)
        {
            InitializeComponent();
            weapons = new List<MountedComponentTemplate>();
            ListBox lb = new ListBox();
            lb.DataSource = weapons.ToList();

            tgt = new StrategyComObj();

            UCLinkObj linkTgt = new UCLinkObj(ParentForm, this, tgt, true, 0);
            linkTgt.CheckAlign = ContentAlignment.MiddleLeft;
            linkTgt.Text = "Target Object";


            GameTableLayoutPanel1.RowCount = 3;
            GameTableLayoutPanel1.ColumnCount = 2;
            while (GameTableLayoutPanel1.RowStyles.Count < GameTableLayoutPanel1.RowCount)
            {
                RowStyle rowstyle = new RowStyle(SizeType.Absolute, 24);
                GameTableLayoutPanel1.RowStyles.Add(rowstyle);
            }
            foreach (RowStyle rowstyle in GameTableLayoutPanel1.RowStyles)
                rowstyle.SizeType = SizeType.Absolute;
            GameTableLayoutPanel1.RowStyles[0].Height = 24;
            GameTableLayoutPanel1.RowStyles[1].Height = 24;
            GameTableLayoutPanel1.RowStyles[2].Height = 48;

            GameTableLayoutPanel1.SetRow(linkTgt, 1);
            GameTableLayoutPanel1.SetColumn(linkTgt, 0);
            GameTableLayoutPanel1.SetColumnSpan(linkTgt, 2);

            GameTableLayoutPanel1.SetRow(lb, 2);
            GameTableLayoutPanel1.SetColumn(lb, 0);
            GameTableLayoutPanel1.SetColumnSpan(lb, 2);
            lb.Dock = DockStyle.Fill;

            GameTableLayoutPanel1.Controls.Add(linkTgt);
            GameTableLayoutPanel1.Controls.Add(lb);
            this.Dock = DockStyle.Fill;
        }
    }
}
