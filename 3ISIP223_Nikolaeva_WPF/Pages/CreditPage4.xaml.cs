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
    public partial class CreditPage4 : Page
    {
        public CreditPage4()
        {
            InitializeComponent();
            UpdateCredit();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Car.DownPaymentPercent = SliderPercent.Value;
            Car.LoanTerm = (int)SliderMonths.Value;

            TextBlockPercent.Text = $"{Car.DownPaymentPercent}%";
            TextBlockMonths.Text = $"{Car.LoanTerm} месяцев";

            UpdateCredit();
        }

        private void UpdateCredit()
        {
            double carPrice = Car.CarTotalPrice;

            TextBlockCarPrice.Text = $"{carPrice:C}";

            double downPayment = carPrice * (Car.DownPaymentPercent / 100);
            double loanAmount = carPrice - downPayment;

            double r = 7.5;
            double i = r / 100 / 12;
            int n = Car.LoanTerm;

            double numerator = i * Math.Pow(1 + i, n);
            double denominator = Math.Pow(1 + i, n) - 1;
            double monthlyPayment = loanAmount * (numerator / denominator);

            TextBlockDownPayment.Text = $"Первоначальный взнос: {downPayment:C}";
            TextBlockLoanAmount.Text = $"Сумма кредита: {loanAmount:C}";
            TextBlockMonthlyPayment.Text = $"Ежемесячный платёж: {monthlyPayment:C}";
        }
    }
}
