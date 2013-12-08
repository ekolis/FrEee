using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FrEee.Game.Objects.Combat2;

using Mogre;
using System.Runtime.InteropServices;

namespace FrEee.WinForms.MogreCombatRender
{
    class ShutdownException : Exception { }
    class MogreFreeMain
    {       
        private static int tickcounter = 0; //the tick age of the game.
        private Root mRoot;
        private RenderWindow mRenderWindow;
        private SceneManager mSceneMgr;
        private Viewport mViewport;

        private MOIS.Keyboard mKeybrd;
        private MOIS.Mouse mMouse;

        protected Camera mCamera;
        protected CameraMan mCameraMan;

        //protected RaySceneQuery mRaySceneQuery = null;      // The ray scene query pointer
        protected SceneNode mNode_lines = null;

        System.Diagnostics.Stopwatch physicsstopwatch = new System.Diagnostics.Stopwatch();

        Dictionary<string, CombatObj> renderObjects = new Dictionary<string, CombatObj>();
        private Battle_Space battle;

        public MogreFreeMain(Battle_Space battle)
        {
            this.battle = new Battle_Space(battle.Sector, true);
            setup();

            try
            {
                CreateRoot();
                DefineResources();
                CreateRenderSystem();
                CreateRenderWindow();
                InitializeResources();


                CreateScene();
                InitializeInput();
                CreateFrameListeners();

                //physicsstopwatch.Start();
                //pyhsics_eventtimer();
             
                EnterRenderLoop();

            }
            catch (OperationCanceledException) { }
        }

        private void setup()
        {


        }


        #region mogresetup

        protected virtual void CreateCamera()
        {
            mCamera = mSceneMgr.CreateCamera("PlayerCam");

            mCamera.Position = new Vector3(750, 2000, 750);

            mCamera.LookAt(new Vector3(0, 0, 0));
            mCamera.NearClipDistance = 5;

            mCameraMan = new CameraMan(mCamera);
        }

        private void CreateRoot()
        {
			try
			{
				mRoot = new Root("MogreCombatRender/plugins.cfg"); //can change location/name of plugins.cfg ogre.cfg and Ogre.log files here using the Root parameters. 
			}
			catch (SEHException ex)
			{
				if (OgreException.IsThrown)
					throw new InvalidOperationException(OgreException.LastException.FullDescription);
				else
					throw;
			}
        }

        private void DefineResources()
        {
            ConfigFile cf = new ConfigFile();
			try
			{
				cf.Load("MogreCombatRender/resources.cfg", "\t:=", true);
			}
			catch (SEHException ex)
			{
				if (OgreException.IsThrown)
					throw new InvalidOperationException(OgreException.LastException.FullDescription);
				else
					throw;
			}

            var section = cf.GetSectionIterator();
            while (section.MoveNext())
            {
                foreach (var line in section.Current)
                {
					try
					{
						ResourceGroupManager.Singleton.AddResourceLocation(
							line.Value, line.Key, section.CurrentKey);
					}
					catch (SEHException ex)
					{
						if (OgreException.IsThrown)
							throw new InvalidOperationException(OgreException.LastException.FullDescription);
						else
							throw;
					}
                }
            }
        }

        private void CreateRenderSystem()
        {
            if (!mRoot.ShowConfigDialog())
                throw new OperationCanceledException();
        }

        private void CreateRenderWindow()
        {
            mRenderWindow = mRoot.Initialise(true, "Main CC Window");
        }

        private void InitializeResources()
        {
            TextureManager.Singleton.DefaultNumMipmaps = 5;
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
        }

