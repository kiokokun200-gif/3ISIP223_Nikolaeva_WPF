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
using System.Windows.Shapes;

namespace _3ISIP223_Nikolaeva_WPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowComplaint.xaml
    /// </summary>
    public partial class WindowComplaint : Window
    {
        private ComplaintTargetType _complaintTargetType;
        private List<ComplaintReason> _complaintReasons;
        private string _targetName;
        private ComplaintReason _selectedComplaintReason;
        
        private int _targetID;
        private bool isRadioChecked = false;
        public WindowComplaint(ComplaintTargetType complaintTargetType, int targerID, string targetName)
        {
            InitializeComponent();
            _complaintReasons = Core.Context.ComplaintReason.ToList();
            ListBoxReasons.ItemsSource = _complaintReasons;
            _complaintTargetType = complaintTargetType;
            _targetID = targerID;
            _targetName = targetName;
            TxtBlcComplaintTarget.Text = $"Жалоба на {complaintTargetType.Name} {targetName}";

        }

        private void BtnComplaint_Click(object sender, RoutedEventArgs e)
        {
            if(!isRadioChecked)
            {
                MessageBox.Show("Выберите причину жалобы");
                return;
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Хотите пожаловаться на на {_complaintTargetType.Name} {_targetName} по причине {_selectedComplaintReason.Name}?", "Подтверждение жалобы", MessageBoxButton.YesNo);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        Complaint complaint = new Complaint
                        {
                            UserID = UserData.CurrentUser.ID,
                            TargetTypeID = _complaintTargetType.ID,
                            TargetID = _targetID,
                            Date = DateTime.Now,
                            ReasonID = _selectedComplaintReason.ID,

                        };
                        Core.Context.Complaint.Add(complaint);
                        Core.Context.SaveChanges();
                        MessageBox.Show("Ваша жалоба отправлена");

                        this.DialogResult = true;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}");
          
                    }
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isRadioChecked = true;
            RadioButton radio = (RadioButton)sender;
            _selectedComplaintReason = (ComplaintReason)radio.DataContext;

        }


    }
}
