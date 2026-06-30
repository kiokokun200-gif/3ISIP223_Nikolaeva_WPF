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
        private List<Dishes> _SelevtedDishes = new List<Dishes>();
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
                Price = _SelevtedDishes.Sum(d => d.Price),
                IsAvailable = true
                
            };
            Core.Context.Combos.Add(newCombo);
            Core.Context.SaveChanges();
            foreach(Dishes dish in _SelevtedDishes)
            {
                if (dish.Name != "Не выбрано")
                {
                    ComboDishes comboDishes = new ComboDishes()
                    {
                        ComboId = newCombo.Id,
                        DishId = dish.Id,
                    };
                    Core.Context.ComboDishes.Add(comboDishes);

                }
                else continue;
                
            }
            
            Core.Context.SaveChanges();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            Dishes dish = comboBox.SelectedItem as Dishes;
            //Dishes dish = Core.Context.Dishes.FirstOrDefault(c => c.Name == comboBox.SelectedItem.ToString());
            if (dish != null)
            {
                var existedDishType = _SelevtedDishes.FirstOrDefault(c => c.DishTypeId == dish.DishTypeId);
                if (existedDishType == null)
                {
                    _SelevtedDishes.Add(dish);
                } 
                else
                {
                    _SelevtedDishes.Remove(existedDishType);
                    _SelevtedDishes.Add(dish);
                }
            }

        }
    }
}
