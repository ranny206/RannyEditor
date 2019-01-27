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
            for (int i = 0; i < Classes.NotArtist.Tools.Count; i++)
            {
                string st = "../icons/" + Classes.NotArtist.Tools[i].GetType().Name + ".bmp";
                ImageBrush img = new ImageBrush();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(st, UriKind.Relative);
                bi3.EndInit();
                img.ImageSource = bi3;
                Button btn = new Button();
                MyLittleToolBar.Children.Add(btn);
                btn.BorderBrush = Brushes.LightCoral;
                btn.Name = "btn" + i;
                btn.Height = 35;
                btn.Width = 35;
                btn.Background = img;
                btn.Content = "";
                btn.Tag = i;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.Click += new RoutedEventHandler(Btn_Click);
            }
            for (int i = 0; i < Classes.NotArtist.colors.Length; i++)
            {
                Button btncolor = new Button();
                if (i <= 10)
                {
                    MyLittlePanel1.Children.Add(btncolor);
                }
                else
                {
                    MyLittlePanel2.Children.Add(btncolor);
                }
                btncolor.BorderBrush = Brushes.Snow;
                btncolor.Name = "btncolor" + i;
                btncolor.Height = 30;
                btncolor.Width = 30;
                btncolor.Content = "";
                //btncolor.Background = ;
                btncolor.Tag = i;
                btncolor.HorizontalAlignment = HorizontalAlignment.Left;
                btncolor.Click += new RoutedEventHandler(Color_Click);
            }
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