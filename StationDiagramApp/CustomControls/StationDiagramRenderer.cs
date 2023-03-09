using StationDiagramApp.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Point = System.Windows.Point;

namespace StationDiagramApp.CustomControls
{
    public class StationDiagramRenderer : Control
    {
        public static readonly DependencyProperty StationDiagramProperty =
            DependencyProperty.Register("StationDiagram", typeof(StationDiagram), typeof(StationDiagramRenderer));

        public static readonly DependencyProperty ShortestPathProperty =
            DependencyProperty.Register("ShortestPath", typeof(IEnumerable<Segment>), typeof(StationDiagramRenderer), new PropertyMetadata(OnPropertyChangedCallBack));

        public static readonly DependencyProperty SelectedParkProperty =
            DependencyProperty.Register("SelectedPark", typeof(Park), typeof(StationDiagramRenderer), new PropertyMetadata(OnPropertyChangedCallBack));

        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Color), typeof(StationDiagramRenderer), new PropertyMetadata(OnPropertyChangedCallBack));

        public StationDiagram StationDiagram
        {
            get { return (StationDiagram)GetValue(StationDiagramProperty); }
            set { SetValue(StationDiagramProperty, value); }
        }

        public IEnumerable<Segment> ShortestPath
        {
            get { return (IEnumerable<Segment>)GetValue(ShortestPathProperty); }
            set { SetValue(ShortestPathProperty, value); }
        }

        public Park SelectedPark
        {
            get { return (Park)GetValue(SelectedParkProperty); }
            set { SetValue(SelectedParkProperty, value); }
        }

        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public StationDiagramRenderer()
        {
            FillColor = Colors.Green;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (StationDiagram == null)
                return;

            DrawSegments(drawingContext);
            FillPark(drawingContext);
            MarkShortestPath(drawingContext);
        }

        private void DrawSegments(DrawingContext drawingContext)
        {
            if (StationDiagram.Segments == null)
                return;

            foreach (Segment segment in StationDiagram.Segments)
            {
                drawingContext.DrawLine(new Pen(Brushes.DarkGray, 3), segment.Start, segment.End);
                drawingContext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 3), segment.Start, 0.5, 0.5);
                drawingContext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 3), segment.End, 0.5, 0.5);
            }
        }

        private void MarkShortestPath(DrawingContext drawingContext)
        {
            if (ShortestPath == null)
                return;

            foreach (Segment segment in ShortestPath)
            {
                drawingContext.DrawLine(new Pen(Brushes.Red, 4), segment.Start, segment.End);
                drawingContext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 3), segment.Start, 0.5, 0.5);
                drawingContext.DrawEllipse(Brushes.Black, new Pen(Brushes.Black, 3), segment.End, 0.5, 0.5);
            }
        }

        private void FillPark(DrawingContext drawingContext)
        {
            if (SelectedPark == null)
                return;

            IEnumerable<Point> pointsByPark = GetPointsByPark(SelectedPark.Id);

            if (pointsByPark.Count() == 0)
                return;

            EndPoints endPoints = GetEndPoints(pointsByPark);

            DrawGeometryForPark(drawingContext, endPoints);
        }

        private static void OnPropertyChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is StationDiagramRenderer renderer)
                renderer.InvalidateVisual();
        }

        private static EndPoints GetEndPoints(IEnumerable<Point> points)
        {
            Point mostLeftPoint = points.First();
            Point mostRightPoint = points.First();
            Point mostTopPoint = points.First();
            Point mostBottomPoint = points.First();

            foreach (var point in points)
            {
                if (point.X < mostLeftPoint.X)
                    mostLeftPoint = point;

                if (point.X > mostRightPoint.X)
                    mostRightPoint = point;

                if (point.Y < mostTopPoint.Y)
                    mostTopPoint = point;

                if (point.Y > mostBottomPoint.Y)
                    mostBottomPoint = point;
            }

            return new EndPoints
            {
                MostBottomPoint= mostBottomPoint,
                MostLeftPoint= mostLeftPoint,
                MostRightPoint= mostRightPoint,
                MostTopPoint= mostTopPoint
            };
        }

        private IEnumerable<Point> GetPointsByPark(int parkId)
        {
            var segmentsByPark = new List<Segment>();
            var points = new List<Point>();

            foreach (Path path in StationDiagram.Paths.Where(x => x.ParkId == parkId))
            {
                var segmentsByPath = StationDiagram.Segments.Where(x => x.PathId == path.Id);
                segmentsByPark.AddRange(segmentsByPath);
            }

            points.AddRange(segmentsByPark.Select(x => x.Start).ToList());
            points.AddRange(segmentsByPark.Select(x => x.End).ToList());

            return points;
        }

        private void DrawGeometryForPark(DrawingContext drawingContext, EndPoints endPoints)
        {
            var geometry = new StreamGeometry();

            using StreamGeometryContext geometryContext = geometry.Open();

            geometryContext.BeginFigure(endPoints.MostLeftPoint, true, true);
            geometryContext.LineTo(endPoints.MostBottomPoint, true, false);
            geometryContext.LineTo(endPoints.MostRightPoint, true, false);
            geometryContext.LineTo(endPoints.MostTopPoint, true, false);

            geometryContext.Close();

            drawingContext.DrawGeometry(new SolidColorBrush(FillColor) { Opacity = 0.5 }, new Pen(Brushes.Red, 0), geometry);

            var text = new FormattedText(SelectedPark.Name, CultureInfo.InvariantCulture, 
                FlowDirection.LeftToRight, new Typeface("Verdana"), 24, Brushes.Black,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            drawingContext.DrawText(text, new Point(endPoints.MostRightPoint.X / 2, endPoints.MostBottomPoint.Y / 2));
        }
    }

    internal enum EndPointType
    {
        Left,
        Right,
        Top,
        Bottom
    }

    internal struct EndPoints
    {
        public Point MostLeftPoint { get; set; }
        public Point MostRightPoint { get; set; }
        public Point MostTopPoint { get; set; }
        public Point MostBottomPoint { get; set; }
    }
}