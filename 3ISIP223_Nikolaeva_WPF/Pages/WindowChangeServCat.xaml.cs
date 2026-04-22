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
    /// Логика взаимодействия для WindowChangeServCat.xaml
    /// </summary>
    public partial class WindowChangeServCat : Window
    {
        private ServCategory _servCategory;
        public WindowChangeServCat(ServCategory servCategory)
        {
            InitializeComponent();
            _servCategory = servCategory;
            DataContext = _servCategory;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var man = Core.Context.ServCategory.FirstOrDefault(m => m.ID == _servCategory.ID);
            man.Name = TxtBoxServCarName.Text;
            man.Description = TxtBoxServCarDesc.Text;
            Core.Context.SaveChanges();
        }
    }
}
