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

namespace _3ISIP223_Nikolaeva_WPF
{
    /// <summary>
    /// Логика взаимодействия для _6PageSeans.xaml
    /// </summary>
    public partial class _6PageSeans : Page
    {
        private Seans _seans;
        private List<Seats> _allSeats = new List<Seats>();
        private List<Button> _seatButtons = new List<Button>();
        private List<Seats> _selectedSeats = new List<Seats>();

        public _6PageSeans(Seans seans)
        {
            InitializeComponent();
            _seans = seans;
            DataContext = _seans;
            Loaded += _6PageSeans_Loaded;
        }

        private void _6PageSeans_Loaded(object sender, RoutedEventArgs e)
        {
            CreateSeats();
            DisplaySeats();
        }

        private void CreateSeats()
        {
            int rows = _seans.Kinozal.RowNumber; 
            int seatsPerRow = _seans.Kinozal.SeatNumber; 

            _allSeats.Clear();

            for (int row = 1; row <= rows; row++)
            {
                for (int seatNum = 1; seatNum <= seatsPerRow; seatNum++)
                {
                    //bool isBooked = new Random().Next(0, 100) < 30; // 30% мест заняты

                    _allSeats.Add(new Seats
                    {
                        RowNumber = row,
                        SeatNumber = seatNum,
                        IsBooked = false,
                        Price = _seans.Kinozal.Kinozal_Rating.Ticket_Price
                    }) ;
                }
            }
        }

        private void DisplaySeats()
        {
            SeatsPanel.Children.Clear();
            _seatButtons.Clear();

            var rows = _allSeats.GroupBy(s => s.RowNumber).OrderBy(g => g.Key);

            foreach (var rowGroup in rows)
            {
                StackPanel rowPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 5, 0, 5),
                    
                };

                TextBlock rowLabel = new TextBlock
                {
                    Text = $"Ряд {rowGroup.Key}:",
                    Width = 60,
                    VerticalAlignment = VerticalAlignment.Center
                };
                rowPanel.Children.Add(rowLabel);

                foreach (var seat in rowGroup.OrderBy(s => s.SeatNumber))
                {
                    Button seatButton = CreateSeatButton(seat);
                    rowPanel.Children.Add(seatButton);
                    _seatButtons.Add(seatButton);
                }

                SeatsPanel.Children.Add(rowPanel);
            }
        }

        private Button CreateSeatButton(Seats seat)
        {
            Button button = new Button
            {
                Content = seat.SeatNumber.ToString(),
                Width = 40,
                Height = 40,
                Margin = new Thickness(2),
                Tag = seat 
            };

            if (seat.IsBooked)
            {
                button.Background = Brushes.Red;
                button.Foreground = Brushes.White;
                button.IsEnabled = false;
                button.Content = "X";
                button.ToolTip = $"Ряд {seat.RowNumber}, Место {seat.SeatNumber} - Занято";
            }
            else
            {
                button.Background = Brushes.LightGreen;
                button.Foreground = Brushes.Black;
                button.IsEnabled = true;
                button.Click += SeatButton_Click;
                button.ToolTip = $"Ряд {seat.RowNumber}, Место {seat.SeatNumber} - Свободно";
            }

            return button;
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Seats seat)
            {
                if (_selectedSeats.Contains(seat))
                {
                    _selectedSeats.Remove(seat);
                    button.Background = Brushes.LightGreen;
                    button.BorderBrush = Brushes.Black;
                    button.BorderThickness = new Thickness(1);
                }
                else // Выбираем место
                {
                    _selectedSeats.Add(seat);
                    button.Background = Brushes.Orange;
                    button.BorderBrush = Brushes.Black;
                    button.BorderThickness = new Thickness(2);
                }

                UpdateBuyButton();
            }
        }

        private void UpdateBuyButton()
        {
            var buyButton = FindName("BuyTicketButton") as Button;
            if (buyButton != null)
            {
                buyButton.IsEnabled = _selectedSeats.Count > 0;
                buyButton.Content = _selectedSeats.Count > 0
                    ? $"Оформить билет ({_selectedSeats.Count})"
                    : "Оформить билет";
            }
        }

        private void HideBookedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var button in _seatButtons)
            {
                if (button.Tag is Seats seat && seat.IsBooked)
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void HideBookedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var button in _seatButtons)
            {
                button.Visibility = Visibility.Visible;
            }
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSeats.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одно место");
                return;
            }

            NavigationService.Navigate(new _7PageTicket(_selectedSeats, _seans));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }


}
