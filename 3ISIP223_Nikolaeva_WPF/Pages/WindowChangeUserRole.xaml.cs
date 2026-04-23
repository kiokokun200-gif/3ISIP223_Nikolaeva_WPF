using System;
using System.Linq;
using System.Windows;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class WindowChangeUserRole : Window
    {
        private User _user;
        private User _currentAdmin;

        public WindowChangeUserRole(User user)
        {
            InitializeComponent();
            _user = user;
            _currentAdmin = UserData.CurrentUser;
            LoadUserData();
            LoadRoles();
        }

        private void LoadUserData()
        {
            TxtBoxFirstName.Text = _user.FirstName;
            TxtBoxLastName.Text = _user.LastName;
            TxtBoxMiddleName.Text = _user.MiddleName;
            TxtBoxPhone.Text = _user.PhoneNumber;
            TxtBoxPassword.Text = _user.Password;
        }

        private void LoadRoles()
        {
            var roles = Core.Context.Role.ToList();

            // Если редактируемый пользователь - админ, убираем возможность менять роль
            if (_user.Role.Name == "Администратор")
            {
                ComboBoxRoles.ItemsSource = roles.Where(r => r.Name == "Администратор").ToList();
                ComboBoxRoles.IsEnabled = false;
                TextBlockWarning.Visibility = Visibility.Visible;
                TextBlockWarning.Text = "⚠ Нельзя изменить роль администратора";
            }
            else
            {
                ComboBoxRoles.ItemsSource = roles;
                ComboBoxRoles.IsEnabled = true;
            }

            ComboBoxRoles.DisplayMemberPath = "Name";

            var currentRole = roles.FirstOrDefault(r => r.ID == _user.RoleID);
            ComboBoxRoles.SelectedItem = currentRole;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (string.IsNullOrEmpty(TxtBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            if (string.IsNullOrEmpty(TxtBoxPassword.Text))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            Role selectedRole = ComboBoxRoles.SelectedItem as Role;
            if (selectedRole == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }

            try
            {
                _user.FirstName = TxtBoxFirstName.Text;
                _user.LastName = TxtBoxLastName.Text;
                _user.MiddleName = TxtBoxMiddleName.Text;
                _user.PhoneNumber = TxtBoxPhone.Text;
                _user.Password = TxtBoxPassword.Text;
                _user.RoleID = selectedRole.ID;

                Core.Context.SaveChanges();

                MessageBox.Show("Данные сохранены!");
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