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
        
        public _3UserAsseblyPage()
        {
            InitializeComponent();
            assemblies = Core.Context.assembly.ToList();
            assemblies[0].partassembly;
            foreach (var assembly in assemblies) {
                var partIds = Core.Context.partassembly.Where(pa => pa.assemblyid == assembly.id).Select(pa => pa.partid).ToList();
                var parts = Core.Context.basepart
            .Where(p => partIds.Contains(p.id))
            .ToList();
            }

        }

        private void Load()
        {
        }
    }
}
