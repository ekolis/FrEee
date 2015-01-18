using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FrEee.Utility.Extensions;

using Mogre;
using FrEee.Utility;

namespace FrEee.WinForms.MogreCombatRender
{

	/// <summary>
	/// http://www.ogre3d.org/tikiwiki/tiki-index.php?page=MOGRE+SpriteManager2d
	/// </summary>
	public static class SpriteManager
	{
		#region Fields

		private static HardwareVertexBufferSharedPtr hardwareBuffer;
		private static RenderOperation renderOp;
		private static SceneManager sceneMan;
		private static LinkedList<Sprite> sprites;

		#endregion Fields

		#region Properties

		public static bool AfterQueue
		{
			get;
			set;
		}

		public static int MinimalHardwareBufferSize
		{
			get;
			set;
		}

		public static RenderQueueGroupID TargetQueue
		{
			get;
			set;
		}

		#endregion Properties

		#region Methods

		public static void Add(Sprite sprite)
		{
			sprites.AddLast(sprite);
		}

		public static void Remove(Sprite sprite)
		{
			sprites.Remove(sprite);
		}

		public static void Initialize(SceneManager sceneManager)
		{
			sceneMan = sceneManager;
			TargetQueue = RenderQueueGroupID.RENDER_QUEUE_OVERLAY;
			AfterQueue = false;
			MinimalHardwareBufferSize = 120;
			sprites = new LinkedList<Sprite>();
			sceneMan.RenderQueueStarted += RenderQueueStarted;
			sceneMan.RenderQueueEnded += RenderQueueEnded;
		}

		public static void Shutdown()
		{
			if (hardwareBuffer != null)
			{
				HardwareBuffer_Destroy();
			}

			if (sceneMan != null)
			{
				sceneMan.RenderQueueStarted -= RenderQueueStarted;
				sceneMan.RenderQueueEnded -= RenderQueueEnded;
			}
		}

		private static void HardwareBuffer_Create(int size)
		{
			VertexDeclaration vd;

			renderOp = new RenderOperation();
			renderOp.vertexData = new VertexData();
			renderOp.vertexData.vertexStart = 0;

			vd = renderOp.vertexData.vertexDeclaration;
			vd.AddElement(
				0,
				0,
				VertexElementType.VET_FLOAT3,
				VertexElementSemantic.VES_POSITION);

			vd.AddElement(
				0,
				VertexElement.GetTypeSize(VertexElementType.VET_FLOAT3),
				VertexElementType.VET_FLOAT2,
				VertexElementSemantic.VES_TEXTURE_COORDINATES);

			hardwareBuffer = HardwareBufferManager.Singleton.CreateVertexBuffer(
				vd.GetVertexSize(0),
				(uint)size,
				HardwareBuffer.Usage.HBU_DYNAMIC_WRITE_ONLY_DISCARDABLE,
				true);

			renderOp.vertexData.vertexBufferBinding.SetBinding(0, hardwareBuffer);

			renderOp.operationType = RenderOperation.OperationTypes.OT_TRIANGLE_LIST;
			renderOp.useIndexes = false;
		}

		private static void HardwareBuffer_Destroy()
		{
			hardwareBuffer.Dispose();
			renderOp.vertexData.Dispose();
			renderOp.Dispose();
		}

