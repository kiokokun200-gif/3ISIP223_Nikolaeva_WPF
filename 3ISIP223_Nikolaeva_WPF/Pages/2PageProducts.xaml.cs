using _3ISIP223_Nikolaeva_WPF.Models;
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
    /// Логика взаимодействия для _2PageProducts.xaml
    /// </summary>
    public partial class _2PageProducts : Page
    {
        private List<Product> _products;
        private List<string> _categoties;
        private List<string> _manufacturers;
        public _2PageProducts()
        {
            InitializeComponent();
            _products = Core.Context.Product.ToList();
            ListBoxProducts.ItemsSource = _products;
            _categoties = Core.Context.ProdCategory.Select(p => p.Name).ToList();
            ComboBoxFiltrProdCat.ItemsSource = _categoties;
            _manufacturers = Core.Context.Manufacturer.Select(p => p.Name).ToList();
            ComboBoxFiltrProdMan.ItemsSource = _manufacturers;

        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListBoxProducts.ItemsSource = _products.Where(p => p.Name.ToLower().Contains(TxtBoxSearch.Text.ToLower())).ToList();

        }

        private void ComboBoxFiltrProdCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBoxFiltrProdMan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnSortRating_Click(object sender, RoutedEventArgs e)
        {
            ListBoxProducts.ItemsSource = _products.OrderBy(p => p.Rating).ToList();
        }

        private void CreateCart()
        {
            Cart cart = new Cart()
            {
                UserID = UserData.CurrentUser.ID,
                TotalAmount = 0,
            };
            Core.Context.Cart.Add(cart);
            Core.Context.SaveChanges();
        }
        private void BtnAddCart_Click(object sender, RoutedEventArgs e)
        {
            if(!UserData.IsLoggedIn)
            {
                MessageBox.Show("Нужна авторизация");
                return;
            }

            if (UserData.UserCart == null) {
                CreateCart();
            }



            Button btn = (Button)sender;
            Product selectedProduct =(Product)btn.DataContext;


        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            if (!UserData.IsLoggedIn)
            {
                MessageBox.Show("Нужна авторизация");
                return;
            }

            NavigationService.Navigate(new _3PageCart());

        }
    }
}
