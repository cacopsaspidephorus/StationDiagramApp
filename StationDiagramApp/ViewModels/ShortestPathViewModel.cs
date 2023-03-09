using CommunityToolkit.Mvvm.ComponentModel;
using StationDiagramApp.Models;
using StationDiagramApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StationDiagramApp.ViewModels
{
    public class ShortestPathViewModel : ObservableObject
    {
        private Segment _startSegment;
        private Segment _endSegment;
        private IEnumerable<Segment> _shortestPath;

        private readonly IStationDiagramService _stationDiagramService;

        public StationDiagram TestStationDiagram { get; private set; }

        public IEnumerable<Segment> ShortestPath
        {
            get => _shortestPath;
            set => SetProperty(ref _shortestPath, value);
        }

        public Segment StartSegment
        {
            get => _startSegment;
            set
            {
                SetProperty(ref _startSegment, value);

                ShortestPath = FindShortestPath(StartSegment, EndSegment);
            }
        }

        public Segment EndSegment
        {
            get => _endSegment;
            set
            {
                SetProperty(ref _endSegment, value);

                ShortestPath = FindShortestPath(StartSegment, EndSegment);
            }
        }

        public ShortestPathViewModel(IStationDiagramService stationDiagramService)
        {
            _stationDiagramService = stationDiagramService;

            TestStationDiagram = _stationDiagramService.GetTestStationDiagram();
        }

        public List<Segment> FindShortestPath(Segment startSegment, Segment endSegment)
        {
            if (startSegment == null || endSegment == null)
                return null;

            var visitedNodes = new Dictionary<Segment, bool>();
            var priorityQueue = new PriorityQueue<Segment, double>();
            var shortestDistances = new Dictionary<Segment, double>();
            var previousNodes = new Dictionary<Segment, Segment>();

            shortestDistances[startSegment] = 0;
            priorityQueue.Enqueue(startSegment, 0);

            while (priorityQueue.Count > 0)
            {
                var currentSegment = priorityQueue.Dequeue();

                if (currentSegment == endSegment)
                {
                    var path = new List<Segment>();

                    while (currentSegment != startSegment)
                    {
                        path.Add(currentSegment);
                        currentSegment = previousNodes[currentSegment];
                    }

                    path.Reverse();

                    return path;
                }

                visitedNodes[currentSegment] = true;

                foreach (var neighborSegment in GetNeighborSegments(currentSegment))
                {
                    if (visitedNodes.ContainsKey(neighborSegment))
                        continue;

                    var tentativeDistance = shortestDistances[currentSegment] + GetDistance(currentSegment, neighborSegment);

                    if (!shortestDistances.ContainsKey(neighborSegment) || tentativeDistance < shortestDistances[neighborSegment])
                    {
                        shortestDistances[neighborSegment] = tentativeDistance;
                        previousNodes[neighborSegment] = currentSegment;
                        var priority = tentativeDistance + GetDistance(neighborSegment, endSegment);

                        priorityQueue.Enqueue(neighborSegment, priority);
                    }
                }
            }

            return null;
        }

        private IEnumerable<Segment> GetNeighborSegments(Segment segment)
        {
            return TestStationDiagram.Segments.Where(s =>
                s != segment &&
                (s.Start == segment.Start || s.Start == segment.End || s.End == segment.Start || s.End == segment.End));
        }

        private double GetDistance(Segment segment1, Segment segment2)
        {
            var midpoint1 = new Point { X = (segment1.Start.X + segment1.End.X) / 2, Y = (segment1.Start.Y + segment1.End.Y) / 2 };
            var midpoint2 = new Point { X = (segment2.Start.X + segment2.End.X) / 2, Y = (segment2.Start.Y + segment2.End.Y) / 2 };

            return Math.Sqrt(Math.Pow(midpoint2.X - midpoint1.X, 2) + Math.Pow(midpoint2.Y - midpoint1.Y, 2));
        }
    }
}
