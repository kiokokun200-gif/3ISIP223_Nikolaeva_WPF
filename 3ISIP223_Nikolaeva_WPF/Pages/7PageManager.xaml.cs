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
        public _7PageManager()
        {
            InitializeComponent();
            _userServices = Core.Context.UserService.ToList();
            ListBoxAppointments.ItemsSource = _userServices;
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

        }
    }
}
