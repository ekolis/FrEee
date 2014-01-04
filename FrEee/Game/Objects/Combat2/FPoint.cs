using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//code taken from: 
//http://stackoverflow.com/questions/605124/fixed-point-math-in-c
//thanks to x4000 for making this code public. 

namespace FrEee.Game.Objects.Combat2
{
    public struct FInt
    {
        public long RawValue;
        public const int SHIFT_AMOUNT = 12; //12 is 4096

        public const long One = 1 << SHIFT_AMOUNT;
        public const int OneI = 1 << SHIFT_AMOUNT;
        public static FInt OneF = FInt.Create(1, true);

        #region Constructors
        public static FInt Create(long StartingRawValue, bool UseMultiple)
        {
            FInt fInt;
            fInt.RawValue = StartingRawValue;
            if (UseMultiple)
                fInt.RawValue = fInt.RawValue << SHIFT_AMOUNT;
            return fInt;
        }
        public static FInt Create(double DoubleValue)
        {
            FInt fInt;
            DoubleValue *= (double)One;
            fInt.RawValue = (int)Math.Round(DoubleValue);
            return fInt;
        }
        #endregion

        public int IntValue
        {
            get { return (int)(this.RawValue >> SHIFT_AMOUNT); }
        }

        public int ToInt()
        {
            return (int)(this.RawValue >> SHIFT_AMOUNT);
        }

        public double ToDouble()
        {
            return (double)this.RawValue / (double)One;
        }

        public FInt Inverse
        {
            get { return FInt.Create(-this.RawValue, false); }
        }

        #region FromParts
        /// <summary>
        /// Create a fixed-int number from parts.  For example, to create 1.5 pass in 1 and 500.
        /// </summary>
        /// <param name="PreDecimal">The number above the decimal.  For 1.5, this would be 1.</param>
        /// <param name="PostDecimal">The number below the decimal, to three digits.  
        /// For 1.5, this would be 500. For 1.005, this would be 5.</param>
        /// <returns>A fixed-int representation of the number parts</returns>
        public static FInt FromParts(int PreDecimal, int PostDecimal)
        {
            FInt f = FInt.Create(PreDecimal, true);
            if (PostDecimal != 0)
                f.RawValue += (FInt.Create(PostDecimal) / 1000).RawValue;

            return f;
        }
        #endregion

        #region *
        public static FInt operator *(FInt one, FInt other)
        {
            FInt fInt;
            fInt.RawValue = (one.RawValue * other.RawValue) >> SHIFT_AMOUNT;
            return fInt;
        }

        public static FInt operator *(FInt one, int multi)
        {
            return one * (FInt)multi;
        }

        public static FInt operator *(int multi, FInt one)
        {
            return one * (FInt)multi;
        }
        #endregion

        #region /
        public static FInt operator /(FInt one, FInt other)
        {
            FInt fInt;
            fInt.RawValue = (one.RawValue << SHIFT_AMOUNT) / (other.RawValue);
            return fInt;
        }

        public static FInt operator /(FInt one, int divisor)
        {
            return one / (FInt)divisor;
        }

        public static FInt operator /(int divisor, FInt one)
        {
            return (FInt)divisor / one;
        }
        #endregion

        #region %
        public static FInt operator %(FInt one, FInt other)
        {
            FInt fInt;
            fInt.RawValue = (one.RawValue) % (other.RawValue);
            return fInt;
        }

        public static FInt operator %(FInt one, int divisor)
        {
            return one % (FInt)divisor;
        }

        public static FInt operator %(int divisor, FInt one)
        {
            return (FInt)divisor % one;
        }
        #endregion

        #region +
        public static FInt operator +(FInt one, FInt other)
        {
            FInt fInt;
            fInt.RawValue = one.RawValue + other.RawValue;
            return fInt;
        }

        public static FInt operator +(FInt one, int other)
        {
            return one + (FInt)other;
        }

        public static FInt operator +(int other, FInt one)
        {
            return one + (FInt)other;
        }
        #endregion

        #region -
        public static FInt operator -(FInt one, FInt other)
        {
            FInt fInt;
            fInt.RawValue = one.RawValue - other.RawValue;
            return fInt;
        }

        public static FInt operator -(FInt one, int other)
        {
            return one - (FInt)other;
        }

        public static FInt operator -(int other, FInt one)
        {
            return (FInt)other - one;
        }
        #endregion

        #region ==
        public static bool operator ==(FInt one, FInt other)
        {
            return one.RawValue == other.RawValue;
        }

        public static bool operator ==(FInt one, int other)
        {
            return one == (FInt)other;
        }

        public static bool operator ==(int other, FInt one)
        {
            return (FInt)other == one;
        }
        #endregion

        #region !=
        public static bool operator !=(FInt one, FInt other)
        {
            return one.RawValue != other.RawValue;
        }

