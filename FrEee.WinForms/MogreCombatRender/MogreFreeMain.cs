using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FrEee.Game.Objects.Combat2;
using System.Runtime.InteropServices;

using Mogre;
using System.Windows.Forms;
using System.Drawing;
using FrEee.WinForms.Forms;

using NewtMath.f16;
using FixMath.NET;
using FrEee.Utility;

namespace FrEee.WinForms.MogreCombatRender
{
	class ShutdownException : Exception { }
	public class MogreFreeMain
	{
		//private static int tickcounter = 0; //the tick age of the game.
		private Root mRoot;
		private RenderWindow mRenderWindow;
		private SceneManager mSceneMgr;
		private Viewport mViewport;

		private MOIS.Keyboard mKeybrd;
		private MOIS.Mouse mMouse;

		protected Camera mCamera;
		protected CameraMan mCameraMan;

		private MogreCombatForm form;
		private float replaySpeed = 1f;

		//protected RaySceneQuery mRaySceneQuery = null;      // The ray scene query pointer
		protected SceneNode mNode_lines = null;

		System.Diagnostics.Stopwatch physicsstopwatch = new System.Diagnostics.Stopwatch();

		//Dictionary<string, CombatObject> renderObjects = new Dictionary<string, CombatObject>();

		/// <summary>
		/// dictionary containing meshname, scale, and effects.
		/// </summary>
		Dictionary<string, GfxObj> dict_GfxObjects = new Dictionary<string, GfxObj>();

		private Battle_Space battle;

		private int SelectedComObj = -1;

		public MogreFreeMain(Battle_Space battle)
		{
			this.battle = battle;
			battle.IsReplay = true;


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

				setup();
				ProcessTick();

			}
			catch (OperationCanceledException) { }
		}

		private void setup()
		{

			battle.SetUpPieces();

			dict_GfxObjects.Clear();

			Console.WriteLine("Setting up combat Rendering Entities");
			foreach (CombatObject comObj in battle.CombatObjects)
			{
				CreateNewEntity(comObj);

				//do_graphics(comObj, comObj.cmbt_loc);
				Console.Write(".");
			}
			Console.WriteLine("Done");
		}

		#region mogresetup

		protected virtual void CreateCamera()
		{
			mCamera = mSceneMgr.CreateCamera("PlayerCam");
			mCamera.Position = new Vector3(0, 0, 1000f);
			mCamera.LookAt(Vector3.ZERO);
			mCamera.NearClipDistance = 5;
			mCamera.FarClipDistance = 10000f;
			mCameraMan = new CameraMan(mCamera);
		}

		private void CreateRoot()
		{
			mRoot = new Root(); //can change location/name of plugins.cfg ogre.cfg and Ogre.log files here using the Root parameters. 
		}

