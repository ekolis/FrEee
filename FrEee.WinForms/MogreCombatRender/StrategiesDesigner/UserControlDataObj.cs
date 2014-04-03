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
    public partial class UserControlDataObj : UserControlBaseObj
    {
        public UserControlDataObj()
        {
            InitializeComponent();
        }
        public UserControlDataObj(string name, StratMainForm parentForm, Canvasdata canvasdata, Type output)
            : base(name, parentForm, canvasdata)
        {
            InitializeComponent();

            this.Height = 20;
            RowStyle style0 = new RowStyle(SizeType.Absolute, 20);
            this.TableLayoutPanel1.RowStyles[0] = style0;

            this.TableLayoutPanel1.RowCount += 1;
            RowStyle style = new RowStyle(SizeType.Absolute, 24);
            this.TableLayoutPanel1.RowStyles.Add(style);
            this.Height += 24;

            UCLinkObj link = new UCLinkObj(parentForm, this, output);
            link.Text = output.Name;
            link.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            link.Anchor = AnchorStyles.Right;
            this.TableLayoutPanel1.SetRow(link, 1);
            this.TableLayoutPanel1.SetColumn(link, 2);
            this.TableLayoutPanel1.SetColumnSpan(link, 2);
            this.TableLayoutPanel1.Controls.Add(link);
        }
    }
}
