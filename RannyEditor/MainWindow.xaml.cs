using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<Figure> figures;

        public MainWindow()
        {
            InitializeComponent();

            Canvas.Children.Add(Classes.NotArtist.FigureHost);
        }

        private void Invalidate()
        {
            Classes.NotArtist.FigureHost.Children.Clear();
            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();
            foreach (var figure in Classes.NotArtist.Figures)
            {
                figure.Draw(drawingContext);
            }
            drawingContext.Close();
            Classes.NotArtist.FigureHost.Children.Add(drawingVisual);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Classes.NotArtist.SelectedTool.MouseMove(e.GetPosition(Canvas));
                Invalidate();
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Classes.NotArtist.SelectedTool.MouseDown(e.GetPosition(Canvas));
            Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Classes.NotArtist.SelectedTool.MouseUp(e.GetPosition(Canvas));
            Invalidate();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Classes.NotArtist.SelectedTool = Classes.NotArtist.Tools[Convert.ToInt32((sender as Button).Tag)];
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Classes.NotArtist.Figures.Clear();
            Classes.NotArtist.FigureHost.Children.Clear();
        }
        private void Color_Click(object sender, RoutedEventArgs e)
        {
            //Classes.NotArtist.SelectedColor = Classes.NotArtist.сolors[];
        }
    }
}