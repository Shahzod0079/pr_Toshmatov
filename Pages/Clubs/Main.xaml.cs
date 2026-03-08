using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Clubs
{
    public partial class Main : Page
    {
        public ClubsContext AllClub = new ClubsContext();
        private System.Collections.Generic.List<Models.Clubs> allClubsList;

        public Main()
        {
            InitializeComponent();
            LoadClubs();
        }

        private void LoadClubs()
        {
            allClubsList = AllClub.Clubs.ToList();

            var currentUser = Pages.Login.CurrentUser;

            if (FindName("Parent") is StackPanel stackPanel)
            {
                stackPanel.Children.Clear();

                if (currentUser == null || currentUser.Role != "Admin")
                {
                    BthAdd.Visibility = Visibility.Collapsed;
                }

                foreach (var club in allClubsList)
                {
                    var item = new Elements.Item(club, this);

                    if (currentUser == null || currentUser.Role != "Admin")
                    {
                        item.HideButtons();
                    }

                    stackPanel.Children.Add(item);
                }
            }
        }

        private void ApplyFilterAndSort()
        {
            try
            {
                var currentUser = Pages.Login.CurrentUser;
                var filtered = allClubsList.AsEnumerable();

                // Фильтр
                if (SearchBox != null && !string.IsNullOrWhiteSpace(SearchBox.Text))
                {
                    filtered = filtered.Where(c => c.Name.ToLower().Contains(SearchBox.Text.ToLower()));
                }

                // Сортировка
                if (SortBox != null && SortBox.SelectedItem != null)
                {
                    string sort = (SortBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    switch (sort)
                    {
                        case "По названию":
                            filtered = filtered.OrderBy(c => c.Name);
                            break;
                        case "По адресу":
                            filtered = filtered.OrderBy(c => c.Address);
                            break;
                    }
                }

                // Отображение
                if (FindName("Parent") is StackPanel stackPanel)
                {
                    stackPanel.Children.Clear();

                    if (currentUser == null || currentUser.Role != "Admin")
                    {
                        BthAdd.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        BthAdd.Visibility = Visibility.Visible;
                    }

                    foreach (var club in filtered)
                    {
                        var item = new Elements.Item(club, this);

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

        private void AddClub(object sender, RoutedEventArgs e)
        {
            if (Pages.Login.CurrentUser == null || Pages.Login.CurrentUser.Role != "Admin")
            {
                MessageBox.Show("У вас нет прав на добавление!");
                return;
            }
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this));
        }
    }
}