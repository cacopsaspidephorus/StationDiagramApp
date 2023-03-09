using System;
using System.Windows;

namespace StationDiagramApp.Models
{
    public class Segment
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public int PathId { get; set; }

        public override string ToString()
        {
            return $"Segment {Start}, {End}";
        }
    }
}
