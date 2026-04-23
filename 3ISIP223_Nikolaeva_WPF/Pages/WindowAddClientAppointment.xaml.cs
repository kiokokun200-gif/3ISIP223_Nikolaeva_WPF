using _3ISIP223_Nikolaeva_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class WindowAddClientAppointment : Window
    {
        private User _selectedClient;
        private User _selectedMaster;
        private Service _selectedService;
        private Schedule _selectedSchedule;
        private List<Service> _allServices;

        public WindowAddClientAppointment()
        {
            InitializeComponent();
            LoadMasters();
            LoadAllClients();
            LoadAllServices();
        }

        private void LoadAllClients()
        {
            var clients = Core.Context.User.Where(u => u.Role.Name == "Клиент").ToList();
            ListBoxClients.ItemsSource = clients;
        }

        private void LoadAllServices()
        {
            _allServices = Core.Context.Service.ToList();
            ComboBoxServices.ItemsSource = _allServices;
            ComboBoxServices.DisplayMemberPath = "Name";
        }

        private void LoadMasters()
        {
            var masters = Core.Context.User.Where(u => u.Role.Name == "Мастер").ToList();
            ComboBoxMasters.ItemsSource = masters;
            ComboBoxMasters.DisplayMemberPath = "FirstName";
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TxtBoxSearch.Text.ToLower();

            var clients = Core.Context.User
                .Where(u => u.Role.Name == "Клиент" &&
                    (u.LastName.ToLower().Contains(search) ||
                     u.FirstName.ToLower().Contains(search) ||
                     u.MiddleName.ToLower().Contains(search) ||
                     u.PhoneNumber.Contains(search)))
                .ToList();

            ListBoxClients.ItemsSource = clients;

            if (clients.Count == 0)
            {
                MessageBox.Show("Клиенты не найдены");
            }
        }

        private void ComboBoxMasters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedMaster = ComboBoxMasters.SelectedItem as User;

            // Сбрасываем выбранную услугу
            ComboBoxServices.SelectedItem = null;
            _selectedService = null;
            ListBoxSlots.ItemsSource = null;
        }

        private void ComboBoxServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedService = ComboBoxServices.SelectedItem as Service;

            if (_selectedService != null && _selectedMaster != null)
            {
                // Проверяем, выполняет ли мастер эту услугу
                var masterService = Core.Context.MasterService
                    .FirstOrDefault(m => m.MasterID == _selectedMaster.ID && m.ServCategotyID == _selectedService.CategoryID);

                if (masterService == null)
                {
                    MessageBox.Show($"Мастер {_selectedMaster.FirstName} не выполняет услугу {_selectedService.Name}");
                    ComboBoxServices.SelectedItem = null;
                    _selectedService = null;
                    ListBoxSlots.ItemsSource = null;
                }
            }
        }

        private void DatePickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDate.SelectedDate == null || _selectedMaster == null || _selectedService == null) return;

            DateTime selectedDate = DatePickerDate.SelectedDate.Value;

            // Получаем начало и конец выбранного дня
            DateTime startOfDay = selectedDate.Date;
            DateTime endOfDay = selectedDate.Date.AddDays(1);

            var slots = Core.Context.Schedule
                .Where(s => s.MasterID == _selectedMaster.ID
                    && s.ServiceID == _selectedService.ID
                    && s.StartTime >= startOfDay
                    && s.StartTime < endOfDay
                    && s.IsAvailable == true)
                .ToList();

            ListBoxSlots.ItemsSource = slots;

            if (slots.Count == 0)
            {
                MessageBox.Show("На выбранную дату нет свободных слотов");
            }
        }

        private void BtnSlot_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            _selectedSchedule = btn.Tag as Schedule;

            // Визуально выделяем выбранный слот (опционально)
            foreach (var item in ListBoxSlots.Items)
            {
                var container = ListBoxSlots.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;
                if (container != null)
                {
                    var button = FindVisualChild<Button>(container);
                    if (button != null)
                    {
                        button.Background = System.Windows.Media.Brushes.LightGray;
                    }
                }
            }

            var selectedContainer = ListBoxSlots.ItemContainerGenerator.ContainerFromItem(_selectedSchedule) as ListBoxItem;
            if (selectedContainer != null)
            {
                var selectedButton = FindVisualChild<Button>(selectedContainer);
                if (selectedButton != null)
                {
                    selectedButton.Background = System.Windows.Media.Brushes.LightGreen;
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _selectedClient = ListBoxClients.SelectedItem as User;

            if (_selectedClient == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }

            if (_selectedMaster == null)
            {
                MessageBox.Show("Выберите мастера");
                return;
            }

            if (_selectedService == null)
            {
                MessageBox.Show("Выберите услугу");
                return;
            }

            if (_selectedSchedule == null)
            {
                MessageBox.Show("Выберите время");
                return;
            }

            try
            {
                UserService userService = new UserService()
                {
                    UserID = _selectedClient.ID,
                    MasterID = _selectedMaster.ID,
                    Date = _selectedSchedule.StartTime,
                    ServiceID = _selectedService.ID,
                    PaymentMethodID = 2,
                    Comment = "",
                    ID_Schedule = _selectedSchedule.ID,
                    Status = "Scheduled"
                };

                Core.Context.UserService.Add(userService);

                var schedule = Core.Context.Schedule.First(s => s.ID == _selectedSchedule.ID);
                schedule.IsAvailable = false;

                Core.Context.SaveChanges();

                MessageBox.Show("Клиент записан!");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при записи: {ex.Message}");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}