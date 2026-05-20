using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class Book
    {
        public List<Genre> GenreNames
        {
            get
            {
                if (BookGenre == null || !BookGenre.Any())
                    return null;

                return BookGenre.Select(bg => bg.Genre).Where(name => name != null).ToList();
            }
        }
        public string GenresString => string.Join(", ", GenreNames.Select(g => g.Name));


        public Visibility IsDefrozingRequest => User.DefrostingRequest.Where(b => b.TargetID == ID && b.TargetTypeID == 1).ToList().Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        public Visibility IsNoDefrozingRequest => User.DefrostingRequest.Where(b => b.TargetID == ID && b.TargetTypeID == 1).ToList().Count > 0 ? Visibility.Visible : Visibility.Collapsed;

    }
}
