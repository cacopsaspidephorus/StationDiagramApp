using StationDiagramApp.Models;
using StationDiagramApp.Services;
using StationDiagramApp.ViewModels;

namespace StationDiagramApp.Tests
{
    public class ShortestPathViewModelTests
    {
        private IStationDiagramService _stationDiagramService;
        private ShortestPathViewModel _shortestPathViewModel;

        [SetUp]
        public void Setup()
        {
            _stationDiagramService = new StationDiagramService();
            _shortestPathViewModel = new ShortestPathViewModel(_stationDiagramService);
        }

        [Test]
        public void FindShortestPath_NoStartSegment()
        {
            StationDiagram diagram = _stationDiagramService.GetTestStationDiagram();
            List<Segment> segments = diagram.Segments;

            var shortestPath = _shortestPathViewModel.FindShortestPath(null, segments[10]);

            Assert.IsNull(shortestPath);
        }

        [Test]
        public void FindShortestPath_PathsNotConnected()
        {
            StationDiagram diagram = _stationDiagramService.GetTestStationDiagram();
            List<Segment> segments = diagram.Segments;

            var shortestPath = _shortestPathViewModel.FindShortestPath(segments[1], segments[38]);

            Assert.IsNull(shortestPath);
        }

        [Test]
        public void FindShortestPath()
        {
            StationDiagram diagram = _stationDiagramService.GetTestStationDiagram();
            List<Segment> segments = diagram.Segments;

            var shortestPath = _shortestPathViewModel.FindShortestPath(segments[0], segments[15]);

            Assert.IsNotNull(shortestPath);
            Assert.AreEqual(shortestPath.Count, 7);
        }
    }
}
