using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class cpu
    {
        public string Desсription 
        {
            get
            {
                return $"Сокет: {socket.name}, Количество ядер:  {numberofcores}, " +
                    $"Частота производительных ядер: {basecorefrequency} ГГц - {maxcorefrequency} ГГц, " +
                    $" Кэш L3: {cachel3}, Графика {igpu.name}, Тепловыделение: {thermalpower} Вт";

            }
        }

        public string FullDescription
        {
            get
            {
                return $"{socket.name}, {numberofcores} × {basecorefrequency} ГГц, L3 - {cachel3} МБ, {igpu.name}, TDP {thermalpower} Вт";
            }
        }
    }
}
