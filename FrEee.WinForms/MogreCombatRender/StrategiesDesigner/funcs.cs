using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Objects.Combat2;
using FixMath.NET;
using NewtMath.f16;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    class UCFunct_Loc:UserControlFunctObj
    {
        public UCFunct_Loc(StratMainForm parent, Canvasdata canvasdata)
            : base("Location", parent, canvasdata, new Type[1] { typeof(CombatObject) }, typeof(PointXd))
        {

        }
    }

    class UCFunct_Vel : UserControlFunctObj
    {
        public UCFunct_Vel(StratMainForm parent, Canvasdata canvasdata)
            : base("Velocity", parent, canvasdata, new Type[1] { typeof(CombatObject) }, typeof(PointXd))
        {

        }
    }

    class UCData_This : UserControlDataObj
    {
        
        public string type = "comObj";
        public UCData_This(StratMainForm parent, Canvasdata canvasdata)
            : base("This", parent, canvasdata, typeof(CombatObject))
        {
 
        }
    }

    class UCData_Clock : UserControlDataObj
    {      
        public UCData_Clock(StratMainForm parent, Canvasdata canvasdata)
            : base("Clock", parent, canvasdata, typeof(int))
        {

        }
    }

    class UCData_EnemyShips : UserControlDataObj
    {
        public UCData_EnemyShips(StratMainForm parent, Canvasdata canvasdata)
            : base("EnemyShips", parent, canvasdata, typeof(List<CombatObject>))
        {

        }
    }

    class UCData_FrendlyShips : UserControlDataObj
    {
        public UCData_FrendlyShips(StratMainForm parent, Canvasdata canvasdata)
            : base("FrendlyShips", parent, canvasdata, typeof(List<CombatObject>))
        {

        }
    }

    class UCData_NearestEnemy : UserControlDataObj
    {
        public UCData_NearestEnemy(StratMainForm parent, Canvasdata canvasdata)
            : base("NearestEnemy", parent, canvasdata, typeof(CombatObject))
        {

        }
    }

    class UCData_NearestComObj_Base : UserControlFunctObj
    {
        public UCData_NearestComObj_Base(StratMainForm parent, Canvasdata canvasdata, Type[] inputs, Type output)
            : base("Nearest", parent, canvasdata, inputs, output)
        {
            //CombatObject from = inputs[0]; //this won't work here, I'm an abstraction level above this... I think.
        }
    }

    class UCData_NearestComObj : UCData_NearestComObj_Base
    {
        public UCData_NearestComObj(StratMainForm parent, Canvasdata canvasdata)
            : base(parent, canvasdata, new Type[2] { typeof(CombatObject), typeof(List<CombatObject>) }, typeof(CombatObject))
        {


        }
    }
}
