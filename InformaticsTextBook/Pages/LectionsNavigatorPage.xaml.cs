using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace InformaticsTextBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для LectionsNavigatorPage.xaml
    /// </summary>
    public partial class LectionsNavigatorPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string _userLoginText { get; set; } = "Пользователь: ";
        public string UserLoginText
        {
            get => _userLoginText;
            set
            {
                if (_userLoginText != value)
                {
                    _userLoginText = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public LectionsNavigatorPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadThemes();
        }

        public async Task LoadThemes()
        {
        }

        public async Task LoadThemeLections()
        {
        }

        private void ThemesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadThemeLections();
        }

        private void LecturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ToProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ProfilePage());
        }
    }
}
