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
    public partial class WindowAddMasterService : Window
    {
        private User _master;
        private ServCategory _selectedCategory;

        public WindowAddMasterService(User master)
        {
            InitializeComponent();
            _master = master;
            LoadServices();
        }

        private void LoadServices()
        {
            var services = Core.Context.ServCategory.ToList();
            ComboBoxServices.ItemsSource = services;
            ComboBoxServices.DisplayMemberPath = "Name";
            ComboBoxServices.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            _selectedCategory = ComboBoxServices.SelectedItem as ServCategory;

            if (_selectedCategory == null)
            {
                MessageBox.Show("Выберите услугу");
                return;
            }

            if (!decimal.TryParse(TxtBoxPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену");
                return;
            }

            var exist = Core.Context.MasterService
                .FirstOrDefault(m => m.MasterID == _master.ID && m.ServCategotyID == _selectedCategory.ID);

            if (exist != null)
            {
                MessageBox.Show("Эта услуга уже добавлена");
                return;
            }

            try
            {
                MasterService masterService = new MasterService()
                {
                    MasterID = _master.ID,
                    ServCategotyID = _selectedCategory.ID,
                    Price = price
                };

                Core.Context.MasterService.Add(masterService);
                Core.Context.SaveChanges();

                MessageBox.Show("Услуга добавлена!");
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
