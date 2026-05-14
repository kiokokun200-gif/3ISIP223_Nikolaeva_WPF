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
    /// Логика взаимодействия для WindowChangeRole.xaml
    /// </summary>
    public partial class WindowChangeRole : Window
    {
        private User _user;
        private List<Role> _roles;
        public WindowChangeRole(User user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
            LoadDate();
        }

        private void LoadDate()
        {
            _roles = Core.Context.Role.ToList();
            Role userRole = _roles.FirstOrDefault(r => r.ID == _user.RoleID);
            ComboRoles.ItemsSource= _roles.Select(r => r.Name);
            ComboRoles.SelectedItem = userRole.Name;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            User user = Core.Context.User.FirstOrDefault(u => u.ID == _user.ID);
            Role role = _roles.FirstOrDefault(r => r.Name == ComboRoles.SelectedItem.ToString());
            user.Role = role;
            Core.Context.SaveChanges();
            MessageBox.Show("Изменения сохранены");
            this.Close();
        }
    }
}
