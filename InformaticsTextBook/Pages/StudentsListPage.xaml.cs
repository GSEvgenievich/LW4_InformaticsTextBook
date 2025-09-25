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

namespace InformaticsTextBook.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentsListPage.xaml
    /// </summary>
    public partial class StudentsListPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static readonly UserService _userService = new();

        private ObservableCollection<User> _students;
        public ObservableCollection<User> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private string _userLoginText;
        public string UserLoginText
        {
            get => _userLoginText;
            set
            {
                _userLoginText = value;
                OnPropertyChanged();
            }
        }

        public StudentsListPage()
        {
            InitializeComponent();
            DataContext = this;
            UserLoginText = $"Преподаватель: {CurrentUser.UserLogin}";
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadStudents();
        }

        private async Task LoadStudents()
        {
            // Получаем всех пользователей с ролью "Студент"
            Students = new ObservableCollection<User>(await _userService.GetAllStudentsAsync());
        }

        private void Student_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StudentsListBox.SelectedItem is User selectedStudent)
            {
                // Открываем профиль студента
                App.CurrentFrame.Navigate(new ProfilePage(selectedStudent.UserId));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ProfilePage(CurrentUser.UserID));
        }

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

