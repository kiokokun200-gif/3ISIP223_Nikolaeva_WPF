using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class BossArchmage : Enemy
    {
        public BossArchmage() : base("Архимаг C++", 45, 24, 2, EnemyType.BossArchmage)
        {
            FreezeChance = 0.25;
        }
    }
}
