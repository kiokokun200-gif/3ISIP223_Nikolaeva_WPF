using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class Mage : Enemy
    {
        public Mage() : base("Маг", 25, 15, 2, "/Image/Mage.png", "/Image/MageAttack.png", EnemyType.Mage)
        {
            FreezeChance = 0.15;
        }
    }
}
