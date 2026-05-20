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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }
        private void BtnClientLogLog_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxLogLogin.Text) || string.IsNullOrEmpty(TxtBoxLogPassword.Password))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (Login(TxtBoxLogLogin.Text, TxtBoxLogPassword.Password))
                {
                    MessageBox.Show($"Вход успешный!");
                    NavigationService.Navigate(new _1PageMain());

                }
                else
                {
                    MessageBox.Show("Пользователь с такими данными не найден");
                }

            }
        }
        /// <summary>
        /// Выполняет авторизацию пользователя в системе
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>true - если пользователь найден, false - если не найден</returns>
        private bool Login(string login, string password)
        {
            var user = Core.Context.User.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null )
            {
                user.Role = Core.Context.Role.FirstOrDefault(r => r.ID == user.RoleID);
                UserData.CurrentUser = user;
                

                return true;
            }
            else { return false; }
        }

        private void BtnClientRegReg_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxNickName.Text) || string.IsNullOrEmpty(TxtBoxLogin.Text) || string.IsNullOrEmpty(TxtBoxEmail.Text)  || string.IsNullOrEmpty(TxtBoxRegPassword.Password))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (Registration(TxtBoxLogin.Text, TxtBoxNickName.Text, TxtBoxEmail.Text, TxtBoxRegPassword.Password))
                {
                    MessageBox.Show("Успешный вход!");
                    NavigationService.Navigate(new _1PageMain());

                }
                else MessageBox.Show("Ошибка регистрации");

            }
        }

        /// <summary>
        /// Выполняет регистрацию нового пользователя
        /// </summary>
        /// <param name="login">Логин нового пользователя</param>
        /// <param name="nickname">Отображаемое имя пользователя</param>
        /// <param name="email">Электронная почта пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>true - если регистрация успешна, false - если произошла ошибка</returns>
        private bool Registration(string login, string nickname, string email, string password)
        {
            try
            {
                var reguser = new User()
                {
                    Login = login,
                    NickName = nickname,
                    Email = email,
                    RoleID = 1,
                    Password = password,
                    IsFrozen = false

                };
                Core.Context.User.Add(reguser);
                Core.Context.SaveChanges();
                Login(login, password);
                return true;
            }

            catch
            {
                return false;
            }
        }

        
    }
}
