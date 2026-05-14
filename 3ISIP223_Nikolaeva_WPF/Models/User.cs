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
        Visibility VisIsAdmin => Role.Name == "Админ" ? Visibility.Visible : Visibility.Collapsed;
    }
}
