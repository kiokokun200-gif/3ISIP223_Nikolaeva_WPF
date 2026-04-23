using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    public partial class User
    {
        public string ContIsFrozen => IsFrozen == true ? "Разморозить" : "Заморозить";
    }
}
