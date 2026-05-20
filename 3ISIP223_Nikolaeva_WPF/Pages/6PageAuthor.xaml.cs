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
    /// Логика взаимодействия для _6PageAuthor.xaml
    /// </summary>
    public partial class _6PageAuthor : Page
    {
        private List<Book> _books;
        private List<Book> _frozenBooks;
        public _6PageAuthor()
        {
            InitializeComponent();
            DataContext = UserData.CurrentUser;
            LoadBooks();
            _frozenBooks = Core.Context.Book.Where(b => b.AuthorID == UserData.CurrentUser.ID && b.IsFrozen).ToList();
            ListBoxFrozenBooks.ItemsSource = _frozenBooks;

        }

        private void LoadBooks()
        {
            _books = Core.Context.Book.Where(b => b.AuthorID == UserData.CurrentUser.ID && !b.IsFrozen).ToList();
            ListBoxBooks.ItemsSource = _books;
        }

        private void BtnEditBook_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            Button btn = (Button)sender;
            Book book = btn.DataContext as Book;
            var wind = new WindowEditBook(book);
            wind.ShowDialog();
        }

        private void BtnAddNewBook_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            NavigationService.Navigate(new _7PageAddBook());
            
        }


        private void BtnDefrozeBook_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            Button btn = (Button)sender;
            Book book = btn.DataContext as Book;
            MessageBoxResult result = MessageBox.Show("Оспорить заморозку книги?", "Вопрос", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DefrostingRequest request = new DefrostingRequest() { 
                        UserID = UserData.CurrentUser.ID,
                        Date = DateTime.Now,
                        TargetTypeID = 1,
                        TargetID = book.ID,
                    };
                    Core.Context.DefrostingRequest.Add(request);
                    Core.Context.SaveChanges();
                    _frozenBooks = Core.Context.Book.Where(b => b.AuthorID == UserData.CurrentUser.ID && b.IsFrozen).ToList();
                    ListBoxFrozenBooks.ItemsSource = _frozenBooks;


                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
            else return;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }
    }
}
