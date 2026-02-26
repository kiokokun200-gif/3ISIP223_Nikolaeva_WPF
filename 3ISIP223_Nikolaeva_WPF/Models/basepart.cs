using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3ISIP223_Nikolaeva_WPF.Pages;

namespace _3ISIP223_Nikolaeva_WPF
{
    public partial class basepart
    {
        public string getdes
        {
            get
            {
                return GetDescription(_2PartTypePage.part.basepart);
            }
        }

        public string GetDescription(basepart part)
        {
            if (part.parttype.name == "CPU")
            {
                return $"Сокет: {part.cpu.socket.name}, Количество ядер:  {part.cpu.numberofcores}, " +
                    $"Частота производительных ядер: {part.cpu.basecorefrequency} ГГц - {part.cpu.maxcorefrequency} ГГц, " +
                    $" Кэш L3: {part.cpu.cachel3}, Графика {part.cpu.igpu.name}, Тепловыделение: {part.cpu.thermalpower} Вт";
            }


            else if (part.parttype.name == "GPU")
            {
                var videoconnectorslist = Core.Context.videoconnectorgpu.Where(p => p.gpuid == part.gpu.id).Select(v => v.videoconnector.name).ToList();
                string videoconnectors = "";
                for (int i = 0; i < videoconnectorslist.Count; i++)
                {
                    if (i == videoconnectorslist.Count - 1) videoconnectors += videoconnectors[i];
                    videoconnectors += videoconnectors[i] + ", ";
                }
                return $"{part.gpu.gpuinterface.name}, GPU {part.gpu.chipfrequency} МГц, {part.gpu.videomemory} ГБ, {part.gpu.memorybus} бит, {videoconnectors}";
            }


            else if (part.parttype.name == "RAM")
            {
                return $"{part.ram.memorytype.name}, {part.ram.capacity} ГБ × {part.ram.count} шт, {part.ram.ghz} МГц, тайминги {part.ram.timings}";
            }


            else if (part.parttype.name == "Motherboard")
            {
                return $"{part.motherboard.socket.name}, {part.motherboard.formfactor.name}, {part.motherboard.memoryslots}x{part.motherboard.memorytype.name}, " +
                    $"{part.motherboard.pcislots}xPCI-E, {part.motherboard.sataports}xSATA, {part.motherboard.usbports}xUSB";
            }


            else if (part.parttype.name == "Case")
            {
                var forms = Core.Context.boardformfactorcase.Where(b => b.caseid == part.@case.id).Select(f => f.formfactor.name).ToList();
                string formfactors = "";
                for (int i = 0; i < forms.Count; i++)
                {
                    if (i == forms.Count - 1) formfactors += forms[i];
                    else formfactors += forms[i] + ", ";
                }
                return $"{part.@case.casesize.name}, {formfactors}, слоты: {part.@case.expansionslots}, вентиляторы: {part.@case.fans}";
            }


            else if (part.parttype.name == "PowerSupply")
            {
                return $"{part.powersupply.power} Вт, {part.powersupply.fandimension.name} mm, {part.powersupply.certificate.name}";
            }


            else if (part.parttype.name == "ProcessorCooler")
            {
                var partsockets = Core.Context.socketprocessorcooler.Where(s => s.processorcoolerid == part.processorcooler.id).ToList();
                string sockets = "";
                for (int i = 0; i < partsockets.Count; i++)
                {
                    if (i == partsockets.Count - 1) sockets += sockets[i];
                    else sockets += sockets[i] + ", ";
                }

                return $"{sockets}, {part.processorcooler.fandimension.name}, теплотрубки: {part.processorcooler.heatpipes}, " +
                    $"{part.processorcooler.minspeed}-{part.processorcooler.maxspeed} об/мин, {part.processorcooler.noiselevel} дБ";
            }


            else {
                string res = "";

                if (part.storagedevice.storagedevicetype.name == "SSD") res = res + "SSD, " + part.storagedevice.ssd.tbw + "ТБ, ";
                else res += "HDD" + part.storagedevice.hdd.rotationspeed + " об/мин, ";
                res += part.storagedevice.capacity + "ГБ, " + part.storagedevice.storagedeviceinterface.name;
                return res;
            }
        }


        
    }
}
