using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HabitTracker.Models;

namespace HabitTracker
{

    public partial class MainWindow : Window
    {

        private ObservableCollection<Habit> _habits = new ObservableCollection<Habit>();

        public MainWindow()
        {
            InitializeComponent();
            HabitsGrid.ItemsSource = _habits;


            _habits.Add(new Habit { Name = "Утренняя зарядка", Time = "07:30", IsCompleted = true });
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTxt.Text) || string.IsNullOrWhiteSpace(LastNameTxt.Text))
            {
                MessageBox.Show("Имя и Фамилия обязательны для заполнения!", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (BirthDatePicker.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Вы не могли родиться в будущем!", "Ошибка даты");
                return;
            }

            StatusInfo.Text = $"Данные пользователя {FirstNameTxt.Text} сохранены в {DateTime.Now.ToShortTimeString()}";
            BottomProgress.Value = 100;
        }


        private void AddHabit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NewHabitName.Text))
            {
                _habits.Add(new Habit
                {
                    Name = NewHabitName.Text,
                    Time = DateTime.Now.ToShortTimeString(),
                    IsCompleted = false
                });
                NewHabitName.Clear();
            }
        }


        private void UpdateProgress(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DayProgress != null && ProdSlider != null && HappySlider != null)
            {
                DayProgress.Value = (ProdSlider.Value + HappySlider.Value) / 2;
            }
        }


        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выберите аватар";
            op.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (op.ShowDialog() == true)
            {
                AvatarImg.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void SetLightTheme(object sender, RoutedEventArgs e) => RootWindow.Background = Brushes.White;

        private void SetDarkTheme(object sender, RoutedEventArgs e) => RootWindow.Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTxt.Clear();
            LastNameTxt.Clear();
            UserPass.Clear();
            BirthDatePicker.SelectedDate = null;
            EduCombo.SelectedIndex = -1;
            HobbyList.SelectedItems.Clear();
        }

        private void ToggleEditMode(object sender, RoutedEventArgs e)
        {
            bool isReadOnly = EditModeToggle.IsChecked == false;
            FirstNameTxt.IsReadOnly = isReadOnly;
            LastNameTxt.IsReadOnly = isReadOnly;
            StatusInfo.Text = isReadOnly ? "Режим просмотра" : "Режим редактирования";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusInfo != null && e.Source is TabControl tab)
            {
                StatusInfo.Text = $"Переход на вкладку: {(tab.SelectedItem as TabItem)?.Header}";
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}