using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Nikolaeva_WPF
{
    public class Cart
    {
        
        //public static List<Products> CartProducts = new List<Products>();
        public int ID_Product { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public Cart(int ID, string prodname, decimal prodpri, string prodim, int quant)
        {
            ID_Product = ID;
            ProductName = prodname;
            ProductPrice = prodpri;
            ProductImage = prodim;
            Quantity = quant;
        }


    }
    public static class Cartlst
    {
        public static List<Cart> Cartlist = new List<Cart>();
    }

}
