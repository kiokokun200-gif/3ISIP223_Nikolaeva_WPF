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
        private List<string> _payments;
        private List<PaymentMethod> _paymentMethods;

        public _5PageAppointments(User master, Service service)
        {
            InitializeComponent();
            _master = master;
            _service = service;
            
            LoadData();

        }

        private void LoadData()
        {
            _paymentMethods = Core.Context.PaymentMethod.ToList();
            _payments = _paymentMethods.Select(p => p.Name).ToList();
            ComboBoxPaymentMethod.ItemsSource = _payments;
            StackService.DataContext = _service;
            StackMaster.DataContext = _master;
            _schedules = Core.Context.Schedule.Where(s => s.IsAvailable == true && s.MasterID == _master.ID && s.ServiceID == _service.ID ).ToList();
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
                    string pay = ComboBoxPaymentMethod.SelectedItem.ToString();
                    PaymentMethod paymentMethod = _paymentMethods.FirstOrDefault(s => s.Name == pay);
                    try
                    {

                    UserService userService = new UserService() {
                        UserID = UserData.CurrentUser.ID,
                        MasterID = _master.ID,
                        Date = schedule.StartTime,
                        ServiceID = _service.ID,
                        PaymentMethodID = paymentMethod.ID,
                        Comment = TxtBoxComment.Text,
                        ID_Schedule = schedule.ID,
                        
                    };

                    Core.Context.UserService.Add(userService);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Запись подтверждена!");
                    NavigationService.Navigate(new _1PageMain());

                    }
                    catch {
                        MessageBox.Show("Ошибка записи");
                    }
                }
                else return;
            }
        }

       

        private void Calendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxAppointments.ItemsSource = _schedules.Where(s => s.StartTime.Date == Calendar.SelectedDate).ToList();
        }

        private void ListBoxAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
