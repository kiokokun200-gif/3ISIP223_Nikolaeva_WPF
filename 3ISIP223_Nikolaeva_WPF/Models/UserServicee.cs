using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace _3ISIP223_Nikolaeva_WPF.Models
{
    public partial class UserService
    {
        public Visibility VisIsClosed => Status == "Scheduled" ? Visibility.Visible : Visibility.Collapsed;
    }
}
