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
using System.Windows.Shapes;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowReview.xaml
    /// </summary>
    public partial class WindowReview : Window
    {
        //private List<int> ratings = new List<int>();
        private Hotel _hotel;
        private List<Category> _categories;
        private Review _review;
        public WindowReview(Hotel hotel)
        {
            InitializeComponent();
            _hotel = hotel;
            LoadDate();
        }

        private void LoadDate()
        {
            _categories = Core.Context.Category.ToList();
            ListBoxReviewCateg.ItemsSource = _categories;
        }

        private void BtnAddReview_Click(object sender, RoutedEventArgs e)
        {
            _review = new Review();
            //_review.TotalRating = 
        }
    }
}
