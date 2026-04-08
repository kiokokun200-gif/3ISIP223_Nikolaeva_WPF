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

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowLogIn.xaml
    /// </summary>
    public partial class WindowLogIn : Window
    {
        private List<string> _roles;
        public WindowLogIn()
        {
            InitializeComponent();
            _roles = Core.Context.Role.Select(r => r.Name).Distinct().ToList();
            ComboBoxRoles.ItemsSource = _roles;
            Owner = Application.Current.MainWindow;
        }

        private void ComboBoxRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
