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
        public _2PageCatalog()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _books = Core.Context.Book.Where(b => !b.IsFrozen).ToList();
            ListBoxBooks.ItemsSource = _books;
            ComboBoxSort.ItemsSource = BookFiltration.SortOptions;
            ComboBoxSort.SelectedIndex = 0;
            var genres = BookFiltration.GenreOptions.Select(g => g.Name).ToList();
            genres.Insert(0, "Все");
            ComboBoxFiltrGenre.ItemsSource = genres;
            ComboBoxFiltrGenre.SelectedIndex = 0;


        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtBoxSearch.Text.Length > 0)
            {
                FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre.SelectedItem.ToString(), TxtBoxSearch.Text);
            }
            else FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre.SelectedItem.ToString(), "");
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
            if (UserData.CurrentUser.Role.Name == "Администратор" || !UserData.IsLoggedIn)
            {
                MessageBox.Show("Войдите в аккаунт под пользователем или автором");
                return;
            }

            else if (UserData.IsLoggedIn)
            {
                Button btn = (Button)sender;
                Book selectedBook = btn.DataContext as Book;
                var userbook = Core.Context.UserBook.FirstOrDefault(b => b.BookID == selectedBook.ID && b.UserID == UserData.CurrentUser.ID);
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

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            if (TxtBoxSearch.Text.Length > 0)
            {
                FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre?.SelectedItem.ToString(), TxtBoxSearch.Text);
            }
            else FiltrationSearch(ComboBoxSort.SelectedItem.ToString(), ComboBoxFiltrGenre.SelectedItem?.ToString(), "");

        }

        private void FiltrationSearch(string selectedSort, string selectedGenre, string search) 
        {

            ListBoxBooks.ItemsSource = BookFiltration.FiltrationSearch(_books, selectedSort, selectedGenre, search);
        }
        
        
    }
}
