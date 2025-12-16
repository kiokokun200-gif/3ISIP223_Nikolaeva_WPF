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
        public static double EnginePrice { get; set; }

        public static string Color { get; set; }
        public static double ColorPrice { get; set; }


        public static bool Option1 { get; set; } 
        public static bool Option2 { get; set; } 
        public static bool Option3 { get; set; } 
        public static bool Option4 { get; set; } 
        public static string Options { get; set; } 

        public static double OptionsPrice { get; set; } 
        public static double CarTotalPrice { get; set; } 

        public static double DownPaymentPercent { get; set; } 
        public static double LoanTerm { get; set; }
        public static double MountlyPayment { get; set; }

        public static string CustomerName { get; set; }
        public static string Phone { get; set; }
        public static string Email { get; set; }
    }
}
