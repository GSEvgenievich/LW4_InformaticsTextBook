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

namespace InformaticsTextBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void AuthorizeButton_Click(object sender, RoutedEventArgs e)//при нажатии на кнопку идет заполнение данных текущего пользователя, если такой зарегестрирован
        {

        }

        private void AuthorizationPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            IncorrectDataLabel.Visibility = Visibility.Hidden;
        }

        private void AuthorizationLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IncorrectDataLabel.Visibility = Visibility.Hidden;
        }
    }
}
