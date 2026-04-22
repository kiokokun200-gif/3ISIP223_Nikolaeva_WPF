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
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class WindowChangeUserRole : Window
    {
        private User _user;
        private Role _selectedRole;

        public WindowChangeUserRole(User user)
        {
            InitializeComponent();
            _user = user;
            TxtBlockUser.Text = $"{user.LastName} {user.FirstName} {user.MiddleName}";
            LoadRoles();
        }

        private void LoadRoles()
        {
            var roles = Core.Context.Role.ToList();
            ComboBoxRoles.ItemsSource = roles;
            ComboBoxRoles.DisplayMemberPath = "Name";

            // Выбираем текущую роль пользователя
            var currentRole = roles.FirstOrDefault(r => r.ID == _user.RoleID);
            ComboBoxRoles.SelectedItem = currentRole;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _selectedRole = ComboBoxRoles.SelectedItem as Role;

            if (_selectedRole == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }

            try
            {
                _user.RoleID = _selectedRole.ID;
                Core.Context.SaveChanges();

                MessageBox.Show("Роль изменена!");
                this.DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
