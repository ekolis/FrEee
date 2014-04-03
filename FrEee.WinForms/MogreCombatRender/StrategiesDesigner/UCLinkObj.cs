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
    public partial class UCLinkObj : UserControl
    {
        Type datatype;
        UCLinkObj linkedto = null;
        StratMainForm parentform;
        UserControlBaseObj parentUC;

        Point offset = new Point(10, 0);
        Canvasdata canvasdata;
        Point loc = new Point(0, 0);

        Point drawloc = new Point(0, 0);

        public UCLinkObj()
        {
            InitializeComponent();
        }

        public UCLinkObj(StratMainForm parentForm, UserControlBaseObj parentUC, Type datatype)
        {
            InitializeComponent();
            this.parentform = parentForm;
            this.datatype = datatype;
            this.parentUC = parentUC;
            parentForm.links.Add(this);
            setoffset();
        }

        

        public Type dataType
        {
            get { return this.datatype; }           
        }

        public UCLinkObj linkedTo
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
            this.offset.Y = this.Height / 2;
            if (CheckAlign == ContentAlignment.MiddleRight)
                offset.X = this.Width - 10;
            else if (CheckAlign == ContentAlignment.MiddleLeft)
                offset.X = 10; 
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
                linkedTo = linkattempt;
                linkattempt.linkedto = this;
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
