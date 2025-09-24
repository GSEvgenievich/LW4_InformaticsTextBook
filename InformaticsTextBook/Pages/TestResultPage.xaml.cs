using ServiceLayer.Models;
using ServiceLayer.Services;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ServiceLayer.Data;

namespace InformaticsTextBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestResult.xaml
    /// </summary>
    public partial class TestResultPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static readonly TestService _testService = new();

        private string _userLoginText;
        public string UserLoginText
        {
            get => _userLoginText;
            set { _userLoginText = value; OnPropertyChanged(); }
        }

        private string _testName;
        public string TestName
        {
            get => _testName;
            set { _testName = value; OnPropertyChanged(); }
        }

        private int _totalQuestions;
        public int TotalQuestions
        {
            get => _totalQuestions;
            set { _totalQuestions = value; OnPropertyChanged(); }
        }

        private int _correctAnswers;
        public int CorrectAnswers
        {
            get => _correctAnswers;
            set { _correctAnswers = value; OnPropertyChanged(); }
        }

        private double _percentage;
        public double Percentage
        {
            get => _percentage;
            set { _percentage = value; OnPropertyChanged(); }
        }

        private string _grade;
        public string Grade
        {
            get => _grade;
            set { _grade = value; OnPropertyChanged(); }
        }

        public Test Test { get; set; }
        public TestResultPage(Test test)
        {
            InitializeComponent();
            DataContext = this;
            Test = test;
            UserLoginText = $"Пользователь: {CurrentUser.UserLogin}";
            LoadTestResults();
        }

        private async void LoadTestResults()
        {
            // Название теста
            TestName = $"Тест по лекции {Test.Lection.LectionName}";

            using (var context = new InformaticTextBookContext())
            {
                List<Question> questionsWithResults = await _testService.GetQuestionsWithResults(Test.TestId, CurrentUser.UserID);

                TotalQuestions = questionsWithResults.Count / 2;
                CorrectAnswers = questionsWithResults.Sum(q => q.QuestionsResults.Any(qr => qr.IsRightAnswer) ? 1 : 0);

                Percentage = TotalQuestions == 0 ? 0 : Math.Round((double)CorrectAnswers / TotalQuestions * 100, 2);

                if (Percentage >= 90) Grade = "Отлично";
                else if (Percentage >= 75) Grade = "Хорошо";
                else if (Percentage >= 60) Grade = "Удовлетворительно";
                else Grade = "Неудовлетворительно";
            }
        }

        private void ToTestButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new TestPage(Test.Lection));
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private void ToProfileButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ProfilePage());
        }
    }
}
