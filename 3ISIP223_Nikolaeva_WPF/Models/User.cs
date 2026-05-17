using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class User
    {
        public Visibility VisIsAdmin =>UserData.CurrentUser.Role.Name == "Админ" ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisIsUserAuthor => UserData.CurrentUser.Role.Name == "Автор" || UserData.CurrentUser.Role.Name == "Читатель" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisIsAuthor => UserData.CurrentUser.Role.Name == "Автор" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisIsFrozen => UserData.CurrentUser.IsFrozen ? Visibility.Visible : Visibility.Collapsed;
    }
}
