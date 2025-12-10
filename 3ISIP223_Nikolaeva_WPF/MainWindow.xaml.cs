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
        private Car car = new Car();    
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ModelPage1(car));

            Progress.Value = 1;

            BtnBack.IsEnabled = false;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.Content is ColorPage2)
            {
                MainFrame.Navigate(new ModelPage1(car));
                Progress.Value = 1;
            }
            if(MainFrame.Content is TotalCostPage3)
            {
                MainFrame.Navigate(new ColorPage2(car));
                Progress.Value = 2;
            }
            if(MainFrame.Content is CreditPage4)
            {
                MainFrame.Navigate(new TotalCostPage3(car));
                Progress.Value = 3;
            }
            if(MainFrame.Content is ResultPage5)
            {
                MainFrame.Navigate(CreditPage4(car));
                Progress.Value = 4;
            }

        }

        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            if(MainFrame.Content is ModelPage1)
            {
                MainFrame.Navigate(new ColorPage2(car));
                Progress.Value = 2;
            }
            if(MainFrame.Content is ColorPage2)
            {
                MainFrame.Navigate(new TotalCostPage3(car));
                Progress.Value = 3;
            }
            if(MainFrame.Content is TotalCostPage3)
            {
                MainFrame.Navigate(new CreditPage4(car));
                Progress.Value = 4;
            }
            if(MainFrame.Content is CreditPage4)
            {
                MainFrame.Navigate(new ResultPage5(car));
                Progress.Value = 5;
            }
                        
        }
    }
}
