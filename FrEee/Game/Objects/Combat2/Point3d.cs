using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrEee.Game.Interfaces;
using FrEee.Utility.Extensions;
using FixMath.NET;

namespace FrEee.Game.Objects.Combat2
{
	public class Point3d
	{
		private List<Fix16> point_list;

        public Point3d(Fix16 x, Fix16 y, Fix16 z)
		{
			this.point_list = new List<Fix16>() { x, y, z };
		}

		public Point3d(Point3d point)
			:this(point.Point_List)
		{
		}
		public Point3d(IEnumerable<Fix16> dimensions)
		{
			// don't want to reuse list references across multiple points, do we?
			this.point_list = new List<Fix16>(dimensions);
		}
        public Point3d() : this((Fix16)0, (Fix16)0, (Fix16)0) { }

		public override string ToString()
		{
			string outstring = "";
			foreach (Fix16 dim in this.point_list)
			{
				outstring += dim.ToString() + " ";
			}
			return outstring;
		}
		public List<Fix16> Point_List
		{
			get { return point_list; }
			set { point_list = value; }
		}
		public Fix16 X
		{
			get { return this.point_list[0]; }
			set { this.point_list[0] = value; }
		}
		public Fix16 Y
		{
			get { return this.point_list[1]; }
			set { this.point_list[1] = value; }
		}
		public Fix16 Z
		{
			get { return this.point_list[2]; }
			set { this.point_list[2] = value; }
		}

		public void ZEROIZE()
		{
			for (int i = 0; i < this.point_list.Count; i++)
			{
                this.point_list[i] = (Fix16)0;
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
			List<Fix16> sum = new List<Fix16>();
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
		public static Point3d operator +(Point3d v1, Fix16 addend)
		{
			List<Fix16> sum = new List<Fix16>();
			for (int i = 0; i < v1.Point_List.Count; i++)
			{
				sum.Add(v1.Point_List[i] + addend);
			}
			return new Point3d(sum);
		}

		public static Point3d operator -(Point3d v1, Point3d v2)
		{
			List<Fix16> difference = new List<Fix16>();
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
		public static Point3d operator -(Point3d v1, Fix16 subend)
		{
			List<Fix16> difference = new List<Fix16>();
			for (int i = 0; i < v1.Point_List.Count; i++)
			{
				difference.Add(v1.Point_List[i] - subend);
			}
			return new Point3d(difference);
		}

		public static Point3d operator *(Point3d v1, Point3d v2)
		{
			List<Fix16> product = new List<Fix16>();
			for (int i = 0; i < v1.Point_List.Count; i++)
			{
				product.Add(v1.Point_List[i] * v2.Point_List[i]);
			}
			return new Point3d(product);
		}
		public static Point3d operator *(Point3d v1, Fix16 multiplicand)
		{
			List<Fix16> product = new List<Fix16>();
			for (int i = 0; i < v1.Point_List.Count; i++)
			{
				product.Add(v1.Point_List[i] * multiplicand);
			}
			return new Point3d(product);
		}

		public static Point3d operator /(Point3d v1, Point3d v2)
		{
			List<Fix16> quotient = new List<Fix16>();
			for (int i = 0; i < v1.Point_List.Count; i++)
			{
				quotient.Add(v1.Point_List[i] / v2.Point_List[i]);
			}
			return new Point3d(quotient);
		}
		public static Point3d operator /(Point3d v1, Fix16 divisor)
		{
			List<Fix16> quotient = new List<Fix16>();
			for (int i = 0; i < v1.Point_List.Count; i++)
			{
				quotient.Add(v1.Point_List[i] / divisor);
			}
			return new Point3d(quotient);
		}
		public static bool operator ==(Point3d p1, Point3d p2)
		{
			if (p1.IsNull() && p2.IsNull())
				return true;
			if (p1.IsNull() || p2.IsNull())
				return false;
			return p1.Point_List.SequenceEqual(p2.Point_List);
		}
		public static bool operator !=(Point3d p1, Point3d p2)
		{
			return !(p1 == p2);
		}
		public override bool Equals(object obj)
		{
			if (obj is Point3d)
				return ((Point3d)obj) == this;
			return false;
		}
		public override int GetHashCode()
		{
			var h = 0;
			foreach (var n in Point_List)
				h ^= n.GetHashCode();
			return h;
		}

		/// <summary>
		/// The length of the vector (distance from origin).
		/// </summary>
		public Fix16 Length
		{
			get
			{
                return ((Fix16)Fix64.Sqrt((Fix64)X * (Fix64)X + (Fix64)Y * (Fix64)Y + (Fix64)Z * (Fix64)Z));
			}
		}
	}
}
