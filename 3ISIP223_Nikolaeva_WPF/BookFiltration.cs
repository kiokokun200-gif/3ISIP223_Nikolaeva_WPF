using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public static class BookFiltration
    {
        public static List<string> SortOptions = new List<string>
        {
            "Все",
            "По названию",
            "По оценке"
        };

        public static List<Genre> GenreOptions = Core.Context.Genre.Distinct().ToList();
        public static List<string> BookStatusesOptions = Core.Context.BookStatus.Select(s => s.Name).ToList();
        public static List<Book> FiltrationSearch(List<Book> books, string selectedSort, string selectedGenre, string search)
        {
            var filt = books.Where(b => b.Name.ToLower().Contains(search.ToLower()) ||
                                         b.User.NickName.ToLower().Contains(search.ToLower())).ToList();

            if (selectedSort != "Все")
            {
                if (selectedSort == "По названию")
                {
                    filt = filt.OrderBy(b => b.Name).ToList();
                }
                else if (selectedSort == "По оценке")
                {
                    filt = filt.OrderByDescending(b => b.AvgRating).ToList();
                }
            }

            if (selectedGenre != null && selectedGenre != "Все")
            {
                filt = filt.Where(b => b.GenresString != null && b.GenresString.Contains(selectedGenre)).ToList();
            }

            return filt;
        }
    }
}
