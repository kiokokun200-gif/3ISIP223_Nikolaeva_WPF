using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class Review
    {
        public string FulDescription { get
            {
                return $"Отзыв #{ID} от {User.NickName} {Date: dd.MM.yyyy} на книгу {Book.Name} \nОценка {Rating} \nТекст отзыва: {Text}";
            } }
        public Visibility VisIsAdmin => UserData.CurrentUser.RoleID == 3 ? Visibility.Visible : Visibility.Collapsed;
    }
}