        protected void InitializeInput()
        {

            MOIS.ParamList pl = new MOIS.ParamList();
            int windowHnd;
            mRenderWindow.GetCustomAttribute("WINDOW", out windowHnd);
            pl.Insert("WINDOW", windowHnd.ToString());
            pl.Insert("w32_mouse", "DISCL_FOREGROUND");
            pl.Insert("w32_mouse", "DISCL_NONEXCLUSIVE");
            pl.Insert("w32_keyboard", "DISCL_FOREGROUND");
            pl.Insert("w32_keyboard", "DISCL_NONEXCLUSIVE");
            //var inputMgr = MOIS.InputManager.CreateInputSystem((uint)windowHnd);
            var inputMgr = MOIS.InputManager.CreateInputSystem(pl);

            mKeybrd = (MOIS.Keyboard)inputMgr.CreateInputObject(MOIS.Type.OISKeyboard, true);
            mMouse = (MOIS.Mouse)inputMgr.CreateInputObject(MOIS.Type.OISMouse, true);

            MOIS.MouseState_NativePtr mouseState = mMouse.MouseState;
            mouseState.width = mViewport.ActualWidth; //! update after resize window
            mouseState.height = mViewport.ActualHeight;

            mKeybrd.KeyPressed += new MOIS.KeyListener.KeyPressedHandler(OnKeyPressed);
            mKeybrd.KeyReleased += new MOIS.KeyListener.KeyReleasedHandler(OnKeyReleased);
            mMouse.MouseMoved += new MOIS.MouseListener.MouseMovedHandler(OnMouseMoved);
            mMouse.MousePressed += new MOIS.MouseListener.MousePressedHandler(OnMousePressed);
            mMouse.MouseReleased += new MOIS.MouseListener.MouseReleasedHandler(OnMouseReleased);

            //mRaySceneQuery = mSceneMgr.CreateRayQuery(new Ray());
        }

