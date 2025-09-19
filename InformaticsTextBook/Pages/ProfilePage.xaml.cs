using System.Windows;
using System.Windows.Controls;

namespace InformaticsTextBook.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
        }

        private async void PassedTests_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void ToNavigator_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new LectionsNavigatorPage());
        }
    }
}
