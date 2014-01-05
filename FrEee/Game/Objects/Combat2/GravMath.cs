using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
    public static class GravMath
    {

        //

        //public const Fix16 bigG_N_kg_ms = (Fix16)6.67428e-11;
        //public const Fix16 bigG_N_kg_ks = (Fix16)6.67428e-20;
        //public const Fix16 bigG_N_kg_mh = (Fix16)7.716049382716049382716049382716e-8;

        /// <summary>
        /// calculates the grav force between two bodies
        /// </summary>
        /// <param name="mass1">first mass in kg</param>
        /// <param name="mass2">second mass in kg</param>
        /// <param name="distance">distance from COM in m</param>
        /// <returns>
        /// Force in Newtons.
        /// </returns>
        /// <remarks>
        /// 
        /// </remarks>
        //public static Fix16 gravForce(Fix16 mass1, Fix16 mass2, Fix16 distance)
        //{

        //    Fix16 force;
        //    //force = bigG_N_kg_mh * (mass1 * mass2 / Math.Pow(distance, 2));
        //    //force = bigG_N_kg_ms * (mass1 * mass2 / Math.Pow(distance, 2));
        //    force = bigG_N_kg_ms * mass1 * mass2 / (distance * distance);
        //    return force;
        //}


        /// <summary>
        /// turns acceleration into a vector
        /// </summary>
        /// <param name="pos2">second position ralitive to the first. ie zeroedloc</param>
        /// <param name="accel">acceleration in m/s</param>
        /// <param name="distance">distance in m</param>
        /// <returns>
        ///  vectorMD
        /// </returns>
        /// <remarks>
        ///  distance so we don't have to calculate this again, since we already did it for gravforce
        /// </remarks>
        public static Point3d accelVector(Point3d pos2, Fix16 accel, Fix16 distance)
        {
            Fix16 multiplier = (distance * (Fix16)0.01) * accel; //wait wait what? why am I distance * 0.01?
            return pos2 * multiplier;
        }


        /// <summary>
        /// calculate the acceleration for a given mass and force.
        /// </summary>
        /// <param name="mass">mass as a Fix16 in kg</param>
        /// <param name="forcevec"> force as a CartVec in newtons</param>
        /// <returns>aceleration as a CartVec</returns>
        public static Point3d accelVector(Fix16 mass, Point3d forcevec)
        {
            //return new VectorMD(forcevec.X / mass, forcevec.Y / mass, forcevec.Z / mass);
            return forcevec / mass;
        }


        /// <summary>
        /// calculates the accelerat of two bodies
        /// </summary>
        /// <param name="mass"> mass in kg</param>
        /// <param name="force">force from gravity in Netwtons</param>
        /// <returns>
        /// acceleration as a Fix16 in m/s.
        /// </returns>
        /// <remarks>
        /// 
        /// </remarks>
        public static Fix16 accelFix16(Fix16 mass, Fix16 force)
        {
            return force / mass;
        }

        /// <summary>
        /// returns the angular acceleration for a given mass, and force at radius from pivot.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="radius"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        public static Fix16 angular_acceleration(Fix16 mass, Fix16 force, Fix16 radius)
        {

            return torque(force, radius) / moment_of_Intertia(mass, radius);
        }

        /// <summary>
        /// returns the tourque as a Fix16. todo: return as a vector.
        /// </summary>
        /// <param name="force"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Fix16 torque(Fix16 force, Fix16 radius)
        {
            return force * radius;
        }

        /// <summary>
        /// returns the moment of intertia for a given mass and radius.
        /// </summary>
        /// <param name="mass"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Fix16 moment_of_Intertia(Fix16 mass, Fix16 radius)
        {
            return mass * (radius * radius);
        }


        //Ship A vs Ship B closing rate:  V=speedVector P=distanceVector Closing rate: (Va-Vb)dot((Pa-Pb)/|Pa-Pb|)

        /// <summary>
        /// the rate at which two points are aproaching each other as a scaler
        /// </summary>
        /// <param name="p1">position 1</param>
        /// <param name="v1">velocity 1</param>
        /// <param name="p2">position 2</param>
        /// <param name="v2">velocity 2</param>
        /// <returns></returns>
        public static Fix16 closingrate(Point3d p1, Point3d v1, Point3d p2, Point3d v2)
        {
            Point3d v = v2 - v1;
            Point3d p = p1 - p2;
            Fix16 closingrate = Trig.dotProduct(v, (p / Trig.hypotinuse(p)));
            return closingrate;
        }
    }
}