		private void DefineResources()
		{
			ConfigFile cf = new ConfigFile();
			cf.Load("resources.cfg", "\t:=", true);

			foreach (var empire in battle.EmpiresArray)
			{
				string racename = empire.ShipsetPath;
				string path = "Pictures\\Races\\";
				path += racename + "\\";
				ResourceGroupManager.Singleton.AddResourceLocation(path, "FileSystem", "General");
			}
			ResourceGroupManager.Singleton.AddResourceLocation("Pictures\\Races\\Default\\", "FileSystem", "General");

			var section = cf.GetSectionIterator();
			while (section.MoveNext())
			{
				foreach (var line in section.Current)
				{
					ResourceGroupManager.Singleton.AddResourceLocation(
						line.Value, line.Key, section.CurrentKey);
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
			// Create Render Window
			mRoot.Initialise(false, "Main Ogre Window");
			NameValuePairList misc = new NameValuePairList();
			form = new MogreCombatForm(battle);
			misc["externalWindowHandle"] = form.Handle.ToString();
			mRenderWindow = mRoot.CreateRenderWindow("Main RenderWindow", 800, 600, false, misc);
			form.Size = new Size(800, 600);
			form.Disposed += form_Disposed;
			form.Resize += form_Resize;
			form.Show();
		}

		void form_Resize(object sender, EventArgs e)
		{
			mRenderWindow.WindowMovedOrResized();
			mCamera.AspectRatio = (float)mViewport.ActualWidth / mViewport.ActualHeight;
		}

		void form_Disposed(object sender, EventArgs e)
		{
			Shutdown();
			mRoot.Dispose();
			mRoot = null;
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
			mSceneMgr = mRoot.CreateSceneManager(SceneType.ST_GENERIC);

			CreateCamera();
			mViewport = mRenderWindow.AddViewport(mCamera);
			mViewport.BackgroundColour = ColourValue.Black;
			mCamera.AspectRatio = (float)mViewport.ActualWidth / mViewport.ActualHeight;


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
			MaterialPtr moMaterialpurple = MaterialManager.Singleton.Create("line_purple", resourceGroupName);
			moMaterialpurple.ReceiveShadows = false;
			moMaterialpurple.GetTechnique(0).SetLightingEnabled(true);
			moMaterialpurple.GetTechnique(0).GetPass(0).SetDiffuse(1, 0, 1, 0);
			moMaterialpurple.GetTechnique(0).GetPass(0).SetAmbient(1, 0, 1);
			moMaterialpurple.GetTechnique(0).GetPass(0).SetSelfIllumination(1, 0, 1);
			moMaterialpurple.Dispose();  // dispose pointer, not the material
			MaterialPtr moMaterialyellow = MaterialManager.Singleton.Create("line_yellow", resourceGroupName);
			moMaterialyellow.ReceiveShadows = false;
			moMaterialyellow.GetTechnique(0).SetLightingEnabled(true);
			moMaterialyellow.GetTechnique(0).GetPass(0).SetDiffuse(1, 1, 0, 0);
			moMaterialyellow.GetTechnique(0).GetPass(0).SetAmbient(1, 1, 0);
			moMaterialyellow.GetTechnique(0).GetPass(0).SetSelfIllumination(1, 1, 0);
			moMaterialyellow.Dispose();  // dispose pointer, not the material
			MaterialPtr moMaterialcyan = MaterialManager.Singleton.Create("line_cyan", resourceGroupName);
			moMaterialcyan.ReceiveShadows = false;
			moMaterialcyan.GetTechnique(0).SetLightingEnabled(true);
			moMaterialcyan.GetTechnique(0).GetPass(0).SetDiffuse(0, 1, 1, 0);
			moMaterialcyan.GetTechnique(0).GetPass(0).SetAmbient(0, 1, 1);
			moMaterialcyan.GetTechnique(0).GetPass(0).SetSelfIllumination(0, 1, 1);
			moMaterialcyan.Dispose();  // dispose pointer, not the material

			mNode_lines = mSceneMgr.RootSceneNode.CreateChildSceneNode(" ", Vector3.ZERO);
			mNode_lines.Position = new Vector3(0, 0, 0);
			mSceneMgr.AmbientLight = new ColourValue(0.5f, 0.5f, 0.5f);

			Light l = mSceneMgr.CreateLight("MainLight");
			l.Position = new Vector3(0, 0, 5000);

			ParticleSystem explosionParticle = mSceneMgr.CreateParticleSystem("Explosion", "Explosion");


			//SceneNode particleNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("Particle");
			//particleNode.AttachObject(explosionParticle);

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
			if (mRoot != null)
				mRoot.StartRendering();
		}

		protected void Shutdown()
		{
			foreach (CombatNode comNode in battle.CombatNodes.ToArray())
			{
				if (comNode.ID < -1)
				{
					battle.CombatNodes.Remove(comNode);
				}
			}
			mRoot.Dispose();
			//throw new ShutdownException();
		}

		#region input control
		protected bool OnKeyPressed(MOIS.KeyEvent evt)
		{
			switch (evt.key)
			{
				case MOIS.KeyCode.KC_W:
				case MOIS.KeyCode.KC_UP:
					//shiplist[0].Thrusting = 1;
					//mCameraMan.GoingForward = true;
					mCameraMan.GoingUp = true;
					break;

				case MOIS.KeyCode.KC_S:
				case MOIS.KeyCode.KC_DOWN:
					//shiplist[0].Thrusting = -1;
					//mCameraMan.GoingBack = true;
					mCameraMan.GoingDown = true;
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
					break;

				case MOIS.KeyCode.KC_PGUP:
					replaySpeed *= 2f;
					break;

				case MOIS.KeyCode.KC_Q:
					break;

				case MOIS.KeyCode.KC_PGDOWN:
					replaySpeed *= 0.5f;
					break;

				case MOIS.KeyCode.KC_SPACE:
					//Console.Out.WriteLine("space was pushed.");
					//Rocks rocksphere = new Rocks(60, 6, 8);
					//Console.Out.WriteLine(rocksphere.ToString());
					//Console.Out.WriteLine(rocksphere.verts[1][1].ToString());
					break;

				case MOIS.KeyCode.KC_LSHIFT:
				case MOIS.KeyCode.KC_RSHIFT:
					mCameraMan.FastMove = true;
					break;

				case MOIS.KeyCode.KC_LBRACKET:
					selectPrev();
					break;

				case MOIS.KeyCode.KC_RBRACKET:
					selectNext();
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

					//mCameraMan.GoingForward = false;
					mCameraMan.GoingUp = false;
					break;

				case MOIS.KeyCode.KC_S:
				case MOIS.KeyCode.KC_DOWN:

					//mCameraMan.GoingBack = false;
					mCameraMan.GoingDown = false;
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
					mCameraMan.FastMove = false;
					break;
			}

			return true;
		}

		protected virtual bool OnMouseMoved(MOIS.MouseEvent evt)
		{
			if (mCameraMan.MouseLook == true)
			{
				mCameraMan.MouseMovement(evt.state.X.rel, evt.state.Y.rel, evt.state.Z.rel);
			}
			else
				mCameraMan.MouseMovement(0, 0, evt.state.Z.rel);
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

		#endregion

		private GfxObj CreateGfxObj(CombatNode ComNode)
		{
			GfxObj gfxobj = new GfxObj();
			string filestring = "Pictures\\Races\\Default\\Delta_Escort.cfg"; //needs to be a proper default shipset. 
			if (ComNode is CombatVehicle)
			{
				CombatVehicle cv = (CombatVehicle)ComNode;
				string racename = cv.StartVehicle.Owner.ShipsetPath;
				string path = "Pictures\\Races\\";
				path += racename + "\\";
				IList<string> names = cv.StartVehicle.Hull.PictureNames;
				bool exsists = false;
				int i = 0;
				while (!exsists && i < names.Count)
				{
					string name = path + racename + "_" + names[i] + ".cfg"; //

					if (System.IO.File.Exists(name))//check path to ensure correct.
					{
						filestring = name;
						exsists = true;
					}
					i++;
				}
			}
			else if (ComNode is CombatPlanet)
			{
				// TODO - planet models
				filestring = "Pictures\\Races\\Default\\Delta_Escort.cfg";
			}
			else if (ComNode is CombatSeeker)
			{
				filestring = "Pictures\\MogreCombat\\Projectiles\\Seeker.cfg";
			}
			else
			{
				filestring = "Pictures\\MogreCombat\\Projectiles\\Sabbot.cfg";
			}

			string jsonstring = System.IO.File.ReadAllText(filestring);
			try
			{
				gfxobj.gfxCfg = Newtonsoft.Json.JsonConvert.DeserializeObject<GfxCfg>(jsonstring);
			}
			catch
			{
				Console.Write("Deserialisation failed for obj: ");
				Console.WriteLine(ComNode.strID);
				Console.WriteLine("check " + filestring + " for errors");
			}
			gfxobj.IDName = ComNode.strID;

			if (dict_GfxObjects.ContainsKey(ComNode.strID))
				dict_GfxObjects[ComNode.strID] = gfxobj;
			else
				dict_GfxObjects.Add(ComNode.strID, gfxobj);
			return gfxobj;
		}

		private void CreateNewEntity(CombatNode comNode)
		{
			GfxObj gfxobj = CreateGfxObj(comNode);
			try
			{
				string meshname = gfxobj.gfxCfg.MainMesh.Name;

				Entity objEnt = mSceneMgr.CreateEntity(comNode.strID, meshname);
				SceneNode objNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(comNode.strID);
				objNode.AttachObject(objEnt);
				gfxobj.gfxEfct = new GfxEfx(mSceneMgr, gfxobj);
				objNode.Scale(new Vector3(gfxobj.gfxCfg.MainMesh.Scale));


				//Entity objEnt = mSceneMgr.CreateEntity(obj.strID, "DeltaShip.mesh");
				//SceneNode objNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(obj.strID);
				//float sizex = objEnt.BoundingBox.Size.x;
				//float sizey = objEnt.BoundingBox.Size.y;
				//float sizez = objEnt.BoundingBox.Size.z;
				//var desiredSize = 0.5f;
				//float scalex = (desiredSize / sizex);
				//float scaley = (desiredSize / sizey);
				//float scalez = (desiredSize / sizez);
				//float scale = System.Math.Min(System.Math.Min(scalex, scaley), scalez);
				//objNode.AttachObject(objEnt);
				//objNode.Scale(scale, scale, scale);
				//objNode.Scale(10, 10, 10);
                Console.WriteLine("OgreEntity Created: " + comNode.strID);

			}
			catch (Exception ex)
			{
                Console.WriteLine("OgreEntity creation failed for " + comNode.strID);
				Console.Error.WriteLine(ex);
			}

		}

		#endregion

		public CombatVehicle selectedObj()
		{
			if (SelectedComObj < 0)
				return null;
			return battle.CombatVehicles.ToList<CombatVehicle>()[SelectedComObj];
		}
		public CombatObject selectNext()
		{
			SelectedComObj++;
			if (SelectedComObj == battle.CombatObjects.Count())
			{
				SelectedComObj = 0;
			}
			return selectedObj();
		}
		public CombatObject selectPrev()
		{
			SelectedComObj--;
			if (SelectedComObj == -1)
			{
				SelectedComObj = battle.CombatObjects.Count() - 1;
			}
			return selectedObj();
		}



		private void ProcessTick()
		{
			int battletic = 0;
			double cmdfreq_countr = 0;

			bool cont = true; // is combat continuing?
			var renderlocs = new SafeDictionary<CombatNode, PointXd>();
			Console.WriteLine("starting Replay");
			while (cont)// && mRoot != null && mRoot.RenderOneFrame())
			{
#if DEBUG
				Console.WriteLine("ReplayingTick: " + battletic);
#endif
				physicsstopwatch.Restart();
				int interpolationcount = 0;
				while (physicsstopwatch.ElapsedMilliseconds < (100 / replaySpeed))
				{
					//foreach (CombatObject comObj in battle.CombatObjects)
					//{
					//    renderlocs[comObj] = battle.InterpolatePosition(comObj, physicsstopwatch.ElapsedMilliseconds / (100f / replaySpeed));
					//    do_graphics(comObj, renderlocs[comObj]);
					//}
					foreach (CombatNode comNode in battle.CombatNodes) //update bullet and explosion objects.
					{
						renderlocs[comNode] = battle.InterpolatePosition(comNode, physicsstopwatch.ElapsedMilliseconds / (100f / replaySpeed));
						do_graphics(comNode, renderlocs[comNode]);
					}
					if (mRoot != null)
						mRoot.RenderOneFrame();
					interpolationcount++;
					Application.DoEvents();
				}
#if DEBUG
				Console.WriteLine("Interpol count is: " + interpolationcount);
#endif

				foreach (var comObj in battle.CombatObjects.ToArray())
					comObj.debuginfo = "";

				foreach (var comObj in battle.CombatObjects.ToArray())
				{
#if DEBUG
					Console.WriteLine("comObj.helm " + comObj.strID);
#endif
					comObj.helm(); //heading and thrust
				}
				foreach (var comObj in battle.CombatObjects.ToArray())
				{
					//firecontrol, these get logged, but we still need to run through it
					//so that prng.next happens, and damage is done.
					battle.firecontrol(battletic, comObj);
				}

				foreach (var comNode in battle.CombatNodes.ToArray())
				{
					//physicsmove objects. 
					PointXd renderloc = battle.SimNewtonianPhysics(comNode);
					renderlocs[comNode] = renderloc;
				}


				foreach (var comObj in battle.CombatObjects.ToArray())
					do_graphics(comObj, renderlocs[comObj]);

				foreach (CombatNode comNode in battle.CombatNodes.Where(n => !(n is CombatObject)).ToArray()) //update bullet and explosion objects.
				{
					if (comNode.deathTick <= battletic)
					{
						battle.CombatNodes.Remove(comNode);
						battle.DeadNodes.Remove(comNode);
						disposeObj(comNode, renderlocs);
					}
					else
					{
						renderlocs[comNode] = battle.SimNewtonianPhysics(comNode);
						do_graphics(comNode, renderlocs[comNode]);
					}
				}

				//readlogs, create and dispose of bullets.
				foreach (var comObj in battle.CombatObjects.ToArray())
					readlogs(comObj, battletic, renderlocs);

				if (cmdfreq_countr >= Battle_Space.CommandFrequencyTicks)
				{
					foreach (var ccobj in battle.ControlledCombatObjects)
					{
						battle.commandAI(ccobj, battletic);
					}
					cmdfreq_countr = 0;
				}

				foreach (CombatNode comNode in battle.FreshNodes.ToArray())
				{
					CreateNewEntity(comNode);
					battle.CombatNodes.Add(comNode);
					battle.FreshNodes.Remove(comNode);
                    renderlocs[comNode] = battle.InterpolatePosition(comNode, physicsstopwatch.ElapsedMilliseconds / (100f / replaySpeed));
                    
                    
#if DEBUG
                    Console.WriteLine("added " + comNode.strID + " to CombatNodes from FreshNodes");
#endif
				}
				foreach (CombatNode comNode in battle.DeadNodes.ToArray())
				{
					disposeObj(comNode, renderlocs);
					battle.CombatNodes.Remove(comNode);
					battle.DeadNodes.Remove(comNode);
					if (comNode is CombatObject)
					{

						foreach (CombatEmpire emp in battle.Empires.Values)
						{
							emp.removeComObj((CombatObject)comNode);
						}
					}
#if DEBUG
					Console.WriteLine("disposing " + comNode.strID);
#endif
				}

				if (battle.ReplayLog.EventsForTick(battletic).OfType<CombatEndBattleEvent>().Any())
					cont = false;
				else
					cont = true;

				do_txt();
				cmdfreq_countr++;
				battletic++;

				Application.DoEvents();
			}

			//might as well keep rendering after the battle is over.
			bool loopafterbattle = true;
			while (loopafterbattle && mRoot != null && mRoot.RenderOneFrame())
			{
				foreach (CombatNode comNode in battle.CombatNodes)
				{

					do_graphics(comNode, renderlocs[comNode]);

				}
				do_txt();
				Application.DoEvents();
			}
		}


		private void readlogs(CombatObject comObj, int battletic, SafeDictionary<CombatNode, PointXd> renderlocs)
		{
			var ourLogs = battle.ReplayLog.EventsForObjectAtTick(comObj, battletic);
			foreach (var comEvent in ourLogs)
			{
				if (comEvent is CombatFireOnTargetEvent)
				{

					// TODO - if type projectile, create whatever sprite and render it flying towards target.
					// or if type beam, draw a beam sprite
					// seekers should really be their own event type, spawning new combat objects that track enemies
					CombatFireOnTargetEvent fireEvent = (CombatFireOnTargetEvent)comEvent;
					var wpninfo = fireEvent.Weapon.weapon.Template.ComponentTemplate.WeaponInfo;

					Console.WriteLine(comObj.strID + " Fires on " + fireEvent.TakeFireEvent.Object.strID);
					Console.WriteLine("With it's " + fireEvent.Weapon.weapon.Name);


					if (fireEvent.Location == comObj.cmbt_loc)
					{
					}
					else
					{
						//Point3d difference = fireEvent.Location - comObj.cmbt_loc;
						//Console.WriteLine("Desync at tick: " + battletic);
						//Console.WriteLine("Loc difference of: " + difference.ToString());
					}
					if (fireEvent.Weapon.weaponType == "Seeker")
					{
					}
					else if (fireEvent.Weapon.weaponType == "Bolt")
					{
					}
					else if (fireEvent.Weapon.weaponType == "Beam")
					{
					}
				}
				else if (comEvent is CombatTakeFireEvent)
				{
					// TODO - kersplosions
					CombatTakeFireEvent takefireEvent = (CombatTakeFireEvent)comEvent;
					if (takefireEvent.IsHit)
					{
						Console.WriteLine(takefireEvent.fireOnEvent.Weapon.weapon.Name + " Hits it's target!");
					}
					else
					{
						Console.WriteLine(takefireEvent.fireOnEvent.Weapon.weapon.Name + " Misses it's target!");
					}
				}
				else if (comEvent is CombatDestructionEvent)
				{
					// - kersplosions and removal of model. 
					string IDName = comEvent.Object.strID;
					SceneNode node = mSceneMgr.GetSceneNode(IDName);
					ParticleSystem expl = mSceneMgr.GetParticleSystem("Explosion");
					node.AttachObject(expl);
				}
				else if (comEvent is CombatLocationEvent)
				{
					CombatLocationEvent locEvent = (CombatLocationEvent)comEvent;
					if (locEvent.Location == comObj.cmbt_loc)
					{
					}
					else
					{
						//Point3d difference = locEvent.Location - comObj.cmbt_loc;
						//Console.WriteLine("Desync at tick: " + battletic);
						//Console.WriteLine("Loc difference of: " + difference.ToString());
					}
				}
			}
		}

		private void disposeObj(CombatNode comNode, SafeDictionary<CombatNode, PointXd> renderlocs)
		{

			SceneNode node = mSceneMgr.GetSceneNode(comNode.strID);
			Entity objEnt = mSceneMgr.GetEntity(comNode.strID);
			mSceneMgr.DestroySceneNode(node);
			mSceneMgr.DestroyEntity(objEnt);
			objEnt.Dispose();
			node.Dispose();

			//remove from the list of game nodes.
			renderlocs.Remove(comNode);

		}

		private void do_txt()
		{
			CombatVehicle comVehic = selectedObj();
			if (comVehic != null)
			{
				Game.Objects.Vehicles.Ship ship = (Game.Objects.Vehicles.Ship)comVehic.WorkingVehicle;

				string txt = ship.Name + "\r\n";
				txt += "Location:\t"
					+ comVehic.cmbt_loc.X.ToString() + "\r\n\t"
					+ comVehic.cmbt_loc.Y.ToString() + "\r\n\t"
					+ comVehic.cmbt_loc.Z.ToString() + "\r\n";
				double speed = (double)Trig.hypotinuse(comVehic.cmbt_vel);
				txt += "Speed:\t" + speed.ToString() + "\r\n";
				txt += "Heading:\t" + comVehic.cmbt_head.Degrees.ToString() + "\r\n";

				txt += "\r\n";

				//Game.Objects.Vehicles.Ship tgtship = (Game.Objects.Vehicles.Ship)comVehic.weaponTarget[0].icomobj_WorkingCopy;
				//txt += "Target:\t" + tgtship.Name + "\r\n";
				txt += "Distance\t" + Trig.hypotinuse(comVehic.cmbt_loc - comVehic.weaponTarget[0].cmbt_loc) + "\r\n";

				txt += comVehic.debuginfo;

				form.updateText(txt);
			}
			else
				form.updateText("Nothing selected");
		}

		private void do_lines(CombatObject comObj, PointXd renderloc)
		{

			//mSceneMgr.DestroyManualObject("toWaypointLine" + IDName);
			//ManualObject toWaypointLine = mSceneMgr.CreateManualObject("toWaypointLine" + IDName);
			//mNode_lines.AttachObject(toWaypointLine);
			//toWaypointLine.Begin("line_purple", RenderOperation.OperationTypes.OT_LINE_LIST);
			//toWaypointLine.Position(node.Position);
			////toWaypointLine.Position(new Vector3((float)comObj.waypointTarget.cmbt_loc.X, (float)comObj.waypointTarget.cmbt_loc.Y, (float)comObj.waypointTarget.cmbt_loc.Z));
			//toWaypointLine.Position(TranslateMogrePhys.smVector_mVector3_xyz(comObj.waypointTarget.cmbt_loc));
			//toWaypointLine.End();


			//mSceneMgr.DestroyManualObject("forceLine" + IDName);
			//ManualObject forceLine = mSceneMgr.CreateManualObject("forceLine" + IDName);
			////forceLine.
			//mNode_lines.AttachObject(forceLine);
			//forceLine.Begin("line_blue", RenderOperation.OperationTypes.OT_LINE_LIST);
			//forceLine.Position(node.Position);
			//forceLine.Position(node.Position + (TranslateMogrePhys.smVector_mVector3_xyz(comObj.cmbt_thrust)));
			//forceLine.End();

			//mSceneMgr.DestroyManualObject("toTargetLine" + IDName);
			//ManualObject toTargetLine = mSceneMgr.CreateManualObject("toTargetLine" + IDName);
			//node.AttachObject(toTargetLine);
			//toTargetLine.Begin("line_yellow", RenderOperation.OperationTypes.OT_LINE_LIST);
			//toTargetLine.Position(new Vector3(0, 0, 0));
			//toTargetLine.Position(new Vector3((float)comObj.waypointTarget.comObj.cmbt_loc.X, (float)comObj.waypointTarget.comObj.cmbt_loc.Y, (float)comObj.waypointTarget.comObj.cmbt_loc.Z));
			//toTargetLine.End();


			//mSceneMgr.DestroyManualObject("forceLine2" + IDName);
			//ManualObject forceLine2 = mSceneMgr.CreateManualObject("forceLine2" + IDName);
			//node.AttachObject(forceLine2);
			//forceLine2.Begin("line_red", RenderOperation.OperationTypes.OT_LINE_LIST);
			//forceLine2.Position(new Vector3(0, 0, 0));
			//forceLine2.Position(forceVec2);
			//forceLine2.End();
			//Console.Out.WriteLine(obj.waypointTarget.comObj.cmbt_loc.ToString());
		}

		private void do_graphics(CombatNode comNode, PointXd renderloc)
		{
			try
			{
				SceneNode node = mSceneMgr.GetSceneNode(comNode.strID);

				node.Position = new Vector3((float)renderloc.X, (float)renderloc.Y, (float)renderloc.Z);
				Quaternion quat = new Quaternion((float)comNode.cmbt_head.Radians, Vector3.NEGATIVE_UNIT_Z);
				node.Orientation = quat;

				dict_GfxObjects[comNode.strID].gfxEfct.set_effects(comNode);
			}
			catch (Exception ex)
			{
				Console.Write("do_graphics broke while rendering object: ");
				Console.WriteLine(comNode.strID);
				Console.WriteLine(ex.GetType() + ": " + ex.Message);
			}
		}
	}
}
