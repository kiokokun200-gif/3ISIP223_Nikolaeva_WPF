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
    public partial class _4PageAccount : Page
    {
        private User _user;
        private List<UserService> _userServices;
        private List<Order> _userOrders;

        public _4PageAccount(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
            LoadData();
        }

        private void LoadData()
        {
            _userServices = Core.Context.UserService.Where(u => u.UserID == _user.ID).ToList();
            ListBoxAppointments.ItemsSource = _userServices;

            _userOrders = Core.Context.Order.Where(o => o.UserID == _user.ID).ToList();
            ListBoxOrders.ItemsSource = _userOrders;
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            UserData.CurrentUser = null;
            NavigationService.GoBack();
        }
    }
}
