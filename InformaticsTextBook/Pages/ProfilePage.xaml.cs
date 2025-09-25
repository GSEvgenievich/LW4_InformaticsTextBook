using ServiceLayer;
using ServiceLayer.DTOs;
using ServiceLayer.Models;
using ServiceLayer.Services;
using System.Windows;
using System.Windows.Controls;

namespace InformaticsTextBook.Pages
{
    public partial class ProfilePage : Page
    {
        public static readonly VisitService _visitService = new();
        public static readonly QuestionsResultsService _resultsService = new();
        public static readonly UserService _userService = new();
        public static readonly TestService _testService = new();
        private static int ProfileOwnerId { get; set; }
        private static User ProfileOwner { get; set; }

        public ProfilePage(int profileOwnerId)
        {
            InitializeComponent();
            ProfileOwnerId = profileOwnerId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private async void LoadData()
        {
            ProfileOwner = await _userService.GetUserByIdAsync(ProfileOwnerId);

            var visits = await _visitService.GetUserVisits(ProfileOwner.UserId);

            var testResults = await _resultsService.GetUserResults(ProfileOwner.UserId);

            // группировка лекций по темам
            var lectionsByTheme = visits
                .GroupBy(v => v.Lection.Theme.ThemeName)
                .Select(g => new
                {
                    ThemeName = g.Key,
                    Lections = g.Select(v => new VisitDTO
                    {
                        LectionName = v.Lection.LectionName,
                        VisitTimeSeconds = v.VisitTime ?? 0
                    }).ToList()
                }).ToList();

            // пройденные тесты
            var passedTests = testResults
                .GroupBy(r => r.Question.Test)
                .Select(g => new
                {
                    TestId = g.Key.TestId,
                    DisplayText = $"{g.Key.Lection.LectionName} — оценка: {(int)(g.Count(r => r.IsRightAnswer) * 5.0 / (g.Count() / 2))}"
                }).ToList();

            var totalSeconds = TimeSpan.FromSeconds(visits.Sum(v => v.VisitTime ?? 0));
            string totalTime;

            if (totalSeconds.TotalMinutes >= 1)
                totalTime = $"{totalSeconds.Minutes} мин {totalSeconds.Seconds} сек";
            else
                totalTime = $"{totalSeconds.Seconds} сек";

            DataContext = new
            {
                CurrentUserLogin = CurrentUser.UserLogin,
                IsTeacherMode = CurrentUser.Role.RoleId == 1 ? Visibility.Visible : Visibility.Collapsed,
                ProfileText = CurrentUser.UserID == ProfileOwner.UserId ? "Личный кабинет" : "Кабинет студента",
                ProfileOwnerLogin = ProfileOwner.UserLogin ?? "Unknown",
                RoleName = ProfileOwner.Role.RoleName ?? "User",
                TotalLections = visits.Count,
                TotalTime = totalTime,
                LectionsByTheme = lectionsByTheme
            };

            PassedTestsList.ItemsSource = passedTests;
        }

        private async void PassedTests_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PassedTestsList.SelectedItem is not null)
            {
                var selected = (dynamic)PassedTestsList.SelectedItem;
                int testId = selected.TestId;

                var test = await _testService.GetTestById(testId);

                if (test != null)
                {
                    App.CurrentFrame.Navigate(new TestResultPage(test, ProfileOwner));
                }
            }
        }
        private void ToStudentsButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new StudentsListPage());
        }

        private void ToNavigator_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new LectionsNavigatorPage());
        }
    }
}
