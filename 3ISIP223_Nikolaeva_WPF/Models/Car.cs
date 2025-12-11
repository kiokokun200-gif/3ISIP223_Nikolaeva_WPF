using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF.Models
{
    public static class Car
    {
        public static string Model { get; set; }
        public static double ModelPrice { get; set; }
        public static string EngineType { get; set; }
        public static double EnginPrice { get; set; }
        public static string Color { get; set; }
        public static List<string> AdditionalOptions { get; set; }
        public static List<double> AdditionalOptionsPrice { get; set; }



    }
}
