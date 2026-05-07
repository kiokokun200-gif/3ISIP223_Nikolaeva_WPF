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
        public _8PageAdmin()
        {
            InitializeComponent();
            DataContext = UserData.CurrentUser;
            LoadDate();
        }

        private void LoadDate()
        {
            _complaints = Core.Context.Complaint.ToList();
            ListBoxComplaints.ItemsSource = _complaints;
        }

        private void BtnAcceptComplaint_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Complaint complaint = (Complaint)button.DataContext;
            complaint.IsConfirmed = true;
            Core.Context.SaveChanges();
        }

        private void BtnRejectComplaint_Click(object sender, RoutedEventArgs e)
        {

            Button button = (Button)sender;
            Complaint complaint = (Complaint)button.DataContext;
            complaint.IsConfirmed = false;
            Core.Context.SaveChanges();
        }
    }
}
