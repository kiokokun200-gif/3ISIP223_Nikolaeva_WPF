using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    internal class FactoryBossKovalsky : Factory
    {
        public override Enemy CreateEnemy()
        {
            return new BossKovalsky();
        }

    }
}
