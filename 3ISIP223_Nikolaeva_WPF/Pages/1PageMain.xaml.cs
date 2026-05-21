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
    /// Логика взаимодействия для _1PageMain.xaml
    /// </summary>
    public partial class _1PageMain : Page
    {
        public _1PageMain()
        {
            InitializeComponent();
            DataContext = UserData.CurrentUser;

        }

        private void BtnCatalogBook_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new _2PageCatalog());

        }

        private void BtnListBook_Click(object sender, RoutedEventArgs e)
        {
   
            MainFrame.Navigate(new _4PageBookList());
           
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new _8PageAdmin());
            
        }

        private void BtnAuthor_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new _6PageAuthor());
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            
            MainFrame.Navigate(new _5PageUser(UserData.CurrentUser));

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Выйти из аккаунта? ", "Вопрос", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                UserData.CurrentUser = null;
                NavigationService.Navigate(new PageLogin());

            }
            else return;
        }
    }
}
