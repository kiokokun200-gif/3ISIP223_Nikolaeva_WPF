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

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _2PageProfile.xaml
    /// </summary>
    public partial class _2PageProfile : Page
    {
        private Users _user = UserData.CurrentUser;
        private List<Ticket> _tickets;
        public _2PageProfile()
        {
            InitializeComponent();
            _tickets = Core.Context.Ticket.Where(t => t.User_ID == _user.ID).ToList();
            StackProfile.DataContext = _user;
            TicketKListBox.ItemsSource = _tickets;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
