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
    /// Логика взаимодействия для _2PageCart.xaml
    /// </summary>
    public partial class _2PageCart : Page
    {
        public _2PageCart()
        {
            InitializeComponent();
            CartListBox.ItemsSource = Cartlst.Cartlist;

            decimal total = Cartlst.Cartlist.Sum(p => p.ProductPrice * p.Quantity);
            TxtBlkTotal.Text = $"Итого: {total} Р";

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _3PageOrder());
        }
    }
}
