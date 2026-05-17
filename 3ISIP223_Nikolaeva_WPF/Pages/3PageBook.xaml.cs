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
    /// Логика взаимодействия для _3PageBook.xaml
    /// </summary>
    public partial class _3PageBook : Page
    {
        private Book _book;
        private List<ComplaintTargetType> _complaintTargetType;
        private List<Review> _reviews;
        public _3PageBook(Book book)
        {
            InitializeComponent();

            _book = book;
            DataContext = _book;
            _complaintTargetType = Core.Context.ComplaintTargetType.ToList();
            _reviews = Core.Context.Review.Where(b => b.BookID == _book.ID).ToList();
            ListBoxReview.ItemsSource = _reviews;
            if(UserData.CurrentUser.RoleID == 3)
            {
                BtnFrozeBook.Visibility = Visibility.Visible;
            }
        }

        private void BtnSeeContentBook_Click(object sender, RoutedEventArgs e)
        {
            TxtBlcSeeContent.Visibility = Visibility.Visible;

        }

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddToList(_book);
            wind.ShowDialog();
            NavigationService.Navigate(new _2PageCatalog());

        }

        private void BtnComplaintBook_Click(object sender, RoutedEventArgs e)
        {
            //if(!UserData.IsLoggedIn || UserData.CurrentUser.RoleID != 1)
            //{
            //    MessageBox.Show("Войдите в аккаунт под пользователем!");
            //    return;
            //}
            var targetType = _complaintTargetType.FirstOrDefault(t => t.Name == "Книга");
            var wind = new WindowComplaint(targetType, _book.ID, _book.Name);
            wind.ShowDialog();
        }

        private void BtnComplaintAuthor_Click(object sender, RoutedEventArgs e)
        {
            //if (!UserData.IsLoggedIn || UserData.CurrentUser.RoleID != 1)
            //{
            //    MessageBox.Show("Войдите в аккаунт под пользователем!");
            //    return;
            //}
            var targetType = _complaintTargetType.FirstOrDefault(t => t.Name == "Автор");
            var wind = new WindowComplaint(targetType, _book.AuthorID, _book.User.NickName);
            wind.ShowDialog();
           if ( wind.DialogResult == true ) {
                NavigationService.Navigate(new _2PageCatalog());

            }
        }

        private void BtnComplaintReview_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender; 
            Review review = button.DataContext as Review;

            var targetType = _complaintTargetType.FirstOrDefault(t => t.Name == "Отзыв");
            var wind = new WindowComplaint(targetType, _book.AuthorID, review.User.NickName);
            wind.ShowDialog();
            if (wind.DialogResult == true)
            {
                NavigationService.Navigate(new _2PageCatalog());

            }
        }

        private void BtnFrozeBook_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Заморозить книгу {_book.Name}", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _book.IsFrozen = true;
                Core.Context.SaveChanges();
                MessageBox.Show("Книга заморожена");

            }
            else return;
        }

        private void BtnAddReview_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddReview(_book);
            wind.ShowDialog();
        }

        private void BtnFrozeReview_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Review review = button.DataContext as Review;

            MessageBoxResult result = MessageBox.Show($"Заморозить отзыв {review.Text} от {review.User.NickName}?", "Вопрос", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                review.IsFrozen = true;
                Core.Context.SaveChanges();
            }
            else return;
        }
    }
}
