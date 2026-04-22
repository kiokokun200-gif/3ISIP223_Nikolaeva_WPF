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
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _7PageManager.xaml
    /// </summary>
    public partial class _7PageManager : Page
    {
        private List<Service> _services;
        private List<UserService> _userServices;
        private List<ProdCategory> _prodCategories;
        private List<Order> _orders;
        private List<Product> _products;
        private List<Manufacturer> _manufacturers;
        private List<ServCategory> _servCategories;
        public _7PageManager()
        {
            InitializeComponent();
            LoadDate();
        }

        private void LoadDate()
        {
            _userServices = Core.Context.UserService.ToList();
            ListBoxAppointments.ItemsSource = _userServices;
            _orders = Core.Context.Order.ToList();
            ListBoxOrders.ItemsSource = _orders;
            _products = Core.Context.Product.ToList();
            ListBoxProduct.ItemsSource = _products;
            _prodCategories = Core.Context.ProdCategory.ToList();
            ListBoxProdCateg.ItemsSource = _prodCategories;
            _manufacturers = Core.Context.Manufacturer.ToList();
            ListBoxManufacturers.ItemsSource = _manufacturers;  
            _servCategories = Core.Context.ServCategory.ToList();
            ListBoxServCat.ItemsSource = _servCategories;


        }

        private void BtnCancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            UserService userService = (UserService)btn.DataContext;
            //var userservBD = Core.Context.UserService.First(u => u.ID == userService.ID);
            Core.Context.UserService.Remove(userService);
            Core.Context.SaveChanges();

        }

        private void BtnChangeAppointment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAddClientAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            UserService userService = (UserService)btn.DataContext;

            var wind = new WindowAddClientAppointment(userService.Service);
            wind.ShowDialog();
        }

        private void BtnCloseOrder_Click(object sender, RoutedEventArgs e)
        {
            //изменить статус 
        }

        private void BtnChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            Product product = (Product)bt.DataContext;
            var wind = new WindowChangeProduct(product);
            wind.ShowDialog();
        }

        private void BtnFrozeProduct_Click(object sender, RoutedEventArgs e)
        {
            //добавит в партиал свойство
        }

        private void BtnAddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddManufacturer();
            wind.ShowDialog();
        }


        private void BtnChangeMan_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Manufacturer manufacturer = (Manufacturer)button.DataContext;
            var wind = new WindowChangeManufacturer(manufacturer);
            wind.ShowDialog();

        }

        private void BtnAddProdCat_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddProdCat();
            wind.ShowDialog();
        }

        private void BtnChangeProdCat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;    
            ProdCategory prodCategory = (ProdCategory)btn.DataContext;
            var wind = new WindowAddProdCat();
            wind.ShowDialog();

        }

        private void BtnAddServCat_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddServCat();
            wind.ShowDialog();
        }

        private void BtnChangeServCat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = ( Button)sender;
            ServCategory servCategory = (ServCategory)btn.DataContext;
            var wind = new WindowChangeServCat(servCategory);
            wind.ShowDialog();
        }
    }
}
