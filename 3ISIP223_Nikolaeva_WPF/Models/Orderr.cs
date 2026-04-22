using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    public partial class Order
    {
        public Visibility VisIsClosed => IsClosed == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}
