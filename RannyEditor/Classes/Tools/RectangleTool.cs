using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Pook.Classes;
using Pook.Classes.DifferentFigures;

namespace Pook.Classes
{
    class RectangleTool : Tool
    {
        private bool Pressed = false;

        public RectangleTool()
        {

        }

        public override void MouseDown(Point point)
        {
            Pressed = true;
            NotArtist.Figures.Add(new Rectangle(point));
           
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
