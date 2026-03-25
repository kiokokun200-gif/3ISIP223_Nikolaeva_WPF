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
       
        public _3PageLogin()
        {
            InitializeComponent();
            if(UserData.IsLoggedIn)
            {
                NavigationService.Navigate(new _2PageProfile());
            }

        }

      

        private bool CheckText()
        {
            bool CorrectEmail = !string.IsNullOrEmpty(TxtBoxEmail.Text);
            bool CorrectPass = !string.IsNullOrEmpty(TxtBoxPassword.Text);
            return (CorrectEmail && CorrectPass);
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


        private void BtnLoginBD_Click(object sender, RoutedEventArgs e)
        {
            if (CheckText())
            {
                bool IsAuth = LogIn(TxtBoxEmail.Text, TxtBoxPassword.Text);
                if (IsAuth)
                {
                    MessageBox.Show("Вход успешный");
                    NavigationService.Navigate(new _1Page());
                }
                else MessageBox.Show("Пользователь с этими данными не найден");
            }
            else MessageBox.Show("Заполните поля");
        }

        public bool LogIn(string login,  string password)
        {
            var user = Core.Context.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                UserData.CurrentUser = user;
                return true;
            }
            else 
            { 
                return false;
            }


        }
    }
}
