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
            if (!(MainFrame.Content is _2PageCatalog))
            {
                MainFrame.Navigate(new _2PageCatalog());
            }
            else return;
        }

        private void BtnListBook_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainFrame.Content is _4PageBookList))
            {
                MainFrame.Navigate(new _4PageBookList());
            }
            else return;
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(UserData.CurrentUser.RoleID == 3)
            {
                MainFrame.Navigate(new _8PageAdmin());
            }
        }

        private void BtnAuthor_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            if ( UserData.CurrentUser.RoleID == 1)
            {
                MainFrame.Navigate(new _5PageUser(UserData.CurrentUser));

            }
            else if(UserData.CurrentUser.RoleID == 2)
            {
                NavigationService.Navigate(new _6PageAuthor());
            }


            else return;
        }
    }
}
