using _3ISIP223_Nikolaeva_WPF.Models;
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
        private List<BookStatus> _bookStatuses;
        private List<UserBook> _userBooks;
        public _4PageBookList()
        {
            InitializeComponent();
            _bookStatuses = Core.Context.BookStatus.ToList();
            _bookStatuses.Insert(0, new BookStatus
            {
                Name = "Все"
            });
            ListBoxStatuses.ItemsSource = _bookStatuses;
            _userBooks = Core.Context.UserBook.Where(b => b.UserID == UserData.CurrentUser.ID).ToList();
            ListBoxBooks.ItemsSource = _userBooks;
        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TxtBoxSearch.Text.ToLower();
            ListBoxBooks.ItemsSource = _userBooks.Where(b => b.Book.Name.ToLower() == text || b.Book.User.NickName.ToLower() == text);
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = ComboBoxSort.SelectedItem.ToString();
            if (selectedItem == "Все")
            {
                ListBoxBooks.ItemsSource = _userBooks;
            }
            else if (selectedItem == "По названию")
            {
                ListBoxBooks.ItemsSource = _userBooks.OrderBy(b => b.Book.Name).ToList();
            }
            else
            {
                ListBoxBooks.ItemsSource = _userBooks.OrderByDescending(b => b.Book.AvgRating).ToList();
            }
        }

        private void BtnMoveBook_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Book book = button.DataContext as Book;
            var wind = new WindowMoveBook(book);
            wind.ShowDialog();

        }
    }
}
