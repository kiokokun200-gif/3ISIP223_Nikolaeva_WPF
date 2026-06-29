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
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        private List<Hotel> _hotels;
        public PageMain()
        {
            InitializeComponent();
            _hotels = Core.Context.Hotel.ToList();
            ListBoxHotels.ItemsSource = _hotels;
        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TxtBoxSearch.Text.ToLower();
            ListBoxHotels.ItemsSource = _hotels.Where(h => h.Name.ToLower().Contains(search));

        }

        private void ListBoxHotels_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Button button = (Button)sender;
            var hotel = ListBoxHotels.SelectedItem as Hotel;
            
            NavigationService.Navigate(new PageHotel(hotel));

        }
    }
}
