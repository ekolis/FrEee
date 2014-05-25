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
        List<UCFireControlTarget> firectrllist = new List<UCFireControlTarget>();
        Dictionary<int, MountedComponentTemplate> dic_weapons = new Dictionary<int, MountedComponentTemplate>();
        Dictionary<StrategyBaseBlock, UCStratBlock> UCStratblocks = new Dictionary<StrategyBaseBlock, UCStratBlock>();
        List<UCLinkObj> linkObjs = new List<UCLinkObj>();
        UCWaypoint wpnt;
        public StratMainForm(IDesign design)
        {
            InitializeComponent();
            canvasloc = new Point((pBx.Width / 2) * -1, pBx.Height / 2);
            canvasdata = new Canvasdata(1, pBx.Width, pBx.Height, canvasloc);
            this.design = design;
            this.Text = "Strategy for: \"" + design.Name + "\"";
            wpnt = new UCWaypoint(this, canvasdata);
            this.waypointblock = wpnt.wpnt;
            tableLayoutPanel1.SetColumn(wpnt, 2);
            tableLayoutPanel1.SetRow(wpnt, 1);
            tableLayoutPanel1.SetRowSpan(wpnt, 2);
            tableLayoutPanel1.Controls.Add(wpnt);

            tableLayoutPanel1.RowStyles[1] = new RowStyle(SizeType.Absolute, 68);
            wpnt.Name = "WayPoint";
            wpnt.Dock = DockStyle.Fill;

            IEnumerable<MountedComponentTemplate> weapons = design.Components.Where(c => c.ComponentTemplate.WeaponInfo != null); 
            for(int i = 0;i<weapons.Count();i++)
            {
                dic_weapons.Add(i, weapons.ToArray()[i]);
            }
           
            //foreach level of multiplex tracking, add a target and list of weapons accociated with that target. 
            //weapons can be dragged between the lists to set up different weapon groups.
            //ie one group might be all missiles, the other all DF, or mixed for some reason. IF there's enough multiplex. 

            int mplx = design.GetAbilityValue("Multiplex Tracking").ToInt();
            mplx = Math.Max(mplx, 1);
            
            for (int i = 1; i <= mplx; i++)
            {
                UCFireControlTarget ucFireCtrl = new UCFireControlTarget(this, canvasdata);
                firectrllist.Add(ucFireCtrl);
                
                //linkTgt.weapons = weapons.ToList();
                ucFireCtrl.Text = "Target Object";
                tableLayoutPanel1.RowCount += 2;
                while (tableLayoutPanel1.RowStyles.Count < tableLayoutPanel1.RowCount)
                {
                    RowStyle rowstyle = new RowStyle(SizeType.Absolute, 24);
                    tableLayoutPanel1.RowStyles.Add(rowstyle);
                }
                tableLayoutPanel1.SetRow(ucFireCtrl, 1 + i * 2);
                tableLayoutPanel1.SetColumn(ucFireCtrl, 2);
                tableLayoutPanel1.SetRowSpan(ucFireCtrl, 2);
                tableLayoutPanel1.Controls.Add(ucFireCtrl);
                tableLayoutPanel1.SetRowSpan(pBx, tableLayoutPanel1.GetRowSpan(pBx) + 2);
                tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount-1].Height = 96;
                
            }
            if (design.Strategy != null)
                loadstrategy(design.Strategy);
            else
            {
                firectrllist[0].Weapons = dic_weapons;
            }      
        }

        private void loadstrategy(StrategyObject strategy)
        {
            txtBx_Name.Text = strategy.Name;

            foreach (StrategyBaseBlock stblock in strategy.blocks)
            {
                //stblock.hasUI = false;
                UCStratBlock ctrlObj = new UCStratBlock(stblock, this, canvasdata);
                this.UCStratblocks.Add(stblock, ctrlObj);
                pBx.Controls.Add(ctrlObj);
                ctrlObj.loadUIlocs();
            }
            this.waypointblock.inputLnks = strategy.waypointObj.inputLnks;



            int i = 0;
            foreach (UCFireControlTarget target in this.firectrllist)
            {
                target.Weapons = strategy.weaponslists[i];
                target.tgt.inputLnks = strategy.targetObjs[i].inputLnks;
                if (strategy.targetObjs.Count() <= this.firectrllist.Count())
                    i++;
            }

            foreach (UCLinkObj uclink in this.linkObjs)
            {
                //UCStratBlock UCblc = link.parentUC;
                foreach (StrategyBaseBlock slink in uclink.strategyblock.inputLnks)
                {
                    if (slink != null)
                    {
                        UCStratBlock ctrlObj = this.UCStratblocks[slink];
                        uclink.linkedTo[0] = ctrlObj.outlink; //connect the link input to the next object up the chain's output. 
                        if (!ctrlObj.outlink.linkedTo.Contains(uclink)) //if the next item up the chain is not linked back
                        {
                            if (ctrlObj.outlink.linkedTo.Contains(null))
                                ctrlObj.outlink.linkedTo[0] = uclink; //if it has no links at all, put it at [0]
                            else
                                ctrlObj.outlink.linkedTo.Add(uclink); //else add it to teh list.
                        }
                    }
                }
            }
            refreshlines();
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

           
        private void btnAddBlock_Click(object sender, EventArgs e)
        {
            PictureBox pbx = null;
            Canvasdata canvas = null;
            Button btn = (Button)sender;

                pbx = pBx;
                canvas = this.canvasdata;

            
            funclist functlisform = new funclist(this, canvas);
            var result = functlisform.ShowDialog();
            UCStratBlock ctrlObj = functlisform.ReturnCtrlObj;
            
            pbx.Controls.Add(ctrlObj);
            this.UCStratblocks.Add(ctrlObj.stratblock, ctrlObj);
            
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
            foreach (UCStratBlock blk in this.UCStratblocks.Values)
            {
                blk.saveUIlocs();
            }
            List<StrategyBaseBlock> targetblocks = new List<StrategyBaseBlock>();
            List<Dictionary<int, MountedComponentTemplate>> wpnlist = new List<Dictionary<int, MountedComponentTemplate>>();
            foreach (UCFireControlTarget fc in firectrllist)
            {
                targetblocks.Add(fc.linkTgt.strategyblock);
                wpnlist.Add(fc.Weapons);
            }
            StrategyObject stratobj = new StrategyObject(txtBx_Name.Text, waypointblock, targetblocks.ToArray());
            stratobj.weaponslists = wpnlist;
            List<StrategyBaseBlock> blocks = new List<StrategyBaseBlock>();
            List<StrategyBaseBlock> blocks1 = new List<StrategyBaseBlock>();
            List<StrategyBaseBlock> blocks2 = new List<StrategyBaseBlock>();
            blocks1 = waypointblock.getlistoflinks();
            foreach (StrategyBaseBlock linkedblock in targetblocks)
            {
                blocks2 = linkedblock.getlistoflinks();
                blocks = blocks1.Union(blocks2).ToList();
            }
            stratobj.blocks = blocks.ToArray();
            
            design.Strategy = stratobj;
        }

        private void StratMainForm_Resize(object sender, EventArgs e)
        {
            int tgtrowsheight = 96;
            int minsize = tgtrowsheight + (int)(tableLayoutPanel1.RowStyles[0].Height + tableLayoutPanel1.RowStyles[1].Height + tableLayoutPanel1.RowStyles[2].Height);            
            int numtgtrows = firectrllist.Count;
            if (numtgtrows * tgtrowsheight > minsize)
            {
                for (int i = 0; i < firectrllist.Count; i++)
                {
                    tableLayoutPanel1.RowStyles[i + 3].SizeType = SizeType.Percent;
                    tableLayoutPanel1.RowStyles[i + 3].Height = 100;
                }
            }
            else
            {
                for (int i = 0; i < firectrllist.Count; i++)
                {
                    tableLayoutPanel1.RowStyles[i + 3].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.RowStyles[i + 3].Height = tgtrowsheight;
                }
            }
        }

        private void btn_LoadStrategy_Click(object sender, EventArgs e)
        {
            //loadstrategy(StratForm_LoadfromDesign.
        }

    }
}
