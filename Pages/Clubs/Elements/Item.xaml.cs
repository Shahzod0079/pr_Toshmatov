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

        private void EditClub(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this.Main, this.Club));

        public void DeleteClub(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.AllClub.Clubs.Remove(Club);
            Main.AllClub.SaveChanges();

            // Исправлено: безопасное удаление
            if (Main.FindName("Parent") is StackPanel stackPanel)
            {
                stackPanel.Children.Remove(this);
            }
        }
    }
}