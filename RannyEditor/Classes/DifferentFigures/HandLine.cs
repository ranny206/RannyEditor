﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pook.Classes
{
    class HandLine : Figure
    {
        public HandLine(Point point)
        {
            Coordinates = new List<Point> { point, point };
            Color = NotArtist.ColorNow;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(null, Coordinates[0], Coordinates[1]);
        }

        public override void AddCord(Point point)
        {
            Coordinates[1] = point;
        }
    }
}
