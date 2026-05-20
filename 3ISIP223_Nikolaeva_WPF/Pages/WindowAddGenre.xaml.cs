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
    /// Логика взаимодействия для WindowAddGenre.xaml
    /// </summary>
    public partial class WindowAddGenre : Window
    {
        
        private List<Genre> _allGenres;
        private List<Genre> _selectedGenres = new List<Genre>();
        private Book _book;
        public WindowAddGenre(Book book)
        {
            InitializeComponent();
            _book = book;

            DataContext = _book;
            LoadDate();
            
        }

        private void LoadDate()
        {
            var existingGenreIds = _book.BookGenre.Select(bg => bg.GenreID).ToList();
            _allGenres = Core.Context.Genre.ToList();
            var genresToShow = _allGenres.Where(g => !existingGenreIds.Contains(g.ID)).ToList();
            ListBoxGenres.ItemsSource = genresToShow;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedGenres.Count > 0) {
                try
                {
                    foreach(var genre in _selectedGenres)
                    {
                        BookGenre bookGenre = new BookGenre
                        {
                            GenreID = genre.ID,
                            BookID = _book.ID,

                        };
                        Core.Context.BookGenre.Add(bookGenre);
                        
                    }
                    Core.Context.SaveChanges();
                    MessageBox.Show("Жанры добавлены");
                    this.Close();
                }
                catch {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
            else {
                MessageBox.Show("Выберите жанры");
                return;

            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            Genre selectedGenre = check.DataContext as Genre;

            if (check.IsChecked == true)
            {
                _selectedGenres.Add(selectedGenre);
            }
            else _selectedGenres.Remove(selectedGenre);
        }
    }
}
