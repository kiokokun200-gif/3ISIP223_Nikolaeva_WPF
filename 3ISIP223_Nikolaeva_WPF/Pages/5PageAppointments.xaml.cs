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
    /// <summary>
    /// Логика взаимодействия для _5PageAppointments.xaml
    /// </summary>
    public partial class _5PageAppointments : Page
    {
        private User _master;
        private Service _service;
        private List<Schedule> _schedules;

        public _5PageAppointments(User master, Service service)
        {
            InitializeComponent();
            _master = master;
            _service = service;
            
            LoadData();

        }

        private void LoadData()
        {
            StackService.DataContext = _service;
            StackMaster.DataContext = _master;
            _schedules = Core.Context.Schedule.Where(s => s.IsAvailable == true && s.MasterID == _master.ID && s.ServiceID == _service.ID && s.StartTime == Calendar.SelectedDate).ToList();
            ListBoxAppointments.ItemsSource = _schedules;
        }

        private void BtnAppointment_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Schedule schedule = btn.DataContext as Schedule;
            if (schedule != null)
            {
                MessageBoxResult result = MessageBox.Show($"Хотите записаться на {schedule.Service.Name} в {schedule.StartTime}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Запись подтверждена!");
                    NavigationService.Navigate(new _1PageMain());
                }
                else return;
            }
        }

        private void Calendar_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ListBoxAppointments.ItemsSource = _schedules.Where(s => s.StartTime == Calendar.SelectedDate).ToList();
        }
    }
}
