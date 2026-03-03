using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    static class UserDataaa
    {
        static public List<basepart> userparts = new List<basepart>();
        static public decimal TotalAmount = 0;

        static public void Sort()
        {
            userparts = userparts.OrderBy(p => p.parttype.id).ToList();
        }
        
    }
}
