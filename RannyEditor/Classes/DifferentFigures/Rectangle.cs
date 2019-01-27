using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Pook.Classes.DifferentFigures
{
    class Rectangle : Figure
    {
        public Rectangle(Point aPoint) : base(aPoint)
        {
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var diagonal = Point.Subtract(coordinates[0], coordinates[1]);

            drawingContext.DrawRectangle(NotArtist.SelectedColor, NotArtist.pen, new Rect(coordinates[1], diagonal));
        }
    }
}
