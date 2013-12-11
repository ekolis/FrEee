using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;

namespace FrEee.Game.Objects.Combat2
{
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

        public Compass(Point3d point1, Point3d point2)
        {
            Radians = Trig.angleto(point1, point2);
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
        public Point3d Point(double distance)
        {
            return Trig.sides_ab(distance, Radians);
        }

		public void normalize()
		{
			heading_degrees = ((heading_degrees % 360) + 360) % 360; // thanks SJ ;)
		}

		public static double operator +(Compass angle, double addend)
		{
			angle.Degrees += addend;
            angle.normalize();
			return angle.Degrees;
		}
		public static double operator -(Compass angle, double subend)
		{
			angle.Degrees -= subend;
            angle.normalize();
			return angle.Degrees;
		}
	}

	public class Trig
	{
		/// <summary>
		/// returns a 2d vector for a given angle and scaler
		/// </summary>
		/// <param name="hypotinuse - note! in 3d space this will be too long and the result will be incorrect"></param>
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


			return new Point3d(side_b, side_a, 0);
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
		/// <returns>an intermediate point between p1 and p2 at distance from p1 </returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <returns>distance between 0,0 and p1.x , p1.y</returns>
        public static double hypotinuse(Point3d p1)
        {
            return pythagC(p1.X, p1.Y);
        }
	}

	public static class GravMath
	{

		//

		public const double bigG_N_kg_ms = 6.67428e-11;
		public const double bigG_N_kg_ks = 6.67428e-20;
		public const double bigG_N_kg_mh = 7.716049382716049382716049382716e-8;

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
		public static double gravForce(double mass1, double mass2, double distance)
		{

			double force;
			//force = bigG_N_kg_mh * (mass1 * mass2 / Math.Pow(distance, 2));
			//force = bigG_N_kg_ms * (mass1 * mass2 / Math.Pow(distance, 2));
			force = bigG_N_kg_ms * mass1 * mass2 / Math.Pow(distance, 2);
			return force;
		}


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
		public static Point3d accelVector(Point3d pos2, double accel, double distance)
		{
			double multiplier = (distance * 0.01) * accel; //wait wait what? why am I distance * 0.01?
			return pos2 * multiplier;
		}


		/// <summary>
		/// calculate the acceleration for a given mass and force.
		/// </summary>
		/// <param name="mass">mass as a double in kg</param>
		/// <param name="forcevec"> force as a CartVec in newtons</param>
		/// <returns>aceleration as a CartVec</returns>
		public static Point3d accelVector(double mass, Point3d forcevec)
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
		/// acceleration as a double in m/s.
		/// </returns>
		/// <remarks>
		/// 
		/// </remarks>
		public static double acceldouble(double mass, double force)
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
		public static double angular_acceleration(double mass, double force, double radius)
		{

			return torque(force, radius) / moment_of_Intertia(mass, radius);
		}

		/// <summary>
		/// returns the tourque as a double. todo: return as a vector.
		/// </summary>
		/// <param name="force"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public static double torque(double force, double radius)
		{
			return force * radius;
		}

		/// <summary>
		/// returns the moment of intertia for a given mass and radius.
		/// </summary>
		/// <param name="mass"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public static double moment_of_Intertia(double mass, double radius)
		{
			return mass * System.Math.Pow(radius, 2);
		}
	}

}
