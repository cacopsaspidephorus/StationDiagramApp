using Microsoft.Extensions.DependencyInjection;
using StationDiagramApp.Services;
using StationDiagramApp.ViewModels;
using System.Windows;

namespace StationDiagramApp.Views
{
    public partial class ShortestPathView : Window
    {
        public ShortestPathView()
        {
            var stationDiagramService = App.AppHost!.Services.GetRequiredService<IStationDiagramService>();

            DataContext = new ShortestPathViewModel(stationDiagramService);

            InitializeComponent();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
                Close();
        }
    }
}
