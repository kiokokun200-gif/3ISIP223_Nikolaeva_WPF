using System;
using System.Collections.Generic;
using System.IO.Packaging;
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
    /// Логика взаимодействия для _3UserAsseblyPage.xaml
    /// </summary>
    public partial class _3UserAsseblyPage : Page
    {
        private List<assembly> assemblies;
        
        public _3UserAsseblyPage(string username)
        {
            InitializeComponent();
            assemblies = Core.Context.assembly.Where(a => a.author == username).ToList();
            foreach (var assembly in assemblies) { 
                 //partassembly = Core.Context.basepart.Where(p => p.partassembly.)
            }

        }
    }
}
