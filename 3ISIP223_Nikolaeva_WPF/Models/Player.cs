using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    public class Player
    {
        public int PlayerHP { get; set; } = 100;
        public int MaxPlayerHP { get; set; } = 100;
        public Item CurrentWeapon { get; set; }
        public Item CurrentArmor { get; set; }
    }
}
