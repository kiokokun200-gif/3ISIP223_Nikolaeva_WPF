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
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ColorPage2.xaml
    /// </summary>
    public partial class ColorPage2 : Page
    {
        public ColorPage2()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Car.Color))
            {
                if (Car.Color == "Черный") RbBlack.IsChecked = true;
                else if (Car.Color == "Белый") RbBWhite.IsChecked = true;
                else if (Car.Color == "Розови") RbPink.IsChecked = true;
            }
            CheckBox1.IsChecked = Car.Option1;
            CheckBox2.IsChecked = Car.Option2;
            CheckBox3.IsChecked = Car.Option3;
            CheckBox4.IsChecked = Car.Option4;
        }


        private void CheckBox1_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Car.Option1 = cb.IsChecked == true;
        }

        private void CheckBox2_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Car.Option2 = cb.IsChecked == true;
        }

        private void CheckBox3_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Car.Option3 = cb.IsChecked == true;
        }

        private void CheckBox4_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Car.Option4 = cb.IsChecked == true;
        }

        private void RbBlack_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Car.Color = rb.Content.ToString();
            Car.ColorPrice = 5000;
        }

        private void RbBWhite_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Car.Color = rb.Content.ToString();
            Car.ColorPrice = 1000;
        }

        private void RbPink_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            Car.Color = rb.Content.ToString();
            Car.ColorPrice = 50000;
        }
    }
}
