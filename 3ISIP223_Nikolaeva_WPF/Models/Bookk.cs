using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class Book
    {
        public List<string> GenreNames
        {
            get
            {
                if (BookGenre == null || !BookGenre.Any())
                    return new List<string>();

                return BookGenre.Select(bg => bg.Genre?.Name).Where(name => name != null).ToList();
            }
        }
        public string GenresString => string.Join(", ", GenreNames);
    }
}
