using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Pook.Classes;
using Pook.Classes.DifferentFigures;

namespace Pook.Classes.Tools
{
    class ZoomTool : Tool
    {
        public override void MouseDown(Point point)
        {
            NotArtist.Figures.Add(new ZoomRectangle(point));
        }

        public override void MouseMove(Point point)
        {
            NotArtist.Figures[NotArtist.Figures.Count - 1].AddCord(point);
        }

        public override void MouseUp(Point point)
        {
            Figure figure = NotArtist.Figures[NotArtist.Figures.Count - 1];
            if (Point.Subtract(figure.Coordinates[0], figure.Coordinates[1]).Length > 50)
            {
                var scaleX = NotArtist.CanvasWidth / Math.Abs(figure.Coordinates[1].X - figure.Coordinates[0].X);
                var scaleY = NotArtist.CanvasHeigth / Math.Abs(figure.Coordinates[1].Y - figure.Coordinates[0].Y);
                NotArtist.ScaleRate = Math.Max(scaleX, scaleY);

                if (figure.Coordinates[1].X > figure.Coordinates[0].X)
                {
                    NotArtist.DistanceToPointX = figure.Coordinates[0].X;
                }
                else
                {
                    NotArtist.DistanceToPointX = figure.Coordinates[1].X;
                }

                if (figure.Coordinates[1].Y > figure.Coordinates[0].Y)
                {
                    NotArtist.DistanceToPointY = figure.Coordinates[0].Y;
                }
                else
                {
                    NotArtist.DistanceToPointY = figure.Coordinates[1].Y;
                }
                NotArtist.ScrollTo(new Point(NotArtist.DistanceToPointX, NotArtist.DistanceToPointY));
            }
            else
            {
                NotArtist.ScaleRate = 1;
                NotArtist.ScaleRate = 1;
                NotArtist.DistanceToPointX = 0;
                NotArtist.DistanceToPointY = 0;
            }
            NotArtist.Figures.Remove(figure);
        }
    }
}