        public static bool operator !=(FInt one, int other)
        {
            return one != (FInt)other;
        }

        public static bool operator !=(int other, FInt one)
        {
            return (FInt)other != one;
        }
        #endregion

        #region >=
        public static bool operator >=(FInt one, FInt other)
        {
            return one.RawValue >= other.RawValue;
        }

        public static bool operator >=(FInt one, int other)
        {
            return one >= (FInt)other;
        }

        public static bool operator >=(int other, FInt one)
        {
            return (FInt)other >= one;
        }
        #endregion

        #region <=
        public static bool operator <=(FInt one, FInt other)
        {
            return one.RawValue <= other.RawValue;
        }

        public static bool operator <=(FInt one, int other)
        {
            return one <= (FInt)other;
        }

        public static bool operator <=(int other, FInt one)
        {
            return (FInt)other <= one;
        }
        #endregion

        #region >
        public static bool operator >(FInt one, FInt other)
        {
            return one.RawValue > other.RawValue;
        }

        public static bool operator >(FInt one, int other)
        {
            return one > (FInt)other;
        }

        public static bool operator >(int other, FInt one)
        {
            return (FInt)other > one;
        }
        #endregion

        #region <
        public static bool operator <(FInt one, FInt other)
        {
            return one.RawValue < other.RawValue;
        }

        public static bool operator <(FInt one, int other)
        {
            return one < (FInt)other;
        }

        public static bool operator <(int other, FInt one)
        {
            return (FInt)other < one;
        }
        #endregion

        public static explicit operator int(FInt src)
        {
            return (int)(src.RawValue >> SHIFT_AMOUNT);
        }

        public static explicit operator FInt(int src)
        {
            return FInt.Create(src, true);
        }

        public static explicit operator FInt(long src)
        {
            return FInt.Create(src, true);
        }

        public static explicit operator FInt(ulong src)
        {
            return FInt.Create((long)src, true);
        }

        public static FInt operator <<(FInt one, int Amount)
        {
            return FInt.Create(one.RawValue << Amount, false);
        }

        public static FInt operator >>(FInt one, int Amount)
        {
            return FInt.Create(one.RawValue >> Amount, false);
        }

        public override bool Equals(object obj)
        {
            if (obj is FInt)
                return ((FInt)obj).RawValue == this.RawValue;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return RawValue.GetHashCode();
        }

        public override string ToString()
        {
            return this.RawValue.ToString();
        }
    }

    public struct FPoint
    {
        public FInt X;
        public FInt Y;

        public static FPoint Create(FInt X, FInt Y)
        {
            FPoint fp;
            fp.X = X;
            fp.Y = Y;
            return fp;
        }

        //public static FPoint FromPoint(Point p)
        //{
        //    FPoint f;
        //    f.X = (FInt)p.X;
        //    f.Y = (FInt)p.Y;
        //    return f;
        //}

        //public static Point ToPoint(FPoint f)
        //{
        //    return new Point(f.X.IntValue, f.Y.IntValue);
        //}

        #region Vector Operations
        public static FPoint VectorAdd(FPoint F1, FPoint F2)
        {
            FPoint result;
            result.X = F1.X + F2.X;
            result.Y = F1.Y + F2.Y;
            return result;
        }

        public static FPoint VectorSubtract(FPoint F1, FPoint F2)
        {
            FPoint result;
            result.X = F1.X - F2.X;
            result.Y = F1.Y - F2.Y;
            return result;
        }

        public static FPoint VectorDivide(FPoint F1, int Divisor)
        {
            FPoint result;
            result.X = F1.X / Divisor;
            result.Y = F1.Y / Divisor;
            return result;
        }
        #endregion

        #region PI, DoublePI
        public static FInt PI = FInt.Create(12868, false); //PI x 2^12
        public static FInt TwoPIF = PI * 2; //radian equivalent of 260 degrees
        public static FInt PIOver180F = PI / (FInt)180; //PI / 180
        #endregion

        #region Sqrt
        public static FInt Sqrt(FInt f, int NumberOfIterations)
        {
            if (f.RawValue < 0) //NaN in Math.Sqrt
                throw new ArithmeticException("Input Error");
            if (f.RawValue == 0)
                return (FInt)0;
            FInt k = f + FInt.OneF >> 1;
            for (int i = 0; i < NumberOfIterations; i++)
                k = (k + (f / k)) >> 1;

            if (k.RawValue < 0)
                throw new ArithmeticException("Overflow");
            else
                return k;
        }

        public static FInt Sqrt(FInt f)
        {
            byte numberOfIterations = 8;
            if (f.RawValue > 0x64000)
                numberOfIterations = 12;
            if (f.RawValue > 0x3e8000)
                numberOfIterations = 16;
            return Sqrt(f, numberOfIterations);
        }
        #endregion

