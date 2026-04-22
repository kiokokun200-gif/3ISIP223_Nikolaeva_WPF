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
    /// Логика взаимодействия для _6PageChoiceService.xaml
    /// </summary>
    public partial class _6PageChoiceService : Page
    {
        private ServCategory _category;
        private List<Service> _services;
        private List<User> _masters;
        private List<MasterService> _masterService;
        private bool isRadioCheck = false;

        private Service _selectedService;
        private User _selectedMaster;
        public _6PageChoiceService(ServCategory category)
        {
            InitializeComponent();
            _category = category;
            DataContext = _category;
            LoadData();
        }

        private void LoadData()
        {
            _masterService = Core.Context.MasterService.Where(m => m.User.Role.Name == "Мастер" && m.ServCategotyID == _category.ID).ToList();
            _masters = _masterService.Select(u => u.User).ToList();
            _services = Core.Context.Service.Where(s => s.CategoryID == _category.ID).ToList();
            ListBoxMasters.ItemsSource = _masters;
            ListBoxServices.ItemsSource = _services;

        }

        private void BtnChoiceMaster_Click(object sender, RoutedEventArgs e)
        {
            if(!UserData.IsLoggedIn)
            {
                MessageBox.Show("Войдите в аккаунт");
                return;
            }
            if (!isRadioCheck)
            {
                MessageBox.Show("Выберите услугу!");
                return;
            }
            Button btn = (Button)sender;
            _selectedMaster = btn.DataContext as User;
            if (_selectedMaster == null && _selectedService == null) return;
            NavigationService.Navigate(new _5PageAppointments(_selectedMaster, _selectedService));
        }

        private void RadioService_Checked(object sender, RoutedEventArgs e)
        {
            isRadioCheck = true;
            RadioButton btn = (RadioButton)sender;
            _selectedService = btn.DataContext as Service;

        }
    }
}
