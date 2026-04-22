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
using System.Windows.Shapes;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowAddClientAppointment.xaml
    /// </summary>
    public partial class WindowAddClientAppointment : Window
    {
        private List<User> _users;
        private Service _service;
        public WindowAddClientAppointment(Service service)
        {
            InitializeComponent();
            _service = service;
            _users = Core.Context.User.Where(u => u.Role.Name =="Клиент").ToList();
            ListBoxClients.ItemsSource = _users;

        }

        private void TxtBoxSearchUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TxtBoxSearchUser.Text.ToLower();
            ListBoxClients.ItemsSource = _users.Where(u => u.FirstName.ToLower().Contains(search) || u.LastName.ToLower().Contains(search) || u.MiddleName.ToLower().Contains(search) || u.PhoneNumber.ToLower().Contains(search)).ToList();

        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User selectedUser = (User)btn.DataContext;
            if (selectedUser != null) {
                
                //UserService userserv = new UserService()
                //{
                //   UserID = selectedUser.ID,

                //}
            }
        }
    }
}
