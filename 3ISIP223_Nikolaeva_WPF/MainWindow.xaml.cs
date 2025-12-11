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
using _3ISIP223_Nikolaeva_WPF.Pages;
using _3ISIP223_Nikolaeva_WPF.Models;

namespace _3ISIP223_Nikolaeva_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ModelPage1());

            Progress.Value = 1;

          
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is ColorPage2)
            {
                MainFrame.Navigate(new ModelPage1());
                Progress.Value = 1;
            }
            if (MainFrame.Content is TotalCostPage3)
            {
                MainFrame.Navigate(new ColorPage2());
                Progress.Value = 2;
            }
            if (MainFrame.Content is CreditPage4)
            {
                MainFrame.Navigate(new TotalCostPage3());
                Progress.Value = 3;
            }
            if (MainFrame.Content is ResultPage5)
            {
                MainFrame.Navigate(new CreditPage4());
                Progress.Value = 4;
            }
            BtnBack.IsEnabled = !(MainFrame.Content is ModelPage1);
        }

        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is ModelPage1)
            {
                MainFrame.Navigate(new ColorPage2());
                Progress.Value = 2;
            }
            if (MainFrame.Content is ColorPage2)
            {
                MainFrame.Navigate(new TotalCostPage3());
                Progress.Value = 3;
            }
            if (MainFrame.Content is TotalCostPage3)
            {
                MainFrame.Navigate(new CreditPage4());
                Progress.Value = 4;
            }
            if (MainFrame.Content is CreditPage4)
            {
                MainFrame.Navigate(new ResultPage5());
                Progress.Value = 5;
            }
            BtnForward.IsEnabled = !(MainFrame.Content is ResultPage5);
        }
    }
}
