using _3ISIP223_Nikolaeva_WPF.Models;
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
    /// Логика взаимодействия для _2MainGamePage.xaml
    /// </summary>
    public partial class _2MainGamePage : Page
    {
        public _2MainGamePage()
        {
            InitializeComponent();
            LoadPicture();


        }

        private void LoadPicture()
        {
            List<BitmapImage> backgroungimages = new List<BitmapImage>()
            {
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca1.jpeg")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca2.png")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca3.jpg")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca4.jpg")),
                new BitmapImage( new Uri("pack://application:,,,/Images/Backgrounds/Loca5.jpg"))
            };

            int n  = Raaandom.GetRandomInt(0, backgroungimages.Count - 1);
            ImagBr.ImageSource = backgroungimages[n];
        }
    }
}