		private static void Render()
		{
			if (sprites.Count == 0)
			{
				return;
			}

			RenderSystem rs = Root.Singleton.RenderSystem;

			Chunk thisChunk = new Chunk();
			List<Chunk> chunks = new List<Chunk>();

			int newSize;

			newSize = sprites.Count * 6;
			if (newSize < MinimalHardwareBufferSize)
			{
				newSize = MinimalHardwareBufferSize;
			}

			// grow hardware buffer if needed
			if (hardwareBuffer == null || hardwareBuffer.NumVertices < newSize)
			{
				if (hardwareBuffer != null)
				{
					HardwareBuffer_Destroy();
				}

				HardwareBuffer_Create(newSize);
			}

			// write quads to the hardware buffer, and remember chunks
			unsafe
			{
				Vertex* buffer = (Vertex*)hardwareBuffer.Lock(HardwareBuffer.LockOptions.HBL_DISCARD);

				LinkedListNode<Sprite> node = sprites.First;
				Sprite currSpr;

				while (node != null)
				{
					currSpr = node.Value;
					currSpr.UpdatePosition();
					thisChunk.Alpha = currSpr.Alpha;
					thisChunk.TexHandle = currSpr.TexHandle;
					thisChunk.Sprite = currSpr;

					for (int i = 0; i < 6; i++)
					{
						*buffer++ = new Vertex(
							currSpr.Pos[i],
							currSpr.UV[i]);
					}

					thisChunk.VertexCount += 6;

					node = node.Next;

					if (node == null || thisChunk.TexHandle != node.Value.TexHandle || thisChunk.Alpha != node.Value.Alpha)
					{
						chunks.Add(thisChunk);
						thisChunk.VertexCount = 0;
					}
				}
			}

			hardwareBuffer.Unlock();

			// set up...
			RenderSystem_Setup();

			// do the real render!
			// do the real render!
			TexturePtr tp = null;
			renderOp.vertexData.vertexStart = 0;
			foreach (Chunk currChunk in chunks)
			{
				renderOp.vertexData.vertexCount = currChunk.VertexCount;
				tp = TextureManager.Singleton.GetByHandle(currChunk.TexHandle);
				if (currChunk.Sprite.Parent == null)
				{
					// use screen coords
					rs._setWorldMatrix(Matrix4.IDENTITY);
					rs._setViewMatrix(Matrix4.IDENTITY);
					rs._setProjectionMatrix(Matrix4.IDENTITY);
				}
				else
				{
					// use local coords
					//var world = currChunk.Sprite.Parent._getFullTransform();
					//rs._setWorldMatrix(world);
					rs._setWorldMatrix(Matrix4.IDENTITY);
					rs._setViewMatrix(sceneMan.CurrentViewport.Camera.ViewMatrix);
					rs._setProjectionMatrix(sceneMan.CurrentViewport.Camera.ProjectionMatrix);
				}
				rs._setTexture(0, true, tp.Name);
				rs._setTextureUnitFiltering(
					 0,
					 FilterOptions.FO_LINEAR,
					 FilterOptions.FO_LINEAR,
					 FilterOptions.FO_POINT);

				// set alpha
				LayerBlendModeEx_NativePtr alphaBlendMode = LayerBlendModeEx_NativePtr.Create();
				alphaBlendMode.alphaArg1 = 0;
				alphaBlendMode.alphaArg2 = currChunk.Alpha;
				alphaBlendMode.source1 = LayerBlendSource.LBS_TEXTURE;
				alphaBlendMode.source2 = LayerBlendSource.LBS_MANUAL;
				alphaBlendMode.blendType = LayerBlendType.LBT_ALPHA;
				alphaBlendMode.operation = LayerBlendOperationEx.LBX_MODULATE;
				alphaBlendMode.factor = currChunk.Alpha;
				rs._setTextureBlendMode(0, alphaBlendMode);

				rs._render(renderOp);
				renderOp.vertexData.vertexStart += currChunk.VertexCount;
				alphaBlendMode.DestroyNativePtr();
			}

			if (tp != null)
			{
				tp.Dispose();
			}
		}

		private static void RenderQueueEnded(byte queueGroupId, string invocation, out bool repeatThisInvocation)
		{
			repeatThisInvocation = false; // shut up compiler
			if (AfterQueue && queueGroupId == (byte)TargetQueue)
			{
				Render();
			}
		}

		private static void RenderQueueStarted(byte queueGroupId, string invocation, out bool skipThisInvocation)
		{
			skipThisInvocation = false; // shut up compiler
			if (!AfterQueue && queueGroupId == (byte)TargetQueue)
			{
				Render();
			}
		}

		private static void RenderSystem_Setup()
		{
			RenderSystem rs = Root.Singleton.RenderSystem;

			LayerBlendModeEx_NativePtr colorBlendMode = LayerBlendModeEx_NativePtr.Create();
			colorBlendMode.blendType = LayerBlendType.LBT_COLOUR;
			colorBlendMode.source1 = LayerBlendSource.LBS_TEXTURE;
			colorBlendMode.operation = LayerBlendOperationEx.LBX_SOURCE1;

			TextureUnitState.UVWAddressingMode uvwAddressMode;
			uvwAddressMode.u = TextureUnitState.TextureAddressingMode.TAM_CLAMP;
			uvwAddressMode.v = TextureUnitState.TextureAddressingMode.TAM_CLAMP;
			uvwAddressMode.w = TextureUnitState.TextureAddressingMode.TAM_CLAMP;

			rs._setTextureMatrix(0, Matrix4.IDENTITY);
			rs._setTextureCoordSet(0, 0);
			rs._setTextureCoordCalculation(0, TexCoordCalcMethod.TEXCALC_NONE);
			rs._setTextureBlendMode(0, colorBlendMode);
			rs._setTextureAddressingMode(0, uvwAddressMode);
			rs._disableTextureUnitsFrom(1);
			rs.SetLightingEnabled(false);
			rs._setFog(FogMode.FOG_NONE);
			rs._setCullingMode(CullingMode.CULL_NONE);
			rs._setDepthBufferParams(false, false);
			rs._setColourBufferWriteEnabled(true, true, true, false);
			rs.SetShadingType(ShadeOptions.SO_GOURAUD);
			rs._setPolygonMode(PolygonMode.PM_SOLID);
			rs.UnbindGpuProgram(GpuProgramType.GPT_FRAGMENT_PROGRAM);
			rs.UnbindGpuProgram(GpuProgramType.GPT_VERTEX_PROGRAM);
			rs._setSeparateSceneBlending(
				SceneBlendFactor.SBF_SOURCE_ALPHA,
				SceneBlendFactor.SBF_ONE_MINUS_SOURCE_ALPHA,
				SceneBlendFactor.SBF_ONE,
				SceneBlendFactor.SBF_ONE);
			rs._setAlphaRejectSettings(CompareFunction.CMPF_ALWAYS_PASS, 0, true);

			colorBlendMode.DestroyNativePtr();
		}

