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
    /// Логика взаимодействия для _5PageFilm.xaml
    /// </summary>
    
    public partial class _5PageFilm : Page
    {
        private Film _film;
        public _5PageFilm(Film film)
        {
            InitializeComponent();
            _film = film;
            DataContext = film;
            LoadPage();
        }
        private void LoadPage()
        {
            var seans = Core.Context.Seans.Where(s => s.Film_ID == _film.ID).ToList();
            SeansListBox.ItemsSource = seans;
            var genres = Core.Context.Film_Genre.Where(s => s.Film_ID == _film.ID).ToList();
            GenresListBox.ItemsSource = genres;
            //попробовать через цикл, просто взять пустую строку и туда вписывать жанры

        }
    }
}
