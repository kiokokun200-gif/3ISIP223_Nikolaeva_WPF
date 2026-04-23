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
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class _8PageMaster : Page
    {
        private User _master;
        private List<UserService> _myAppointments;
        private List<MasterService> _myServices;

        public _8PageMaster(User master)
        {
            InitializeComponent();
            _master = master;
            DataContext = _master;
            LoadData();
        }

        private void LoadData()
        {
            _myAppointments = Core.Context.UserService
                .Where(u => u.MasterID == _master.ID)
                .ToList();
            ListBoxAppointments.ItemsSource = _myAppointments;

            _myServices = Core.Context.MasterService
                .Where(m => m.MasterID == _master.ID)
                .ToList();
            ListBoxServices.ItemsSource = _myServices;
        }

        private void BtnCompleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            UserService appointment = btn.Tag as UserService;

            if (appointment != null)
            {
                MessageBoxResult result = MessageBox.Show($"Завершить запись клиента {appointment.User.FirstName}?",
                    "Подтверждение", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    appointment.Status = "Completed";

                    var schedule = Core.Context.Schedule.FirstOrDefault(s => s.ID == appointment.ID_Schedule);
                    if (schedule != null)
                    {
                        schedule.IsAvailable = true;
                    }

                    Core.Context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Запись завершена!");
                }
            }
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddMasterService(_master);
            wind.ShowDialog();
            LoadData();
        }

        private void BtnRemoveService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            MasterService service = btn.Tag as MasterService;

            if (service != null)
            {
                MessageBoxResult result = MessageBox.Show($"Удалить услугу {service.ServCategory.Name}?",
                    "Подтверждение", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    Core.Context.MasterService.Remove(service);
                    Core.Context.SaveChanges();
                    LoadData();
                }
            }
        }

       
    }
}