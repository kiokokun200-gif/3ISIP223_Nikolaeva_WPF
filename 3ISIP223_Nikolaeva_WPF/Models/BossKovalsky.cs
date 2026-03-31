using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class BossKovalsky : Enemy
    {
        public BossKovalsky() : base("Ковальский", 100, 13, 7, "/Images/BossKovalsky.png", "/Images/BossKovalskyAttack.png", EnemyType.BossKovalsky)
        {
            IgnoreDefense = true;
        }
    }
}
