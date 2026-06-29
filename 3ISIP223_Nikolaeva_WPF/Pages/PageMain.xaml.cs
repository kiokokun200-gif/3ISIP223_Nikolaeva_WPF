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
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        private List<Combos> _combos;
        private List<Dishes> _dishes;
        public PageMain()
        {
            InitializeComponent();
            _dishes = Core.Context.Dishes.ToList();
            _combos = Core.Context.Combos.ToList();
            ListBoxCombos.ItemsSource = _combos;
            ListBoxDishes.ItemsSource = _dishes;

        }

        private void BtnAddPodnos_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageAddPodnos());
        }
    }
}
