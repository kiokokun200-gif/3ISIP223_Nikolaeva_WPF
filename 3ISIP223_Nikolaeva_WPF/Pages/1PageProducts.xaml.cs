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

            List<Cart>cart = 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        
        { 
            Button button = (Button)sender;
            Products selectedProd = (Products)button.DataContext;

            if (ProductsListBox.SelectedItem != null)
            {
                Products products = ProductsListBox.SelectedItem as Products;

                if (Cart.CartProducts.FirstOrDefault(c => c.ID_Product == selectedProd.ID_Product) == null)
                    Cart.CartProducts.Add(ProductsListBox.SelectedItem as Products);
                else Cart.CartProducts
            }
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
