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
        }

        private void RBColor_Cheked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string color = rb.Content.ToString();
            if (color == "Черный") Car.ColorPrice = 5000;
            else if (color == "Белый") Car.ColorPrice = 1000;
            else Car.ColorPrice = 50000;
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            bool isChecked = cb.IsChecked == true;

            if (cb == CheckBox1) Car.Option1 = isChecked;
            else if (cb == CheckBox2) Car.Option2 = isChecked;
            else if (cb == CheckBox3) Car.Option3 = isChecked;
            else if (cb == CheckBox4) Car.Option4 = isChecked;
        }
    }
}
