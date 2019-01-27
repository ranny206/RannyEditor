﻿using System;
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
    class SelectionTool : Tool
    {
        public override void MouseDown(Point point)
        {
            NotArtist.Figures.Add(new ZoomRectangle(point));
            foreach (var figure in NotArtist.Figures)
            {
                figure.UnSelected();
            }
            MainWindow.Instance.PropToolBarPanel.Children.Clear();
        }

        public override void MouseMove(Point point)
        {
            Figure sel = NotArtist.Figures[NotArtist.Figures.Count - 1];
            sel.AddCord(point);
            Point p1 = new Point(sel.Coordinates[1].X, sel.Coordinates[0].Y);
            Point p2 = sel.Coordinates[1];
            Point p1_h = new Point(sel.Coordinates[0].X, sel.Coordinates[1].Y);
            Point p2_h = sel.Coordinates[1];
            var p1Min = new Point(Math.Min(sel.Coordinates[1].X, sel.Coordinates[0].X), Math.Min(sel.Coordinates[1].Y, sel.Coordinates[0].Y));
            var p2Max = new Point(Math.Max(sel.Coordinates[1].X, sel.Coordinates[0].X), Math.Max(sel.Coordinates[1].Y, sel.Coordinates[0].Y));
            foreach (Figure figure in NotArtist.Figures.ToArray())
            {
                if (figure.Type == "Pencil")
                {
                    foreach (Point pnt in figure.Coordinates)
                    {
                        if (p1Min.X < pnt.X && pnt.X < p2Max.X && p1Min.Y < pnt.Y && pnt.Y < p2Max.Y)
                        {
                            figure.Selected();
                            break;
                        }
                        else
                        {
                            figure.UnSelected();
                        }
                    }
                }
                else if (figure.Type == "Line")
                {
                    Point p3 = figure.Coordinates[0];
                    Point p4 = figure.Coordinates[1];
                    var v1 = Point.Subtract(p1, p2);
                    var v2 = Point.Subtract(p1, p3);
                    var v3 = Point.Subtract(p1, p4);
                    var v4 = Point.Subtract(p3, p4);
                    var v5 = Point.Subtract(p3, p2);
                    var v6 = Point.Subtract(p3, p1);
                    var v1_h = Point.Subtract(p1_h, p2_h);
                    var v2_h = Point.Subtract(p1_h, p3);
                    var v3_h = Point.Subtract(p1_h, p4);
                    var v4_h = Point.Subtract(p3, p4);
                    var v5_h = Point.Subtract(p3, p2_h);
                    var v6_h = Point.Subtract(p3, p1_h);
                    var coord1 = v1.X * v2.Y - v1.Y * v2.X;
                    var coord2 = v1.X * v3.Y - v1.Y * v3.X;
                    var coord3 = v4.X * v5.Y - v4.Y * v5.X;
                    var coord4 = v4.X * v6.Y - v4.Y * v6.X;
                    var coord1_h = v1_h.X * v2_h.Y - v1_h.Y * v2_h.X;
                    var coord2_h = v1_h.X * v3_h.Y - v1_h.Y * v3_h.X;
                    var coord3_h = v4_h.X * v5_h.Y - v4_h.Y * v5_h.X;
                    var coord4_h = v4_h.X * v6_h.Y - v4_h.Y * v6_h.X;
                    if ((coord1 * coord2 < 0 & coord3 * coord4 < 0) ||
                        (coord1_h * coord2_h < 0 & coord3_h * coord4_h < 0) ||
                        ((p1Min.X < figure.Coordinates[0].X) &
                        (figure.Coordinates[0].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[0].Y) &
                        (figure.Coordinates[0].Y < p2Max.Y)) ||
                        ((p1Min.X < figure.Coordinates[1].X) &
                        (figure.Coordinates[1].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[1].Y) &
                        (figure.Coordinates[1].Y < p2Max.Y)))
                    {
                        figure.Selected();
                    }
                    else
                    {
                        figure.UnSelected();
                    }
                }
                else
                {
                    Point p3 = figure.Coordinates[0];
                    Point p4 = new Point(figure.Coordinates[1].X, figure.Coordinates[0].Y);
                    var v1 = Point.Subtract(p1, p2);
                    var v2 = Point.Subtract(p1, p3);
                    var v3 = Point.Subtract(p1, p4);
                    var v4 = Point.Subtract(p3, p4);
                    var v5 = Point.Subtract(p3, p2);
                    var v6 = Point.Subtract(p3, p1);
                    var coord1 = v1.X * v2.Y - v1.Y * v2.X;
                    var coord2 = v1.X * v3.Y - v1.Y * v3.X;
                    var coord3 = v4.X * v5.Y - v4.Y * v5.X;
                    var coord4 = v4.X * v6.Y - v4.Y * v6.X;
                    if ((coord1 * coord2 < 0 & coord3 * coord4 < 0) ||
                        ((p1Min.X < figure.Coordinates[0].X) &
                        (figure.Coordinates[0].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[0].Y) &
                        (figure.Coordinates[0].Y < p2Max.Y)) ||
                        ((p1Min.X < figure.Coordinates[1].X) &
                        (figure.Coordinates[1].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[1].Y) &
                        (figure.Coordinates[1].Y < p2Max.Y)))
                    {
                        figure.Selected();
                        continue;
                    }
                    else
                    {
                        figure.UnSelected();
                    }

                    p3 = new Point(figure.Coordinates[0].X, figure.Coordinates[1].Y);
                    p4 = figure.Coordinates[1];
                    v1 = Point.Subtract(p1, p2);
                    v2 = Point.Subtract(p1, p3);
                    v3 = Point.Subtract(p1, p4);
                    v4 = Point.Subtract(p3, p4);
                    v5 = Point.Subtract(p3, p2);
                    v6 = Point.Subtract(p3, p1);
                    coord1 = v1.X * v2.Y - v1.Y * v2.X;
                    coord2 = v1.X * v3.Y - v1.Y * v3.X;
                    coord3 = v4.X * v5.Y - v4.Y * v5.X;
                    coord4 = v4.X * v6.Y - v4.Y * v6.X;
                    if ((coord1 * coord2 < 0 & coord3 * coord4 < 0) ||
                        ((p1Min.X < figure.Coordinates[0].X) &
                        (figure.Coordinates[0].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[0].Y) &
                        (figure.Coordinates[0].Y < p2Max.Y)) ||
                        ((p1Min.X < figure.Coordinates[1].X) &
                        (figure.Coordinates[1].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[1].Y) &
                        (figure.Coordinates[1].Y < p2Max.Y)))
                    {
                        figure.Selected();
                        continue;
                    }
                    else
                    {
                        figure.UnSelected();
                    }

                    p3 = figure.Coordinates[0];
                    p4 = new Point(figure.Coordinates[0].X, figure.Coordinates[1].Y);
                    var v1_h = Point.Subtract(p1_h, p2_h);
                    var v2_h = Point.Subtract(p1_h, p3);
                    var v3_h = Point.Subtract(p1_h, p4);
                    var v4_h = Point.Subtract(p3, p4);
                    var v5_h = Point.Subtract(p3, p2_h);
                    var v6_h = Point.Subtract(p3, p1_h);
                    var coord1_h = v1_h.X * v2_h.Y - v1_h.Y * v2_h.X;
                    var coord2_h = v1_h.X * v3_h.Y - v1_h.Y * v3_h.X;
                    var coord3_h = v4_h.X * v5_h.Y - v4_h.Y * v5_h.X;
                    var coord4_h = v4_h.X * v6_h.Y - v4_h.Y * v6_h.X;
                    if ((coord1_h * coord2_h < 0 & coord3_h * coord4_h < 0) ||
                        ((p1Min.X < figure.Coordinates[0].X) &
                        (figure.Coordinates[0].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[0].Y) &
                        (figure.Coordinates[0].Y < p2Max.Y)) ||
                        ((p1Min.X < figure.Coordinates[1].X) &
                        (figure.Coordinates[1].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[1].Y) &
                        (figure.Coordinates[1].Y < p2Max.Y)))
                    {
                        figure.Selected();
                        continue;
                    }
                    else
                    {
                        figure.UnSelected();
                    }

                    p3 = new Point(figure.Coordinates[1].X, figure.Coordinates[0].Y);
                    p4 = figure.Coordinates[1];
                    v1_h = Point.Subtract(p1_h, p2_h);
                    v2_h = Point.Subtract(p1_h, p3);
                    v3_h = Point.Subtract(p1_h, p4);
                    v4_h = Point.Subtract(p3, p4);
                    v5_h = Point.Subtract(p3, p2_h);
                    v6_h = Point.Subtract(p3, p1_h);
                    coord1_h = v1_h.X * v2_h.Y - v1_h.Y * v2_h.X;
                    coord2_h = v1_h.X * v3_h.Y - v1_h.Y * v3_h.X;
                    coord3_h = v4_h.X * v5_h.Y - v4_h.Y * v5_h.X;
                    coord4_h = v4_h.X * v6_h.Y - v4_h.Y * v6_h.X;
                    if ((coord1_h * coord2_h < 0 & coord3_h * coord4_h < 0) ||
                        ((p1Min.X < figure.Coordinates[0].X) &
                        (figure.Coordinates[0].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[0].Y) &
                        (figure.Coordinates[0].Y < p2Max.Y)) ||
                        ((p1Min.X < figure.Coordinates[1].X) &
                        (figure.Coordinates[1].X < p2Max.X) &
                        (p1Min.Y < figure.Coordinates[1].Y) &
                        (figure.Coordinates[1].Y < p2Max.Y)))
                    {
                        figure.Selected();
                        continue;
                    }
                    else
                    {
                        figure.UnSelected();
                    }

                }
            }
        }

        public override void MouseUp(Point point)
        {
            NotArtist.Figures.Remove(NotArtist.Figures[NotArtist.Figures.Count - 1]);

            double i = 0;

            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.SelectRect != null)
                {
                    i = figure.PenThikness;
                    Buttons.PropertyButtonGeneration();
                    break;
                }
            }

            var CheckThikness = false;

            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    if (i == figure.PenThikness) { CheckThikness = true; }
                    else { CheckThikness = false; break; }
                }
            }
            if (CheckThikness)
            {
                Buttons.RowThicknessButton(i);
            }

        }
    }
}
