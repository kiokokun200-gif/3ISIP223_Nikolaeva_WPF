using _3ISIP223_Nikolaeva_WPF.Models;
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

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _1PageMain.xaml
    /// </summary>
    public partial class _1PageMain : Page
    {
        private List<string> _services;
        private List<string> _masters;
        private List<Service> _serviceslist;
        //private WindowLogIn _wind;
        public _1PageMain()
        {
            InitializeComponent();

            LoadFiltr();
            UpdateAccount();

        }
        private void LoadFiltr()
        {
            _services = Core.Context.ServCategory.Select(s => s.Name).Distinct().ToList();
            _services.Insert(0, "Все");
            ComboBoxServices.ItemsSource = _services;
            _masters = Core.Context.User.Where(m => m.Role.Name == "Мастер").Select(u => u.LastName).Distinct().ToList();
            _masters.Insert(0, "Все");
            ComboBoxMasters.ItemsSource = _masters;
        }

        public void UpdateAccount()
        {
            if(UserData.IsLoggedIn)
            {
                //Image image = new Image();
                //image.Source = new BitmapImage( new Uri("/Images/accicon.png", UriKind.Relative));
                BtnLogin.Content = "Аккаунт";
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!UserData.IsLoggedIn)
            {
                var _wind = new WindowLogIn(this);
                _wind.ShowDialog();
            }
            else if(UserData.IsLoggedIn && UserData.CurrentUser.Role.Name == "Клиент")
            {
                //переход на страницу аккаунта 
            }
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _2PageProducts());
            //UpdateAccount();
        }
    }
}
