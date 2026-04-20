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
using System.Windows.Shapes;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : Window
    {
        private List<string> _payments;
        private List<PaymentMethod> _paymentMethods;
        
        
        public WindowOrder()
        {
            InitializeComponent();
            LoadDate();
            
        }
        private void LoadDate()
        {
            _paymentMethods = Core.Context.PaymentMethod.ToList();
            _payments = _paymentMethods.Select( p => p.Name).ToList();
            ComboBoxPayments.ItemsSource = _payments;
            ComboBoxPayments.SelectedIndex = 0;
            TxtBlockTotalPrice.Text = $"{UserData.UserCart.TotalAmount} Р";
            var currentdate = DateTime.Now;
            List <DateTime> dates = new List<DateTime>();
            for (int i = 0; i <= 7; i++) 
            {
                dates.Add(currentdate.AddDays(i));
            }

            ListBoxDates.ItemsSource = dates;

        }

        private void BtnDate_Click(object sender, RoutedEventArgs e)
        {
            
            Button btn = (Button)sender;
            DateTime date = (DateTime)btn.DataContext;

            MessageBoxResult result = MessageBox.Show($"Забрать заказ {date:dd.MM.yyyy}?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                List<ProductInCart> prodincart = Core.Context.ProductInCart.Where(p => p.CartID == UserData.UserCart.ID).ToList();
                Order order = new Order
                {
                    Date = date,
                    TotalAmount = UserData.UserCart.TotalAmount,
                    UserID = UserData.CurrentUser.ID
                };
                Core.Context.Order.Add(order);
                Core.Context.SaveChanges();
                foreach (ProductInCart prod in prodincart)
                {
                    OrderItems orderItems = new OrderItems
                    {
                        OrderID = order.ID,
                        ProductID = prod.ProductID,
                        Quantity = prod.Quantity
                    };
                    Core.Context.OrderItems.Add(orderItems);
                }

                Core.Context.SaveChanges();
                
            }
            else return;
            

        }
    }
}
