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
        private List<RoleRequest> _roleRequests;
        private List<Book> _frozeBooks;
        private List<User> _frozeUsers;
        private List<Review> _frozenReviews;
        private List<User> _users;
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
            LoadRoleReq();
            _frozeBooks = Core.Context.Book.Where(b => b.IsFrozen).ToList();
            ListBoxFrozeBooks.ItemsSource = _frozeBooks;
            _frozenReviews = Core.Context.Review.Where(r => r.IsFrozen).ToList();
            ListBoxFrozeReviews.ItemsSource = _frozenReviews;
            _frozeUsers = Core.Context.User.Where(u => u.IsFrozen).ToList();
            ListBoxFrozeUsers.ItemsSource = _frozeUsers;
            LoadUsers();
        }
        private void LoadUsers()
        {
            _users = Core.Context.User.Where(u => !u.IsFrozen).ToList();
            ListBoxUsers.ItemsSource = _users;

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

        private void LoadRoleReq()
        {
            _roleRequests = Core.Context.RoleRequest.ToList();
            ListBoxRoleRequests.ItemsSource = _roleRequests;
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
            LoadDateComp();
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
            Button button = (Button)sender;
            DefrostingRequest defrostingRequest = (DefrostingRequest)button.DataContext;
            defrostingRequest.IsConfirmed = true;
            Core.Context.SaveChanges();
            switch (defrostingRequest.ComplaintTargetType.Name)
            {
                case "Книга":
                    {
                        var book = Core.Context.Book.FirstOrDefault(b => b.ID == defrostingRequest.TargetID);
                        book.IsFrozen = false;
                        Core.Context.SaveChanges();
                        break;
                    }

                case "Автор":
                    {
                        var avt = Core.Context.User.FirstOrDefault(a => a.ID == defrostingRequest.TargetID);
                        avt.IsFrozen = false;
                        Core.Context.SaveChanges();
                        break;
                    }

            }
            LoadDefReq();
        }

        private void BtnRejectDefRequest_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            DefrostingRequest defrostingRequest = (DefrostingRequest)button.DataContext;
            defrostingRequest.IsConfirmed = true;
            Core.Context.SaveChanges();
            LoadDefReq();
        }

        private void BtnAcceptAuthorRequest_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RoleRequest roleRequest = (RoleRequest)button.DataContext;
            roleRequest.IsConfirmed = true;
            User author = Core.Context.User.FirstOrDefault(a => a.ID == roleRequest.UserID);
            author.RoleID = 2;
            Core.Context.SaveChanges();
            LoadRoleReq();
        }

        private void BtnRejectAuthorRequest_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            RoleRequest roleRequest = (RoleRequest)button.DataContext;
            roleRequest.IsConfirmed = false;
            Core.Context.SaveChanges();
            LoadRoleReq();
        }

        private void BtnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User user = (User)btn.DataContext;
            var wind = new WindowChangeRole(user);
            wind.ShowDialog();
            LoadUsers();
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            User user = (User)btn.DataContext;
            var wind = new WindowChangePassword(user);
            wind.ShowDialog();
            LoadUsers();
        }
    }
}
