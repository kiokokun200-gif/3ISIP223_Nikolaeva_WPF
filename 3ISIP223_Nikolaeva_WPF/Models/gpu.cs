using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class gpu
    {
        public string FullDescription
        {
            get
            {
                return $"Интерфейс: {gpuinterface.name} \nЧастота видеочипа: {chipfrequency} МГц \nВидеопамять: {videomemory} ГБ \nШина памяти: {memorybus}";
            }
        }
        public string Description
        {
            get
            {
                return $"{gpuinterface.name}, GPU {chipfrequency} МГц, {videomemory} ГБ, ";
            }
        }
    }
}
