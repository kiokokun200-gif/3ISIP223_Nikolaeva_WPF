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
    /// Логика взаимодействия для WindowAddToList.xaml
    /// </summary>
    public partial class WindowAddToList : Window
    {
        private Book _book;
        private List<BookStatus> _bookStatuses;
        private BookStatus _bookStatus;
        private bool IsRaidoCheked = false;
        public WindowAddToList(Book book)
        {
            InitializeComponent();
            _book = book;
            DataContext = _book;
            _bookStatuses = Core.Context.BookStatus.ToList();
            ListBoxStatuses.ItemsSource = _bookStatuses;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IsRaidoCheked = true;
            RadioButton radio = (RadioButton)sender;
            _bookStatus = (BookStatus)radio.DataContext;

        }

        private void BtnAddToList_Click(object sender, RoutedEventArgs e)
        {
            if(!UserData.IsLoggedIn)
            {
                MessageBox.Show("Войдите в аккаунт");
            }
            if(!IsRaidoCheked)
            {
                MessageBox.Show("Выберите статус");
                return;
            }
            else
            {
                UserBook userBook = new UserBook()
                {
                    BookID = _book.ID,
                    UserID = UserData.CurrentUser.ID,
                    BookStatusID = _bookStatus.ID,
                };
            }
        }
    }
}
