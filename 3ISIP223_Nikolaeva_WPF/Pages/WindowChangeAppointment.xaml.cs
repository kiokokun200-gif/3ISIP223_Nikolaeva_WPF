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
    public partial class WindowChangeAppointment : Window
    {
        private UserService _appointment;
        private Schedule _selectedSchedule;

        public WindowChangeAppointment(UserService appointment)
        {
            InitializeComponent();
            _appointment = appointment;
            LoadCurrentAppointment();
        }

        private void LoadCurrentAppointment()
        {
            var master = Core.Context.User.FirstOrDefault(u => u.ID == _appointment.MasterID);

            TxtBlockClient.Text = $"Клиент: {_appointment.User.FirstName} {_appointment.User.LastName}";
            TxtBlockService.Text = $"Услуга: {_appointment.Service.Name}";
            TxtBlockMaster.Text = $"Мастер: {master.FirstName} {master.LastName}";
            TxtBlockDate.Text = $"Дата: {_appointment.Date:dd.MM.yyyy HH:mm}";

            DatePickerNewDate.SelectedDate = _appointment.Date.Date;
        }

        private void DatePickerNewDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerNewDate.SelectedDate == null) return;

            DateTime selectedDate = DatePickerNewDate.SelectedDate.Value;

            // Получаем начало и конец выбранного дня
            DateTime startOfDay = selectedDate.Date;
            DateTime endOfDay = selectedDate.Date.AddDays(1);

            // Ищем свободные слоты на выбранную дату для того же мастера и услуги
            var slots = Core.Context.Schedule
                .Where(s => s.MasterID == _appointment.MasterID
                    && s.ServiceID == _appointment.ServiceID
                    && s.StartTime >= startOfDay
                    && s.StartTime < endOfDay
                    && s.IsAvailable == true)
                .ToList();

            ListBoxSlots.ItemsSource = slots;
        }


        private void BtnSlot_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            _selectedSchedule = btn.Tag as Schedule;

            if (_selectedSchedule != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Перенести запись на {_selectedSchedule.StartTime:HH:mm}?",
                    "Подтверждение", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Освобождаем старый слот
                        var oldSchedule = Core.Context.Schedule
                            .FirstOrDefault(s => s.ID == _appointment.ID_Schedule);
                        if (oldSchedule != null)
                        {
                            oldSchedule.IsAvailable = true;
                        }

                        // Обновляем запись
                        _appointment.Date = _selectedSchedule.StartTime;
                        _appointment.ID_Schedule = _selectedSchedule.ID;

                        // Занимаем новый слот
                        _selectedSchedule.IsAvailable = false;

                        Core.Context.SaveChanges();

                        MessageBox.Show("Запись перенесена!");
                        this.DialogResult = true;
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при переносе записи");
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
