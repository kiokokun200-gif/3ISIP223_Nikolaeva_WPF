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

    }
}
