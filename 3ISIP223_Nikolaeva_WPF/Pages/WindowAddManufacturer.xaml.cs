using _3ISIP223_Nikolaeva_WPF.Models;
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
    /// Логика взаимодействия для WindowAddManufacturer.xaml
    /// </summary>
    public partial class WindowAddManufacturer : Window
    {
        public WindowAddManufacturer()
        {
            InitializeComponent();
        }

        private void BtnConfAddMan_Click(object sender, RoutedEventArgs e)
        {
            string manname = TxtBoxNameMan.Text;
            if (manname.Length > 0)
            {
                try
                {
                    Manufacturer manufacturer = new Manufacturer
                    {
                        Name = manname,
                    };
                    Core.Context.Manufacturer.Add(manufacturer);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Производитель добавлен");


                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                    return;
                }
            }
            else MessageBox.Show("Заполните название");


        }

        
    }
}
