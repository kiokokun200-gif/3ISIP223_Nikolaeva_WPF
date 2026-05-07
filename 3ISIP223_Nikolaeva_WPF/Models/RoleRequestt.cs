using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class RoleRequest
    {
        public Visibility VisIsConfirmed => IsConfirmed == null ? Visibility.Visible : Visibility.Collapsed;

        public Visibility VisIsClosed => IsConfirmed != null ? Visibility.Visible : Visibility.Collapsed;
    }
}
