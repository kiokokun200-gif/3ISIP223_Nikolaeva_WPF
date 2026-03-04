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
        private List<basepart> userassembly;
        public _1MainPage()
        {

            InitializeComponent();
            parttypes = Core.Context.parttype.ToList(); //нет
            PartTypesListBox.ItemsSource = parttypes;
            //picture = new List<string>()
            //{
            //    "Images\\cpu.png",
            //    "Images\\gpu.jpg",
            //    "Images\\ram.png",
            //    "Images\\motherboard.png",
            //    "Images\\case.png",
            //    "Images\\powersupply.jpg",
            //    "Images\\processorcooler.png",
            //    "Images\\StorageDevice.png"  
            //};

            userassembly = UserDataaa.userparts;
            CurrentAssemblyListBox.ItemsSource=userassembly;
            TxtBoxTotalAmount.Text = $"{UserDataaa.TotalAmount} $";  


        }

        private void BtnChoose_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var selectpart = (parttype)btn.DataContext;
            NavigationService.Navigate(new _2PartTypePage(selectpart));
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //проверка
            assembly userassembly = new assembly() { author = TxtBoxAssemblyAuthor.Text, name = TxtBoxAssemblyName.Text };
            Core.Context.assembly.Add(userassembly);
            foreach(var part in UserDataaa.userparts)
            {
                partassembly partassem = new partassembly
                {
                    assemblyid = userassembly.id,
                    partid = part.id,
                };
                Core.Context.partassembly.Add(partassem);
            }
            Core.Context.SaveChanges();
            UserDataaa.username = userassembly.author;

        }

        private void BtnAssembly_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new _3UserAsseblyPage(UserDataaa.username));
        }
    }
}
