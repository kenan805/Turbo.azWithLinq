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
using Turbo.azWithLinq.Views;
using Color = Turbo.azWithLinq.Data.Color;

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

        private void btn_searchCars_Click(object sender, RoutedEventArgs e)
        {
            CarsDataWindow carsDataWindow = new CarsDataWindow();
            if (cmbAllBanTypes.SelectedIndex != -1
                || cmbAllColors.SelectedIndex != -1
                || cmbAllFuelTypes.SelectedIndex != -1
                || cmbAllGears.SelectedIndex != -1
                || cmbAllMakes.SelectedIndex != -1
                || cmbAllModels.SelectedIndex != -1
                || cmbAllRegions.SelectedIndex != -1
                || cmbAllTransmissions.SelectedIndex != -1
                || cmbMaxEngVolume.SelectedIndex != -1
                || txbMinMileage != null
                || txbMaxMileage != null
                || txbMinPrice != null
                || txbMaxPrice != null
                || txbMinRegYear != null
                || txbMaxRegYear != null
                )
            {
                carsDataWindow.dataGridCars.ItemsSource = from car in dtx.Cars
                                                          where car.Make.Name == (cmbAllMakes.SelectedItem as Make).Name
                                                          && car.Model.Name == (cmbAllModels.SelectedItem as Model).Name
                                                          && car.BanType.Name == (cmbAllBanTypes.SelectedItem as BanType).Name
                                                          && car.Color.Name == (cmbAllColors.SelectedItem as Color).Name
                                                          && car.FuelType.Name == (cmbAllFuelTypes.SelectedItem as FuelType).Name
                                                          && car.Gear.Name == (cmbAllGears.SelectedItem as Gear).Name
                                                          && car.Region.Name == (cmbAllRegions.SelectedItem as Region).Name
                                                          && car.Transmission.Name == (cmbAllTransmissions.SelectedItem as Transmission).Name
                                                          && car.Mileage >= int.Parse(txbMinMileage.Text)
                                                          && car.Mileage <= int.Parse(txbMaxMileage.Text)
                                                          && car.Price >= int.Parse(txbMinPrice.Text)
                                                          && car.Price <= int.Parse(txbMaxPrice.Text)
                                                          && car.RegYear <= int.Parse(txbMinRegYear.Text)
                                                          && car.RegYear <= int.Parse(txbMaxRegYear.Text)
                                                          && car.EngineVolume.Volume <= int.Parse(cmbMinEngVolume.Text)
                                                          && car.EngineVolume.Volume <= int.Parse(cmbMaxEngVolume.Text)
                                                          select new
                                                          {
                                                              City = car.Region.Name,
                                                              Makes = car.Make.Name,
                                                              Model = car.Model.Name,
                                                              Price = car.Price,
                                                              RegistrationYear = car.RegYear,
                                                              BanType = car.BanType.Name,
                                                              Color = car.Color.Name,
                                                              EngineVolume = car.EngineVolume.Volume,
                                                              FuelType = car.FuelType.Name,
                                                              Mileage = car.Mileage,
                                                              Transmission = car.Transmission.Name
                                                          };
                carsDataWindow.ShowDialog();
            }
            else MessageBox.Show("KHAKHDSA");
        }
    }
}
