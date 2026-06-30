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
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        private Users _user;
        private List<Conferences> _conferences;
        public PageUser(Users user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
            _conferences = Core.Context.Registrations.Where(u => u.UserId == _user.Id).Select(u => u.Conferences).ToList();
            ListBoxConferences.ItemsSource = _conferences;  
        }
    }
}
