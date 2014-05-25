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
    public partial class UCWaypoint : UserControlBaseObj
    {
        //List<MountedComponentTemplate> weapons { get; set; }
        public StrategyWayPoint wpnt { get; set; }
        public List<UCLinkObj> inputlinks { get; set; }
        public UCWaypoint(StratMainForm ParentForm, Canvasdata canvasdata)
            : base("WayPoint", ParentForm, canvasdata)
        {
            InitializeComponent();




            wpnt = new StrategyWayPoint();
            inputlinks = new List<UCLinkObj>();
            this.GameTableLayoutPanel1.ColumnCount = 1;
            GameTableLayoutPanel1.ColumnCount = 1;
            GameTableLayoutPanel1.SetColumnSpan(lbl_FunctName, 1);
            GameTableLayoutPanel1.SetRow(lbl_FunctName, 0);
            GameTableLayoutPanel1.SetColumn(lbl_FunctName, 0);

            for (int i = 0; i < wpnt.inputtypes.Length; i++)
            {

                UCLinkObj linkinp = new UCLinkObj(ParentForm, this, wpnt, true, i);
                inputlinks.Add(linkinp);
                linkinp.Text = wpnt.inputtypes[i].Name;
                linkinp.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;

                this.GameTableLayoutPanel1.RowCount += 1;
                RowStyle style1 = new RowStyle(SizeType.Absolute, 24);
                this.GameTableLayoutPanel1.RowStyles.Add(style1);

                this.GameTableLayoutPanel1.SetRow(linkinp, GameTableLayoutPanel1.RowCount - 1);
                this.GameTableLayoutPanel1.SetColumn(linkinp, 0);
                this.GameTableLayoutPanel1.SetColumnSpan(linkinp, 1);
                this.Height += 24;
                this.GameTableLayoutPanel1.Controls.Add(linkinp);
            }




            //GameTableLayoutPanel1.RowCount = 3;
            //GameTableLayoutPanel1.ColumnCount = 2;
            //while (GameTableLayoutPanel1.RowStyles.Count < GameTableLayoutPanel1.RowCount)
            //{
            //    RowStyle rowstyle = new RowStyle(SizeType.Absolute, 24);
            //    GameTableLayoutPanel1.RowStyles.Add(rowstyle);
            //}
            //foreach (RowStyle rowstyle in GameTableLayoutPanel1.RowStyles)
            //    rowstyle.SizeType = SizeType.Absolute;
            //GameTableLayoutPanel1.RowStyles[0].Height = 24;
            //GameTableLayoutPanel1.RowStyles[1].Height = 24;
            //GameTableLayoutPanel1.RowStyles[2].Height = 48;

            //GameTableLayoutPanel1.SetRow(linkLoc, 1);
            //GameTableLayoutPanel1.SetColumn(linkLoc, 0);
            //GameTableLayoutPanel1.SetColumnSpan(linkLoc, 2);

  

            //GameTableLayoutPanel1.Controls.Add(linkLoc);

            this.Dock = DockStyle.Fill;
        }
    }
}

