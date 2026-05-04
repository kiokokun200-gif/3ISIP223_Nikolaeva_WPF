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
        private Book _book;
        public WindowMoveBook(Book book)
        {
            InitializeComponent();
            _booksStatuses = Core.Context.BookStatus.ToList();
            ListBoxBookStatuses.ItemsSource = _booksStatuses;
            _book = book;

        }

        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
