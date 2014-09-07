using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Mogre;

namespace FrEee.WinForms.MogreCombatRender
{
    class GfxCfg
    {
        //public string MainMesh { get; set; }
        public MainMesh_Properties MainMesh { get; set; }
        public Dictionary<string, Dictionary<string, Effect>> Effects { get; set; }
        //public Dictionary<string, Effect> ForwardThrust_Effect
        //{
        //    get { return Effects["ForwardThrust_Effect"]; }
        //}
        //public Dictionary<string, string> getdata { get { return data; } }
    }

    class MainMesh_Properties
    {
        public string Name { get;set;}
        public float[] Scale {get; set;}
    }

    class Effect
    {
        public string ParticleEffect { get; set; }
        public float[] Loc { get; set; }
        public float[] Dir { get; set; }
    }
}
