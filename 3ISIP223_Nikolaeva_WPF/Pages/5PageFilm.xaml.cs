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
            var genres = Core.Context.Film_Genre.Where(s => s.Film_ID == _film.ID).Select(g => g.Genre.Name).ToList();
            string genresList = "";
            for(int i = 0; i < genres.Count; i++)
            {
                if(i == genres.Count - 1) { genresList+= genres[i]; }
                else genresList = genresList + genres[i] + ", ";

            }
            TxtBoxGenres.Text = $"Жанры: {genresList}";

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void BtnSeans_Click(object sender, RoutedEventArgs e)
        {
            if (UserData.IsLoggedIn)
            {
                Button btn = (Button)sender;

                var selectseans = (Seans)btn.DataContext;
                NavigationService.Navigate(new _6PageSeans(selectseans));
            }
            else MessageBox.Show("Войдите в аккаунт!!!");
        }
    }
}
