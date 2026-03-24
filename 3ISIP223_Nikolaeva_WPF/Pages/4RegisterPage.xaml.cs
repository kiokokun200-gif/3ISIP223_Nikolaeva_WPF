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

        private bool CheckText()
        {
            bool CorrectName = !string.IsNullOrEmpty(TxtBoxName.Text) && TxtBoxName.Text.Length > 1;
            bool CorrectEmail = !string.IsNullOrEmpty(TxtBoxEmail.Text) && TxtBoxEmail.Text.Contains("@") && TxtBoxEmail.Text.Contains(".");
            bool CorrectPass = !string.IsNullOrEmpty(TxtBoxPassword.Text) && TxtBoxPassword.Text.Length > 6 && TxtBoxPassword.Text.Any(char.IsDigit);
            bool CorrectConfirmPass = !string.IsNullOrEmpty(TxtBoxConfirmPassword.Text) && TxtBoxConfirmPassword.Text.Length > 6 && TxtBoxConfirmPassword.Text.Any(char.IsDigit);

            bool SamePass = TxtBoxPassword.Text == TxtBoxConfirmPassword.Text;

            return CorrectName && CorrectEmail && CorrectPass && CorrectConfirmPass && SamePass;
        }

        //private void Register_TextChanged(object sender, TextChangedEventArgs e)
        //{
           
        //    BtnRegisterBD.IsEnabled = CheckText();


        //}

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {      
             NavigationService.Navigate(new _3PageLogin());
        }

     

       

    

        private void BtnRegisterBD_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;
            bool isRegistered = Registration(TxtBoxName.Text, TxtBoxEmail.Text, TxtBoxPassword.Text, out errorMessage);

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

        public bool Registration(string name, string email, string password, out string errorMessage)
        {
            if (!CheckText())
            {
                errorMessage = "Заполните все поля корректно";
                return false;
            }

            var existingUser = Core.Context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                errorMessage = "Пользователь с такой почтой уже существует!";
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

        private void TxtBoxConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBoxPassword.Text != TxtBoxConfirmPassword.Text)
            {
                TxtBlckErrPass.Text = "Пароли не совпадают";
            }
            else TxtBlckErrPass.Text = "";
        }

        private void TxtBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBoxPassword.Text))
            {
                TxtBlckErrPass1.Text = "Заполните поле";
            }
            else if (!TxtBoxPassword.Text.Any(char.IsDigit) || (TxtBoxPassword.Text.Length <= 6))
            {

                TxtBlckErrPass1.Text = "Пароль должен содержать цифры и больше 6 символов";

            }
            else TxtBlckErrPass1.Text = "";
        }

        private void TxtBoxName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxName.Text)) {
                TxtBlckErrName.Text = "Заполните поле";
            }
            else if (TxtBoxName.Text.Length <= 1) {
                TxtBlckErrName.Text = "Имя должно быть длиннее 1 символа";
            }
            else TxtBlckErrName.Text = "";
            
        }

        private void TxtBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBoxEmail.Text))
            {
                TxtBlckErrEmail.Text = "Заполните поле";
            }
            else if (!TxtBoxEmail.Text.Contains("@") || (!TxtBoxEmail.Text.Contains(".")))
            {

                TxtBlckErrEmail.Text = "Почта должна содержать @ и .";

            }
            else TxtBlckErrEmail.Text = "";
        }
    }
}
