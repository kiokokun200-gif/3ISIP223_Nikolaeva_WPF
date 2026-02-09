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
    /// Логика взаимодействия для _7PageTicket.xaml
    /// </summary>
    public partial class _7PageTicket : Page
    {
        private List<Seats> _selectedseats;
        private Seans _seans;
        public _7PageTicket(List<Seats> selectedseats, Seans seans)
        {
            InitializeComponent();
            _selectedseats = selectedseats;
            _seans = seans;

            StackSeans.DataContext = seans;
            TicketListBox.ItemsSource = _selectedseats.ToList();

         
        }
        
    }
}
