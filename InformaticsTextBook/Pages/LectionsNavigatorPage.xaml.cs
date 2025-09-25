using ServiceLayer;
using ServiceLayer.Models;
using ServiceLayer.Services;
using System.Collections.ObjectModel;
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

        public static readonly ThemeService _themeService = new();
        public static readonly LectionService _lectionService = new();
        public static readonly UserService _userService = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Theme> _themes;
        public ObservableCollection<Theme> Themes
        {
            get => _themes;
            set
            {
                if (_themes != value)
                {
                    _themes = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Lection> _selectedThemeLections;
        public ObservableCollection<Lection> SelectedThemeLections
        {
            get => _selectedThemeLections;
            set
            {
                if (_selectedThemeLections != value)
                {
                    _selectedThemeLections = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public Theme _currentTheme { get; set; }
        public Theme CurrentTheme
        {
            get => _currentTheme;
            set
            {
                if (_currentTheme != value)
                {
                    _currentTheme = value;
                    OnPropertyChanged();
                }
            }
        }

        public Lection _selectedLection { get; set; }
        public Lection SelectedLection
        {
            get => _selectedLection;
            set
            {
                if (_selectedLection != value)
                {
                    _selectedLection = value;
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
            UserLoginText = $"Пользователь: {CurrentUser.UserLogin}";
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadThemes();
        }

        public async Task LoadThemes()
        {
            Themes = new ObservableCollection<Theme>(await _themeService.GetThemesAsync());
        }

        public async Task LoadThemeLections()
        {
            SelectedThemeLections = new ObservableCollection<Lection>(await _lectionService.GetLectionsByThemeIdAsync(CurrentTheme.ThemeId));
        }

        private void ThemesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadThemeLections();
        }

        private void LecturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            App.CurrentFrame.Navigate(new LectionPage(listBox.SelectedItem as Lection));
        }

        private async void ToProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ProfilePage(CurrentUser.UserID));
        }
    }
}
