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
    public partial class UCLinkObj : UserControl
    {
        Type datatype;
        List<UCLinkObj> linkedto = new List<UCLinkObj>();
        StratMainForm parentform;
        UserControlBaseObj parentUC;
        StrategyBaseBlock strategyblock;
        int linkdex = 0;
        bool isinput;
        Point offset = new Point(10, 0);
        Canvasdata canvasdata;
        Point loc = new Point(0, 0);

        Point drawloc = new Point(0, 0);



        public UCLinkObj(StratMainForm parentForm, UserControlBaseObj parentUC, StrategyBaseBlock strategyblock, bool isinput, int linkindex)
        {
            InitializeComponent();
            linkedto.Add(null);
            this.linkdex = linkindex;
            this.parentform = parentForm;
            this.strategyblock = strategyblock;
            this.isinput = isinput;
            if (isinput)
                this.datatype = strategyblock.inputtypes[linkdex];
            else
                this.datatype = strategyblock.outputType;
            this.parentUC = parentUC;
            parentForm.links.Add(this);
            setoffset();
        }

        public UCLinkObj()
        {
            InitializeComponent();
        }

        public Type dataType
        {
            get { return this.datatype; }           
        }

        public List<UCLinkObj> linkedTo
        {
            get { return this.linkedto; }
            set { this.linkedto = value; }
        }

        public bool Checked
        {
            get { return this.checkBox1.Checked; }
            set { this.checkBox1.Checked = value; }
            
        }

        public string Text
        {
            get { return this.checkBox1.Text; }
            set { this.checkBox1.Text = value; }
        }

        public System.Drawing.ContentAlignment CheckAlign
        {
            get { return this.checkBox1.CheckAlign; }
            set 
            { 
                this.checkBox1.CheckAlign = value;
                setoffset();
            }
        }

        private void setoffset()
        {
            this.offset.Y = this.Height / 2 -3;
            if (this.parentUC == null)
                this.offset.Y -= 37;
            if (CheckAlign == ContentAlignment.MiddleRight)
                offset.X = this.Width + 2;
            else if (CheckAlign == ContentAlignment.MiddleLeft)
                offset.X = -2; 
        }
        
        private void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Checked = true;  
            this.DoDragDrop(this, DragDropEffects.All);
        }

        private void checkBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_DragDrop(object sender, DragEventArgs e)
        {
            UCLinkObj linkattempt = (UCLinkObj)e.Data.GetData(typeof(UCLinkObj));

            if (linkattempt.dataType == this.dataType)
            {
                //linkedTo = linkattempt;
                //linkattempt.linkedto = this;
                

                if (this.isinput)
                {
                    this.strategyblock.makelink(linkdex, linkattempt.strategyblock);
                    linkedto[0] = linkattempt;
                    
                    if (!linkattempt.linkedto.Contains(this))
                    {
                        if (linkattempt.linkedto.Contains(null))
                            linkattempt.linkedto[0] = this;
                        else
                            linkattempt.linkedto.Add(this);
                    }
                }
                else
                {
                    linkattempt.strategyblock.makelink(linkattempt.linkdex, this.strategyblock);
                    linkattempt.linkedto[0] = this;
                    if (!this.linkedto.Contains(linkattempt))
                    {
                        if (this.linkedto.Contains(null))
                            this.linkedto[0] = linkattempt;
                        else
                            this.linkedto.Add(linkattempt);
                    }
                }
                this.Checked = true;
                linkattempt.Checked = true;
                parentform.refreshlines();
            }
            else
            {
                linkattempt.Checked = false;
                this.checkBox1.Checked = false;
            }

        }

        private void checkBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        public Point drawLoc
        {
            get {

                Point drawloc = new Point();
                drawloc.X = Location.X + offset.X;
                drawloc.Y = Location.Y + offset.Y;
                if (parentUC != null)
                {
                    drawloc.X += parentUC.Location.X;
                    drawloc.Y += parentUC.Location.Y;
                }
                return drawloc; }   
        }
    }

}
