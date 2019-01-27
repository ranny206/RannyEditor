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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Pook.Classes;
using System.Drawing;
using Brushes = System.Windows.Media.Brushes;

namespace Pook
{
    public partial class MainWindow: Window
    {
        public static MainWindow Instance { get; private set; }

        bool ClikOnCanvas = false;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            Canvas.Children.Add(Classes.NotArtist.FigureHost);
            Buttons.Generation();
            NotArtist.AddCondition();
            NotArtist.ScrollTo = ScrollTo;
        }

        private void ScrollTo(System.Windows.Point p)
        {
            ScrollViewerCanvas.ScrollToVerticalOffset(p.Y);
            ScrollViewerCanvas.ScrollToHorizontalOffset(p.X);
        }

        private void Invalidate()
        {
            Classes.NotArtist.FigureHost.Children.Clear();
            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();
            foreach (var figure in NotArtist.Figures)
            {
                figure.Draw(drawingContext);
                if (figure.SelectRect != null)
                {
                    figure.SelectRect.Draw(drawingContext);
                }
            }

            drawingContext.Close();
            Classes.NotArtist.FigureHost.Children.Add(drawingVisual);
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                NotArtist.ToolNow.MouseDown(e.GetPosition(Canvas));
                Mouse.Capture(Canvas, CaptureMode.SubTree);
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                NotArtist.tempBrush = NotArtist.BrushNow;
                NotArtist.BrushNow = NotArtist.ColorNow;
                NotArtist.ColorNow = NotArtist.tempBrush;
                NotArtist.tempStringBrush = NotArtist.BrushStringNow;
                NotArtist.BrushStringNow = NotArtist.ColorStringNow;
                NotArtist.ColorStringNow = NotArtist.tempStringBrush;
                NotArtist.ToolNow.MouseDown(e.GetPosition(Canvas));
                NotArtist.tempBrush = NotArtist.BrushNow;
                NotArtist.BrushNow = NotArtist.ColorNow;
                NotArtist.ColorNow = NotArtist.tempBrush;
                NotArtist.tempStringBrush = NotArtist.BrushStringNow;
                NotArtist.BrushStringNow = NotArtist.ColorStringNow;
                NotArtist.ColorStringNow = NotArtist.tempStringBrush;
            }
            ClikOnCanvas = true;
            Invalidate();
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (ClikOnCanvas)
            {
                NotArtist.ToolNow.MouseMove(e.GetPosition(Canvas));
                if (NotArtist.ToolNow == NotArtist.Transform["Hand"])
                {
                    ScrollViewerCanvas.ScrollToVerticalOffset(NotArtist.HandScrollY);
                    ScrollViewerCanvas.ScrollToHorizontalOffset(NotArtist.HandScrollX);
                }
                Invalidate();
            }
        }
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (ClikOnCanvas)
            {
                NotArtist.ToolNow.MouseUp(e.GetPosition(Canvas));
                Mouse.Capture(Canvas, CaptureMode.None);

                if (NotArtist.ToolNow != NotArtist.Transform["Selection"] & NotArtist.ToolNow != NotArtist.Transform["ZoomRectangle"] & NotArtist.ToolNow != NotArtist.Transform["Hand"])
                {
                    NotArtist.AddCondition();
                    gotoPastState.IsEnabled = true;
                    gotoNextState.IsEnabled = false;
                }
                if (NotArtist.ToolNow == NotArtist.Transform["ZoomRectangle"])
                {
                    Canvas.LayoutTransform = new ScaleTransform(NotArtist.ScaleRate, NotArtist.ScaleRate);
                    ScrollViewerCanvas.ScrollToVerticalOffset(NotArtist.DistanceToPointY * NotArtist.ScaleRate);
                    ScrollViewerCanvas.ScrollToHorizontalOffset(NotArtist.DistanceToPointX * NotArtist.ScaleRate);
                }
                if (NotArtist.ToolNow == NotArtist.HandTool)
                {
                    NotArtist.ToolNow = NotArtist.Transform["Selection"];
                }
                ClikOnCanvas = false;
                Invalidate();
            }
        }

        private void Canvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        public void ButtonChangeTool(object sender, RoutedEventArgs e)
        {
            NotArtist.ToolNow = NotArtist.Transform[(sender as System.Windows.Controls.Button).Tag.ToString()];
            if ((sender as System.Windows.Controls.Button).Tag.ToString() == "RoundRectangle")
            {
                textBoxRoundRectX.IsEnabled = true;
                textBoxRoundRectY.IsEnabled = true;
            }
            else
            {
                textBoxRoundRectX.IsEnabled = false;
                textBoxRoundRectY.IsEnabled = false;
            }
            foreach (Figure figure in NotArtist.Figures)
            {
                figure.UnSelected();
            }
            Invalidate();
            PropToolBarPanel.Children.Clear();
        }

        public void ButtonChangeColor(object sender, RoutedEventArgs e)
        {
            if (NotArtist.FirstPress)
            {
                NotArtist.ColorNow = NotArtist.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()];
                NotArtist.ColorStringNow = (sender as System.Windows.Controls.Button).Tag.ToString();
                if ((sender as System.Windows.Controls.Button).Background == null) { button_firstColor.Background = Brushes.Gray; }
                else { button_firstColor.Background = (sender as System.Windows.Controls.Button).Background; }

            }
            else
            {
                NotArtist.BrushNow = NotArtist.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()];
                NotArtist.BrushStringNow = (sender as System.Windows.Controls.Button).Tag.ToString();
                if ((sender as System.Windows.Controls.Button).Background == null) { button_secondColor.Background = Brushes.Gray; }
                else { button_secondColor.Background = (sender as System.Windows.Controls.Button).Background; }
            }
        }

        private void ThiknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NotArtist.ThicnessNow = ThiknessSlider.Value;
        }

        private void FirstColor(object sender, RoutedEventArgs e)
        {
            NotArtist.FirstPress = true;
            NotArtist.SecondPress = false;
            button_firstColor.BorderThickness = new Thickness(5);
            button_secondColor.BorderThickness = new Thickness(0);
        }

        private void SecondColor(object sender, RoutedEventArgs e)
        {
            NotArtist.FirstPress = false;
            NotArtist.SecondPress = true;
            button_secondColor.BorderThickness = new Thickness(5);
            button_firstColor.BorderThickness = new Thickness(0);
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            NotArtist.CanvasHeigth = Canvas.Height;
            NotArtist.CanvasWidth = Canvas.Width;
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            NotArtist.CanvasHeigth = Canvas.Height;
            NotArtist.CanvasWidth = Canvas.Width;
        }

        public void CleanCanvas(object sender, RoutedEventArgs e)
        {
            NotArtist.FigureHost.Children.Clear();
            NotArtist.Figures.Clear();
            NotArtist.StateNumber = 0;
            NotArtist.CanvasState.Clear();
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = false;
            gotoNextState.IsEnabled = false;
        }

        public void MinusZoomCanvas(object sender, RoutedEventArgs e)
        {
            Canvas.LayoutTransform = new ScaleTransform(1, 1);
            ScrollViewerCanvas.ScrollToVerticalOffset(0);
            ScrollViewerCanvas.ScrollToHorizontalOffset(0);
        }

        private void ChangeSelectionDash(object sender, SelectionChangedEventArgs e)
        {
            NotArtist.DashNow = NotArtist.TransformDash[comboBoxDash.SelectedIndex];
            if (comboBoxDash.SelectedIndex == 0)
            {
                NotArtist.DashStringhNow = "―――――";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "1")
            {
                NotArtist.DashStringhNow = "— — — — — —";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "2")
            {
                NotArtist.DashStringhNow = "— ∙ — ∙ — ∙ — ∙ —";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "3")
            {
                NotArtist.DashStringhNow = "— ∙ ∙ — ∙ ∙ — ∙ ∙ — ";
            }
            if (comboBoxDash.SelectedIndex.ToString() == "4")
            {
                NotArtist.DashStringhNow = "∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙";
            }
        }

        private void textBoxRoundRectX_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotArtist.RoundXNow = Convert.ToDouble(textBoxRoundRectX.Text);
        }

        private void textBoxRoundRectY_TextChanged(object sender, TextChangedEventArgs e)
        {
            NotArtist.RoundYNow = Convert.ToDouble(textBoxRoundRectY.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить как";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Files(*.bin)|*.bin";
                sfd.ShowDialog();
                if (sfd.FileName != "")
                {
                    FileStream file = (FileStream)sfd.OpenFile();
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(file, NotArtist.Figures);
                    file.Close();
                };

        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotArtist.Figures.Count != 0)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Сохранить текущее изображение?", "", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog(); //	Запрашивает у пользователя местоположение для сохранения файла.
                    sfd.Title = "Сохранить как";
                    sfd.OverwritePrompt = true;
                    sfd.CheckPathExists = true;
                    sfd.Filter = "Files(*.bin)|*.bin";
                    sfd.ShowDialog();
                    if (sfd.FileName != "")
                    {
                        FileStream file = (FileStream)sfd.OpenFile();
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(file, NotArtist.Figures);
                        file.Close();
                    }
                }
            }
            NotArtist.Figures.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Files(*.bin)|*.bin";
            ofd.Title = "Открыть";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                Stream file = (FileStream)ofd.OpenFile();
                BinaryFormatter deserializer = new BinaryFormatter();
                NotArtist.Figures = (List<Figure>)deserializer.Deserialize(file);
                file.Close();
                Invalidate();
            }
            NotArtist.CanvasState.Clear();
            NotArtist.StateNumber = 0;
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = false;
            gotoNextState.IsEnabled = false;
        }

      

        public void changeRoundX(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangeRoundX(e.NewValue);
                }
            }
            Invalidate();
        }

        public void changeRoundY(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangeRoundY(e.NewValue);
                }
            }
            Invalidate();
        }

        public void ChangeStrokeColor(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(NotArtist.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()], (sender as System.Windows.Controls.Button).Tag.ToString());
                }
            }
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = true;
            gotoNextState.IsEnabled = false;
            Invalidate();
        }

        public void ChangeBrushColor(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(NotArtist.TransformColor[(sender as System.Windows.Controls.Button).Tag.ToString()], (sender as System.Windows.Controls.Button).Tag.ToString(), new bool());
                }
            }
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = true;
            gotoNextState.IsEnabled = false;
            Invalidate();
        }

        public void ChangeDash(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(NotArtist.TransformDashProp[(sender as System.Windows.Controls.Button).Content.ToString()], (sender as System.Windows.Controls.Button).Content.ToString());
                }
            }
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = true;
            gotoNextState.IsEnabled = false;
            Invalidate();
        }

        public void ClearSelectedFigure(object sender, RoutedEventArgs e)
        {
            foreach (Figure figure in NotArtist.Figures.ToArray())
            {
                if (figure.Select == true)
                {
                    NotArtist.Figures.Remove(figure);
                }
            }
            PropToolBarPanel.Children.Clear();
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = true;
            gotoNextState.IsEnabled = false;
            Invalidate();
        }

        public void RowThicnessChange(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (Figure figure in NotArtist.Figures)
            {
                if (figure.Select == true)
                {
                    figure.ChangePen(e.NewValue);
                }
            }
            Invalidate();
        }

        public void HandForSelectedFigure(object sender, RoutedEventArgs e)
        {
            NotArtist.ToolNow = NotArtist.HandTool;
        }

        public void SldMouseUp(object sender, MouseButtonEventArgs e)
        {
            NotArtist.AddCondition();
            gotoPastState.IsEnabled = true;
            gotoNextState.IsEnabled = false;
        }

    }
}
