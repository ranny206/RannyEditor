using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Pook.Classes;
using Pook.Classes.Tools;

namespace Pook.Classes
{
    static class NotArtist
    {
        public static List<Figure> Figures = new List<Figure>();

        public static FigureHost FigureHost = new FigureHost();

        public static System.Windows.Media.Pen pen = new System.Windows.Media.Pen(Brushes.LightCoral, 3);


        public static readonly List<Tool> Tools = new List<Tool>
        {
           new PencilTool(),
        };

        public static Tool SelectedTool = Tools[0];
        public static readonly Brush[] colors =
            {
                Brushes.Black, Brushes.Gray, Brushes.Brown, Brushes.Red, Brushes.OrangeRed, Brushes.Yellow, Brushes.Green,
                Brushes.CornflowerBlue, Brushes.Blue, Brushes.DarkViolet, Brushes.White, Brushes.WhiteSmoke, Brushes.Brown,
                Brushes.Pink, Brushes.Orange, Brushes.SandyBrown, Brushes.LightGreen, Brushes.SkyBlue, Brushes.LightSteelBlue,
                Brushes.Violet
            };
        public static Brush SelectedColor = colors[0];
    }
}
