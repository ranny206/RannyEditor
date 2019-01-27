using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Pook.Classes;
using Pook.Classes.Tools;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Pook.Classes
{
    static class NotArtist
    {
        public static List<Figure> Figures = new List<Figure>();

        public static FigureHost FigureHost = new FigureHost();

        public static Tool ToolNow = new PencilTool();

        public static List<List<Figure>> CanvasState = new List<List<Figure>>();

        public static int StateNumber = 0;

        public static Tool HandTool = new HandTool1();

        public static Action<Point> ScrollTo;


        public static void AddCondition()
        {
            List<Figure> figuresNow = new List<Figure>();
            foreach (Figure figure in Figures)
            {
                figuresNow.Add(figure.Clone());
            }
            CanvasState.Add(figuresNow);
            StateNumber++;
            if (StateNumber != CanvasState.Count)
            {
                CanvasState.RemoveRange(StateNumber - 1, CanvasState.Count - StateNumber);
            }
            Figures.Clear();
            foreach (Figure figure in figuresNow)
            {
                Figures.Add(figure.Clone());
            }
            foreach (Figure figure in CanvasState[StateNumber - 1])
            {
                figure.Select = false;
                figure.SelectRect = null;
            }
            if (CanvasState.Count > 1)
            {
                foreach (Figure figure in CanvasState[StateNumber - 2])
                {
                    figure.Select = false;
                    figure.SelectRect = null;
                }
            }
        }

        public static void gotoPastState()
        {
            if (StateNumber != 1)
            {
                StateNumber--;
                Figures.Clear();
                foreach (Figure figure in CanvasState[StateNumber - 1])
                {
                    Figures.Add(figure.Clone());
                }
            }
        }

        public static void gotoNextState()
        {
            if (StateNumber != CanvasState.Count)
            {
                StateNumber++;
                Figures.Clear();
                foreach (Figure figure in CanvasState[StateNumber - 1])
                {
                    Figures.Add(figure.Clone());
                }
            }
        }

        public static Brush BrushNow = null;
        public static string BrushStringNow = "null";
        public static Brush tempBrush = null;
        public static Brush ColorNow = Brushes.Black;
        public static string ColorStringNow = "Black";
        public static string tempStringBrush = "";
        public static double ThicnessNow = 4;
        public static DashStyle DashNow = DashStyles.Solid;
        public static string DashStringhNow = "―――――";
        public static double RoundXNow = 0;
        public static double RoundYNow = 0;


        public static double ScaleRate = 1;
        public static double DistanceToPointX;
        public static double DistanceToPointY;
        public static double HandScrollX;
        public static double HandScrollY;
        public static bool FirstPress = true;
        public static bool SecondPress = false;
        public static double CanvasWidth;
        public static double CanvasHeigth;

        [field: NonSerialized]
        public static readonly Dictionary<string, Tool> Transform = new Dictionary<string, Tool>()
        {
            { "Line", new LineTool() },
            { "Rectangle", new RectangleTool() },
            { "Ellipse", new EllipseTool() },
            { "RoundRectangle", new RoundRectangleTool() },
            { "Pencil", new PencilTool() },
            { "Hand", new HandTool() },
            { "ZoomRectangle", new ZoomTool() },
            { "Selection", new SelectionTool() },

        };

        [field: NonSerialized]
        public static readonly DashStyle[] TransformDash = new DashStyle[]
        {
            DashStyles.Solid,
            DashStyles.Dash,
            DashStyles.DashDot,
            DashStyles.DashDotDot,
            DashStyles.Dot,
        };

        [field: NonSerialized]
        public static readonly Dictionary<string, DashStyle> TransformDashProp = new Dictionary<string, DashStyle>()
        {
            { "―――――", DashStyles.Solid },
            { "— — — — — —", DashStyles.Dash },
            { "— ∙ — ∙ — ∙ — ∙ —", DashStyles.DashDot },
            { "— ∙ ∙ — ∙ ∙ — ∙ ∙ — ", DashStyles.DashDotDot },
            { "∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙", DashStyles.Dot },

        };

        [field: NonSerialized]
        public static readonly Dictionary<string, Brush> TransformColor = new Dictionary<string, Brush>()
        {
            { "Black", Brushes.Black },
            { "Red", Brushes.Red },
            { "Gray", Brushes.Gray },
            { "Orange", Brushes.Orange },
            { "Yellow", Brushes.Yellow },
            { "Blue", Brushes.Blue },
            { "Purple", Brushes.Purple },
            { "Coral", Brushes.Coral },
            { "White", Brushes.White },
            { "null", null }
        };
    }
}
