using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

using FrEee.Game.Objects.Combat2;

namespace FrEee.WinForms.MogreCombatRender
{
    public static class TranslateMogrePhys
    {
        public static Mogre.Vector3 smVector_mVector3_xyz(Point3d cmVector)
        {
            return new Mogre.Vector3((float)cmVector.X, (float)cmVector.Y, (float)cmVector.Z);
        }
        public static Mogre.Vector3 smVector_mVector3_xzy(Point3d cmVector)
        {
            return new Mogre.Vector3((float)cmVector.X, (float)cmVector.Z, (float)cmVector.Y);
        }
        public static Mogre.Vector2 smVector_mVector2(Point3d cmVector)
        {
            return new Mogre.Vector2((float)cmVector.X, (float)cmVector.Y);
        }
        public static Mogre.Vector3 smVector2_mVector3(Point3d cmVector)
        {
            return new Mogre.Vector3((float)cmVector.X, (float)cmVector.Y, 0);
        }

        public static Point3d mVector3_cartPoint_xyz(Vector3 mVector3)
        {
            return new Point3d(mVector3.x, mVector3.y, mVector3.z);
        }
        public static Point3d mVector3_cartPoint_xzy(Vector3 mVector3)
        {
            return new Point3d(mVector3.x, mVector3.z, mVector3.y);
        }
    }
}
