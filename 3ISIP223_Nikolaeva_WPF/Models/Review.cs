using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class Review
    {
        public string FulDescription { get
            {
                return $"Отзыв #{ID} от {User.NickName} {Date: dd.MM.yyyy} на книгу {Book.Name} \nОценка {Rating} \nТекст отзыва: {Text}";
            } }
    }
}
