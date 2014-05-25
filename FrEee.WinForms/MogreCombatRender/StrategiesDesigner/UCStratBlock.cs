using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    public partial class UCStratBlock : UserControlBaseObj
    {
        public StrategyBaseBlock stratblock { get; protected set; }
        public List<UCLinkObj> inputlinks { get; set; }
        public UCLinkObj outlink { get; set; }  
        public UCStratBlock() : base() { }

        public UCStratBlock(StrategyBaseBlock stratblock, StratMainForm parentForm, Canvasdata canvasdata)
            : base(stratblock.name, parentForm, canvasdata)
        {
            this.stratblock = stratblock;
            InitializeComponent();
            inputlinks = new List<UCLinkObj>();
            this.Name = stratblock.name;
            RowStyle style0 = new RowStyle(SizeType.Absolute, 20);
            this.GameTableLayoutPanel1.RowStyles[0] = style0;

            if (stratblock.inputtypes.Count() > 0)
            {
                createInputs(parentForm);
            }
            else 
            {
                RowStyle style1 = new RowStyle(SizeType.Absolute, 24);
                this.GameTableLayoutPanel1.RowStyles.Add(style1);
                this.Height += 24;
            }
            createOutputs(parentForm);
        }

        protected virtual void createInputs(StratMainForm parentForm)
        {
            for (int i = 0; i < stratblock.inputtypes.Length; i++)
            {

                UCLinkObj linkinp = new UCLinkObj(parentForm, this, stratblock, true, i);
                this.inputlinks.Add(linkinp);
                linkinp.Text = linkinp.dataType.Name;//stratblock.inputtypes[i].Name;
                linkinp.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;

                this.GameTableLayoutPanel1.RowCount += 1;
                RowStyle style1 = new RowStyle(SizeType.Absolute, 24);
                this.GameTableLayoutPanel1.RowStyles.Add(style1);

                this.GameTableLayoutPanel1.SetRow(linkinp, GameTableLayoutPanel1.RowCount - 1);
                this.GameTableLayoutPanel1.SetColumn(linkinp, 0);
                this.GameTableLayoutPanel1.SetColumnSpan(linkinp, 2);
                this.Height += 24;
                this.GameTableLayoutPanel1.Controls.Add(linkinp);
            }
        }

        protected void createOutputs(StratMainForm parentForm)
        {
            this.outlink = new UCLinkObj(parentForm, this, stratblock, false, 0);

            outlink.Text = outlink.dataType.Name;//stratblock.outputType.Name;
            outlink.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            outlink.Anchor = AnchorStyles.Right;
            this.GameTableLayoutPanel1.SetRow(outlink, 1);
            this.GameTableLayoutPanel1.SetColumn(outlink, 2);
            this.GameTableLayoutPanel1.SetColumnSpan(outlink, 2);
            this.GameTableLayoutPanel1.Controls.Add(outlink);
        }

        public void link(int myinputIndex, StrategyBaseBlock otherBlock)
        {
            this.stratblock.makelink(myinputIndex, otherBlock);
        }

        public void loadUIlocs()
        {
            this.Location = stratblock.GUIloc;
        }
        public void saveUIlocs()
        {
            stratblock.GUIloc = this.Location;
        }
    }
}
