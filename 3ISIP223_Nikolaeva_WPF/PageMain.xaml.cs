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

namespace _3ISIP223_Nikolaeva_WPF
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        private List<Conferences> _conferences;
        public PageMain()
        {
            InitializeComponent();
            _conferences = Core.Context.Conferences.ToList();
            Calendar.SelectedDate = DateTime.Now;
            UpdateList();
        }

        private void UpdateList()
        {
            ListBoxConferences.ItemsSource = _conferences.Where(c => c.ConferenceDate == Calendar.SelectedDate).ToList();

        }

        private void ListBoxConferences_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Conferences conf = ListBoxConferences.SelectedItem as Conferences;
            if (conf != null)
            {
                NavigationService.Navigate(new PageConference(conf));
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateList();
        }
    }
}
