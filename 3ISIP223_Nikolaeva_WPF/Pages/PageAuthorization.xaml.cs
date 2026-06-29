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
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        public PageAuthorization()
        {
            InitializeComponent();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtboxemail.Text) || string.IsNullOrWhiteSpace(txtboxname.Text) || string.IsNullOrWhiteSpace(passwordbox.Password))
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                User newUser = new User()
                {
                    FullName = txtboxname.Text,
                    Login = txtboxemail.Text,
                    Password = passwordbox.Password,
                };
                Core.Context.User.Add(newUser);
                Core.Context.SaveChanges();
                MessageBox.Show("Успешно");
                NavigationService.Navigate(new PageMain());
            }
        }

        private void BtnAuthoriz_Click(object sender, RoutedEventArgs e)
        {
            var user = Core.Context.User.FirstOrDefault(u => u.Login == txtboxemailau.Text && u.Password == passwordboxau.Password);
            if (user == null)
            {
                MessageBox.Show("Пользователь с такими данными не найден");

            }
            else { 
                MessageBox.Show("Успешный вход");
                NavigationService.Navigate(new PageMain());
            }
        }
    }
}
