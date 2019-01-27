using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Pook.Classes.DifferentFigures
{
    class RoundRectangle : Figure
    {
        public RoundRectangle(Point aPoint) : base(aPoint)
        {
        }

        public override void Draw(DrawingContext drawingContext)
        {
            var square = Point.Subtract(coordinates[0], coordinates[1]);

            drawingContext.DrawRoundedRectangle(NotArtist.SelectedColor, NotArtist.pen, new Rect(coordinates[1], square), 10.0, 10.0);
        }
    }
}
