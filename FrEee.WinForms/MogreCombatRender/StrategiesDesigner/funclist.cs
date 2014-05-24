using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    public partial class funclist : Form
    {

        Dictionary<string, UserControlBaseObj> functItems = new Dictionary<string, UserControlBaseObj>();

        public UserControlBaseObj ReturnCtrlObj { get; set; }

        public funclist(StratMainForm parent, Canvasdata canvasdata)
        {
            InitializeComponent();

            UCStratBlock ThisObj = new UCStratBlock(new StrategyThisObj(), parent, canvasdata);
            UCStratBlock Closesed = new UCStratBlock(new StrategyClosest(), parent, canvasdata);
            UCStratBlock thisobjEnemys = new UCStratBlock(new StrategyThisEnemys(), parent, canvasdata);
            UCStratBlock loc = new UCStratBlock(new StrategyLocdata(), parent, canvasdata);
            UCStratBlock vel = new UCStratBlock(new StrategyVeldata(), parent, canvasdata);
            UCStratBlock thisobjempireobj = new UCStratBlock(new StrategyThisEmpireObj(), parent, canvasdata);
            UCStratBlock mass = new UCStratBlock(new StrategyMassdata(), parent, canvasdata);
            UCStratBlock range = new UCStratBlock(new StrategyRange(), parent, canvasdata);
            UCStrat_Userinput fix16inp = new UCStrat_Userinput(new Strategyinput_fix16(), parent, canvasdata);
            UCStratBlock wpns = new UCStratBlock(new StrategyWeapons(), parent, canvasdata);
            UCStratBlock filtrsize = new UCStratBlock(new StrategyFilter_HullSize(), parent, canvasdata);
            
            functItems.Add(ThisObj.name, ThisObj);
            functItems.Add(Closesed.name, Closesed);
            functItems.Add(thisobjEnemys.name, thisobjEnemys);
            functItems.Add(thisobjempireobj.name, thisobjempireobj);
            functItems.Add(loc.name, loc);
            functItems.Add(vel.name, vel);         
            functItems.Add(range.name, range);
            functItems.Add(fix16inp.name, fix16inp);
            functItems.Add(mass.name, mass);
            functItems.Add(wpns.name, wpns);
            this.listBox1.DataSource = functItems.Keys.ToList();
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ReturnCtrlObj = functItems[listBox1.SelectedItem.ToString()];
            //this.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            ReturnCtrlObj = functItems[listBox1.SelectedItem.ToString()];
            this.Close();
        }
    }

    
}
