using System.Windows.Controls;

namespace pr_26_Toshmatov.Pages.Clubs.Elements
{
    public partial class Item : UserControl
    {
        Main Main;
        Models.Clubs Club;

        public Item(Models.Clubs club, Main main)
        {
            InitializeComponent();

            this.Main = main;
            this.Club = club;

            // Исправлено: ClubName вместо club
            this.club.Text = club.Name;
            this.Address.Text = club.Address;
            this.WorkTime.Text = club.WorkTime;
        }

        private void EditClub(object sender, System.Windows.RoutedEventArgs e)
        {
            // Проверка на админа
            if (Pages.Login.CurrentUser == null || Pages.Login.CurrentUser.Role != "Admin")
            {
                System.Windows.MessageBox.Show("У вас нет прав на изменение!");
                return;
            }
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this.Main, this.Club));
        }

        public void DeleteClub(object sender, System.Windows.RoutedEventArgs e)
        {
            // Проверка на админа
            if (Pages.Login.CurrentUser == null || Pages.Login.CurrentUser.Role != "Admin")
            {
                System.Windows.MessageBox.Show("У вас нет прав на удаление!");
                return;
            }

            Main.AllClub.Clubs.Remove(Club);
            Main.AllClub.SaveChanges();

            if (Main.FindName("Parent") is StackPanel stackPanel)
            {
                stackPanel.Children.Remove(this);
            }
        }

        public void HideButtons()
        {
            // Скрываем кнопки изменения и удаления
            EditButton.Visibility = System.Windows.Visibility.Collapsed;
            DeleteButton.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}