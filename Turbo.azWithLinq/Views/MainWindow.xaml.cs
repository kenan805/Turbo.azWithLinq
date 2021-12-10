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

            _cars = from car in _reposCars.GetAll()
                    where car.Make.Name == selectedCarMake.Name
                    select car;

        }

        private void Btn_searchCars_Click(object sender, RoutedEventArgs e)
        {
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
            if (cars.Count() == 0) _carsDataWindow.DataCount = false;
            else _carsDataWindow.DataCount = true;
            _carsDataWindow.ShowDialog();
        }

        private void CmbAllModels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarModel = cmbAllModels.SelectedItem as Model;
            _cars = from car in _reposCars.GetAll()
                    where car.Model.Name == selectedCarModel.Name
                    select car;
        }

        private void CmbAllBanTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarBanType = cmbAllBanTypes.SelectedItem as BanType;
            _cars = from car in _reposCars.GetAll()
                    where car.BanType.Name == selectedCarBanType.Name
                    select car;
        }

        private void CmbAllColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarColor = cmbAllColors.SelectedItem as Color;
            _cars = from car in _reposCars.GetAll()
                    where car.Color.Name == selectedCarColor.Name
                    select car;
        }

        private void CmbAllRegions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarRegion = cmbAllRegions.SelectedItem as Region;
            _cars = from car in _reposCars.GetAll()
                    where car.Region.Name == selectedCarRegion.Name
                    select car;
        }

        private void CmbAllFuelTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarFuelType = cmbAllFuelTypes.SelectedItem as FuelType;
            _cars = from car in _reposCars.GetAll()
                    where car.FuelType.Name == selectedCarFuelType.Name
                    select car;
        }

        private void CmbAllGears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarGear = cmbAllGears.SelectedItem as Gear;
            _cars = from car in _reposCars.GetAll()
                    where car.Gear.Name == selectedCarGear.Name
                    select car;
        }

        private void CmbAllTransmissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCarTransmission = cmbAllTransmissions.SelectedItem as Transmission;
            _cars = from car in _reposCars.GetAll()
                    where car.Transmission.Name == selectedCarTransmission.Name
                    select car;
        }

        private void txbMinMileage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbMaxMileage.Text == string.Empty)
            {
                try
                {
                    var selectedCarMinMil = int.Parse(txbMinMileage.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Mileage >= selectedCarMinMil
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var selectedCarMinMil = int.Parse(txbMinMileage.Text);
                    var selectedCarMaxMil = int.Parse(txbMaxMileage.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Mileage >= selectedCarMinMil && car.Mileage <= selectedCarMaxMil
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txbMaxMileage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbMinMileage.Text == string.Empty)
            {
                try
                {
                    var selectedCarMaxMil = int.Parse(txbMaxMileage.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Mileage <= selectedCarMaxMil
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var selectedCarMinMil = int.Parse(txbMinMileage.Text);
                    var selectedCarMaxMil = int.Parse(txbMaxMileage.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Mileage >= selectedCarMinMil && car.Mileage <= selectedCarMaxMil
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txbMinPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbMaxPrice.Text == string.Empty)
            {
                try
                {
                    var selectedCarMinPrice = int.Parse(txbMinPrice.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Price >= selectedCarMinPrice
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var selectedCarMinPrice = int.Parse(txbMinPrice.Text);
                    var selectedCarMaxPrice = int.Parse(txbMaxPrice.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Price >= selectedCarMinPrice && car.Price <= selectedCarMaxPrice
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txbMaxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbMinPrice.Text == string.Empty)
            {
                try
                {
                    var selectedCarMaxPrice = int.Parse(txbMaxPrice.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Price <= selectedCarMaxPrice
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var selectedCarMinPrice = int.Parse(txbMinPrice.Text);
                    var selectedCarMaxPrice = int.Parse(txbMaxPrice.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.Price >= selectedCarMinPrice && car.Price <= selectedCarMaxPrice
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txbMinRegYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbMaxRegYear.Text == string.Empty)
            {
                try
                {
                    var selectedCarMinRegYear = int.Parse(txbMinRegYear.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.RegYear >= selectedCarMinRegYear
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var selectedCarMinRegYear = int.Parse(txbMinRegYear.Text);
                    var selectedCarMaxRegYear = int.Parse(txbMaxRegYear.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.RegYear >= selectedCarMinRegYear && car.RegYear <= selectedCarMaxRegYear
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txbMaxRegYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txbMinRegYear.Text == string.Empty)
            {
                try
                {
                    var selectedCarMaxRegYear = int.Parse(txbMaxRegYear.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.RegYear <= selectedCarMaxRegYear
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    var selectedCarMinRegYear = int.Parse(txbMinRegYear.Text);
                    var selectedCarMaxRegYear = int.Parse(txbMaxRegYear.Text);
                    _cars = from car in _reposCars.GetAll()
                            where car.RegYear >= selectedCarMinRegYear && car.Price <= selectedCarMaxRegYear
                            select car;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


    }
}
