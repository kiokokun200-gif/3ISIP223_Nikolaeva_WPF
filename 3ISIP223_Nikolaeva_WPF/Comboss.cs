using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class Combos
    {
        public string FullDescription { get
            {
                return string.Join("\n", ComboDishes.Select(c => c.Dishes.Name));
            }

            
        } 

    }
}
