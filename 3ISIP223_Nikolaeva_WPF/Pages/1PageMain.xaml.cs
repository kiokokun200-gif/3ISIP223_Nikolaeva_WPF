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
        private List<string>  _services;
        private List<string>  _masters;
        private List<User> _allmasters;
        private List<ServCategory> _serviceslist;
        private List<MasterService> _masterService;

        //private WindowLogIn _wind;
        public _1PageMain()
        {
            InitializeComponent();

            LoadFiltr();
            UpdateAccount();

        }
        private void LoadFiltr()
        {
            _serviceslist = Core.Context.ServCategory.ToList();
            ListBoxServicesTypes.ItemsSource = _serviceslist;

            _services = _serviceslist.Select(s => s.Name).Distinct().ToList();
            _services.Insert(0, "Все");
            ComboBoxServices.ItemsSource = _services;

            _allmasters = Core.Context.User.Where(u => u.Role.Name == "Мастер").ToList();
            _masters = _allmasters.Select(u => u.FirstName).Distinct().ToList();
            _masters.Insert(0, "Все");
            ComboBoxMasters.ItemsSource = _masters;
            _masterService = Core.Context.MasterService.Where(m => m.User.Role.Name == "Мастер").ToList();
        }

        public void UpdateAccount()
        {
            if(UserData.IsLoggedIn)
            {
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
                NavigationService.Navigate(new _4PageAccount(UserData.CurrentUser));
            }
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _2PageProducts());
            //UpdateAccount();
        }

        private void ComboBoxServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedService = (string)ComboBoxServices.SelectedItem;
            string selectedMaster = (string)ComboBoxMasters.SelectedItem;
            if (selectedMaster == null || selectedService == null) return;
            Filtr(selectedService, selectedMaster);

        }

        private void Filtr(string selectCategory, string selectMaster)
        {
            List<ServCategory> serv = _serviceslist;

            
            if (selectCategory == "Все" && selectMaster == "Все")
            {
                serv = serv;
            }
            else if (selectCategory == "Все" && selectMaster != "Все")
            {
                List<MasterService> masterserv = _masterService.Where(m => m.User.FirstName == selectMaster).ToList();
                serv = masterserv.Select(u => u.ServCategory).ToList();
            }
            else if (selectCategory != "Все" && selectMaster == "Все")
            {
                serv = serv.Where(p => p.Name == selectCategory).ToList();
            }
            else
            {
                List<MasterService> masterserv = _masterService.Where(m => m.User.FirstName == selectMaster).ToList();
                serv = masterserv.Where(u => u.ServCategory.Name == selectCategory).Select(a => a.ServCategory).ToList();
                serv = serv.Where(p => p.Name == selectCategory).ToList();
            }
           

            ListBoxServicesTypes.ItemsSource = serv;
        }
    }
}
