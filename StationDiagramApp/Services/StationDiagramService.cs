using StationDiagramApp.Models;
using System.Collections.Generic;
using System.Windows;

namespace StationDiagramApp.Services
{
    public class StationDiagramService : IStationDiagramService
    {
        private StationDiagram _testStationDiagram;

        public StationDiagramService()
        {
            Init();
        }

        public StationDiagram GetTestStationDiagram()
        {
            return _testStationDiagram;
        }

        private void Init()
        {
            _testStationDiagram = new StationDiagram();

            _testStationDiagram.Parks = new List<Park>
            {
                new Park
                {
                    Id= 1,
                    Name = "Park 1"
                },
                new Park
                {
                    Id= 2,
                    Name = "Park 2"
                }
            };

            _testStationDiagram.Paths = new List<Path>
            {
                new Path
                {
                    Id= 1,
                    ParkId = 1,
                },
                new Path
                {
                    Id= 2,
                    ParkId = 2,
                }
            };

            _testStationDiagram.Segments = new List<Segment>
            {
                new Segment
                {
                    Start = new Point(0, 150),
                    End = new Point(90, 150)
                },
                new Segment
                {
                    Start = new Point(90, 150),
                    End = new Point(700, 150),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(700, 150),
                    End = new Point(800, 150),
                },
                new Segment
                {
                    Start = new Point(50, 130),
                    End = new Point(130, 130),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(90, 150),
                    End = new Point(130, 130),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(130, 130),
                    End = new Point(150, 130),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(150, 130),
                    End = new Point(500, 130),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(150, 130),
                    End = new Point(190, 110),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(190, 110),
                    End = new Point(270, 110),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(270, 110),
                    End = new Point(660, 110),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(270, 110),
                    End = new Point(310, 90),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(310, 90),
                    End = new Point(260, 90),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(260, 90),
                    End = new Point(110, 90),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(310, 90),
                    End = new Point(640, 90),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(310, 90),
                    End = new Point(330, 80),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(260, 90),
                    End = new Point(300, 70),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(300, 70),
                    End = new Point(180, 70),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(300, 70),
                    End = new Point(390, 70),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(390, 70),
                    End = new Point(550, 70),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(390, 70),
                    End = new Point(420, 50),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(420, 50),
                    End = new Point(520, 50),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(520, 50),
                    End = new Point(550, 70),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(550, 70),
                    End = new Point(580, 70),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(640, 90),
                    End = new Point(660, 110),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(660, 110),
                    End = new Point(680, 130),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(680, 130),
                    End = new Point(580, 130),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(680, 130),
                    End = new Point(700, 150),
                    PathId = 1
                },
                new Segment
                {
                    Start = new Point(0, 170),
                    End = new Point(40, 170),
                },
                new Segment
                {
                    Start = new Point(40, 170),
                    End = new Point(700, 170),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(700, 170),
                    End = new Point(800, 170),
                },
                new Segment
                {
                    Start = new Point(40, 170),
                    End = new Point(50, 180),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(50, 180),
                    End = new Point(100, 180),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(50, 180),
                    End = new Point(60, 190),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(60, 190),
                    End = new Point(120, 190),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(100, 180),
                    End = new Point(120, 190),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(120, 190),
                    End = new Point(680, 190),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(680, 190),
                    End = new Point(700, 170),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(120, 190),
                    End = new Point(150, 210),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(150, 210),
                    End = new Point(660, 210),
                    PathId = 2
                },
                new Segment
                {
                    Start = new Point(660, 210),
                    End = new Point(680, 190),
                    PathId = 2
                },
            };
        }
    }
}
