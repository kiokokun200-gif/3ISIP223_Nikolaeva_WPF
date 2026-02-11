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
    /// Логика взаимодействия для _3PageLogin.xaml
    /// </summary>
    public partial class _3PageLogin : Page
    {
        private bool _CorrectEmail = false;
        private bool _CorrectPass = false;
        public _3PageLogin()
        {
            InitializeComponent();
            if(UserData.IsLoggedIn)
            {
                NavigationService.Navigate(new _2PageProfile());
            }

        }

        private void UpdateButton()
        {
            BtnLoginBD.IsEnabled = (_CorrectEmail && _CorrectPass);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Content is _4RegisterPage)
            {
                NavigationService.Navigate(new _3PageLogin());
            }
            else return;
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
          
            NavigationService.Navigate(new _4RegisterPage());

        }


        private void TxtBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        { 
            _CorrectEmail = TxtBoxEmail.Text.Contains("@") && TxtBoxEmail.Text.Contains(".");
            UpdateButton();

        }

        private void TxtBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {    
            _CorrectPass = TxtBoxPassword.Text.Length > 6 && TxtBoxPassword.Text.Any(char.IsDigit);
            UpdateButton();
        }

        private void BtnLoginBD_Click(object sender, RoutedEventArgs e)
        {
            var user = Core.Context.Users.FirstOrDefault(u => u.Email == TxtBoxEmail.Text && u.Password == TxtBoxPassword.Text);
            if (user != null)
            {
                MessageBox.Show("Вход успешный");
                //добавить перезод на страницу
                NavigationService.Navigate(new _1Page());

                UserData.CurrentUser = user;
            }
            else MessageBox.Show("Ошибка входа");
        }
    }
}
