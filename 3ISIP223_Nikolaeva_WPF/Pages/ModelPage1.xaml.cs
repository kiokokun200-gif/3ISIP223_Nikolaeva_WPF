using _3ISIP223_Nikolaeva_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для ModelPage1.xaml
    /// </summary>
    public partial class ModelPage1 : Page
    {
        public ModelPage1()
        {
            InitializeComponent();

            //List<string> models = new List<string>();
            //models.Add("Toyota Camry");
            //models.Add("Kia Rio");
            //models.Add("Hyundai Solaris");
            //models.Add("Skoda Octavia");
            //models.Add("BMW 3 Series");
        }

        private void RBMod_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string model = rb.Content.ToString();
            Car.Model = model;
            if (model == "Renault Logan") Car.ModelPrice = 5000000;
            else if (model == "Hyundai Solaris") Car.ModelPrice = 10000000;
            else Car.ModelPrice = 100000000;
            CarItog.Text = $"{Car.Model} {Car.ModelPrice}";
        }

        private void RBEngine_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            string engine = rb.Content.ToString();
            Car.EngineType = engine;
            if (engine == "Бензиновый") Car.EnginPrice = 50000;
            else if (engine == "Дизельный") Car.EnginPrice = 100000;
            else Car.EnginPrice = 70000;
            CarItog.Text += $" {Car.EngineType} {Car.EnginPrice}";
        }
    }
}
