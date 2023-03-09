using Microsoft.Extensions.DependencyInjection;
using StationDiagramApp.Services;
using StationDiagramApp.ViewModels;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace StationDiagramApp
{
    public partial class StationDiagramView : Window
    {
        public StationDiagramView()
        {
            var stationDiagramService = App.AppHost!.Services.GetRequiredService<IStationDiagramService>();

            DataContext = new StationDiagramViewModel(stationDiagramService);

            InitializeComponent();
        }

        private void ColorsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedItem = (PropertyInfo)ColorsComboBox.SelectedItem;

            StationDiagramRenderer.FillColor = (Color)selectedItem.GetValue(null, null); ;
        }
    }
}
