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
    /// Логика взаимодействия для _2PartTypePage.xaml
    /// </summary>
    public partial class _2PartTypePage : Page
    {
        static public parttype part {get;set;}
        public List<basepart> parts { get; set; }
        public _2PartTypePage(parttype p)
        {
            DataContext = this;
            part = p;
            InitializeComponent();
            Lod();
        }

        public void Lod()
        {
            parts = Core.Context.basepart.Where(d => d.parttype.id == part.id).ToList();
            PartsListBox.ItemsSource = parts;

        }
    }
}
