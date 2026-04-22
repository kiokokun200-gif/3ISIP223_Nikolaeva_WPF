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
    public partial class _7PageManager : Page
    {
        private List<UserService> _userServices;
        private List<Order> _orders;
        private List<Product> _products;
        private List<ProdCategory> _prodCategories;
        private List<Manufacturer> _manufacturers;
        private List<ServCategory> _servCategories;

        public _7PageManager()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
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

        // ==================== ЗАПИСИ ====================

        private void BtnCancelAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            UserService userService = (UserService)btn.Tag;

            MessageBoxResult result = MessageBox.Show($"Отменить запись клиента {userService.User.FirstName}?",
                "Подтверждение", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                userService.Status = "Cancelled";

                var schedule = Core.Context.Schedule.FirstOrDefault(s => s.ID == userService.ID_Schedule);
                if (schedule != null)
                {
                    schedule.IsAvailable = true;
                }

                Core.Context.SaveChanges();
                LoadData();
                MessageBox.Show("Запись отменена");
            }
        }

        private void BtnChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            UserService userService = (UserService)btn.Tag;

            var wind = new WindowChangeAppointment(userService);
            wind.ShowDialog();
            LoadData();
        }

        private void BtnAddClientAppointment_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddClientAppointment();
            wind.ShowDialog();
            LoadData();
        }

        // ==================== ЗАКАЗЫ ====================

        private void BtnCloseOrder_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Order order = (Order)btn.Tag;

            if (order.IsClosed == false)
            {
                MessageBoxResult result = MessageBox.Show($"Выдать заказ №{order.ID} клиенту {order.User.FirstName}?",
                    "Подтверждение", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    order.IsClosed = true;
                    Core.Context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Заказ выдан");
                }
            }
        }

        // ==================== ТОВАРЫ ====================

        private void BtnChangeProduct_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Product product = (Product)btn.Tag;
            var wind = new WindowChangeProduct(product);
            wind.ShowDialog();
            LoadData();
        }

        private void BtnFrozeProduct_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Product product = (Product)btn.Tag;

            product.IsFrozen = !product.IsFrozen;
            Core.Context.SaveChanges();
            LoadData();

            string message = product.IsFrozen ? "Товар заморожен" : "Товар разморожен";
            MessageBox.Show(message);
        }

        // ==================== ПРОИЗВОДИТЕЛИ ====================

        private void BtnAddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddManufacturer();
            wind.ShowDialog();
            LoadData();
        }

        private void BtnChangeMan_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Manufacturer manufacturer = (Manufacturer)btn.Tag;
            var wind = new WindowChangeManufacturer(manufacturer);
            wind.ShowDialog();
            LoadData();
        }

        // ==================== ТИПЫ ТОВАРОВ ====================

        private void BtnAddProdCat_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddProdCat();
            wind.ShowDialog();
            LoadData();
        }

        private void BtnChangeProdCat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ProdCategory prodCategory = (ProdCategory)btn.Tag;
            var wind = new WindowChangeProdCategory(prodCategory);
            wind.ShowDialog();
            LoadData();
        }

        // ==================== ТИПЫ УСЛУГ ====================

        private void BtnAddServCat_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddServCat();
            wind.ShowDialog();
            LoadData();
        }

        private void BtnChangeServCat_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ServCategory servCategory = (ServCategory)btn.Tag;
            var wind = new WindowChangeServCat(servCategory);
            wind.ShowDialog();
            LoadData();
        }
    }
}
