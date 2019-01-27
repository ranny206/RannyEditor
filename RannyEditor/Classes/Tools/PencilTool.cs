using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Pook.Classes;
using Pook.Classes.DifferentFigures;

namespace Pook.Classes.Tools
{
    public class PencilTool : Tool
    {
        public override void MouseDown(Point point)
        {
            NotArtist.Figures.Add(new Pencil(point));
        }

        public override void MouseMove(Point point)
        {
            NotArtist.Figures[NotArtist.Figures.Count - 1].AddCord(point);
        }
    }
}
