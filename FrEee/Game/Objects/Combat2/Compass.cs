using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FixMath.NET;
namespace FrEee.Game.Objects.Combat2
{
    public class Compass
    {
        Fix16 heading_degrees;
        public Compass()
        {
            // leave it initialized to zero
        }
        public Compass(Fix16 angle, bool radians = true)
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
            Fix16 rads = Trig.angleto(point1, point2);
            Radians = rads;
            //this.heading_degrees = rads * (Fix16.Pi / (Fix16)(180));
            //normalize();

        }

        public Fix16 Degrees
        {
            get { return this.heading_degrees; }
            set
            {
                this.heading_degrees = value;
                normalize();
            }
        }
        public Fix16 Radians
        {
            get
            {
                Fix16 result = heading_degrees * (Fix16.Pi / (Fix16)180);
                return result; 
            }
            set
            {
				this.heading_degrees = value * ((Fix16)180 / Fix16.Pi);
                normalize();
            }
        }
        public Point3d Point(Fix16 distance)
        {
            return Trig.sides_ab(distance, Radians);
        }

        public void normalize()
        {
            heading_degrees = (NormalizeDegrees(heading_degrees));
        }

        public static Fix16 NormalizeDegrees(Fix16 degrees)
        {
            return ((degrees % (Fix16)360) + (Fix16)360) % (Fix16)360; // thanks SJ ;)
        }

        public static Fix16 operator +(Compass angle, Fix16 addend)
        {
            angle.Degrees += addend;
            angle.normalize();
            return angle.Degrees;
        }
        public static Fix16 operator -(Compass angle, Fix16 subend)
        {
            angle.Degrees -= subend;
            angle.normalize();
            return angle.Degrees;
        }

        public override string ToString()
        {
            return Degrees.ToString() + " degrees";
        }

        //public static bool operator ==(Compass c1, Compass c2)
        //{
        //    if (c1.IsNull() && c2.IsNull())
        //        return true;
        //    if (c1.IsNull() || c2.IsNull())
        //        return false;
        //    return c1.Degrees == c2.Degrees;
        //}
        //public static bool operator !=(Compass c1, Compass c2)
        //{
        //    return !(c1 == c2);
        //}
        public override bool Equals(object obj)
        {
            if (obj is Compass)
                return ((Compass)obj) == this;
            return false;
        }
        public override int GetHashCode()
        {
            return Degrees.GetHashCode();
        }

		public static Compass operator +(Compass c1, Compass c2)
		{
			return new Compass(c1.Radians + c2.Radians);
		}

		public static Compass operator -(Compass c1, Compass c2)
		{
			return new Compass(c1.Radians - c2.Radians);
		}
    }
}
