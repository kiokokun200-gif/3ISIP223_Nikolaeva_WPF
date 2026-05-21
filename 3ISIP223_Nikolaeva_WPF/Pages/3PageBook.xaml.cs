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
        private bool IsShowText = false;
        public _3PageBook(Book book)
        {
            InitializeComponent();

            _book = book;
            DataContext = _book;
            _complaintTargetType = Core.Context.ComplaintTargetType.ToList();
            LoadReviews();
            if(UserData.CurrentUser.RoleID == 3)
            {
                BtnFrozeBook.Visibility = Visibility.Visible;
            }
        }
        private void LoadReviews()
        {
            _reviews = Core.Context.Review.Where(b => b.BookID == _book.ID && !b.IsFrozen).OrderByDescending(bo => bo.Date).ToList();
            ListBoxReview.ItemsSource = _reviews;
            _book = Core.Context.Book.FirstOrDefault(b => b.ID ==  _book.ID);
            DataContext = _book;
        }

        private void BtnSeeContentBook_Click(object sender, RoutedEventArgs e)
        {
            IsShowText = !IsShowText;
            if (IsShowText)
            {
                TxtBlcSeeContent.Visibility = Visibility.Visible;
                BtnSeeContentBook.Content = "Скрыть фрагмент ↑";
            }
            else
            {
                TxtBlcSeeContent.Visibility = Visibility.Collapsed;
                BtnSeeContentBook.Content = "Посмотреть фрагмент ↓";
            

        }
        }

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.CurrentUser.Role.Name == "Администратор" || !UserData.IsLoggedIn)
            {
                MessageBox.Show("Войдите в аккаунт под пользователем или автором");
                return;
            }
            else if(UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            else if (UserData.IsLoggedIn)
            {
                Button btn = (Button)sender;
                Book selectedBook = btn.DataContext as Book;
                
                var userbook = Core.Context.UserBook.FirstOrDefault(b => b.BookID == selectedBook.ID);
                if (userbook == null)
                {
                    var wind = new WindowAddToList(selectedBook);
                    wind.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show($"Книга {selectedBook.Name} уже в списке в статусе {userbook.BookStatus.Name}");
                }
                
            }
           

        }

        private void BtnComplaintBook_Click(object sender, RoutedEventArgs e)
        {
            if (!UserData.IsLoggedIn || UserData.CurrentUser.RoleID != 1)
            {
                MessageBox.Show("Войдите в аккаунт под пользователем!");
                return;
            }
            else if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            var targetType = _complaintTargetType.FirstOrDefault(t => t.Name == "Книга");
            var wind = new WindowComplaint(targetType, _book.ID, _book.Name);
            wind.ShowDialog();
        }

        private void BtnComplaintAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (!UserData.IsLoggedIn || UserData.CurrentUser.RoleID != 1)
            {
                MessageBox.Show("Войдите в аккаунт под пользователем!");
                return;
            }
            else if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            var targetType = _complaintTargetType.FirstOrDefault(t => t.Name == "Автор");
            var wind = new WindowComplaint(targetType, _book.AuthorID, _book.User.NickName);
            wind.ShowDialog();
           
        }

        private void BtnComplaintReview_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender; 
            Review review = button.DataContext as Review;

            var targetType = _complaintTargetType.FirstOrDefault(t => t.Name == "Отзыв");
            var wind = new WindowComplaint(targetType, _book.AuthorID, review.User.NickName);
            wind.ShowDialog();
           
        }

        private void BtnFrozeBook_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Заморозить книгу {_book.Name}", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _book.IsFrozen = true;
                Core.Context.SaveChanges();
                MessageBox.Show("Книга заморожена");
                NavigationService.Navigate(new _2PageCatalog());
            }
            else return;
        }

        private void BtnAddReview_Click(object sender, RoutedEventArgs e)
        {
            if (!UserData.IsLoggedIn || UserData.CurrentUser.RoleID != 1)
            {
                MessageBox.Show("Войдите в аккаунт под пользователем!");
                return;
            }
            else if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            var wind = new WindowAddReview(_book);
            wind.ShowDialog();
            LoadReviews();
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
                LoadReviews();
            }
            else return;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
