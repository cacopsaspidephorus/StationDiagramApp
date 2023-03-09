using StationDiagramApp.Services;

namespace StationDiagramApp.Tests
{
    public class StationDiagramServiceTests
    {
        private IStationDiagramService _stationDiagramService;

        [SetUp]
        public void Setup()
        {
            _stationDiagramService = new StationDiagramService();
        }

        [Test]
        public void GetTestStationDiagram()
        {
            var diagram = _stationDiagramService.GetTestStationDiagram();

            Assert.IsNotNull(diagram);
        }
    }
}