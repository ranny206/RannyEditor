using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pook.Classes.DifferentFigures
{
    class Line : Figure
    {
        public Line(Point aPoint) : base(aPoint)
        {
        }

        public override void Draw(DrawingContext drawingContext)
        {
            for (int i = 0; i < coordinates.Count - 1; i++)
                drawingContext.DrawLine(NotArtist.pen, coordinates[i], coordinates[i + 1]);
        }

        public void AddCord(Point point)
        {
            coordinates.Add(point);
        }

    }
}
