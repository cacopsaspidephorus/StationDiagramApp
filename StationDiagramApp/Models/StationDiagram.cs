using System.Collections.Generic;

namespace StationDiagramApp.Models
{
    public class StationDiagram
    {
        public List<Park> Parks { get; set; }
        public List<Path> Paths { get; set; }
        public List<Segment> Segments { get; set; }
    }
}