        #region Sin
        public static FInt Sin(FInt i)
        {
            FInt j = (FInt)0;
            for (; i < 0; i += FInt.Create(25736, false)) ;
            if (i > FInt.Create(25736, false))
                i %= FInt.Create(25736, false);
            FInt k = (i * FInt.Create(10, false)) / FInt.Create(714, false);
            if (i != 0 && i != FInt.Create(6434, false) && i != FInt.Create(12868, false) &&
                i != FInt.Create(19302, false) && i != FInt.Create(25736, false))
                j = (i * FInt.Create(100, false)) / FInt.Create(714, false) - k * FInt.Create(10, false);
            if (k <= FInt.Create(90, false))
                return sin_lookup(k, j);
            if (k <= FInt.Create(180, false))
                return sin_lookup(FInt.Create(180, false) - k, j);
            if (k <= FInt.Create(270, false))
                return sin_lookup(k - FInt.Create(180, false), j).Inverse;
            else
                return sin_lookup(FInt.Create(360, false) - k, j).Inverse;
        }

        private static FInt sin_lookup(FInt i, FInt j)
        {
            if (j > 0 && j < FInt.Create(10, false) && i < FInt.Create(90, false))
                return FInt.Create(SIN_TABLE[i.RawValue], false) +
                    ((FInt.Create(SIN_TABLE[i.RawValue + 1], false) - FInt.Create(SIN_TABLE[i.RawValue], false)) /
                    FInt.Create(10, false)) * j;
            else
                return FInt.Create(SIN_TABLE[i.RawValue], false);
        }

        private static int[] SIN_TABLE = {
        0, 71, 142, 214, 285, 357, 428, 499, 570, 641, 
        711, 781, 851, 921, 990, 1060, 1128, 1197, 1265, 1333, 
        1400, 1468, 1534, 1600, 1665, 1730, 1795, 1859, 1922, 1985, 
        2048, 2109, 2170, 2230, 2290, 2349, 2407, 2464, 2521, 2577, 
        2632, 2686, 2740, 2793, 2845, 2896, 2946, 2995, 3043, 3091, 
        3137, 3183, 3227, 3271, 3313, 3355, 3395, 3434, 3473, 3510, 
        3547, 3582, 3616, 3649, 3681, 3712, 3741, 3770, 3797, 3823, 
        3849, 3872, 3895, 3917, 3937, 3956, 3974, 3991, 4006, 4020, 
        4033, 4045, 4056, 4065, 4073, 4080, 4086, 4090, 4093, 4095, 
        4096
    };
        #endregion

        private static FInt mul(FInt F1, FInt F2)
        {
            return F1 * F2;
        }

        #region Cos, Tan, Asin
        public static FInt Cos(FInt i)
        {
            return Sin(i + FInt.Create(6435, false));
        }

        public static FInt Tan(FInt i)
        {
            return Sin(i) / Cos(i);
        }

        public static FInt Asin(FInt F)
        {
            bool isNegative = F < 0;
            F = Abs(F);

            if (F > FInt.OneF)
                throw new ArithmeticException("Bad Asin Input:" + F.ToDouble());

            FInt f1 = mul(mul(mul(mul(FInt.Create(145103 >> FInt.SHIFT_AMOUNT, false), F) -
                FInt.Create(599880 >> FInt.SHIFT_AMOUNT, false), F) +
                FInt.Create(1420468 >> FInt.SHIFT_AMOUNT, false), F) -
                FInt.Create(3592413 >> FInt.SHIFT_AMOUNT, false), F) +
                FInt.Create(26353447 >> FInt.SHIFT_AMOUNT, false);
            FInt f2 = PI / FInt.Create(2, true) - (Sqrt(FInt.OneF - F) * f1);

            return isNegative ? f2.Inverse : f2;
        }
        #endregion

        #region ATan, ATan2
        public static FInt Atan(FInt F)
        {
            return Asin(F / Sqrt(FInt.OneF + (F * F)));
        }

        public static FInt Atan2(FInt F1, FInt F2)
        {
            if (F2.RawValue == 0 && F1.RawValue == 0)
                return (FInt)0;

            FInt result = (FInt)0;
            if (F2 > 0)
                result = Atan(F1 / F2);
            else if (F2 < 0)
            {
                if (F1 >= 0)
                    result = (PI - Atan(Abs(F1 / F2)));
                else
                    result = (PI - Atan(Abs(F1 / F2))).Inverse;
            }
            else
                result = (F1 >= 0 ? PI : PI.Inverse) / FInt.Create(2, true);

            return result;
        }
        #endregion

        #region Abs
        public static FInt Abs(FInt F)
        {
            if (F < 0)
                return F.Inverse;
            else
                return F;
        }
        #endregion
    }
}
