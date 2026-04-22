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
    public partial class WindowOrder : Window
    {
        private List<string> _payments;
        private List<PaymentMethod> _paymentMethods;
        private _3PageCart _cartPage;

        public WindowOrder(_3PageCart cartPage)
        {
            InitializeComponent();
            _cartPage = cartPage;
            LoadDate();
        }

        private void LoadDate()
        {
            _paymentMethods = Core.Context.PaymentMethod.ToList();
            _payments = _paymentMethods.Select(p => p.Name).ToList();
            ComboBoxPayments.ItemsSource = _payments;
            ComboBoxPayments.SelectedIndex = 0;
            TxtBlockTotalPrice.Text = $"{UserData.UserCart.TotalAmount} Р";
            var currentdate = DateTime.Now;
            List<DateTime> dates = new List<DateTime>();
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

                if (prodincart.Count == 0)
                {
                    MessageBox.Show("Корзина пуста");
                    return;
                }

                try
                {
                    Order order = new Order
                    {
                        Date = DateTime.Now,
                        TotalAmount = UserData.UserCart.TotalAmount,
                        UserID = UserData.CurrentUser.ID,
                        DeliverDate = date,
                        IsClosed = false
                    };
                    Core.Context.Order.Add(order);
                    Core.Context.SaveChanges();

                    foreach (ProductInCart prod in prodincart)
                    {
                        OrderItems orderItems = new OrderItems
                        {
                            OrderID = order.ID,
                            ProductID = prod.ProductID,
                            Quantity = prod.Quantity,
                        };
                        Core.Context.OrderItems.Add(orderItems);
                    }

                    // ОЧИЩАЕМ КОРЗИНУ
                    Core.Context.ProductInCart.RemoveRange(prodincart);

                    // ОБНОВЛЯЕМ ДАННЫЕ КОРЗИНЫ
                    UserData.UserCart.TotalAmount = 0;
                    UserData.UserCart.TotalQuantity = 0;

                    var cartBD = Core.Context.Cart.FirstOrDefault(c => c.ID == UserData.UserCart.ID);
                    if (cartBD != null)
                    {
                        cartBD.TotalAmount = 0;
                        cartBD.TotalQuantity = 0;
                    }

                    Core.Context.SaveChanges();

                    MessageBox.Show("Заказ оформлен! Корзина очищена.");

                    // ОБНОВЛЯЕМ СТРАНИЦУ КОРЗИНЫ (если она ещё открыта)
                    if (_cartPage != null)
                    {
                        _cartPage.LoadData();
                    }

                    // ЗАКРЫВАЕМ ОКНО ЗАКАЗА
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                }
            }
        }
    }
}