        private void CreateScene()
        {
            mSceneMgr = mRoot.CreateSceneManager(SceneType.ST_EXTERIOR_CLOSE);
            mSceneMgr.SetWorldGeometry("terrain.cfg");
            //Camera camera = mSceneMgr.CreateCamera("Camera");
            //camera.Position = new Vector3(50, 1000, 0);
            //camera.LookAt(Vector3.ZERO);
            CreateCamera();
            mViewport = mRenderWindow.AddViewport(mCamera);
            mViewport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = (float)mViewport.ActualWidth / mViewport.ActualHeight;


            foreach (CombatObj obj in renderObjects.Values)
            {
                //Entity dudeEnt = mSceneMgr.CreateEntity(obj.getID, obj.MeshName);
                //SceneNode dudeNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(obj.getID);

                //dudeNode.Yaw(new Degree(-90));

                //dudeNode.AttachObject(dudeEnt);
                //dudeNode.Position = new Vector3((float)obj.Loc.X, 0, (float)obj.Loc.Y);
                CreateNewEntity(obj);

            }

            String resourceGroupName = "lines";
            if (ResourceGroupManager.Singleton.ResourceGroupExists(resourceGroupName) == false)
                ResourceGroupManager.Singleton.CreateResourceGroup(resourceGroupName);

            MaterialPtr moMaterialblue = MaterialManager.Singleton.Create("line_blue", resourceGroupName);
            moMaterialblue.ReceiveShadows = false;
            moMaterialblue.GetTechnique(0).SetLightingEnabled(true);
            moMaterialblue.GetTechnique(0).GetPass(0).SetDiffuse(0, 0, 1, 0);
            moMaterialblue.GetTechnique(0).GetPass(0).SetAmbient(0, 0, 1);
            moMaterialblue.GetTechnique(0).GetPass(0).SetSelfIllumination(0, 0, 1);
            moMaterialblue.Dispose();  // dispose pointer, not the material
            MaterialPtr moMaterialred = MaterialManager.Singleton.Create("line_red", resourceGroupName);
            moMaterialred.ReceiveShadows = false;
            moMaterialred.GetTechnique(0).SetLightingEnabled(true);
            moMaterialred.GetTechnique(0).GetPass(0).SetDiffuse(1, 0, 0, 0);
            moMaterialred.GetTechnique(0).GetPass(0).SetAmbient(1, 0, 0);
            moMaterialred.GetTechnique(0).GetPass(0).SetSelfIllumination(1, 0, 0);
            moMaterialred.Dispose();  // dispose pointer, not the material
            MaterialPtr moMaterialgreen = MaterialManager.Singleton.Create("line_green", resourceGroupName);
            moMaterialgreen.ReceiveShadows = false;
            moMaterialgreen.GetTechnique(0).SetLightingEnabled(true);
            moMaterialgreen.GetTechnique(0).GetPass(0).SetDiffuse(0, 1, 0, 0);
            moMaterialgreen.GetTechnique(0).GetPass(0).SetAmbient(0, 1, 0);
            moMaterialgreen.GetTechnique(0).GetPass(0).SetSelfIllumination(0, 1, 0);
            moMaterialgreen.Dispose();  // dispose pointer, not the material

            mNode_lines = mSceneMgr.RootSceneNode.CreateChildSceneNode(" ", Vector3.ZERO);
            //foreach (ControlledSO ship in shiplist)
            //{
            //    Entity shipEnt = sceneMgr.CreateEntity(ship.IDName, ship.Meshname);
            //    //Entity shipEnt = sceneMgr.CreateEntity(ship.IDName, "ogrehead.mesh");
            //    SceneNode shipNode = sceneMgr.RootSceneNode.CreateChildSceneNode(ship.IDName);

            //    shipNode.AttachObject(shipEnt);
            //    shipNode.Position = new Vector3((float)ship.Location.X, (float)ship.Location.Y, 0);

            //}

            //foreach (SpaceObjects rock in rocklist)
            //{
            //    //Mogre.MeshPtr rockmesh = MeshManager.Singleton.CreateManual("create", "general");

            //    ManualObject rockMobj = sceneMgr.CreateManualObject();
            //    rockMobj.Begin("Ogre/Eyes", RenderOperation.OperationTypes.OT_TRIANGLE_FAN);
            //    //rockMobj.Begin("Ogre/Eyes", RenderOperation.OperationTypes.OT_TRIANGLE_STRIP);
            //    rockMobj.Position(0.0f, 0.0f, 0.0f);
            //    rockMobj.Position(100.0f, 0.0f, 0.0f);
            //    rockMobj.Position(75.0f, 75.0f, 0.0f);
            //    rockMobj.Position(0.0f, 100.0f, 0.0f);
            //    rockMobj.Position(-75.0f, 75.0f, 0.0f);
            //    rockMobj.Position(-100.0f, 0.0f, 0.0f);
            //    rockMobj.Position(-75.0f, -75.0f, 0.0f);
            //    rockMobj.Index(0);//Front Side
            //    rockMobj.Index(1);
            //    rockMobj.Index(2);
            //    rockMobj.Index(3);
            //    rockMobj.Index(4);
            //    rockMobj.Index(5);
            //    rockMobj.Index(6);
            //    rockMobj.Index(7);
            //    rockMobj.End();

            //    SceneNode rockNode = sceneMgr.RootSceneNode.CreateChildSceneNode(rock.IDName);

            //    rockNode.AttachObject(rockMobj);
            //    rockNode.Position = new Vector3((float)rock.Location.X, (float)rock.Location.Y, 0);
            //}


            mSceneMgr.AmbientLight = new ColourValue(0.5f, 0.5f, 0.5f);

            Light l = mSceneMgr.CreateLight("MainLight");
            l.Position = new Vector3(0, 0, 5000);

        }

        private void CreateFrameListeners()
        {
            mRoot.FrameRenderingQueued += new FrameListener.FrameRenderingQueuedHandler(ProcessBufferedInput);
        }

        private bool ProcessBufferedInput(FrameEvent evt)
        {
            //mTimer -= evt.timeSinceLastFrame;
            //return (mTimer > 0);
            mKeybrd.Capture();
            mMouse.Capture();
            mCameraMan.UpdateCamera(evt.timeSinceLastFrame);
            return true;
        }

        private void EnterRenderLoop()
        {
            mRoot.StartRendering();
        }

        protected void Shutdown()
        {
            throw new ShutdownException();
        }

