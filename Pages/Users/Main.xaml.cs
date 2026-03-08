using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Users
{
    public partial class Main : Page
    {
        public UserContext AllUsers = new UserContext();
        private System.Collections.Generic.List<Models.Users> allUsersList;

        public Main()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            allUsersList = AllUsers.Users.ToList();
            ApplyFilterAndSort();
        }

        private void ApplyFilterAndSort()
        {
            try
            {
                var currentUser = Pages.Login.CurrentUser;
                var filtered = allUsersList.AsEnumerable();

                // Фильтр по ФИО
                if (SearchBox != null && !string.IsNullOrWhiteSpace(SearchBox.Text))
                {
                    filtered = filtered.Where(u => u.FIO.ToLower().Contains(SearchBox.Text.ToLower()));
                }

                // Сортировка
                if (SortBox != null && SortBox.SelectedItem != null)
                {
                    string sort = (SortBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                    switch (sort)
                    {
                        case "По ФИО":
                            filtered = filtered.OrderBy(u => u.FIO);
                            break;
                        case "По дате":
                            filtered = filtered.OrderByDescending(u => u.RentStart);
                            break;
                    }
                }

                // Отображение
                if (FindName("Parent") is StackPanel stackPanel)
                {
                    stackPanel.Children.Clear();

                    // Скрываем кнопку добавления для не-админов
                    if (currentUser == null || currentUser.Role != "Admin")
                    {
                        BthAdd.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        BthAdd.Visibility = Visibility.Visible;
                    }

                    // Добавляем пользователей
                    foreach (var user in filtered)
                    {
                        var item = new Elements.Item(user, this);

                        // Скрываем кнопки в карточке для не-админов
                        if (currentUser == null || currentUser.Role != "Admin")
                        {
                            item.HideButtons();
                        }

                        stackPanel.Children.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilterAndSort();
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilterAndSort();
        }

        public void RefreshUsers()
        {
            LoadUsers();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (Pages.Login.CurrentUser == null || Pages.Login.CurrentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление!");
                return;
            }
            MainWindow.init.OpenPages(new Pages.Users.Add(this));
        }
    }
}