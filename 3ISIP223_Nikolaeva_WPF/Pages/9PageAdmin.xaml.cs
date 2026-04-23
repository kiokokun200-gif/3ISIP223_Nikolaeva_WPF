using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class _9PageAdmin : Page
    {
        private List<User> _users;

        public _9PageAdmin()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _users = Core.Context.User.ToList();
            ListBoxUsers.ItemsSource = _users;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddUser();
            wind.ShowDialog();
            LoadData();
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User selectedUser = btn.Tag as User;

            if (selectedUser != null)
            {
                var wind = new WindowChangeUserRole(selectedUser);
                wind.ShowDialog();
                LoadData();
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User selectedUser = btn.Tag as User;

            if (selectedUser != null)
            {
                if (selectedUser.ID == UserData.CurrentUser.ID)
                {
                    MessageBox.Show("Нельзя удалить самого себя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                MessageBoxResult result = MessageBox.Show($"Удалить пользователя {selectedUser.FirstName} {selectedUser.LastName}?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Core.Context.User.Remove(selectedUser);
                        Core.Context.SaveChanges();
                        LoadData();
                        MessageBox.Show("Пользователь удалён");
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка удаления. Возможно, у пользователя есть записи или заказы.");
                    }
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnFroze_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User selectedUser = btn.DataContext as User;

            selectedUser.IsFrozen = !selectedUser.IsFrozen;
            Core.Context.SaveChanges();
            LoadData();

            string message = selectedUser.IsFrozen ? "Пользователь заморожен" : "Пользователь разморожен";
            MessageBox.Show(message);
        }
    }
}