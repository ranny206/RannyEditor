using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Pook
{
    public class Figure
    {
        protected List<Point> coordinates;

        public Figure(Point aPoint)
        {
            coordinates = new List<Point> { aPoint, aPoint };
        }

        public virtual void Draw(DrawingContext graphics)
        {

        }

        public virtual void ChangeCord(Point point)
        {
            coordinates[coordinates.Count - 1] = point;
        }
    }
}