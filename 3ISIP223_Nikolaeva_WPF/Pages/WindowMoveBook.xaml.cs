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
    /// Логика взаимодействия для WindowMoveBook.xaml
    /// </summary>
    public partial class WindowMoveBook : Window
    {
        private List<BookStatus> _booksStatuses;
        private UserBook _userBook;
        private Book _book;
        public WindowMoveBook(Book book)
        {
            InitializeComponent();
            _book = book;
            LoadDate();
        }

        private void LoadDate()
        {
            _booksStatuses = Core.Context.BookStatus.ToList();
            ComboBoxStatuses.ItemsSource = _booksStatuses.Select(b => b.Name);
            _userBook = Core.Context.UserBook.FirstOrDefault(u => u.UserID == UserData.CurrentUser.ID && u.BookID == _book.ID);
            ComboBoxStatuses.SelectedItem = _userBook.BookStatus.Name;

        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            _userBook.BookStatus = ComboBoxStatuses.SelectedItem as BookStatus;
            Core.Context.SaveChanges();
            MessageBox.Show($"Книга {_book.Name} перемещена в {_userBook.BookStatus}");
            this.Close();
        }
    }
}
