using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Turbo.azWithLinq.Data;

namespace Turbo.azWithLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClassesCarsDataContext dtx = new DataClassesCarsDataContext();
        public MainWindow()
        {
            InitializeComponent();
            cmbAllBanTypes.ItemsSource = dtx.BanTypes;
            cmbAllColors.ItemsSource = dtx.Colors;
            cmbAllFuelTypes.ItemsSource = dtx.FuelTypes;
            cmbAllGears.ItemsSource = dtx.Gears;
            cmbAllMakes.ItemsSource = dtx.Makes;
            cmbAllRegions.ItemsSource = dtx.Regions;
            cmbAllTransmissions.ItemsSource = dtx.Transmissions;
            cmbMaxEngVolume.ItemsSource = dtx.EngineVolumes;
            cmbMinEngVolume.ItemsSource = dtx.EngineVolumes;

        }

        private void CmbAllMakes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarMake = cmbAllMakes.SelectedItem as Make;
            var models = dtx.Models.Where(x => x.MakeId == selectedCarMake.Id);
            cmbAllModels.ItemsSource = models;
        }
    }
}
