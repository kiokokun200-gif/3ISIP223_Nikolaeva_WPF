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
    /// Логика взаимодействия для PageAddPodnos.xaml
    /// </summary>
    public partial class PageAddPodnos : Page
    {
        private List<DishTypes> _DishTypes;
        public PageAddPodnos()
        {
            InitializeComponent();
            _DishTypes = Core.Context.DishTypes.ToList();
            ListBoxTypeDishes.ItemsSource = _DishTypes;
            
        }

        private void BtnAddPodnos_Click(object sender, RoutedEventArgs e)
        {
            var newCombo = new Combos()
            {
                Name = "",
                
            };
            foreach(DishTypes type in _DishTypes)
            {
                if (type.selectedDish.Name != "Не выбрано")
                {
                    ComboDishes comboDishes = new ComboDishes()
                    {
                        ComboId = newCombo.Id,
                        DishId = type.selectedDish.Id,
                    };
                    Core.Context.ComboDishes.Add(comboDishes);

                }
                else continue;
                
            }
            newCombo.Price = _DishTypes.Where(d => d.selectedDish.Name != "Не выбрано").Sum(di => di.selectedDish.Price);
            Core.Context.SaveChanges();
        }
    }
}
