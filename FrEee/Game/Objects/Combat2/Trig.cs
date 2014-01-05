using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
    public class Trig
    {


        /// <summary>
        /// returns a 2d vector for a given angle and scaler
        /// </summary>
        /// <param name="hypotinuse - note! in 3d space this will be too long and the result will be incorrect"></param>
        /// <param name="angle_A"></param>
        /// <param name="radians">if angle_A is given in deg, make this false</param>
        /// <returns>a 2d point</returns>
        public static Point3d sides_ab(Fix16 hypotinuse, Fix16 angle_A, bool radians = true)
        {
            if (!radians)
            { angle_A = (angle_A * (Fix16.Pi / (Fix16)180) ); }
            Fix16 side_a;
            Fix16 side_b;

      
            side_b = (Fix16.Sin(angle_A) * hypotinuse);
            side_a = (Fix16.Cos(angle_A) * hypotinuse);


            return new Point3d(side_b, side_a, (Fix16)0);
        }



        /// <summary>
        /// law of cosines
        /// </summary>
        /// <param name="side_b"></param>
        /// <param name="side_c"></param>
        /// <param name="angle_A">in degrees</param>
        /// <returns>sidea</returns>
        public static Fix16 lawofcos_sidea(Fix16 side_b, Fix16 side_c, Fix16 angle_A)
        {
            return Fix16.Sqrt((side_b * side_b) + (side_c * side_c) - (Fix16)2 * side_b * side_c * Fix16.Cos(angle_A));
        }

        /// <summary>
        /// pythagoras c = sqrt(a^2 + b^2)
        /// </summary>
        /// <param name="side_a"></param>
        /// <param name="side_b"></param>
        /// <returns>side c</returns>
        public static Fix16 pythagC(Fix16 side_a, Fix16 side_b)
        {
            return Fix16.Sqrt((side_a * side_a) + (side_b * side_b));
        }

        /// <summary>
        /// pythagoras a = sqrt(c^2 - b^2) 
        /// </summary>
        /// <param name="side_b"></param>
        /// <param name="side_c"></param>
        /// <returns>side a</returns>
        public static Fix16 pythagA(Fix16 side_b, Fix16 side_c)
        {
            return Fix16.Sqrt((side_c * side_c) - (side_b * side_b));
        }

        /// <summary>
        /// Fix16.Atan2(point.X, point.Y);
        /// we swap X and Y, and negate Y, so that we can use "naval coordinates" instead of "geometric coordinates"
        /// i.e. zero degrees is north, not east, and positive angles are clockwise, not counterclockwise
        /// </summary>
        /// <param name="point"></param>
        /// <returns>angleA</returns>
        public static Fix16 angleA(Point3d point)
        {
            Fix16 angle = Fix16.Atan2(point.X, point.Y);
            return angle;
        }

        /// <summary>
        /// sides_ab(distance, angleA(point2 - point1))); 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="distance"></param>
        /// <returns>an intermediate point between p1 and p2 at distance from p1 </returns>
        public static Point3d intermediatePoint(Point3d point1, Point3d point2, Fix16 distance)
        {
            return new Point3d(sides_ab(distance, angleA(point2 - point1)));
        }

        /// <summary>
        /// calculates the angle of p2 in relation to p1
        /// currently only handles 2d ie: X and Y 
        /// </summary>
        /// <param name="p1">location from</param>
        /// <param name="p2">location to</param>
        /// <returns>relitive angle of p2</returns>
        public static Fix16 angleto(Point3d p1, Point3d p2)
        {
            Fix16 angle = angleA(p2 - p1);
            return angle;
        }

        /// <summary>
        /// distance between p1 and p2
        /// currently only 2d ie X and Y
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>distance of p2 ralitive to p1</returns>
        public static Fix16 distance(Point3d p1, Point3d p2)
        {
            var d = p2 - p1;
            return pythagC(d.X, d.Y);
        }

        //public static Point3d vectorDistance(Point3d p1, Point3d p2)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <returns>distance between 0,0 and p1.x , p1.y</returns>
        public static Fix16 hypotinuse(Point3d p1)
        {
            return pythagC(p1.X, p1.Y);
        }

        /// <summary>
        /// dot product between two points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Fix16 dotProduct(Point3d p1, Point3d p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y + p1.Z * p2.Z;
        }

        /// <summary>
        /// cross product of two points.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Point3d crossProduct(Point3d p1, Point3d p2)
        {
            Fix16 cx = p1.Y * p2.Z - p1.Z * p2.Y;
            Fix16 cy = p1.Z * p2.X - p1.X * p2.Z;
            Fix16 cz = p1.X * p2.Y - p1.Y * p2.X;

            return new Point3d(cx, cy, cz);
        }
    }


}
