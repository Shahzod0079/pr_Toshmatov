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

                // Обновление отображения
                if (FindName("Parent") is StackPanel stackPanel)
                {
                    stackPanel.Children.Clear();
                    foreach (var user in filtered)
                    {
                        stackPanel.Children.Add(new Elements.Item(user, this));
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
            MainWindow.init.OpenPages(new Pages.Users.Add(this));
        }
    }
}