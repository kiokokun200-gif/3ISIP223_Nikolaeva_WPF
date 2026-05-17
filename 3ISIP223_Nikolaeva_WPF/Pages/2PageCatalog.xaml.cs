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
    /// Логика взаимодействия для _2PageCatalog.xaml
    /// </summary>
    public partial class _2PageCatalog : Page
    {
        private List<Book> _books;
        private List<string> _sort;
        private List<string> _filtr;
        private List<BookGenre> _bookGenres;
        public _2PageCatalog()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _books = Core.Context.Book.Where(b => !b.IsFrozen).ToList();
            ListBoxBooks.ItemsSource = _books;
           _sort = new List<string>()
           {
               "Все",
               "По названию", 
               "По оценке"
           };

            _filtr = Core.Context.Genre.Distinct().Select(f => f.Name).ToList();
            _filtr.Insert(0, "Все");
            _bookGenres = Core.Context.BookGenre.Where(b => !b.Book.IsFrozen).ToList();
            

        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TxtBoxSearch.Text.ToLower();
            ListBoxBooks.ItemsSource = _books.Where(b => b.Name.ToLower() == text || b.User.NickName.ToLower() == text);
        }

        private void ListBoxBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Book selectedBook = ListBoxBooks.SelectedItem as Book;
            if (selectedBook != null)
            {
                NavigationService.Navigate(new _3PageBook(selectedBook));
            }
        }

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Book selectedBook = btn.DataContext as Book;
            var wind = new WindowAddToList(selectedBook);
            wind.ShowDialog();
            NavigationService.Navigate(new _2PageCatalog());

        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedSort = ComboBoxSort.SelectedItem.ToString();
            string selectedGenre = ComboBoxFiltrGenre.SelectedItem.ToString();
            if (selectedSort == "Все" && selectedGenre == "Все")
            {
                ListBoxBooks.ItemsSource = _books;
            }
            else if(selectedSort == "По названию" && selectedGenre != "Все")
            {
                ListBoxBooks.ItemsSource = _books.Where(bu => bu.GenresString.Contains(selectedGenre)).OrderBy(b => b.Name).ToList();
            }
            else if (selectedSort == "По оценке" && selectedGenre != "Все")
            {
                ListBoxBooks.ItemsSource = _books.Where(bu => bu.GenresString.Contains(selectedGenre)).OrderByDescending(b => b.AvgRating).ToList();
            }
        }

        
        
    }
}
