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
    /// Логика взаимодействия для _1Page.xaml
    /// </summary>
    public partial class _1Page : Page
    {
        private List<Film> _allFilms;
        private bool sortaya = true;
        private bool sortrating21 = true;

        public _1Page()
        {
            InitializeComponent();
            PageLoad();
            //List<Film> films = Core.Context.Film.ToList();
            //FilmListBox.ItemsSource = films;
        }

        private void PageLoad()
        {
            _allFilms = Core.Context.Film.ToList();
            FilmListBox.ItemsSource = _allFilms;
            if(sortaya) BtnSortName.Content = "Сортировка по названию ↓";
            else BtnSortName.Content = "Сортировка по названию ↑";

            if (sortrating21) BtnSortRating.Content = "Сортировка по рейтингу ↓";
            else BtnSortRating.Content = "Сортировка по рейтингу ↑";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
            
        {
            if(sortaya) { 
            
            var sortedFilms = _allFilms.OrderBy(f => f.Name).ToList();
            FilmListBox.ItemsSource = sortedFilms;
                sortaya = false;
                BtnSortName.Content = "Сортировка по названию ↑";
            }
            else
            {
                var sortedFilms = _allFilms.OrderByDescending(f => f.Name).ToList();
                FilmListBox.ItemsSource = sortedFilms;
                sortaya = true;
                BtnSortName.Content = "Сортировка по названию ↓";
            }
        }

        private void BtnSortRating_Click(object sender, RoutedEventArgs e)
        {
            if (sortrating21)
            {
                var sortedFilms = _allFilms.OrderByDescending(f => f.Rating).ToList();
                FilmListBox.ItemsSource = sortedFilms;
                sortrating21 = false;
                BtnSortRating.Content = "Сортировка по рейтингу ↑";
            }

            else {
                var sortedFilms = _allFilms.OrderBy(f => f.Rating).ToList();
                FilmListBox.ItemsSource = sortedFilms;
                sortrating21 = true;
                BtnSortRating.Content = "Сортировка по рейтингу ↓";
            }
        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Film> films = Core.Context.Film.ToList();
            var search = TxtBoxSearch.Text.ToLower();
            films = films.Where(p => p.Name.ToLower().Contains(TxtBoxSearch.Text.ToLower())).ToList();
            FilmListBox.ItemsSource = films;
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _2PageProfile());
        }
    }
}
