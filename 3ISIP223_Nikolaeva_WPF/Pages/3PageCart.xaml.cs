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
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _3PageCart.xaml
    /// </summary>
    public partial class _3PageCart : Page
    {
        private Cart _cart;
        private List<ProductInCartViewModel> _products = new List<ProductInCartViewModel>();
        public _3PageCart(Cart cart)
        {
            
            InitializeComponent();
            _cart = cart;
            LoadData();
            
        }

        private void LoadData()
        {
            if(_cart == null)
            {
                TxtBlockEmptyCart.Visibility = Visibility.Visible;
                ListBoxProductsInCart.Visibility = Visibility.Collapsed;
            }
            else
            {
                var productsInCart = Core.Context.ProductInCart.Where(p => p.CartID == _cart.ID).ToList();
                
                for (int i = 0; i < productsInCart.Count; i++)
                {
                    var product = new ProductInCartViewModel
                    {
                        ProductID = productsInCart[i].ProductID,
                        ProductInCartID = productsInCart[i].ID,
                        Name = productsInCart[i].Product.Name,
                        Price = productsInCart[i].Product.Cost,
                        Quantity = productsInCart[i].Quantity,
                        Image = productsInCart[i].Product.Image

                    };
                    _products.Add(product);
                }
            
                ListBoxProductsInCart.ItemsSource = _products;
            }
        }

        private void BtnMinusProd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnPlusProd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteProd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
