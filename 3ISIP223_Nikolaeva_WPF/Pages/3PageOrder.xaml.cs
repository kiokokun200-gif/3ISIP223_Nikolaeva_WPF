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
    /// Логика взаимодействия для _3PageOrder.xaml
    /// </summary>
    public partial class _3PageOrder : Page
    {
        private bool isFIOValid = false;
        private bool isEmailValid = false;
        private bool isAddressValid = false;
        public _3PageOrder()
        {
            InitializeComponent();
            TotalListBox.ItemsSource = Cartlst.Cartlist;
            decimal total = Cartlst.Cartlist.Sum(p => p.ProductPrice * p.Quantity);
            TxtBlkTotal.Text = $"Итого: {total} Р";

            UpdateOrderButton();
        }

        private void UpdateOrderButton()
        {
            BtnOrder.IsEnabled = (isFIOValid && isEmailValid && isAddressValid && Cartlst.Cartlist.Any());

        }

            private void TxtFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            string fio = TxtFIO.Text;
            isFIOValid = !string.IsNullOrWhiteSpace(fio) && fio.Length >= 5 && fio.Contains(" ");
            UpdateOrderButton();

        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            string email = TxtEmail.Text;
            isEmailValid = !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.Contains(".");
            UpdateOrderButton();

        }

        private void TxtAdress_TextChanged(object sender, TextChangedEventArgs e)
        {
            string adress = TxtAdress.Text;
            isAddressValid = !string.IsNullOrWhiteSpace(adress) && adress.Length > 5;
            UpdateOrderButton();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal total = Cartlst.Cartlist.Sum(p => p.ProductPrice * p.Quantity);
            var order = new Order
            {
                Date = DateTime.Now,
                FIO = TxtFIO.Text.ToString(),
                Email = TxtEmail.Text.ToString(),
                DeliveryAdress = TxtAdress.Text.ToString(), 
                TotalAmount = total
            };

            Core.Context.Order.Add(order);
            Core.Context.SaveChanges();

            foreach(var cartItem in Cartlst.Cartlist)
            {
                var productOrder = new OrderProducts
                {
                    ID_Order = order.ID_Order,
                    ID_Product = cartItem.ID_Product,
                    Quantity = cartItem.Quantity
                };
                Core.Context.OrderProducts.Add(productOrder);
            }
            Core.Context.SaveChanges();
            Cartlst.Cartlist.Clear();
            Application.Current.Shutdown();
        }

    }
}
