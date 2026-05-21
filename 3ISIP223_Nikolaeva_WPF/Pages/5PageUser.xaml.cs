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
    /// Логика взаимодействия для _5PageUser.xaml
    /// </summary>
    public partial class _5PageUser : Page
    {
        private User _user;
        private List<Review> _reviews;
        public _5PageUser(User user)
        {
            InitializeComponent();
            _user = user;
            LoadDate();
        }

        private void LoadDate()
        {
            if(_user.IsFrozen)
            {
                Complaint complaint = Core.Context.Complaint.OrderByDescending(r => r.Date).FirstOrDefault(c => c.IsConfirmed == true && c.TargetID == _user.ID);
                if (complaint != null)
                {
                    TxtBlockFrozenUser.DataContext = complaint;
                }
                


            }
            
            DataContext = _user;
            _reviews = Core.Context.Review.Where(r => r.UserID == _user.ID && !r.IsFrozen).ToList();
            ListBoxReviews.ItemsSource = _reviews;

        }
        private void BtnAuthorRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Податься на роль автора?", "Вопрос", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) {
                try
                {
                    RoleRequest roleRequest = new RoleRequest() {
                        UserID = _user.ID,
                        Date = DateTime.Now,
                    };
                    Core.Context.RoleRequest.Add(roleRequest);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Заявка принята!");
                }

                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
        }

        private void BtnDefrozeRequest_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Подать заявку на разморозку?", "Вопрос", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DefrostingRequest request = new DefrostingRequest()
                    {
                        UserID = _user.ID,
                        Date = DateTime.Now,
                        TargetTypeID = 2,
                        TargetID = _user.ID,
                    };

                    Core.Context.DefrostingRequest.Add(request);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Заявка принята!");
                    DataContext = _user;
                }

                catch
                {
                    MessageBox.Show("Ошибка сохранения");
                }
            }
        }

       
    }
}
