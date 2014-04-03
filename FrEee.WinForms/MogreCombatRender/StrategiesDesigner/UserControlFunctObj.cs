using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    public partial class UserControlFunctObj : UserControlBaseObj
    {
        //Point offset = new Point(10, 10);
        //Point loc = new Point(0, 0);

        public UserControlFunctObj()
        {
            InitializeComponent();
        }

        public UserControlFunctObj(string name, StratMainForm parentForm, Canvasdata canvasdata, Type[] inputs, Type output) : base (name, parentForm, canvasdata)
        {
            InitializeComponent();
            this.Name = name;
            this.lbl_FunctName.Text = name;
            this.Height = 20;
            RowStyle style0 = new RowStyle(SizeType.Absolute, 20);
            this.TableLayoutPanel1.RowStyles[0] = style0;
            int i = 0;
            foreach (var input in inputs)
            {
                i++;

                UCLinkObj linkinp = new UCLinkObj(parentForm, this, input);
                linkinp.Text = input.Name;
                linkinp.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft;

                this.TableLayoutPanel1.RowCount += 1;
                RowStyle style = new RowStyle(SizeType.Absolute, 24);
                this.TableLayoutPanel1.RowStyles.Add(style);

                this.TableLayoutPanel1.SetRow(linkinp, TableLayoutPanel1.RowCount - 1);
                this.TableLayoutPanel1.SetColumn(linkinp, 0);
                this.TableLayoutPanel1.SetColumnSpan(linkinp, 2);
                this.Height += 24;
                this.TableLayoutPanel1.Controls.Add(linkinp);
            }
            UCLinkObj linkout = new UCLinkObj(parentForm, this, output);
            linkout.Text = output.Name;
            linkout.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            linkout.Anchor = AnchorStyles.Right;
            this.TableLayoutPanel1.SetRow(linkout, 1);
            this.TableLayoutPanel1.SetColumn(linkout, 2);
            this.TableLayoutPanel1.SetColumnSpan(linkout, 2);
            this.TableLayoutPanel1.Controls.Add(linkout);

        }
    }
}
