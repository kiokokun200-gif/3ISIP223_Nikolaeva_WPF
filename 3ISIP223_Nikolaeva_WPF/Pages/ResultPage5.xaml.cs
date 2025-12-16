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
       bool correctname = false;
       bool correctphone = false;
       bool correctemail = false;
       bool dataEntered = false;
        public ResultPage5()
        {
            InitializeComponent();
            ShowSummary();
            if (!string.IsNullOrEmpty(Car.CustomerName))
            {
                TextBoxName.Text = Car.CustomerName;
                correctname = true;
                dataEntered = true;
            }
            if (!string.IsNullOrEmpty(Car.Phone))
            {
                TextBoxPhone.Text = Car.Phone;
                correctphone = true;
                dataEntered = true;
            }
            if (!string.IsNullOrEmpty(Car.Email))
            {
                TextBoxEmail.Text = Car.Email;
                correctemail = true;
                dataEntered = true;
            }
            CheckAllFields();
        }

        private void ShowSummary()
        {
            double totalPrice = Car.CarTotalPrice;

            double downPayment = totalPrice * (Car.DownPaymentPercent / 100);
            double loanAmount = totalPrice - downPayment;

            if (TextBlockSummary != null)
            {
                TextBlockSummary.Text = $"Модель: {Car.Model}\n" +
        $"Двигатель: {Car.EngineType}\n" +
        $"Цвет: {Car.Color}\n" +
        $"Опции: {Car.Options}\n" +
        $"Итоговая цена: {Car.CarTotalPrice}\n" +
        $"Первоначальный взнос: {Car.DownPaymentPercent}%\n" +
        $"Сумма кредита: {loanAmount:C}\n" +
        $"Срок кредита: {Car.LoanTerm} месяцев\n" +
        $"Ежемесячный платеж: {Car.MountlyPayment}";
            }
        }


        private void TextBoxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]);
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxName.Text.Length < 2) correctname = false;
            else {
                correctname = true;
                Car.CustomerName = TextBoxName.Text;    
                    
                    }
            if (!string.IsNullOrEmpty(TextBoxName.Text))
                dataEntered = true;

            CheckAllFields();
        }

        private void TextBoxPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TextBoxPhone.Text.Length < 10) correctphone = false;
            else
            {
                correctphone = true;
                Car.Phone = TextBoxPhone.Text;
            }
            if (!string.IsNullOrEmpty(TextBoxPhone.Text))
                dataEntered = true;
            CheckAllFields();
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TextBoxEmail.Text.Length < 5 || !TextBoxEmail.Text.Contains('@'))
            {
                correctemail = false;
            }
            else
            {
                correctemail = true;
                Car.Email = TextBoxEmail.Text;
            }
            CheckAllFields();
        }
        private void CheckAllFields()
        {
            bool allCorrect = correctname && correctphone && correctemail;

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.BtnForward.IsEnabled = allCorrect;
            }
        }

        public bool ShouldShowWarning()
        {
            return dataEntered && !(correctname && correctphone && correctemail);
        }

        public void ResetData()
        {
            TextBoxName.Text = "";
            TextBoxPhone.Text = "";
            TextBoxEmail.Text = "";

            correctname = false;
            correctphone = false;
            correctemail = false;
            dataEntered = false;

            Car.CustomerName = "";
            Car.Phone = "";
            Car.Email = "";

            CheckAllFields();
        }

        public bool IsFormValid()
        {
            return correctname && correctphone && correctemail;
        }
    }
}