        protected bool OnKeyPressed(MOIS.KeyEvent evt)
        {
            switch (evt.key)
            {
                case MOIS.KeyCode.KC_W:
                case MOIS.KeyCode.KC_UP:
                    //shiplist[0].Thrusting = 1;
                    mCameraMan.GoingForward = true;
                    break;

                case MOIS.KeyCode.KC_S:
                case MOIS.KeyCode.KC_DOWN:
                    //shiplist[0].Thrusting = -1;
                    mCameraMan.GoingBack = true;
                    break;

                case MOIS.KeyCode.KC_A:
                case MOIS.KeyCode.KC_LEFT:
                    //shiplist[0].Strafing = -1;
                    mCameraMan.GoingLeft = true;
                    break;

                case MOIS.KeyCode.KC_D:
                case MOIS.KeyCode.KC_RIGHT:
                    //shiplist[0].Strafing = 1;
                    mCameraMan.GoingRight = true;
                    break;

                case MOIS.KeyCode.KC_E:
                case MOIS.KeyCode.KC_PGUP:
                    //shiplist[0].Rotating = 1;
                    break;

                case MOIS.KeyCode.KC_Q:
                case MOIS.KeyCode.KC_PGDOWN:
                    //shiplist[0].Rotating = -1;
                    break;

                case MOIS.KeyCode.KC_SPACE:
                    Console.Out.WriteLine("space was pushed.");
                    //Rocks rocksphere = new Rocks(60, 6, 8);
                    //Console.Out.WriteLine(rocksphere.ToString());
                    //Console.Out.WriteLine(rocksphere.verts[1][1].ToString());
                    break;

                case MOIS.KeyCode.KC_LSHIFT:
                case MOIS.KeyCode.KC_RSHIFT:
                    //mCameraMan.FastMove = true;
                    break;

                case MOIS.KeyCode.KC_T:
                    //CycleTextureFilteringMode();
                    break;

                case MOIS.KeyCode.KC_R:
                    //CyclePolygonMode();
                    break;

                case MOIS.KeyCode.KC_F5:
                    //ReloadAllTextures();
                    break;

                case MOIS.KeyCode.KC_SYSRQ:
                    //TakeScreenshot();
                    break;



                case MOIS.KeyCode.KC_ESCAPE:
                    Shutdown();
                    break;
            }

            return true;
        }

        protected bool OnKeyReleased(MOIS.KeyEvent evt)
        {
            switch (evt.key)
            {
                case MOIS.KeyCode.KC_W:
                case MOIS.KeyCode.KC_UP:
     
                    mCameraMan.GoingForward = false;
                    break;

                case MOIS.KeyCode.KC_S:
                case MOIS.KeyCode.KC_DOWN:
                    
                    mCameraMan.GoingBack = false;
                    break;

                case MOIS.KeyCode.KC_A:
                case MOIS.KeyCode.KC_LEFT:
                    
                    mCameraMan.GoingLeft = false;
                    break;

                case MOIS.KeyCode.KC_D:
                case MOIS.KeyCode.KC_RIGHT:
                  
                    mCameraMan.GoingRight = false;
                    break;

                case MOIS.KeyCode.KC_E:
                case MOIS.KeyCode.KC_PGUP:
                  
                    mCameraMan.GoingUp = false;
                    break;

                case MOIS.KeyCode.KC_Q:
                case MOIS.KeyCode.KC_PGDOWN:
                   
                    mCameraMan.GoingDown = false;
                    break;

                case MOIS.KeyCode.KC_LSHIFT:
                case MOIS.KeyCode.KC_RSHIFT:
                    //mCameraMan.FastMove = false;
                    break;
            }

            return true;
        }

        protected virtual bool OnMouseMoved(MOIS.MouseEvent evt)
        {
            if (mCameraMan.MouseLook == true)
            {
                mCameraMan.MouseMovement(evt.state.X.rel, evt.state.Y.rel);
            }
            return true;
        }

