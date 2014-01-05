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

        public Compass(double angle, bool radians = true)
        {
            if (radians)
            {
                Radians = (Fix16)angle;
            }
            else
            {
                Degrees = (Fix16)angle;
            }
        }

        public Compass(Point3d point1, Point3d point2)
        {
            Radians = (Trig.angleto(point1, point2));
            Fix16 num = new Fix16();
            num = (Fix16)(180.0d);
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
                //double hd = heading_degrees;
                //double hd = 180.0;
                //double pi = Math.PI;
                //double pi180 = 0.01745329251; //pi / 180;
                //double result = (float)(hd * pi180);
                //double result = 3.14159265359;
                //double result = (heading_degrees * Trig.PI180);
                
                
                Fix16 result = heading_degrees * Fix16.Pi / Fix16.FromRaw(180);
                return result;
                //return heading_degrees * Math.PI / 180; 
            }
            set
            {
                this.heading_degrees = (value * Fix16.FromRaw(180) / Fix16.Pi);
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

    }
}
