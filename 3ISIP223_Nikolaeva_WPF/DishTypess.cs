using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class DishTypes
    {
        public List<string> DishesListNames
        {
            get
            {
                var l = Dishes.Select(d => d.Name).ToList();
                l.Insert(0, "Не выбрано");
                return l;
            }
        }

        public Dishes selectedDish { get; set; }
    }
}
