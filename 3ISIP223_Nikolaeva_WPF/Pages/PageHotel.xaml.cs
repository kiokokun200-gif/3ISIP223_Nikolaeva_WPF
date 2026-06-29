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
    /// Логика взаимодействия для PageHotel.xaml
    /// </summary>
    public partial class PageHotel : Page
    {
        private Hotel _hotel;
        public PageHotel(Hotel hotel)
        {
            InitializeComponent();
            _hotel = hotel;
            DataContext = _hotel;
        }

        private void BtnAddReview_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowReview(_hotel);
            wind.ShowDialog();
        }
    }
}
