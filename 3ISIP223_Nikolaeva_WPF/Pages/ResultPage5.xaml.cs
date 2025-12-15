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
using _3ISIP223_Nikolaeva_WPF.Models;


namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    public partial class ResultPage5 : Page
    {
        public ResultPage5()
        {
            InitializeComponent();
            ShowSummary();
        }

        private void ShowSummary()
        {
            double totalPrice = Car.CarTotalPrice;

            double downPayment = totalPrice * (Car.DownPaymentPercent / 100);
            double loanAmount = totalPrice - downPayment;

            TextBlockSummary.Text = $"Модель: {Car.Model}\n" +
                                   $"Двигатель: {Car.EngineType}\n" +
                                   $"Цвет: {Car.Color}\n" +
                                   $"Итоговая цена: {totalPrice:C}\n" +
                                   $"Первоначальный взнос: {Car.DownPaymentPercent}%\n" +
                                   $"Сумма кредита: {loanAmount:C}\n" +
                                   $"Срок кредита: {Car.LoanTerm} месяцев";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Car.CustomerName = TextBoxName.Text;
            Car.Phone = TextBoxPhone.Text;
            Car.Email = TextBoxEmail.Text;
        }
    }
}