		#endregion Methods

		#region Nested Types

		internal struct Chunk
		{
			#region Properties

			public Sprite Sprite
			{
				get;
				set;
			}

			public float Alpha
			{
				get;
				set;
			}

			public ulong TexHandle
			{
				get;
				set;
			}

			public uint VertexCount
			{
				get;
				set;
			}

			#endregion Properties
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct Vertex
		{
			[FieldOffset(0)]
			public Vector3 Pos;
			[FieldOffset(12)]
			public Vector2 UV;

			#region Constructors

			public Vertex(Vector3 pos, Vector2 uv)
			{
				this.Pos = pos;
				this.UV = uv;
			}

			#endregion Constructors
		}

		#endregion Nested Types
	}

	public class Sprite
	{
		public Sprite(SceneNode parent, ulong texhandle, float alpha, float tx1 = 0, float ty1 = 0, float tx2 = 1, float ty2 = 1)
		{
			TexHandle = texhandle;
			Alpha = alpha;
			Parent = parent;

			UpdatePosition();

			UV = new Vector2[6];

			UV[0] = new Vector2(tx1, ty2);

			UV[1] = new Vector2(tx2, ty1);

			UV[2] = new Vector2(tx1, ty1);

			UV[3] = new Vector2(tx1, ty2);

			UV[4] = new Vector2(tx2, ty1);

			UV[5] = new Vector2(tx2, ty2);

		}

		#region Properties

		public SceneNode Parent
		{
			get;
			set;
		}

		public float Alpha
		{
			get;
			set;
		}

		internal Vector3[] Pos
		{
			get;
			set;
		}

		public ulong TexHandle
		{
			get;
			set;
		}

		internal Vector2[] UV
		{
			get;
			set;
		}

		#endregion Properties

		#region Methods

		public void UpdatePosition()
		{
			if (Parent != null)
			{
				var x = Parent.Position.x;
				var y = Parent.Position.y;
				var s = System.Math.Max(Parent.GetScale().x, Parent.GetScale().y);
				var x1 = x - s / 2f;
				var x2 = x + s / 2f;
				var y1 = y - s / 2f;
				var y2 = y + s / 2f;

				Pos = new Vector3[6];
				UV = new Vector2[6];

				var z = -1f;

				Pos[0] = new Vector3(x1, y2, z);

				Pos[1] = new Vector3(x2, y1, z);

				Pos[2] = new Vector3(x1, y1, z);

				Pos[3] = new Vector3(x1, y2, z);

				Pos[4] = new Vector3(x2, y1, z);

				Pos[5] = new Vector3(x2, y2, z);
			}
			else
			{
				var x1 = -1;
				var x2 = 1;
				var y1 = -1;
				var y2 = 1;

				Pos = new Vector3[6];
				UV = new Vector2[6];

				var z = -1f;

				Pos[0] = new Vector3(x1, y2, z);

				Pos[1] = new Vector3(x2, y1, z);

				Pos[2] = new Vector3(x1, y1, z);

				Pos[3] = new Vector3(x1, y2, z);

				Pos[4] = new Vector3(x2, y1, z);

				Pos[5] = new Vector3(x2, y2, z);
			}
		}

		public static bool operator !=(Sprite left, Sprite right)
		{
			if (left.SafeEquals(null) && right.SafeEquals(null))
				return false;
			if (left.SafeEquals(null) || right.SafeEquals(null))
				return true;

			return !left.Equals(right);
		}

		public static bool operator ==(Sprite left, Sprite right)
		{
			if (left.SafeEquals(null) && right.SafeEquals(null))
				return true;
			if (left.SafeEquals(null) || right.SafeEquals(null))
				return false;

			return left.Equals(right);
		}

		public override bool Equals(object obj)
		{
			if (obj is Sprite)
			{
				return this.Equals((Sprite)obj); // use Equals method below
			}
			else
			{
				return false;
			}
		}

		public bool Equals(Sprite other)
		{
			if (this.SafeEquals(null) && other.SafeEquals(null))
				return true;
			if (this.SafeEquals(null) || other.SafeEquals(null))
				return false;

			bool equal = this.TexHandle == other.TexHandle && this.Alpha == other.Alpha;

			if (!equal)
			{
				return false;
			}

			for (int i = 0; i < 6; i++)
			{
				if (this.Pos[i] != other.Pos[i])
				{
					return false;
				}

				if (this.UV[i] != other.UV[i])
				{
					return false;
				}
			}

			return true;
		}

		public override int GetHashCode()
		{
			return HashCodeMasher.Mash(Alpha, Pos, UV, TexHandle);
		}

		#endregion Methods
	}
}
