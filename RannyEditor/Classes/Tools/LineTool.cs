using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Pook.Classes.DifferentFigures;

namespace Pook.Classes
{
    class LineTool : Tool
    {
        public override void MouseDown(Point point)
        {
            NotArtist.Figures.Add(new Line(point));
        }

        public override void MouseMove(Point point)
        {
            NotArtist.Figures[NotArtist.Figures.Count - 1].AddCord(point);
        }
    }
}

