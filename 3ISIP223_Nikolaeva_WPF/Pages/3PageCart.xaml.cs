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
            //if(_cart == null)
            //{
            //    TxtBlockEmptyCart.Visibility = Visibility.Visible;
            //    ListBoxProductsInCart.Visibility = Visibility.Collapsed;
            //}
            
            var productsInCart = Core.Context.ProductInCart.Where(p => p.CartID == _cart.ID).ToList();
                
            for (int i = 0; i < productsInCart.Count; i++)
            {
                var product = new ProductInCartViewModel
                {
                    ProductID = productsInCart[i].ProductID,
                    ProductInCartID = productsInCart[i].ID,
                    Name = productsInCart[i].Product.Name,
                    Price = productsInCart[i].Product.Cost - (productsInCart[i].Product.Cost * (decimal)(productsInCart[i].Product.Discount / 100)),
                    Quantity = productsInCart[i].Quantity,
                    Image = productsInCart[i].Product.Image

                };
                _products.Add(product);
            }
                
            ListBoxProductsInCart.ItemsSource = _products;
            UpdateTotalQuantity();
            
        }

        private void UpdateProductQuantity(ProductInCartViewModel productInCart, int newQuantity)
        {
            var prod = Core.Context.ProductInCart.FirstOrDefault(p => p.ProductID == productInCart.ProductID);
            if (newQuantity >= 1)
            {
                if (prod != null)
                {
                    prod.Quantity = newQuantity;
                    Core.Context.SaveChanges();
                }
                productInCart.Quantity = newQuantity;
            }
            else
            {
                
                if(prod != null)
                {
                    Core.Context.ProductInCart.Remove(prod);
                    Core.Context.SaveChanges();
                }
                _products.Remove(productInCart);   
                
            }
            ListBoxProductsInCart.ItemsSource = null;
            ListBoxProductsInCart.ItemsSource = _products;
            UpdateTotalQuantity();
        }

        private void UpdateTotalQuantity()
        {
            
            int totalQuantity = _products.Sum(u => u.Quantity);
            decimal totalPrice = _products.Sum(u => u.Price * u.Quantity);
            _cart.TotalQuantity = totalQuantity;
            _cart.TotalAmount = totalPrice;
            UserData.UserCart.TotalQuantity = totalQuantity;
            UserData.UserCart.TotalAmount = totalPrice;
            TxtBlockCartQuantity.Text = totalQuantity.ToString();
            TxtBlcTotalPrice.Text = $"{totalPrice} Р";

            var cartBD = Core.Context.Cart.FirstOrDefault(c => c.ID == _cart.ID);
            if (cartBD != null)
            {
                cartBD.TotalQuantity = totalQuantity;
                cartBD.TotalAmount = totalPrice;
                Core.Context.SaveChanges();
            }

            if (totalQuantity == 0)
            {

                TxtBlockEmptyCart.Visibility = Visibility.Visible;
                ListBoxProductsInCart.Visibility = Visibility.Collapsed;
                StackTotalPrice.Visibility = Visibility.Collapsed;
            }
        }
        private void BtnMinusProd_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var selectedproduct = btn.DataContext as ProductInCartViewModel;

            UpdateProductQuantity(selectedproduct, selectedproduct.Quantity - 1);
        }

        private void BtnPlusProd_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var selectedproduct = btn.DataContext as ProductInCartViewModel;

            UpdateProductQuantity(selectedproduct, selectedproduct.Quantity +1);
        }

        private void BtnDeleteProd_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var selectedproduct = btn.DataContext as ProductInCartViewModel;

            UpdateProductQuantity(selectedproduct, 0);
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowOrder();
            wind.Show();
        }
    }
}
