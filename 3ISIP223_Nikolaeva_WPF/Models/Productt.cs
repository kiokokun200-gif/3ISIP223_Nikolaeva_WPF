using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    public partial class Product
    {
        public bool HasDiscountMore15 => Discount > 15;
        public decimal PriceWithDiscount => Cost - (Cost * (decimal)(Discount / 100));
        public bool HasDiscount => Discount > 0;
        public Visibility DiscountVisibility => Discount > 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}
