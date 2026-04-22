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
    public partial class WindowAddClientAppointment : Window
    {
        private User _selectedClient;
        private User _selectedMaster;
        private Service _selectedService;
        private Schedule _selectedSchedule;

        public WindowAddClientAppointment()
        {
            InitializeComponent();
            LoadMasters();
        }

        private void LoadMasters()
        {
            var masters = Core.Context.User.Where(u => u.Role.Name == "Мастер").ToList();
            ComboBoxMasters.ItemsSource = masters;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = TxtBoxSearch.Text.ToLower();

            var clients = Core.Context.User
                .Where(u => u.Role.Name == "Клиент" &&
                    (u.LastName.ToLower().Contains(search) ||
                     u.FirstName.ToLower().Contains(search) ||
                     u.PhoneNumber.Contains(search)))
                .ToList();

            ListBoxClients.ItemsSource = clients;
        }

        private void ComboBoxMasters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedMaster = ComboBoxMasters.SelectedItem as User;
            if (_selectedMaster != null)
            {
                var services = Core.Context.MasterService
                    .Where(m => m.MasterID == _selectedMaster.ID)
                    .Select(m => m.ServCategory)
                    .ToList();
                ComboBoxServices.ItemsSource = services;
            }
        }

        private void ComboBoxServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServCategory selectedCategory = ComboBoxServices.SelectedItem as ServCategory;
            if (selectedCategory != null && _selectedMaster != null)
            {
                var service = Core.Context.Service
                    .FirstOrDefault(s => s.CategoryID == selectedCategory.ID);
                _selectedService = service;
            }
        }

        private void DatePickerDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerDate.SelectedDate == null || _selectedMaster == null || _selectedService == null) return;

            DateTime selectedDate = DatePickerDate.SelectedDate.Value;

            var slots = Core.Context.Schedule
                .Where(s => s.MasterID == _selectedMaster.ID
                    && s.ServiceID == _selectedService.ID
                    && s.StartTime.Date == selectedDate.Date
                    && s.IsAvailable == true)
                .ToList();

            ListBoxSlots.ItemsSource = slots;
        }

        private void BtnSlot_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            _selectedSchedule = btn.Tag as Schedule;
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
            catch
            {
                MessageBox.Show("Ошибка при записи");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
