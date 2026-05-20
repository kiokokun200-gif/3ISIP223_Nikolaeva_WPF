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
    /// Логика взаимодействия для WindowEditBook.xaml
    /// </summary>
    public partial class WindowEditBook : Window
    {
        private Book _book;
        private List<BookGenre> _bookGenres;
        public WindowEditBook(Book book)
        {
            InitializeComponent();
            _book = book;
            DataContext = _book;
            _bookGenres = _book.BookGenre.ToList();
            //_bookGenres = Core.Context.BookGenre.Where(b => b.BookID == _book.ID).ToList();
            //var genres = db.BookGenre
            //.Where(bg => bg.BookID == _currentBook.ID)
            //.Select(bg => bg.Genre.Name)
            //.ToList();
            //GenresTextBlock.Text = _bookGenres.Any() ? string.Join(", ", _bookGenres)
            //: "Жанры не выбраны";
            ListBoxGenres.ItemsSource = _bookGenres;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtBoxImagePath.Text))
                {
                    MessageBox.Show("Введите путь картинки");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(TxtBoxName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;

                }

                else if (string.IsNullOrWhiteSpace(TxtBoxDescription.Text))
                {
                    MessageBox.Show("Введите описание");
                    return;
                }

                else if (string.IsNullOrWhiteSpace(TxtBoxText.Text))
                {
                    MessageBox.Show("Введите текст книги");
                    return;
                }
                _book.CoverImage = TxtBoxImagePath.Text;
                _book.Name = TxtBoxName.Text;
                _book.Description = TxtBoxDescription.Text;
                _book.Text = TxtBoxText.Text;
                Core.Context.SaveChanges();
                MessageBox.Show("Изменения сохранены");
                this.Close();

            }
            catch 
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        private void BtnDeleteGenre_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            BookGenre bookGenre = btn.DataContext as BookGenre;
            Core.Context.BookGenre.Remove(bookGenre);
            Core.Context.SaveChanges();
            _bookGenres = _book.BookGenre.ToList();
            ListBoxGenres.ItemsSource = _bookGenres;
        }

        private void BtnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowAddGenre(_book);
            wind.ShowDialog();
            _bookGenres = _book.BookGenre.ToList();
            ListBoxGenres.ItemsSource = _bookGenres;
        }
    }
}
