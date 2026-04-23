using System;
using System.Linq;
using System.Windows;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class WindowAddUser : Window
    {
        public WindowAddUser()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            var roles = Core.Context.Role.ToList();
            ComboBoxRoles.ItemsSource = roles;
            ComboBoxRoles.SelectedIndex = 0;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
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
                User newUser = new User
                {
                    FirstName = TxtBoxFirstName.Text,
                    LastName = TxtBoxLastName.Text,
                    MiddleName = TxtBoxMiddleName.Text,
                    PhoneNumber = TxtBoxPhone.Text,
                    Password = TxtBoxPassword.Text,
                    RoleID = selectedRole.ID
                };

                Core.Context.User.Add(newUser);
                Core.Context.SaveChanges();

                MessageBox.Show("Пользователь создан!");
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