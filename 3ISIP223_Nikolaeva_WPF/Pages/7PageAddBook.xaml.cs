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
    /// Логика взаимодействия для _7PageAddBook.xaml
    /// </summary>
    public partial class _7PageAddBook : Page
    {
        public _7PageAddBook()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtBoxImagePath.Text) && TxtBoxImagePath.Text.Length < 100)
                {
                    MessageBox.Show("Введите путь к обложке");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(TxtBoxName.Text))
                {
                    MessageBox.Show("Введите название");
                    return;

                }

                else if (string.IsNullOrWhiteSpace(TxtBoxDescription.Text))
                {
                    MessageBox.Show("Введите описание");
                    return;
                }

                else if (string.IsNullOrWhiteSpace(TxtBoxText.Text))
                {
                    MessageBox.Show("Введите текст книги");
                    return;
                }
                Book book = new Book() {
                    Name = TxtBoxName.Text,
                    Description = TxtBoxDescription.Text,
                    CoverImage = TxtBoxImagePath.Text,
                    Text = TxtBoxText.Text,
                    AuthorID = UserData.CurrentUser.ID,
                    IsFrozen = false,


                };
                Core.Context.Book.Add(book);
                Core.Context.SaveChanges();
                MessageBox.Show("Книга добавлена");
                NavigationService.GoBack();

            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }
    }
}
