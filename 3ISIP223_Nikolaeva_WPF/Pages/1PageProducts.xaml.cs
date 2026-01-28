using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _1PageProducts.xaml
    /// </summary>
    public partial class _1PageProducts : Page
    {
        public _1PageProducts()
        {
            InitializeComponent();
            //DataContext = this;
            List<Products>products = Core.Context.Products.ToList();
            ProductsListBox.ItemsSource = products;

            BtnCart.IsEnabled = Cartlst.Cartlist.Any();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        
        { 
            Button button = (Button)sender;
            Products selectedProd = (Products)button.DataContext;

           
            Cart exist_products = Cartlst.Cartlist.FirstOrDefault(c => c.ID_Product == selectedProd.ID_Product);

            if (exist_products == null)
            {
                Cart Add_cart = new Cart(selectedProd.ID_Product, selectedProd.Name, selectedProd.Price, selectedProd.Image, 1);
                Cartlst.Cartlist.Add(Add_cart);
            }
            else exist_products.Quantity += 1;

            BtnCart.IsEnabled = Cartlst.Cartlist.Any();

        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _2PageCart());
        }

       
    }
}
