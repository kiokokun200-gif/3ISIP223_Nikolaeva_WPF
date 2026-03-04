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
        //static public basepart part {get;set;}

        public List<basepart> parts { get; set; }

        private List<string> manufacturers;
        public _2PartTypePage(parttype p)
        {
            InitializeComponent();
            DataContext = this;
            parts = Core.Context.basepart.Where(d => d.parttype.id == p.id).ToList();
            PartsListBox.ItemsSource = parts;
            manufacturers = parts.Select(d => d.manufacturer.name).Distinct().ToList();
            manufacturers.Insert(0, "Все");
            ComboManufactured.ItemsSource = manufacturers;

        }

        private void BtnAddPart_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            basepart selectedpart = (basepart)btn.DataContext;

            if (CheckCompatibility.CanAddPart(UserDataaa.userparts, selectedpart, out string errorMessage, out basepart partToReplace))
            {
                if (partToReplace != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"В вашей сборке уже есть {partToReplace.parttype.name}: {partToReplace.name}. Заменить на {selectedpart.name}?",
                        "Замена детали",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        UserDataaa.userparts.Remove(partToReplace);
                        UserDataaa.userparts.Add(selectedpart);
                        
                    }
                }
                else
                {
                    UserDataaa.userparts.Add(selectedpart);
                    
                }
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show(errorMessage, "Ошибка совместимости",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            UserDataaa.Sort();
            UpdatePrice();

        }
        private void UpdatePrice()
        {
            UserDataaa.TotalAmount = UserDataaa.userparts.Sum(p => p.price);

        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchtext = TxtBoxSearch.Text.ToLower();
            PartsListBox.ItemsSource = parts.Where(pa => pa.name.ToLower().Contains(searchtext)).ToList();
        }

        private void ComboManufactured_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string combotext = ComboManufactured.SelectedItem.ToString();
            if (combotext == "Все")
            {
                PartsListBox.ItemsSource = parts;
            }

            else
            {
                PartsListBox.ItemsSource = parts.Where(pa => pa.manufacturer.name == combotext).ToList();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
