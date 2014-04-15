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
        
        public UCStratBlock() : base() { }

        public UCStratBlock(StrategyBaseBlock stratblock, StratMainForm parentForm, Canvasdata canvasdata)
            : base(stratblock.name, parentForm, canvasdata)
        {
            this.stratblock = stratblock;
            InitializeComponent();

            
            RowStyle style0 = new RowStyle(SizeType.Absolute, 20);
            this.TableLayoutPanel1.RowStyles[0] = style0;

            if (stratblock.inputtypes != null)
            {
                for (int i = 0; i < stratblock.inputtypes.Length; i++)
                {

                    UCLinkObj linkinp = new UCLinkObj(parentForm, this, stratblock, true, i);

                    linkinp.Text = stratblock.inputtypes[i].Name;
                    linkinp.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;

                    this.TableLayoutPanel1.RowCount += 1;
                    RowStyle style1 = new RowStyle(SizeType.Absolute, 24);
                    this.TableLayoutPanel1.RowStyles.Add(style1);

                    this.TableLayoutPanel1.SetRow(linkinp, TableLayoutPanel1.RowCount - 1);
                    this.TableLayoutPanel1.SetColumn(linkinp, 0);
                    this.TableLayoutPanel1.SetColumnSpan(linkinp, 2);
                    this.Height += 24;
                    this.TableLayoutPanel1.Controls.Add(linkinp);
                }
            }
            else 
            {
                RowStyle style1 = new RowStyle(SizeType.Absolute, 24);
                this.TableLayoutPanel1.RowStyles.Add(style1);
                this.Height += 24;
            }

            UCLinkObj linkout = new UCLinkObj(parentForm, this, stratblock, false, 0);

            linkout.Text = stratblock.outputType.Name;
            linkout.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            linkout.Anchor = AnchorStyles.Right;
            this.TableLayoutPanel1.SetRow(linkout, 1);
            this.TableLayoutPanel1.SetColumn(linkout, 2);
            this.TableLayoutPanel1.SetColumnSpan(linkout, 2);
            this.TableLayoutPanel1.Controls.Add(linkout);
            
        }

        public void link(int myinputIndex, StrategyBaseBlock otherBlock)
        {
            this.stratblock.makelink(myinputIndex, otherBlock);
        }
    }
}
