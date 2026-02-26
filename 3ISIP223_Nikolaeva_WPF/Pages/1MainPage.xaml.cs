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

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для _1MainPage.xaml
    /// </summary>
    public partial class _1MainPage : Page
    {
        private List<parttype> parttypes;
        private List<string> picture;
        public _1MainPage()
        {

            InitializeComponent();
            parttypes = Core.Context.parttype.ToList(); //нет
            PartTypesListBox.ItemsSource = parttypes;
            picture = new List<string>()
            {
                "Images\\cpu.png",
                "Images\\gpu.jpg",
                "Images\\ram.png",
                "Images\\motherboard.png",
                "Images\\case.png",
                "Images\\powersupply.jpg",
                "Images\\processorcooler.png",
                "Images\\StorageDevice.png"  
            };
            

        }

        private void BtnChoose_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var selectpart = (parttype)btn.DataContext;
            NavigationService.Navigate(new _2PartTypePage(selectpart));
        }
    }
}
