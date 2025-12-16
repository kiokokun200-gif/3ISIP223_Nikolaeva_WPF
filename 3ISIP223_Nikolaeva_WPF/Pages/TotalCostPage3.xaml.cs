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
    public partial class TotalCostPage3 : Page
    {
        public TotalCostPage3()
        {
            InitializeComponent();
            ShowInfo();
        }

        private void ShowInfo()
        {
            double optionsPrice = 0;
            if (Car.Option1) optionsPrice += 50000;
            if (Car.Option2) optionsPrice += 70000;
            if (Car.Option3) optionsPrice += 30000;
            if (Car.Option4) optionsPrice += 20000;
            Car.OptionsPrice = optionsPrice;

            double totalPrice = Car.ModelPrice + Car.EnginePrice + Car.ColorPrice + optionsPrice;

            TextBlockInfo.Text = $"Модель: {Car.Model} - {Car.ModelPrice}\n" +
                               $"Двигатель: {Car.EngineType} - {Car.EnginePrice}\n" +
                               $"Цвет: {Car.Color} - {Car.ColorPrice}\n" +
                               $"Опции: {GetOptionsText()} - {optionsPrice}";


            TextBlockTotal.Text = $"Итоговая цена: {totalPrice}";
            Car.CarTotalPrice = totalPrice;
        }

        private string GetOptionsText()
        {
            string text = "";
            if (Car.Option1) text += "Кожаный салон, ";
            if (Car.Option2) text += "Панорамная крыша, ";
            if (Car.Option3) text += "Навигация, ";
            if (Car.Option4) text += "Подогрев сидений, ";

            if (text == "") return "нет";
            text = text.TrimEnd(',', ' ');
            Car.Options = text;
            return text;
        }
    }
}
