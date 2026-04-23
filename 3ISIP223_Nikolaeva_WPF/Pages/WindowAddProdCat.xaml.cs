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
    /// Логика взаимодействия для WindowAddProdCat.xaml
    /// </summary>
    public partial class WindowAddProdCat : Window
    {
         
        public WindowAddProdCat()
        {
            InitializeComponent();
        }

        private void BtnConfAddProdCat_Click(object sender, RoutedEventArgs e)
        {
            string prodcattname = TxtBoxNameProdCat.Text;
            if (prodcattname.Length <= 0)
            {
                try
                {
                    ProdCategory prodCategory = new ProdCategory()
                    {
                        Name = prodcattname,

                    };
                    Core.Context.ProdCategory.Add(prodCategory);
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
