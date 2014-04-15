using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FrEee.Game.Interfaces;
using FrEee.Game.Objects.Vehicles;
using FrEee.Game.Objects.Technology;
using FrEee.WinForms.Forms;
using FrEee.Utility.Extensions;
using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    public partial class StratMainForm  : Form
    {
        Canvasdata canvasdata;
        Point canvasloc;
        //Canvasdata canvasdataFC;
        //Point canvaslocFC;
        IDesign design;
        StrategyWayPoint waypointblock = new StrategyWayPoint();
        List<StrategyComObj> tgtlist = new List<StrategyComObj>();

        List<UCLinkObj> linkObjs = new List<UCLinkObj>();

        public StratMainForm(IDesign design)
        {
            InitializeComponent();
            canvasloc = new Point((pBx.Width / 2) * -1, pBx.Height / 2);
            canvasdata = new Canvasdata(1, pBx.Width, pBx.Height, canvasloc);
            this.design = design;
            
            Type typHloc = typeof(NewtMath.f16.PointXd);
            UCLinkObj linkHloc = new UCLinkObj(this, null, waypointblock, true, 0);
            linkHloc.CheckAlign = ContentAlignment.MiddleLeft;
            linkHloc.Text = "Location";
            tableLayoutPanel1.SetColumn(linkHloc, 2);           
            tableLayoutPanel1.SetColumnSpan(linkHloc, 2);
            tableLayoutPanel1.SetRow(linkHloc, 1);
            tableLayoutPanel1.Controls.Add(linkHloc);

            Type typHvel = typeof(NewtMath.f16.PointXd);
            UCLinkObj linkHvel = new UCLinkObj(this, null, waypointblock, true, 1);
            linkHvel.CheckAlign = ContentAlignment.MiddleLeft;
            linkHvel.Text = "Velocity";          
            tableLayoutPanel1.SetColumn(linkHvel, 2);
            tableLayoutPanel1.SetColumnSpan(linkHvel, 2);
            tableLayoutPanel1.SetRow(linkHvel, 2);         
            tableLayoutPanel1.Controls.Add(linkHvel);

            //List<MountedComponentTemplate> weapons = new List<MountedComponentTemplate>();
            IEnumerable<MountedComponentTemplate> weapons = design.Components.Where(c => c.ComponentTemplate.WeaponInfo != null); //this doesnt work or something.
           
            

            //foreach level of multiplex tracking, add a target and list of weapons accociated with that target. 
            //weapons can be dragged between the lists to set up different weapon groups.
            //ie one group migth be all missiles, the other all DF, or mixed for some reason. IF there's enough multiplex. 

            int mplx = design.GetAbilityValue("Multiplex Tracking").ToInt();
            mplx = Math.Max(mplx, 1);
            
            for (int i = 1; i <= mplx; i++)
            {
                ListBox lb = new ListBox();
                lb.DataSource = weapons.ToList();
                //Type typTgt = typeof(Game.Objects.Combat2.CombatObject);
                StrategyComObj tgt = new StrategyComObj();
                tgtlist.Add(tgt);
                UCLinkObj linkTgt = new UCLinkObj(this, null, tgt, true, 0);
                linkTgt.CheckAlign = ContentAlignment.MiddleLeft;
                linkTgt.Text = "Target Object";
                tableLayoutPanel1.RowCount += 2;
                
                while (tableLayoutPanel1.RowStyles.Count < tableLayoutPanel1.RowCount)
                { 
                    RowStyle rowstyle = new RowStyle(SizeType.Absolute, 24); 
                    tableLayoutPanel1.RowStyles.Add(rowstyle); 
                }
                tableLayoutPanel1.RowStyles[2 + i * 2].Height = 24;
                tableLayoutPanel1.RowStyles[3 + i * 2].Height = 48;

                tableLayoutPanel1.SetRowSpan(pBx, tableLayoutPanel1.GetRowSpan(pBx) + 2);
                
                tableLayoutPanel1.SetRow(linkTgt, 2 + i * 2);
                tableLayoutPanel1.SetColumn(linkTgt, 2);
                tableLayoutPanel1.SetColumnSpan(linkTgt, 2);

                tableLayoutPanel1.SetRow(lb, 3 + i * 2);
                tableLayoutPanel1.SetColumn(lb, 2);
                tableLayoutPanel1.SetColumnSpan(lb, 2);
                lb.Dock = DockStyle.Fill;

                tableLayoutPanel1.Controls.Add(linkTgt);
                tableLayoutPanel1.Controls.Add(lb);

            }
            
        }

        public List<UCLinkObj> links
        {
            get { return this.linkObjs; }
        }

        public IDesign Design
        {
            get { return design; }
            set
            {
                design = value;              
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

           
        private void button1_Click(object sender, EventArgs e)
        {
            PictureBox pbx = null;
            Canvasdata canvas = null;
            Button btn = (Button)sender;

                pbx = pBx;
                canvas = this.canvasdata;

            
            funclist functlisform = new funclist(this, canvas);
            var result = functlisform.ShowDialog();
            UserControlBaseObj ctrlObj = functlisform.ReturnCtrlObj;
            
            pbx.Controls.Add(ctrlObj);
        }

        public void refresh()
        { 
            pBx.Invalidate();
            //pBx_FireCtrl.Invalidate();
            
        }

        public void refreshlines()
        {
            //pictureBox1.Invalidate();
            if (links.Count > 0)
            {
                pBx.Paint += new PaintEventHandler(this.drawlines);
                pBx.Invalidate();
            }
        }

        public void drawlines(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        
            
            foreach (UCLinkObj linkobj in linkObjs)
            {
                //linkobj.location(canvasdata);
            }
            
            foreach (UCLinkObj linkobj in linkObjs)
            {
                
                if (linkobj.linkedTo[0] != null && linkobj.CheckAlign == ContentAlignment.MiddleLeft) //don't draw lines if null, 
                    //and only draw lines from input objects(not back again, or that'd be drawing a line where we've already done one)
                {
                    Pen linkline = new Pen(Color.Blue, 2);
                    Point pt1 = linkobj.drawLoc;//canvasdata.canvasLocation(linkobj.drawLoc);
                    Point pt2 = linkobj.linkedTo[0].drawLoc;//canvasdata.canvasLocation(linkobj.linkedTo.drawLoc);
                    g.DrawLine(linkline, pt1, pt2);
                }
            }
            
        }

        private void btn_SaveStrategy_Click(object sender, EventArgs e)
        {
            StrategyObjects stratobj = new StrategyObjects(waypointblock, tgtlist.ToArray());
            List<StrategyBaseBlock> blocks = new List<StrategyBaseBlock>();
            List<StrategyBaseBlock> blocks1 = new List<StrategyBaseBlock>();
            List<StrategyBaseBlock> blocks2 = new List<StrategyBaseBlock>();
            blocks1 = waypointblock.getlistoflinks();
            foreach (StrategyBaseBlock linkedblock in tgtlist)
            {
                blocks2 = linkedblock.getlistoflinks();
                blocks = blocks1.Union(blocks2).ToList();
            }
            stratobj.blocks = blocks.ToArray();           
        }
    }
}
