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

namespace _3ISIP223_Nikolaeva_WPF
{
    /// <summary>
    /// Логика взаимодействия для PageConference.xaml
    /// </summary>
    public partial class PageConference : Page
    {
        private Conferences _conference;
        private Users _user = null;
        private List<Users> _members;
        public PageConference(Conferences conference)
        {
            InitializeComponent();
            _conference = conference;
            DataContext = _conference;
            _members = Core.Context.Registrations.Where(r => r.ConferenceId == _conference.Id).Select(u => u.Users).ToList();
            ListBoxMembers.ItemsSource = _members;

        }

        private void BtnAutoZap_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtBoxLogin.Text;
            if(string.IsNullOrWhiteSpace( login))
            {
                MessageBox.Show("Заполните логин");
                return;
            }
            _user = Core.Context.Users.FirstOrDefault(u => u.Email == login);
            if (_user == null)
            {
                MessageBox.Show("ПОльзователь с таким логином не найден");
                return ;
            }
            TxtBoxUserName.Text = _user.FullName;
            TxtBoxOrganization.Text = _user.Organization;

        }

        private void UserConfReg()
        {
            Registrations reg = new Registrations()
            {
                UserId = _user.Id,
                ConferenceId = _conference.Id,
                RegistrationDate = DateTime.Now,
            };
            Core.Context.Registrations.Add(reg);
            Core.Context.SaveChanges();
        }

        private void BtnRegConference_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TxtBoxLogin.Text) || string.IsNullOrEmpty(TxtBoxOrganization.Text) || string.IsNullOrEmpty(TxtBoxUserName.Text))
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            if(_user == null)
            {
                var user = new Users();
                //дальше создаем и назначаем _user
            }

            else if (Core.Context.Registrations.FirstOrDefault(u => u.UserId == _user.Id && u.ConferenceId == _conference.Id) == null)
            {
                MessageBox.Show("Успешно");
            }
            else
            {
                MessageBox.Show("Вы уже зарегистрированы на эту конференцию");
            }
        }

        private void ListBoxMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Users user = ListBoxMembers.SelectedItem as Users;
            if (user != null)
            {
                NavigationService.Navigate(new PageUser(user));
            }
            else return;
        }
    }
}
