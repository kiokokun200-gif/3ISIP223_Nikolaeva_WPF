using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 40, 10, 5, "/Images/Skeleton.png", "/Images/SkeletonAttack.png", EnemyType.Skeleton)
        {
            IgnoreDefense = true;
        }
    }
}
