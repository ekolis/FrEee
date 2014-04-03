using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FrEee.WinForms.MogreCombatRender.StrategiesDesigner
{
    /// <summary>
    /// Canvasdata an object which describes a control such as picturebox 
    /// width height etc as well as zoom and zero offset for 
    /// ease of passing information between other classes
    /// </summary>
    public class Canvasdata
    {
        public int zoom;
        public int width;
        public int height;
        public Point zero;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zoom"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="zero">describes the canvas location(topleft) from zeropoint(ie where an object would be if it's location is 0,0)
        /// think of a window with 0,0 at top left ontop of a map which has 0,0 at center</param>
        public Canvasdata(int zoom, int width, int height, Point zero)
        {
            this.zoom = zoom;
            this.width = width;
            this.height = height;
            this.zero = zero;
        }

        /// <summary>
        /// translates a point to where it should be drawn on the control
        /// </summary>
        /// <param name="pnt"></param>
        /// <param name="canvas">see Canvasdata obj</param>
        /// <returns></returns>
        public Point canvasLocation(Point pnt)
        {
            Point drawpoint = new Point();

            drawpoint.X = (zero.X - pnt.X) * zoom * -1;
            drawpoint.Y = (zero.Y - pnt.Y) * zoom;

            return drawpoint;
        }

        public Point reversecanvasLocation(Point pnt)
        {
            Point drawpoint = new Point();

            drawpoint.X = (zero.X + pnt.X) * zoom;
            drawpoint.Y = (zero.Y - pnt.Y) * zoom;

            return drawpoint;
        }

        /// <summary>
        /// zeroedpoint
        /// </summary>
        /// <param name="thispoint"></param>
        /// <param name="otherpoint"></param>
        /// <returns>point ralitive to thispoint</returns>
        public Point zeroedpoint(Point thispoint, Point otherpoint)
        {
            int zerox = otherpoint.X - thispoint.X;
            int zeroy = otherpoint.Y - thispoint.Y;

            return new Point(zerox, zeroy);
        }

        /// <summary>
        /// add two points
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns>sum of two points</returns>
        public Point add(Point pointA, Point pointB)
        {
            pointA.X += pointB.X;
            pointA.Y += pointB.Y;
            return pointA;
        }

        /// <summary>
        /// subtract pointB from pointA
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <returns></returns>
        public Point sub(Point pointA, Point pointB)
        {
            pointA.X -= pointB.X;
            pointA.Y -= pointB.Y;
            return pointA;
        }
    }
}
