using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using StationDiagramApp.Models;
using StationDiagramApp.Services;
using StationDiagramApp.Views;
using System.Collections.Generic;

namespace StationDiagramApp.ViewModels
{
    public class StationDiagramViewModel : ObservableObject
    {
        private Park _selectedPark;

        private readonly IStationDiagramService _stationDiagramService;
        
        public StationDiagram TestStationDiagram { get; private set; }

        public IRelayCommand ShortestPathViewRelayCommand { get; private set; }

        public Park SelectedPark
        {
            get => _selectedPark;
            set => SetProperty(ref _selectedPark, value);
        }

        public StationDiagramViewModel(IStationDiagramService stationDiagramService)
        {
            _stationDiagramService = stationDiagramService;

            TestStationDiagram = _stationDiagramService.GetTestStationDiagram();

            ShortestPathViewRelayCommand = new RelayCommand(ShortestPathView);
        }

        private void ShortestPathView()
        {
            var shortestPathView = App.AppHost!.Services.GetRequiredService<ShortestPathView>();

            shortestPathView.ShowDialog();
        }
    }
}
