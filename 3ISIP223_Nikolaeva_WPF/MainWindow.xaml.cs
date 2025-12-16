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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ModelPage1());
            Progress.Value = 1;
            BtnBack.IsEnabled = false;
        }
        
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is ResultPage5 resultPage)
            {
                if (resultPage.ShouldShowWarning())
                {
                    var result = MessageBox.Show(
                        "У вас есть незаполненные данные. Всё равно уйти? Данные будут потеряны.",
                        "Подтверждение",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        resultPage.ResetData(); 
                        
                        if (MainFrame.CanGoBack)
                        {
                            MainFrame.GoBack();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (MainFrame.CanGoBack)
                    {
                        MainFrame.GoBack();
                    }
                }
            }
            else
            {
                if (MainFrame.CanGoBack)
                {
                    MainFrame.GoBack();
                }
            }
        }
        
        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.Content is ModelPage1)
            {
                MainFrame.Navigate(new ColorPage2());
                Progress.Value = 2;
            }
            else if (MainFrame.Content is ColorPage2)
            {
                MainFrame.Navigate(new TotalCostPage3());
                Progress.Value = 3;
            }
            else if (MainFrame.Content is TotalCostPage3)
            {
                MainFrame.Navigate(new CreditPage4());
                Progress.Value = 4;
            }
            else if (MainFrame.Content is CreditPage4)
            {
                MainFrame.Navigate(new ResultPage5());
                Progress.Value = 5;
                BtnForward.Content = "Готово";
                BtnForward.IsEnabled = false; 
            }
            else if (MainFrame.Content is ResultPage5 resultPage)
            {
                if (resultPage.IsFormValid())
                {
                    MessageBox.Show("Заявка оформлена! \n" + $" Модель: { Car.Model}\n" +
        $"Двигатель: {Car.EngineType}\n" +
        $"Цвет: {Car.Color}\n" +
        $"Опции: {Car.Options}\n" +
        $"Итоговая цена: {Car.CarTotalPrice}\n" +
        $"Первоначальный взнос: {Car.DownPaymentPercent}%\n" +
        $"Срок кредита: {Car.LoanTerm} месяцев\n" +
        $"Ежемесячный платеж: {Car.MountlyPayment}\n" +
        $"Имя: {Car.CustomerName}\n" +
        $"Телефон: {Car.Phone}\n" +
        $"Почта: {Car.Email}", "Успех");
                    Application.Current.Shutdown();

                }
                else
                {
                    MessageBox.Show("Заполните все поля корректно!", "Ошибка");
                }
            }
        }
        
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (MainFrame.Content is ModelPage1)
                Progress.Value = 1;
            else if (MainFrame.Content is ColorPage2)
                Progress.Value = 2;
            else if (MainFrame.Content is TotalCostPage3)
                Progress.Value = 3;
            else if (MainFrame.Content is CreditPage4)
                Progress.Value = 4;
            else if (MainFrame.Content is ResultPage5)
                Progress.Value = 5;
            
            BtnBack.IsEnabled = MainFrame.CanGoBack;
            
            if (MainFrame.Content is ResultPage5)
            {
                BtnForward.Content = "Готово";
            }
            else
            {
                BtnForward.Content = "Далее";
                BtnForward.IsEnabled = true; 
            }
        }
    }
}