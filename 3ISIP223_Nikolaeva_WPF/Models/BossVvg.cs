using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class BossVvg : Enemy
    {
        public BossVvg() : base("ВВГ", 60, 18, 4, EnemyType.BossVvg)
        {
            CriticalChance = 0.3;
        }
        
    }
}
