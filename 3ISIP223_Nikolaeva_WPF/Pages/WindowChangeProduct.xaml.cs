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
    public partial class WindowChangeProduct : Window
    {
        private Product _product;

        public WindowChangeProduct(Product product)
        {
            InitializeComponent();
            _product = product;
            DataContext = _product;
            LoadData();
        }

        private void LoadData()
        {
            TxtBoxName.Text = _product.Name;
            TxtBoxCost.Text = _product.Cost.ToString();
            TxtBoxDiscount.Text = _product.Discount.ToString();
            TxtBoxRating.Text = _product.Rating.ToString();
            TxtBoxDescription.Text = _product.Description;
            TxtBoxImage.Text = _product.Image;

            var categories = Core.Context.ProdCategory.ToList();
            ComboBoxCategory.ItemsSource = categories;
            ComboBoxCategory.SelectedItem = categories.FirstOrDefault(c => c.ID == _product.CategoryID);

            var manufacturers = Core.Context.Manufacturer.ToList();
            ComboBoxManufacturer.ItemsSource = manufacturers;
            ComboBoxManufacturer.SelectedItem = manufacturers.FirstOrDefault(m => m.ID == _product.ManufacturerID);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtBoxName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;
                }

                if (!decimal.TryParse(TxtBoxCost.Text, out decimal cost) || cost <= 0)
                {
                    MessageBox.Show("Введите корректную цену");
                    return;
                }

                if (!double.TryParse(TxtBoxDiscount.Text, out double discount) || discount < 0 || discount > 100)
                {
                    MessageBox.Show("Введите скидку от 0 до 100");
                    return;
                }

                if (!double.TryParse(TxtBoxRating.Text, out double rating) || rating < 0 || rating > 5)
                {
                    MessageBox.Show("Введите рейтинг от 0 до 5");
                    return;
                }

                ProdCategory selectedCategory = ComboBoxCategory.SelectedItem as ProdCategory;
                if (selectedCategory == null)
                {
                    MessageBox.Show("Выберите категорию");
                    return;
                }

                Manufacturer selectedManufacturer = ComboBoxManufacturer.SelectedItem as Manufacturer;
                if (selectedManufacturer == null)
                {
                    MessageBox.Show("Выберите производителя");
                    return;
                }

                _product.Name = TxtBoxName.Text;
                _product.Cost = cost;
                _product.Discount = discount;
                _product.Rating = rating;
                _product.Description = TxtBoxDescription.Text;
                _product.Image = TxtBoxImage.Text;
                _product.CategoryID = selectedCategory.ID;
                _product.ManufacturerID = selectedManufacturer.ID;

                Core.Context.SaveChanges();

                MessageBox.Show("Товар сохранён!");
                this.DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
