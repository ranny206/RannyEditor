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
        private bool Pressed = false;

        public LineTool()
        {

        }

        public override void MouseDown(Point point)
        {
            Pressed = true;
            NotArtist.Figures.Add(new Line(point));
        }

        public override void MouseMove(Point point)
        {
            if (Pressed)
                NotArtist.Figures[NotArtist.Figures.Count - 1].ChangeCord(point);
        }
        public override void MouseUp(Point point)
        {
            Pressed = false;
        }
    }
}

