using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Pook.Classes
{
    class HandTool : Tool
    {

        public override void MouseDown(Point point)
        {
            NotArtist.Figures.Add(new HandLine(point));
            NotArtist.HandScrollX = point.X;
            NotArtist.HandScrollY = point.Y;
        }

        public override void MouseMove(Point point)
        {
            NotArtist.HandScrollX += NotArtist.Figures[NotArtist.Figures.Count - 1].Coordinates[0].X - NotArtist.Figures[NotArtist.Figures.Count - 1].Coordinates[1].X;
            NotArtist.HandScrollY += NotArtist.Figures[NotArtist.Figures.Count - 1].Coordinates[0].Y - NotArtist.Figures[NotArtist.Figures.Count - 1].Coordinates[1].Y;
            NotArtist.Figures[NotArtist.Figures.Count - 1].AddCord(point);
        }

        public override void MouseUp(Point point)
        {
            NotArtist.Figures.Remove(NotArtist.Figures[NotArtist.Figures.Count - 1]);
        }
    }
}
