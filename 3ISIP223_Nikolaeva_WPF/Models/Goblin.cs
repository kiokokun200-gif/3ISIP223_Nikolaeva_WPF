using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class Goblin : Enemy
    {
        public Goblin() : base("Гоблин", 30, 12, 3, "/Images/Goblin.png", "/Images/GoblinAttack.png", EnemyType.Goblin)
        {
            CriticalChance = 0.2;
        }
    }
}
