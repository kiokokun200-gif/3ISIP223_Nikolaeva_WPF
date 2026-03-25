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
    /// Логика взаимодействия для _4RegisterPage.xaml
    /// </summary>
    public partial class _4RegisterPage : Page
    {
        public _4RegisterPage()
        {
            InitializeComponent();
            if (UserData.IsLoggedIn)
            {
                NavigationService.Navigate(new _2PageProfile());
            }
        }

   

        private bool CorrectEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }

        private bool CorrectName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length > 1;
        }

        private bool CorrectPass(string pass) 
        {
           return !string.IsNullOrEmpty(pass) && pass.Length > 5 && pass.Any(char.IsDigit);

        }

       private bool SamePass(string pass, string confpass)
        {
            return pass == confpass;
        }

        public bool CorrectAll(string name, string email, string password, string confirmPassword, out string errorMessage)
        {
            if (!CorrectName(name))
            {
                errorMessage = "Имя должно быть длиннее 1 символа";
                return false;
            }

            if (!CorrectEmail(email))
            {
                errorMessage = "Введите корректный email (должен содержать @ и .)";
                return false;
            }

            if (!CorrectPass(password))
            {
                errorMessage = "Пароль должен содержать цифры и быть длиннее 5 символов";
                return false;
            }

            if (!SamePass(password, confirmPassword))
            {
                errorMessage = "Пароли не совпадают";
                return false;
            }

            errorMessage = null;
            return true;
        }



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {      
             NavigationService.Navigate(new _3PageLogin());
        }

        private void BtnRegisterBD_Click(object sender, RoutedEventArgs e)
        {

            string errorMessage;
            bool isRegistered = Registration(TxtBoxName.Text, TxtBoxEmail.Text, TxtBoxPassword.Text, TxtBoxConfirmPassword.Text, out errorMessage);

            if (isRegistered)
            {
                MessageBox.Show("Регистрация успешна");
                NavigationService.Navigate(new _1Page());
            }
            else
            {
                MessageBox.Show(errorMessage);
            }
        }

        public bool Registration(string name, string email, string password, string confirmPassword, out string errorMessage)
        {
            if (!CorrectAll(name, email, password, confirmPassword, out errorMessage))
            {
                return false;
            }

            var existingUser = Core.Context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                errorMessage = "Пользователь с такой почтой уже существует";
                return false;
            }

            var newUser = new Users
            {
                Name = name,
                Password = password,
                Email = email
            };

            Core.Context.Users.Add(newUser);
            Core.Context.SaveChanges();
            UserData.CurrentUser = newUser;

            errorMessage = null;
            return true;
        }

        private void TxtBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!CorrectName(TxtBoxName.Text))
            {
                TxtBlckErrName.Text = "Имя должно быть длиннее 1 символа";
            }
            else
            {
                TxtBlckErrName.Text = "";
            }
        }

        private void TxtBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!CorrectEmail(TxtBoxEmail.Text))
            {
                TxtBlckErrEmail.Text = "Введите корректный email (должен содержать @ и .)";
            }
            else
            {
                TxtBlckErrEmail.Text = "";
            }
        }

        private void TxtBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!CorrectPass(TxtBoxPassword.Text))
            {
                TxtBlckErrPass1.Text = "Пароль должен содержать цифры и быть длиннее 5 символов";
            }
            else
            {
                TxtBlckErrPass1.Text = "";
            }

            if (!string.IsNullOrEmpty(TxtBoxConfirmPassword.Text))
            {
                CheckPasswordsMatch();
            }
        }

        private void TxtBoxConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckPasswordsMatch();
        }

        private void CheckPasswordsMatch()
        {
            if (!SamePass(TxtBoxPassword.Text, TxtBoxConfirmPassword.Text))
            {
                TxtBlckErrPass.Text = "Пароли не совпадают";
            }
            else
            {
                TxtBlckErrPass.Text = "";
            }
        }
    }
}
