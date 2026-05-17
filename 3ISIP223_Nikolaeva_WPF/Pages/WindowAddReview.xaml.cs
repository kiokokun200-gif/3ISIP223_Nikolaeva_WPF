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
    /// Логика взаимодействия для WindowAddReview.xaml
    /// </summary>
    public partial class WindowAddReview : Window
    {
        private Book _book;
        public WindowAddReview(Book book)
        {
            InitializeComponent();
            _book = book;
            DataContext = _book;
            LoadDate();
        }

        private void LoadDate()
        {
            List<int> ratings = new List<int>();
            for(int i = 1; i <= 10; i++)
            {
                ratings.Add(i);
            }
            ComboBoxRating.ItemsSource = ratings;

        }

        private void BtnAddReview_Click(object sender, RoutedEventArgs e)
        {
            int rating = (int)ComboBoxRating.SelectedItem;
            string reviewText = TxtBoxTextReview.Text;
            try
            {
                Review review = new Review()
                {
                    Rating = rating,
                    Text = reviewText,
                    UserID = UserData.CurrentUser.ID,
                    BookID = _book.ID,
                    Date = DateTime.Now,
                    IsFrozen = false
                };
                Core.Context.Review.Add(review);
                Core.Context.SaveChanges();
                MessageBox.Show("Отзыв добавлен!");
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
            this.Close();
        }
    }
}
