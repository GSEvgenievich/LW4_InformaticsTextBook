using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace InformaticsTextBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для LectionPage.xaml
    /// </summary>
    public partial class LectionPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Stopwatch _stopwatch;
        private DispatcherTimer _timer;
        private string _elapsedTimeText = "0 сек";
        public string ElapsedTimeText
        {
            get => _elapsedTimeText;
            set
            {
                if (_elapsedTimeText != value)
                {
                    _elapsedTimeText = value;
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
        private BitmapImage _currentImage;
        public BitmapImage CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public LectionPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        private async void LectionPage_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private async void LectionPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        public void SetImage(string imageName)
        {
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new LectionsNavigatorPage());
        }

        private void ToTestButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new TestPage());
        }

        private void ToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ProfilePage());
        }
    }
}
