using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class DishTypes
    {
        public List<Dishes> DishesListNames
        {
            get
            {
                List<Dishes> l = Dishes.ToList();
                l.Insert(0, new Dishes() {
                    Id = 0,
                    Name = "Не выбрано"
                });
                return l;
            }
        }

        public Dishes selectedDish { get; set; }
    }
}
