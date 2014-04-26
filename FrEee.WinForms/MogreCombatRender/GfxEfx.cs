using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mogre;
using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender
{
    class GfxEfx
    {
        /// <summary>
        /// keys: 
        /// ForwardThrust_Effect
        /// ReverseThrust_Effect
        /// StrafeRThrust_Effect
        /// StrafeLThrust_Effect
        /// RotateRThrust_Effect
        /// RotateLThrust_Effect
        /// </summary>
        public Dictionary<string, List<SceneNode>> nodesDict { get; private set;}

        public GfxEfx(SceneManager sceneMgr, GfxObj gfxobj)
        {
            
            nodesDict = new Dictionary<string,List<SceneNode>>();

            foreach (KeyValuePair<string, Dictionary<string, Effect>> dict in gfxobj.gfxCfg.Effects)
            {
                string topkey = dict.Key;
                Dictionary<string, Effect> topval = dict.Value;
                List<SceneNode> nodeslist = new List<SceneNode>();
                foreach (KeyValuePair<string, Effect> effect in topval)
                {                   
                    SceneNode shipNode = (SceneNode)sceneMgr.RootSceneNode.GetChild(gfxobj.IDName);
                    SceneNode effectnode = shipNode.CreateChildSceneNode(gfxobj.IDName + topkey + effect.Key, new Vector3(effect.Value.Loc));
                    Vector3 lookat = new Vector3(effect.Value.Dir);
                    effectnode.LookAt(lookat, Node.TransformSpace.TS_LOCAL);

                    ParticleSystem pThrust = sceneMgr.CreateParticleSystem(gfxobj.IDName + topkey + effect.Key, effect.Value.ParticleEffect);
                    pThrust.Emitting = false;
                    effectnode.AttachObject(pThrust);
                    nodeslist.Add(effectnode);
                   
                }
                nodesDict.Add(topkey, nodeslist);
                
            }
        }

        /// <summary>
        /// turns partcle.Emitting on or off for this effect. 
        /// </summary>
        /// <param name="keyname">the keyname for this effect.
        /// ForwardThrust_Effect
        /// ReverseThrust_Effect
        /// StrafeRThrust_Effect
        /// StrafeLThrust_Effect
        /// RotateRThrust_Effect
        /// RotateLThrust_Effect</param>
        /// <param name="doornot">if null or not assigned, will switch off if on and on if off</param>
        public void do_Emmitt(string keyname, bool? doornot = null)
        {
            List<SceneNode> nodes = new List<SceneNode>();
            if(nodesDict.ContainsKey(keyname))
                nodes.AddRange(nodesDict[keyname]);
            foreach (SceneNode node in nodes)
            {
                SceneNode.ObjectIterator iterator = node.GetAttachedObjectIterator();

                foreach (ParticleSystem partclesys in iterator)
                {
                    
                    if (doornot == null)                      
                        partclesys.Emitting = !partclesys.Emitting;
                    else
                        partclesys.Emitting = (bool)doornot;
                } 
            }
        }

        public void set_effects(CombatNode comNode)
        {
            
            if (comNode is CombatObject)
            {
                CombatObject comObj = (CombatObject)comNode;
                if (comObj.cmbt_thrust.Compass.Degrees < 90 || comObj.cmbt_thrust.Compass.Degrees > 270)
                {
                    do_Emmitt("ForwardThrust_Effect", true);
                    do_Emmitt("ReverseThrust_Effect", false);
                }
                else if (comObj.cmbt_thrust.Compass.Degrees > 90 && comObj.cmbt_thrust.Compass.Degrees < 270)
                {
                    do_Emmitt("ReverseThrust_Effect", true);
                    do_Emmitt("ForwardThrust_Effect", false);
                }
                else
                {
                    do_Emmitt("ReverseThrust_Effect", false);
                    do_Emmitt("ForwardThrust_Effect", false);
                }

                if (comObj.cmbt_thrust.Compass.Degrees > 0 && comObj.cmbt_thrust.Compass.Degrees < 180)
                {
                    do_Emmitt("StrafeRThrust_Effect", false);
                    do_Emmitt("StrafeLThrust_Effect", true);
                }
                else if (comObj.cmbt_thrust.Compass.Degrees < 360 && comObj.cmbt_thrust.Compass.Degrees > 180)
                {
                    do_Emmitt("StrafeRThrust_Effect", true);
                    do_Emmitt("StrafeLThrust_Effect", false);
                }
                else
                {
                    do_Emmitt("StrafeRThrust_Effect", false);
                    do_Emmitt("StrafeLThrust_Effect", false);
                }

                //if (comObj.Rotating > 0)
                //{
                //    do_Emmitt("RotateRThrust_Effect", true);
                //    do_Emmitt("RotateLThrust_Effect", false);
                //}
                //else if (comObj.Rotating < 0)
                //{
                //    do_Emmitt("RotateRThrust_Effect", false);
                //    do_Emmitt("RotateLThrust_Effect", true);
                //}
                //else
                //{
                //    do_Emmitt("RotateRThrust_Effect", false);
                //    do_Emmitt("RotateLThrust_Effect", false);
                //}
            }
        }
    }
}
