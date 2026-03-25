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

        public bool CorrectEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }
        public bool CorrectPass(string pass)
        {
            return !string.IsNullOrEmpty(pass) && pass.Length > 5;
        }
        public bool CorrectAll(string email, string password, out string errorMessage)
        {
            if (!CorrectEmail(email))
            {
                errorMessage = "Введите корректный email (должен содержать @ и .)";
                return false;
            }

            if (!CorrectPass(password))
            {
                errorMessage = "Пароль должен быть длиннее 5 символов";
                return false;
            }

            errorMessage = null;
            return true;
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


        public bool LogIn(string login, string password, out string errorMessage)
        {
            if (!CorrectAll(login, password, out errorMessage))
            {
                return false;
            }

            var user = Core.Context.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                UserData.CurrentUser = user;
                errorMessage = null;
                return true;
            }

            errorMessage = "Пользователь с этими данными не найден";
            return false;
        }

        private void BtnLoginBD_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;
            bool isAuth = LogIn(TxtBoxEmail.Text, TxtBoxPassword.Text, out errorMessage);

            if (isAuth)
            {
                MessageBox.Show("Вход успешный");
                NavigationService.Navigate(new _1Page());
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }
    }
}
