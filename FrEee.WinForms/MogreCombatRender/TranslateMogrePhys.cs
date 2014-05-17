using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using NewtMath.f16;
using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender
{
    public static class TranslateMogrePhys
    {
        public static Mogre.Vector3 smVector_mVector3_xyz(PointXd cmVector)
        {
            return new Mogre.Vector3((float)cmVector.X, (float)cmVector.Y, (float)cmVector.Z);
        }
        public static Mogre.Vector3 smVector_mVector3_xzy(PointXd cmVector)
        {
            return new Mogre.Vector3((float)cmVector.X, (float)cmVector.Z, (float)cmVector.Y);
        }
        public static Mogre.Vector2 smVector_mVector2(PointXd cmVector)
        {
            return new Mogre.Vector2((float)cmVector.X, (float)cmVector.Y);
        }
        public static Mogre.Vector3 smVector2_mVector3(PointXd cmVector)
        {
            return new Mogre.Vector3((float)cmVector.X, (float)cmVector.Y, 0);
        }

        public static PointXd mVector3_cartPoint_xyz(Vector3 mVector3)
        {
            return new PointXd(mVector3.x, mVector3.y, mVector3.z);
        }
        public static PointXd mVector3_cartPoint_xzy(Vector3 mVector3)
        {
            return new PointXd(mVector3.x, mVector3.z, mVector3.y);
        }
    }

    public static class ImageConv
    {
        //public static Mogre.Image ImagetoImage(System.Drawing.Image image)
        //    {
        //     System.IO.Stream oStream = new System.IO.MemoryStream();

        //     image.Save(oStream, System.Drawing.Imaging.ImageFormat.Png); //why?
        //     /* Back to the start of the stream */
        //     oStream.Position = 0;

        //     /* read all the stream in a buffer */
        //     System.IO.BinaryReader oBinaryReader = new System.IO.BinaryReader(oStream);
        //     byte[] pBuffer = oBinaryReader.ReadBytes((int)oBinaryReader.BaseStream.Length);
        //     oStream.Close(); /*No more needed */

        //     Mogre.Image oMogreImage = new Mogre.Image();

        //     unsafe
        //     {
        //         System.Runtime.InteropServices.GCHandle handle = System.Runtime.InteropServices.GCHandle.Alloc(pBuffer, System.Runtime.InteropServices.GCHandleType.Pinned);
        //         byte* pUnsafeByte = (byte*)handle.AddrOfPinnedObject();
        //         void* pUnsafeBuffer = (void*)handle.AddrOfPinnedObject();

        //         MemoryDataStream oMemoryStream = new MemoryDataStream(pUnsafeBuffer, (uint)pBuffer.Length);
        //         DataStreamPtr oPtrDataStream = new DataStreamPtr(oMemoryStream);
        //         oMogreImage = oMogreImage.Load(oPtrDataStream, "png");
        //         handle.Free();
        //     }
        //     return oMogreImage;
        //}
    }
}
