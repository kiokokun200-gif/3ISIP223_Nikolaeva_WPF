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
        public _3PageOrder()
        {
            InitializeComponent();
            TotalListBox.ItemsSource = Cartlst.Cartlist;
            decimal total = Cartlst.Cartlist.Sum(p => p.ProductPrice * p.Quantity);
            TxtBlkTotal.Text = $"Итого: {total} Р";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
