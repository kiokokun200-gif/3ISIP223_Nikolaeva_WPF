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
    /// Логика взаимодействия для WindowChangeManufacturer.xaml
    /// </summary>
    public partial class WindowChangeManufacturer : Window
    {
        private Manufacturer _manufacturer;
        public WindowChangeManufacturer(Manufacturer manufacturer)
        {
            InitializeComponent();
            _manufacturer = manufacturer;
            DataContext = _manufacturer;

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var man = Core.Context.Manufacturer.FirstOrDefault(m => m.ID == _manufacturer.ID);
            man.Name = TxtBoxChangeName.Text;
            Core.Context.SaveChanges();
        }
    }
}
