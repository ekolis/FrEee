using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Combat2
{
    public class combatWaypoint
    {
        public combatWaypoint()
        { }
        public combatWaypoint(Point3d cmbt_loc)
        {
            this.cmbt_loc = cmbt_loc;
        }
        public combatWaypoint(CombatObj comObj)
        {
            this.comObj = comObj;
            this.cmbt_loc = comObj.cmbt_loc;
            this.cmbt_vel = comObj.cmbt_vel;
        }

        /// <summary>
        /// location within the sector
        /// </summary>
        public Point3d cmbt_loc { get; set; }

        /// <summary>
        /// combat velocity
        /// </summary>
        public Point3d cmbt_vel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CombatObj comObj { get; set; }
 
    }

    public class CombatObj
    {
        private ICombatObject comObj;

        public CombatObj(ICombatObject comObj)
        {
            this.comObj = comObj;
            Vehicles.SpaceVehicle ship = (Vehicles.SpaceVehicle)comObj;
            this.Accel = ship.Speed;
            this.Strafe = ship.Speed / 4;
            this.Rotate = ship.Speed / 12;
        }

        /// <summary>
        /// location within the sector
        /// </summary>
        public Point3d cmbt_loc { get; set; }

        /// <summary>
        /// facing towards this point
        /// </summary>
        public Point3d cmbt_face { get; set; }

        /// <summary>
        /// combat velocity
        /// </summary>
        public Point3d cmbt_vel { get; set; }

        public ICombatObject icomobj
        {

            get { return this.comObj; }
            set { this.comObj = value;}
        }

        public combatWaypoint movetarget { get; set; }
        public Point3d lastvectortotarget { get; set; }

        public List<CombatObj> weaponTarget { get; set; }

        public int Accel { get; set; }
        public int Strafe { get; set; }
        public int Rotate { get; set; }
       
    }

    public class EmpireinCombat
    {      
        public List<CombatObj> ownships;
        public List<CombatObj> friendly;
        public List<CombatObj> nutral; //not actualy used.
        public List<CombatObj> hostile;
        public EmpireinCombat()
        { }
    }

    public class Point3d
    {
        private List<double> point_list;


        public Point3d(double x, double y, double z)
        {
            this.point_list = new List<double>() { x, y, z };
        }
        public Point3d(double x, double y)
        {
            this.point_list = new List<double>() { x, y };
        }
        public Point3d(Point3d point)
        {
            this.point_list = point.Point_List;
        }
        public Point3d(List<double> dimensions)
        {
            this.point_list = dimensions;
        }

        public override string ToString()
        {
            string outstring = "";
            foreach (double dim in this.point_list)
            {
                outstring += dim.ToString() + " ";
            }
            return outstring;
        }
        public List<double> Point_List
        {
            get { return point_list; }
            set { point_list = value; }
        }
        public double X
        {
            get { return this.point_list[0]; }
            set { this.point_list[0] = value; }
        }
        public double Y
        {
            get { return this.point_list[1]; }
            set { this.point_list[1] = value; }
        }
        public double Z
        {
            get { return this.point_list[2]; }
            set { this.point_list[2] = value; }
        }

        public void ZEROIZE()
        {
            for (int i = 0; i < this.point_list.Count; i++)
            {
                this.point_list[i] = 0;
            }
        }
        public void addition(Point3d addend)
        {
            for (int i = 0; i < this.point_list.Count; i++)
            {
                point_list[i] += addend.Point_List[i];
            }
        }
        public void subtraction(Point3d subend)
        {
            for (int i = 0; i < this.point_list.Count; i++)
            {
                point_list[i] -= subend.Point_List[i];
            }
        }
        public void multiplication(Point3d multiplicand)
        {
            for (int i = 0; i < this.point_list.Count; i++)
            {
                point_list[i] *= multiplicand.Point_List[i];
            }
        }
        public void division(Point3d divisor)
        {
            for (int i = 0; i < this.point_list.Count; i++)
            {
                point_list[i] /= divisor.Point_List[i];
            }
        }

        public static Point3d operator +(Point3d v1, Point3d v2)
        {
            List<double> sum = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                sum.Add(v1.Point_List[i] + v2.Point_List[i]);
                //if (i < v2.Dimension_List.Count)
                //{
                //sum.Add(v1.Dimension_List[i] + v2.Dimension_List[i]);
                //}
            }
            return new Point3d(sum);
        }
        public static Point3d operator +(Point3d v1, double addend)
        {
            List<double> sum = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                sum.Add(v1.Point_List[i] + addend);
            }
            return new Point3d(sum);
        }

        public static Point3d operator -(Point3d v1, Point3d v2)
        {
            List<double> difference = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                difference.Add(v1.Point_List[i] - v2.Point_List[i]);
                //if (i < v2.dimension_list.Count)
                //{
                //    difference.Add(v1.Dimension_List[i] - v2.Dimension_List[i]);
                //}
            }
            return new Point3d(difference);
        }
        public static Point3d operator -(Point3d v1, double subend)
        {
            List<double> difference = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                difference.Add(v1.Point_List[i] - subend);
            }
            return new Point3d(difference);
        }

        public static Point3d operator *(Point3d v1, Point3d v2)
        {
            List<double> product = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                product.Add(v1.Point_List[i] * v2.Point_List[i]);
            }
            return new Point3d(product);
        }
        public static Point3d operator *(Point3d v1, double multiplicand)
        {
            List<double> product = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                product.Add(v1.Point_List[i] * multiplicand);
            }
            return new Point3d(product);
        }

        public static Point3d operator /(Point3d v1, Point3d v2)
        {
            List<double> quotient = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                quotient.Add(v1.Point_List[i] / v2.Point_List[i]);
            }
            return new Point3d(quotient);
        }
        public static Point3d operator /(Point3d v1, double divisor)
        {
            List<double> quotient = new List<double>();
            for (int i = 0; i < v1.Point_List.Count; i++)
            {
                quotient.Add(v1.Point_List[i] / divisor);
            }
            return new Point3d(quotient);
        }
    }

    public class Compass
    {
        double heading_degrees;
        public Compass(double angle, bool radians = true)
        {
            if (radians)
            {
                Radians = angle;
            }
            else
            {
                Degrees = angle;
            }
        }

        public double Degrees
        {
            get { return this.heading_degrees; }
            set
            {
                this.heading_degrees = value;
                normalize();
            }
        }
        public double Radians
        {
            get { return this.heading_degrees * Math.PI / 180; }
            set
            {
                this.heading_degrees = value * 180 / Math.PI;
                normalize();
            }
        }

        public void normalize()
        {
            if (heading_degrees <= 360 || heading_degrees >= 0)
            {
                return;
            }
            if (heading_degrees > 360)
            {
                heading_degrees -= 360;
            }
            if (heading_degrees < 0)
            {
                heading_degrees += 360;
            }
            normalize();
        }

        public static double operator +(Compass angle, double addend)
        {
            angle.Degrees += addend;
            return angle.Degrees;
        }
        public static double operator -(Compass angle, double subend)
        {
            angle.Degrees -= subend;
            return angle.Degrees;
        }
    }

    public class Trig
    {
        /// <summary>
        /// returns a 2d vector for a given angle and scaler
        /// </summary>
        /// <param name="hypotinuse"></param>
        /// <param name="angle_A"></param>
        /// <param name="radians">if angle_A is given in deg, make this false</param>
        /// <returns>a 2d point</returns>
        public static Point3d sides_ab(double hypotinuse, double angle_A, bool radians = true)
        {
            if (!radians)
            { angle_A = angle_A * Math.PI / 180; }
            double side_a = 0;
            double side_b = 0;

            side_b = Math.Sin(angle_A) * hypotinuse;
            side_a = Math.Cos(angle_A) * hypotinuse;


            return new Point3d(side_b, side_a);
        }

        /// <summary>
        /// law of cosines
        /// </summary>
        /// <param name="side_b"></param>
        /// <param name="side_c"></param>
        /// <param name="angle_A">in degrees</param>
        /// <returns>sidea</returns>
        public static double lawofcos_sidea(double side_b, double side_c, double angle_A)
        {
            return Math.Sqrt(Math.Pow(side_b, 2) + Math.Pow(side_c, 2) - 2 * side_b * side_c * Math.Cos(angle_A));
        }

        /// <summary>
        /// pythagoras c = sqrt(a^2 + b^2)
        /// </summary>
        /// <param name="side_a"></param>
        /// <param name="side_b"></param>
        /// <returns>side c</returns>
        public static double pythagC(double side_a, double side_b)
        {
            return Math.Sqrt(Math.Pow(side_a, 2) + Math.Pow(side_b, 2));
        }

        /// <summary>
        /// pythagoras a = sqrt(c^2 - b^2) 
        /// </summary>
        /// <param name="side_b"></param>
        /// <param name="side_c"></param>
        /// <returns>side a</returns>
        public static double pythagA(double side_b, double side_c)
        {
            return Math.Sqrt(Math.Pow(side_c, 2) - Math.Pow(side_b, 2));
        }

        /// <summary>
        /// Math.Atan2(point.X, point.Y);
        /// </summary>
        /// <param name="point"></param>
        /// <returns>angleA</returns>
        public static double angleA(Point3d point)
        {
            return Math.Atan2(point.X, point.Y);
        }

        /// <summary>
        /// sides_ab(distance, angleA(point2 -= point1))); 
        /// I'm sure this is misnamed.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="distance"></param>
        /// <returns> </returns>
        public static Point3d intermediatePoint(Point3d point1, Point3d point2, double distance)
        {
            return new Point3d(sides_ab(distance, angleA(point2 -= point1)));
        }

        /// <summary>
        /// calculates the angle of p2 in relation to p1
        /// currently only handles 2d ie: X and Y 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>relitive angle of p2</returns>
        public static double angleto(Point3d p1, Point3d p2)
        {
            p2 -= p1;
            return angleA(p2);
        }

        /// <summary>
        /// distance between p1 and p2
        /// currently only 2d ie X and Y
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns>distance of p2 ralitive to p1</returns>
        public static double distance(Point3d p1, Point3d p2)
        {
            p2 -= p1;
            return pythagC(p2.X, p2.Y);
        }

    }
}
