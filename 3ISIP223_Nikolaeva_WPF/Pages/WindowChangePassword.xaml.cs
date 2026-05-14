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

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowChangePassword.xaml
    /// </summary>
    public partial class WindowChangePassword : Window
    {
        private User _user;
        public WindowChangePassword(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            User us = Core.Context.User.FirstOrDefault(u => u.ID == _user.ID);
            us.Password = TxtBoxPasswoed.Text;
            Core.Context.SaveChanges();
            MessageBox.Show("Изменения сохранены");
            this.Close();
        }
    }
}
