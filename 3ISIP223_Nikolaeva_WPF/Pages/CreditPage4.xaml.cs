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

        private void SliderPercent_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Car.DownPaymentPercent = e.NewValue;
            if (TextBlockPercent != null)
            TextBlockPercent.Text = $"{Car.DownPaymentPercent}%";
            UpdateCredit();
        }

        private void SliderMonths_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Car.LoanTerm = e.NewValue;
            if (TextBlockMonths != null)
            TextBlockMonths.Text = $"{Car.LoanTerm} месяцев";
            UpdateCredit();
        }

        private void UpdateCredit()
        {
            double C = Car.CarTotalPrice;
            TextBlockCarPrice.Text = $"Цена на авто: {C} Р";
            double P = C * (Car.DownPaymentPercent / 100);

            double S = C - P;

            double r = 7.5;

            double i = r / 100/ 12;

            double n = Car.LoanTerm;

            double A = S * (i * Math.Pow(1 + i, n)) / (Math.Pow(1 + i, n) - 1);
            Car.MountlyPayment = A;

            if (TextBlockDownPayment != null) TextBlockDownPayment.Text = $"Первоначальный взнос: {P:C}";
            if (TextBlockLoanAmount != null) TextBlockLoanAmount.Text = $"Сумма кредита: {S:C}";
            if(TextBlockMonthlyPayment != null) TextBlockMonthlyPayment.Text = $"Ежемесячный платёж: {A:C}";
        }
    }
}