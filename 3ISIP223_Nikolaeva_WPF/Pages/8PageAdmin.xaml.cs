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
    /// Логика взаимодействия для _8PageAdmin.xaml
    /// </summary>
    
    public partial class _8PageAdmin : Page
    {
        private List<Complaint> _complaints;
        private List<DefrostingRequest> _defrostingRequests;
        public _8PageAdmin()
        {
            InitializeComponent();
            DataContext = UserData.CurrentUser;

            LoadDate();
        }

        private void LoadDate()
        {
            LoadDateComp();
            LoadDefReq();
        }
        private void LoadDateComp()
        {
            _complaints = Core.Context.Complaint.ToList();
            ListBoxComplaints.ItemsSource = _complaints;
        }
        private void LoadDefReq()
        {
            _defrostingRequests =Core.Context.DefrostingRequest.ToList();
            ListBoxDefrozeRequests.ItemsSource = _defrostingRequests;
        }

        private void BtnAcceptComplaint_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Complaint complaint = (Complaint)button.DataContext;
            complaint.IsConfirmed = true;
            Core.Context.SaveChanges();
            switch (complaint.ComplaintTargetType.Name) {
                case "Книга": {
                        var book = Core.Context.Book.FirstOrDefault(b => b.ID == complaint.TargetID);
                        book.IsFrozen = true;
                        Core.Context.SaveChanges();
                        break;
                    }

                case "Автор":
                    {
                        var avt = Core.Context.User.FirstOrDefault(a => a.ID == complaint.TargetID);
                        avt.IsFrozen = true;
                        Core.Context.SaveChanges(); 
                        break;
                    }
                case "Отзыв":
                    {
                        var rev = Core.Context.Review.FirstOrDefault( r=> r.ID == complaint.TargetID);
                        rev.IsFrozen = true;
                        Core.Context.SaveChanges();
                        break;
                    }
            }
        }

        private void BtnRejectComplaint_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            Complaint complaint = (Complaint)button.DataContext;
            complaint.IsConfirmed = false;
            Core.Context.SaveChanges();
            LoadDateComp();
        }

        private void BtnAcceptDefRequest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRejectDefRequest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
