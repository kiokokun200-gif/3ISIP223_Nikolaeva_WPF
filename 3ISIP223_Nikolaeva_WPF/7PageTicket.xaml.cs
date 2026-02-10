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
using _3ISIP223_Nikolaeva_WPF.Pages;

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
            string total = _selectedseats.Sum(s => s.Price).ToString();
            TxtBlkTotal.Text = $"Итого: {total} P";
        }


        private void BtnRegTicket_Click(object sender, RoutedEventArgs e)
        {
            foreach(var seat in _selectedseats)
            {
                try
                {
                    var ticket = new Ticket
                    {
                        User_ID = UserData.CurrentUser.ID,
                        Seans_ID = _seans.ID,
                        RowNumber = seat.RowNumber,
                        SeatNumber = seat.SeatNumber,
                        Price = seat.Price
                    };

                    Core.Context.Ticket.Add(ticket);
                }
                catch (Exception ex) { 
                MessageBox.Show(ex.Message);
                }
            }
            Core.Context.SaveChanges();
            MessageBox.Show("Билет успешно оформлен");
            NavigationService.Navigate(new _1Page());
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
