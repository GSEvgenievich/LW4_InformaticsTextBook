using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace InformaticsTextBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Stopwatch _stopwatch;
        private DispatcherTimer _timer;
        private TimeSpan _timeLimit = TimeSpan.FromMinutes(5);
        private TimeSpan _remainingTime;
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

        public string _testName { get; set; }
        public string TestName
        {
            get => _testName;
            set
            {
                if (_testName != value)
                {
                    _testName = value;
                    OnPropertyChanged();
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        
        public TestPage()
        {
        }

        private string _remainingTimeText = "05:00";
        public string RemainingTimeText
        {
            get => _remainingTimeText;
            set
            {
                if (_remainingTimeText != value)
                {
                    _remainingTimeText = value;
                    OnPropertyChanged();
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTestAsync();
        }

        private async Task LoadTestAsync()
        {
        }

        private void ToLectionButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ToNavigatorButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new LectionsNavigatorPage());
        }

        private async Task FinishTest()
        {
        }

        private async void FinishTestButton_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _stopwatch.Stop();
            await FinishTest();
        }

        private void ToResultButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new TestResultPage());
        }

        private void StartTestButton_Click(object sender, RoutedEventArgs e)
        {
            StartTestButton.Visibility = Visibility.Collapsed;
            ToLectionButton.Visibility = Visibility.Collapsed;
            ToNavigatorButton.Visibility = Visibility.Collapsed;
            BottomPanel.Visibility = Visibility.Visible;
            QuestionsPanel.Visibility = Visibility.Visible;

            _stopwatch = Stopwatch.StartNew();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _remainingTime = _timeLimit - _stopwatch.Elapsed;

            if (_remainingTime.TotalSeconds <= 0)
            {
                _remainingTime = TimeSpan.Zero;
                TimeExpired();
            }

            RemainingTimeText = _remainingTime.ToString(@"mm\:ss");
        }

        private async void TimeExpired()
        {
            _timer.Stop();
            _stopwatch.Stop();

            // Показываем сообщение о завершении времени
            MessageBox.Show("Время вышло! Тест будет автоматически завершен.", "Время истекло",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Автоматически завершаем тест
            await FinishTest();
        }
    }
}
