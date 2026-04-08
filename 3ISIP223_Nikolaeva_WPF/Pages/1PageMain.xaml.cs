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
    /// Логика взаимодействия для _1PageMain.xaml
    /// </summary>
    public partial class _1PageMain : Page
    {
        private List<string> _services;
        private List<string> _masters;
        public _1PageMain()
        {
            InitializeComponent();

            _services = Core.Context.ServCategory.Select(s => s.Name).Distinct().ToList();
            _services.Insert(0, "Все");
            ComboBoxServices.ItemsSource = _services;
            _masters = Core.Context.User.Where(m => m.Role.Name == "Мастер").Select(u => u.LastName).Distinct().ToList();
            _masters.Insert(0, "Все");
            ComboBoxMasters.ItemsSource = _masters;


        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var wind = new WindowLogIn();
            //NavigationService.Navigate(new UserControl1());
            wind.ShowDialog();
        }
    }
}
