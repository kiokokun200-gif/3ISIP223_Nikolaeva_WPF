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
        public Visibility VisIsAdmin => Role.Name == "Администратор" ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisIsUserAuthor => Role.Name == "Автор" || Role.Name == "Читатель" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisIsAuthor => Role.Name == "Автор" ? Visibility.Visible : Visibility.Collapsed;
        public Visibility VisIsFrozen => IsFrozen ? Visibility.Visible : Visibility.Collapsed;
    }
}
