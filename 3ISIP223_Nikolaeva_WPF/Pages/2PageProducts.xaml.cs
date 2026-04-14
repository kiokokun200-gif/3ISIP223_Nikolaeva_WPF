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
        private bool IsFiltr = false;
        public _2PageProducts()
        {
            InitializeComponent();
            LoadPage();

        }

        private void LoadPage()
        {
            _products = Core.Context.Product.ToList();
            ListBoxProducts.ItemsSource = _products;
            _categoties = Core.Context.ProdCategory.Select(p => p.Name).ToList();
            _categoties.Insert(0, "Все");
            ComboBoxFiltrProdCat.ItemsSource = _categoties;
            _manufacturers = Core.Context.Manufacturer.Select(p => p.Name).ToList();
            _manufacturers.Insert(0, "Все");
            ComboBoxFiltrProdMan.ItemsSource = _manufacturers;
        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListBoxProducts.ItemsSource = _products.Where(p => p.Name.ToLower().Contains(TxtBoxSearch.Text.ToLower())).ToList();

        }

        private void ComboBoxFiltrProdCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectCategory = (string)ComboBoxFiltrProdCat.SelectedItem;
            string selectManufacturer = (string)ComboBoxFiltrProdMan.SelectedItem;
            if(selectManufacturer == null || selectCategory == null) return;
            Filrt(selectCategory, selectManufacturer);


        }

        private void Filrt(string selectCategory, string selectManufacturer)
        {
            List<Product> prod;
            if (ListBoxProducts.ItemsSource == null  || (ListBoxProducts.ItemsSource as List<Product>).Count == 0 ) prod = _products;
            else
            {

                prod = ListBoxProducts.ItemsSource as List<Product>;
            }
            if(selectCategory == "Все" &&  selectManufacturer == "Все")
            {
                prod = prod;
            }
            else if(selectCategory == "Все" && selectManufacturer != "Все")
            {
                prod = prod.Where(p => p.Manufacturer.Name == selectManufacturer).ToList();
            }
            else if( selectCategory != "Все" &&  selectManufacturer == "Все")
            {
                prod = prod.Where(p => p.ProdCategory.Name == selectCategory).ToList();
            }
            else
            {
               prod = prod.Where(p => p.ProdCategory.Name == selectCategory && p.Manufacturer.Name == selectManufacturer).ToList();
            }
            if (IsFiltr)
                prod = prod.OrderByDescending(p => p.Rating).ToList();
            ListBoxProducts.ItemsSource = prod;



        }

        

        private void BtnSortRating_Click(object sender, RoutedEventArgs e)
        {
            IsFiltr = !IsFiltr;

            if (true)
            {
                 //ListBoxProducts.ItemsSource = _products.OrderByDescending(p => p.Rating).ToList();
                Filrt((string)ComboBoxFiltrProdCat.SelectedItem, (string)ComboBoxFiltrProdMan.SelectedItem);

            }
            //ListBoxProducts.ItemsSource = _products.OrderByDescending(p => p.Rating).ToList();
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
