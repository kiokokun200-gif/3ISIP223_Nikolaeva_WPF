using _3ISIP223_Nikolaeva_WPF.Models;
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
    /// Логика взаимодействия для WindowLogIn.xaml
    /// </summary>
    public partial class WindowLogIn : Window
    {
        _1PageMain _pageMain;
        public WindowLogIn( _1PageMain page)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            _pageMain = page;
        }

        private void BtnClientLogLog_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxLogNumber.Text) || string.IsNullOrEmpty(TxtBoxLogPassword.Password))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (Login(TxtBoxLogNumber.Text, TxtBoxLogPassword.Password))
                {
                    MessageBox.Show($"Вход успешный! роль {UserData.CurrentUser.Role.Name}");
                    _pageMain.UpdateAccount();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Пользователь с такими данными не найден");
                }

            }
        }

        private bool Login(string number, string password)
        {
            var user = Core.Context.User.FirstOrDefault(u => u.PhoneNumber == number && u.Password == password);

            if (user != null && !user.IsFrozen)
            {
                user.Role = Core.Context.Role.FirstOrDefault(r => r.ID == user.RoleID);
                UserData.CurrentUser = user;
                var cart = Core.Context.Cart.FirstOrDefault(u => u.UserID == user.ID);
                if (cart != null) {
                    UserData.UserCart = cart;
                }
                
                return true;
            }
            else { return false; }
        }

        private void BtnClientRegReg_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(TxtBoxRegFName.Text) || string.IsNullOrEmpty(TxtBoxRegLName.Text) || string.IsNullOrEmpty(TxtBoxRegMName.Text) || string.IsNullOrEmpty(TxtBoxRegNumber.Text) || string.IsNullOrEmpty(TxtBoxRegPassword.Password))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                if (Registration(TxtBoxRegFName.Text, TxtBoxRegLName.Text, TxtBoxRegMName.Text, TxtBoxRegNumber.Text, TxtBoxRegPassword.Password))
                {
                    MessageBox.Show("Успешный вход!");
                    
                    this.Close();


                }
                else MessageBox.Show("Ошибка регистрации");

            }
        }

        private bool Registration(string fname, string lname, string mname, string number, string password)
        {
            try
            {
                var reguser = new User()
                {
                    FirstName = fname,
                    LastName = lname,
                    MiddleName = mname,
                    PhoneNumber = number,
                    RoleID = 1,
                    Password = password,
                    IsFrozen = false

                };
                Core.Context.User.Add(reguser);
                Core.Context.SaveChanges();
                Login(number, password);
                //UserData.CurrentUser = ;
                return true;
            }

            catch
            {
                return false;
            }
        }

        private void TxtBoxLogNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);

        }


        private void TxtBoxRegNumber_TextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }


    }
}
