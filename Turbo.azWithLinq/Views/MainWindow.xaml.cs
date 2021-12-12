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
using Turbo.azWithLinq.DataAccess.Abstract;
using Turbo.azWithLinq.DataAccess.Concrete;
using Turbo.azWithLinq.DataAccess.Context;
using Turbo.azWithLinq.Views;
using Color = Turbo.azWithLinq.DataAccess.Context.Color;

namespace Turbo.azWithLinq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClassesCarsDataContext dtx = new DataClassesCarsDataContext();
        CarsDataWindow _carsDataWindow;

        IQueryable<Car> _cars;
        IGenericRepository<Car> _reposCars;
        IGenericRepository<Model> _reposModels;
        IGenericRepository<Make> _reposMakes;
        IGenericRepository<BanType> _reposBanTypes;
        IGenericRepository<Color> _reposColors;
        IGenericRepository<Gear> _reposGears;
        IGenericRepository<FuelType> _reposFuelTypes;
        IGenericRepository<Region> _reposRegions;
        IGenericRepository<Transmission> _reposTransmissions;
        IGenericRepository<EngineVolume> _reposEngineVolumes;
        public MainWindow()
        {
            InitializeComponent();
            _reposCars = new GenericRepository<Car>();
            _reposModels = new GenericRepository<Model>();
            _reposEngineVolumes = new GenericRepository<EngineVolume>();
            _reposBanTypes = new GenericRepository<BanType>();
            _reposColors = new GenericRepository<Color>();
            _reposFuelTypes = new GenericRepository<FuelType>();
            _reposMakes = new GenericRepository<Make>();
            _reposGears = new GenericRepository<Gear>();
            _reposRegions = new GenericRepository<Region>();
            _reposTransmissions = new GenericRepository<Transmission>();



            cmbAllBanTypes.ItemsSource = _reposBanTypes.GetAll();
            cmbAllColors.ItemsSource = _reposColors.GetAll();
            cmbAllFuelTypes.ItemsSource = _reposFuelTypes.GetAll();
            cmbAllGears.ItemsSource = _reposGears.GetAll();
            cmbAllMakes.ItemsSource = _reposMakes.GetAll();
            cmbAllRegions.ItemsSource = _reposRegions.GetAll();
            cmbAllTransmissions.ItemsSource = _reposTransmissions.GetAll();
            cmbMaxEngVolume.ItemsSource = _reposEngineVolumes.GetAll();
            cmbMinEngVolume.ItemsSource = _reposEngineVolumes.GetAll();
            cmbMaxEngVolume.SelectedIndex = _reposEngineVolumes.GetAll().Count() - 1;

        }


        private void CmbAllMakes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarMake = cmbAllMakes.SelectedItem as Make;
            var models = _reposModels.Query(x => x.MakeId == selectedCarMake.Id);
            cmbAllModels.ItemsSource = models;

        }

        private void Btn_searchCars_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IQueryable<Car> carsResult = _reposCars.GetAll();
                #region all combo boxes filter
                if (cmbAllMakes.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Make.Name == (cmbAllMakes.SelectedItem as Make).Name);
                if (cmbAllModels.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Model.Name == (cmbAllModels.SelectedItem as Model).Name);
                if (cmbAllBanTypes.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.BanType.Name == (cmbAllBanTypes.SelectedItem as BanType).Name);
                if (cmbAllColors.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Color.Name == (cmbAllColors.SelectedItem as Color).Name);
                if (cmbAllRegions.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Region.Name == (cmbAllRegions.SelectedItem as Region).Name);
                if (cmbAllFuelTypes.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.FuelType.Name == (cmbAllFuelTypes.SelectedItem as FuelType).Name);
                if (cmbAllGears.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Gear.Name == (cmbAllGears.SelectedItem as Gear).Name);
                if (cmbAllTransmissions.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Transmission.Name == (cmbAllTransmissions.SelectedItem as Transmission).Name);
                if (cmbAllTransmissions.SelectedItem != null)
                    carsResult = carsResult.Where(c => c.Transmission.Name == (cmbAllTransmissions.SelectedItem as Transmission).Name);
                #endregion
                #region Mileage filter
                if (txbMinMileage.Text != string.Empty)
                {
                    carsResult = carsResult.Where(c => c.Mileage >= int.Parse(txbMinMileage.Text));
                }
                if (txbMaxMileage.Text != string.Empty)
                {
                    carsResult = carsResult.Where(c => c.Mileage <= int.Parse(txbMaxMileage.Text));
                }
                #endregion
                #region Price filter
                if (txbMinPrice.Text != string.Empty)
                {
                    carsResult = carsResult.Where(c => c.Price >= decimal.Parse(txbMinPrice.Text));
                }
                if (txbMaxPrice.Text != string.Empty)
                {
                    carsResult = carsResult.Where(c => c.Price <= decimal.Parse(txbMaxPrice.Text));
                }
                #endregion
                #region Year filter
                if (txbMinRegYear.Text != string.Empty)
                {
                    carsResult = carsResult.Where(c => c.RegYear >= int.Parse(txbMinRegYear.Text));
                }
                if (txbMaxRegYear.Text != string.Empty)
                {
                    carsResult = carsResult.Where(c => c.RegYear <= int.Parse(txbMaxRegYear.Text));
                }
                #endregion

                _cars = carsResult;
                var cars = from car in _cars
                           select new
                           {
                               City = car.Region.Name,
                               Makes = car.Make.Name,
                               Model = car.Model.Name,
                               Price = car.Price,
                               RegistrationYear = car.RegYear,
                               BanType = car.BanType.Name,
                               Mileage = car.Mileage,
                               Color = car.Color.Name,
                               EngineVolume = car.EngineVolume.Volume,
                               FuelType = car.FuelType.Name,
                               Gear = car.Gear.Name,
                               Transmission = car.Transmission.Name
                           };

                cars = cars.Where(x => x.EngineVolume >= (cmbMinEngVolume.SelectedItem as EngineVolume).Volume
                && x.EngineVolume <= (cmbMaxEngVolume.SelectedItem as EngineVolume).Volume);

                switch (cmbSorting.SelectedIndex)
                {
                    case 0:
                        cars = cars.OrderBy(x => x.Price);
                        break;
                    case 1:
                        cars = cars.OrderByDescending(x => x.Price);
                        break;
                    case 2:
                        cars = cars.OrderBy(x => x.Mileage);
                        break;
                    case 3:
                        cars = cars.OrderByDescending(x => x.RegistrationYear);
                        break;
                    default:
                        break;
                }


                _carsDataWindow = new CarsDataWindow();
                _carsDataWindow.dataGridCars.ItemsSource = cars;
                if (cars.Count() == 0) _carsDataWindow.IsData = false;
                else _carsDataWindow.IsData = true;
                _carsDataWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
