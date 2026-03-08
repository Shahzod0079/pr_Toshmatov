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
            ApplyFilterAndSort();
        }

        private void ApplyFilterAndSort()
        {
            try
            {
                var filtered = allClubsList.AsEnumerable();

                // Фильтр по названию
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

                // Обновление отображения
                if (FindName("Parent") is StackPanel stackPanel)
                {
                    stackPanel.Children.Clear();
                    foreach (var club in filtered)
                    {
                        stackPanel.Children.Add(new Elements.Item(club, this));
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

        public void RefreshClubs()
        {
            LoadClubs();
        }

        private void AddClub(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this));
        }
    }
}