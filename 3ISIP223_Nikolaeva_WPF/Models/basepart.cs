using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class basepart
    {
        public string GetDescription(basepart part)
        {
            string desc;
            if (part.parttype.name == "CPU")
            {
                return $"Сокет: {part.cpu.socket.name}, Количество ядер:  {part.cpu.numberofcores}, " +
                    $"Частота производительных ядер: {part.cpu.basecorefrequency} ГГц - {part.cpu.maxcorefrequency} ГГц, " +
                    $" Кэш L3: {part.cpu.cachel3}, Графика {part.cpu.igpu.name}, Тепловыделение: {part.cpu.thermalpower} Вт"; ;
            }
            else if (part.parttype.name == "GPU")
            {
                var videoconnectorslist = Core.Context.videoconnectorgpu.Where(p => p.gpuid == part.gpu.id).Select(v => v.videoconnector.name).ToList();
                string videoconnectors = "";
                for(int i = 0; i < videoconnectorslist.Count; i++)
                {
                    if(i == videoconnectorslist.Count - 1) videoconnectors += videoconnectors[i];
                    videoconnectors += videoconnectors[i] + ", ";
                }
                return $"{part.gpu.gpuinterface.name}, GPU {part.gpu.chipfrequency} МГц, {part.gpu.videomemory} ГБ, {part.gpu.memorybus} бит, {videoconnectors}";
            }
            else if (part.parttype.name == "RAM")
            {
                return $"{part.ram.memorytype.name}, {part.ram.capacity} ГБ × {part.ram.count} шт, {part.ram.ghz} МГц, тайминги {part.ram.timings}";
            }
            else if ( part.parttype.name == "Motherboard")
            {
                return $"";
            }
        }
    }
}
