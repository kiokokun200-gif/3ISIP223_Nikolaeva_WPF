using _3ISIP223_Nikolaeva_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для WindowAddServCat.xaml
    /// </summary>
    public partial class WindowAddServCat : Window
    {
        public WindowAddServCat()
        {
            InitializeComponent();
        }

        private void BtnConfAddServCat_Click(object sender, RoutedEventArgs e)
        {
            string prodservcatname = TxtBoxServCat.Text;
            string prodservcatdesc = TxtBoxServCatDesc.Text;

            if (string.IsNullOrEmpty(prodservcatname))
            {

                try
                {
                    ServCategory prodcat = new ServCategory
                    {
                        Name = prodservcatname,
                        Description = prodservcatdesc



                    };
                    Core.Context.ServCategory.Add(prodcat);
                    Core.Context.SaveChanges();

                }
                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                    return;
                }


                MessageBox.Show("Производитель добавлен");
            }
            else
            {
                MessageBox.Show("Заполните название типа услуги");
            }
        }
    }
}
