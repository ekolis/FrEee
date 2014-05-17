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
    public partial class UCStrat_Userinput : UCStratBlock
    {
        MaskedTextBox mtxtbx_inp = new MaskedTextBox();
        public UCStrat_Userinput(Strategyinput_fix16 stratblock, StratMainForm parent, Canvasdata canvasdata ):
            base(stratblock, parent, canvasdata)        
        {
            InitializeComponent();

        }
        protected override void createInputs(StratMainForm parent)
        {
            this.Height += 24;
           
            mtxtbx_inp.Mask = "000000000";

            // 
            // maskedTextBox1
            // 

            this.mtxtbx_inp.Name = "Input";
            //this.mtxtbx_inp.Size = new System.Drawing.Size(90, 20);
            //this.mtxtbx_inp.TabIndex = 11;
            this.mtxtbx_inp.Leave += new System.EventHandler(this.maskedTextBox1_Leave);

            GameTableLayoutPanel1.SetRow(mtxtbx_inp, 1);
            GameTableLayoutPanel1.SetColumn(mtxtbx_inp, 0);
            GameTableLayoutPanel1.Controls.Add(mtxtbx_inp);
            GameTableLayoutPanel1.SetColumnSpan(mtxtbx_inp, 2);
        }
        private void maskedTextBox1_Leave(object sender, EventArgs e)
        {
            Strategyinput_fix16 sbf16 = (Strategyinput_fix16)stratblock;
            sbf16.inputnum = float.Parse(mtxtbx_inp.Text);
        }
    }
}
