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

        bool ischecked = false;

        ContentAlignment checkAlign = ContentAlignment.MiddleLeft;

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
            setAlign();
            this.Dock = DockStyle.Fill;
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
            get { return this.ischecked; }
            set 
            { 
                this.ischecked = value;
                setpicture();
            }           
        }

        void setpicture()
        {
            if (ischecked)
            {
                this.gamePictureBox1.Image = FrEee.WinForms.Properties.Resources.check_ethernet_checked;
            }
            else 
            {
                this.gamePictureBox1.Image = FrEee.WinForms.Properties.Resources.check_ethernet_clear;
            }
        }

        void setAlign()
        {
            if (checkAlign == ContentAlignment.MiddleLeft)
            {
                gameTableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
                gameTableLayoutPanel1.ColumnStyles[0].Width = 20;              
                gameTableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                gameTableLayoutPanel1.ColumnStyles[1].Width = 100;
                gameTableLayoutPanel1.SetColumn(gamePictureBox1, 0);
                gameTableLayoutPanel1.SetColumn(label1, 1);
            }
            else if (checkAlign == ContentAlignment.MiddleRight)
            {
                gameTableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                gameTableLayoutPanel1.ColumnStyles[0].Width = 100;
                gameTableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Absolute;
                gameTableLayoutPanel1.ColumnStyles[1].Width = 20;
                gameTableLayoutPanel1.SetColumn(gamePictureBox1, 1);
                gameTableLayoutPanel1.SetColumn(label1, 0);
            }
        }


        public override string Text
        {
            get { return this.label1.Text; }
            set { this.label1.Text = value; }
        }

        public System.Drawing.ContentAlignment CheckAlign
        {
            get { return this.checkAlign; }
            set 
            {
                this.checkAlign = value;
                setAlign();
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
            //this.Checked = true;  
            this.DoDragDrop(this, DragDropEffects.All);
        }

        private void checkBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void checkBox1_DragDrop(object sender, DragEventArgs e)
        {
            UCLinkObj linkattempt = (UCLinkObj)e.Data.GetData(typeof(UCLinkObj));
            if (linkattempt != null)
            {
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
                    this.Checked = false;
                }
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
