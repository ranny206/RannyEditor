using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pook.Classes.DifferentFigures
{
    class Ellipse : Figure
    {
        public Ellipse(Point aPoint) : base(aPoint)
        {
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var space = Vector.Divide(Point.Subtract(coordinates[0], coordinates[1]), 2.0);
            var center = Point.Add(coordinates[1], space);

            drawingContext.DrawEllipse(NotArtist.SelectedColor, NotArtist.pen, center, space.X, space.Y);
        }
    }
}
