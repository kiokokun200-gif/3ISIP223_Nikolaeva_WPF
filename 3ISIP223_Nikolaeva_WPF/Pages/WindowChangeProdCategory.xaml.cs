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
    /// Логика взаимодействия для WindowChangeProdCategory.xaml
    /// </summary>
    public partial class WindowChangeProdCategory : Window
    {
        private ProdCategory _prodCategory;
        public WindowChangeProdCategory(ProdCategory prodCategory)
        {
            InitializeComponent();
            _prodCategory = prodCategory;
            DataContext = _prodCategory;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var man = Core.Context.ProdCategory.FirstOrDefault(m => m.ID == _prodCategory.ID);
            man.Name = TxtBoxChangeName.Text;
            Core.Context.SaveChanges();

        }


    }
}
