
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
    /// Логика взаимодействия для _4PageBookList.xaml
    /// </summary>
    public partial class _4PageBookList : Page
    {
        private List<UserBook> _userBooks;
        private List<Book> _books;
        
        public _4PageBookList()
        {
            InitializeComponent();
            LoadDate();   
        }

        private void LoadDate()
        {
            _userBooks = Core.Context.UserBook.Where(b => b.UserID == UserData.CurrentUser.ID).ToList();
            _books = _userBooks.Select(u => u.Book).ToList();

            var statuses = BookFiltration.BookStatusesOptions.ToList();
            statuses.Insert(0, "Все");
            ComboStatuses.ItemsSource = statuses;
            ComboStatuses.SelectedIndex = 0;
            ComboBoxSort.ItemsSource = BookFiltration.SortOptions;
            ComboBoxSort.SelectedIndex = 0;
            var genres = BookFiltration.GenreOptions.Select(g => g.Name).ToList();
            genres.Insert(0, "Все");
            ComboBoxFiltrGenre.ItemsSource = genres;
            ComboBoxFiltrGenre.SelectedIndex = 0;
        }
        private void FiltrationSearch(string selectedSort, string selectedGenre, string search)
        {
            var books = Category(ComboStatuses.SelectedItem.ToString()).Select(b => b.Book).ToList();
            var filteredBooks = BookFiltration.FiltrationSearch(books, selectedSort, selectedGenre, search);
            var result = _userBooks.Where(ub => filteredBooks.Select(b => b.ID).Contains(ub.BookID)).ToList();
            ListBoxBooks.ItemsSource = result;
        }
        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtBoxSearch.Text.Length > 0)
            {

                FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre?.SelectedItem.ToString(), TxtBoxSearch.Text);
            }
            else FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre.SelectedItem?.ToString(), "");
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TxtBoxSearch.Text.Length > 0)
            {
                FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre?.SelectedItem.ToString(), TxtBoxSearch.Text);
            }
            else FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre.SelectedItem?.ToString(), "");
        }

        private void BtnMoveBook_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.CurrentUser.IsFrozen)
            {
                MessageBox.Show("Вы заморожены !");
                return;
            }
            Button button = sender as Button;
            UserBook userBook = button.DataContext as UserBook;
            Book book = userBook?.Book;

            if (book == null) return;
           
            var wind = new WindowMoveBook(book);
            wind.ShowDialog();
            LoadDate();
            FiltrationSearch(ComboBoxSort.SelectedItem?.ToString(), ComboBoxFiltrGenre.SelectedItem?.ToString(), TxtBoxSearch.Text);
        }

        



        private List<UserBook> Category(string bookStatus)
        {
            if (bookStatus != "Все")
            {
                return _userBooks.Where(b => b.BookStatus.Name == bookStatus).ToList();
            }
            else
            {
                return _userBooks;
            }
        }

        private void ComboStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string status = ComboStatuses.SelectedItem.ToString();
            FiltrationSearch(ComboBoxSort.SelectedItem?.ToString(), ComboBoxFiltrGenre.SelectedItem?.ToString(), TxtBoxSearch.Text);
        }
    }
}