        protected virtual bool OnMousePressed(MOIS.MouseEvent evt, MOIS.MouseButtonID id)
        {
            if (id == MOIS.MouseButtonID.MB_Right)
            {
                mCameraMan.MouseLook = true;
            }
            else if (id == MOIS.MouseButtonID.MB_Left)
            {

            }
            return true;
        }

        protected virtual bool OnMouseReleased(MOIS.MouseEvent evt, MOIS.MouseButtonID id)
        {
            if (id == MOIS.MouseButtonID.MB_Right)
            {
                mCameraMan.MouseLook = false;
            }
            return true;
        }

        private void CreateNewEntity(CombatObj obj)
        {

            Entity objEnt = mSceneMgr.CreateEntity(obj.icomobj.ID.ToString(), "DeltaShip.mesh");
            SceneNode objNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(obj.icomobj.ID.ToString());
            float sizex = objEnt.BoundingBox.Size.x;
            float sizey = objEnt.BoundingBox.Size.y;
            float sizez = objEnt.BoundingBox.Size.z;
            //float scalex = ((float)(obj.Size.X) / sizex);
            //float scaley = ((float)(obj.Size.Z) / sizey);
            //float scalez = ((float)(obj.Size.Y) / sizez);
            float scalex = ((float)(50) / sizex);
            float scaley = ((float)(50) / sizey);
            float scalez = ((float)(100) / sizez);
            objNode.Scale(scalex, scaley, scalez);
            //objNode.Yaw(new Degree(-90));

            objNode.AttachObject(objEnt);
            objNode.Position = new Vector3((float)obj.cmbt_loc.X, (float)obj.cmbt_loc.Z, (float)obj.cmbt_loc.Y);
            //renderObjects.Add(obj);
        }
        #endregion

        private void startloop()
        {
            bool running = true;
            while (running)
            {
                physicsstopwatch.Restart();
                double battletic = 0;
                while (physicsstopwatch.ElapsedMilliseconds < 1000)
                {
                    foreach (CombatObj comObj in renderObjects.Values)
                    {
                        Point3d renderloc = new Point3d(battle.simPhysTic(comObj, battletic, physicsstopwatch.ElapsedMilliseconds));
                        do_graphics(comObj, renderloc);
                    }
                }
                battletic++;
                foreach (CombatObj comObj in renderObjects.Values)
                {
                    Point3d renderloc = new Point3d(battle.simPhysTic(comObj, battletic));
                    do_graphics(comObj, renderloc);
                }
            }

        }

        private void do_graphics(CombatObj obj, Point3d renderloc)
        {
            //foreach (CombatObj obj in renderObjects.Values)
            //{

                //Console.Out.WriteLine(obj.Loc.ToString());

                //this stuff is for ground combat
                /*
                Ray heightRay = new Ray(new Vector3((float)obj.cmbt_loc.X, 5000.0f, (float)obj.cmbt_loc.Y), Vector3.NEGATIVE_UNIT_Y);
                mRaySceneQuery.Ray = heightRay;

                //// Execute query
                RaySceneQueryResult result = mRaySceneQuery.Execute();
                RaySceneQueryResult.Enumerator itr = (RaySceneQueryResult.Enumerator)(result.GetEnumerator());
                if ((itr != null) && itr.MoveNext())
                {
                    float terrainHeight = itr.Current.worldFragment.singleIntersection.y;
                    //Console.Out.WriteLine("T_height" + terrainHeight);
                    obj.cmbt_loc.Z = terrainHeight;
                }
                */

                SceneNode node = mSceneMgr.GetSceneNode(obj.icomobj.ID.ToString());
                Vector3 mvector3 = (TranslateMogrePhys.smVector_mVector3_xzy(renderloc));
                node.Position = mvector3;
                Vector3 rotateaxis = new Vector3(0f, 1f, 0f);
                Quaternion quat = new Quaternion((float)(Trig.angleA(obj.cmbt_face) - 1.57079633), rotateaxis);
                node.Orientation = quat;
            //}
        }
    }
}
