using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Users.Elements
{
    public partial class Item : UserControl
    {
        ClubsContext AllClub = new ClubsContext();
        Main Main;
        Models.Users User;

        // ОДИН конструктор (не два!)
        public Item(Models.Users user, Main main)
        {
            InitializeComponent();

            this.Main = main;
            this.User = user;

            this.FIO.Text = user.FIO;
            this.RentDate.Text = user.RentStart.ToString("yyyy-MM-dd");
            this.RentTime.Text = user.RentStart.ToString("HH:mm");
            this.Duration.Text = user.Duration.ToString();

            var club = AllClub.Clubs.FirstOrDefault(x => x.Id == user.IdClub);
            this.ClubName.Text = club != null ? club.Name : "Клуб не найден";
        }

        private void EditUser(object sender, System.Windows.RoutedEventArgs e)
        {
            // Проверка на админа
            if (Pages.Login.CurrentUser == null || Pages.Login.CurrentUser.Role != "Admin")
            {
                System.Windows.MessageBox.Show("У вас нет прав на изменение!");
                return;
            }
            MainWindow.init.OpenPages(new Pages.Users.Add(this.Main, this.User));
        }

        private void DeleteUser(object sender, System.Windows.RoutedEventArgs e)
        {
            // Проверка на админа
            if (Pages.Login.CurrentUser == null || Pages.Login.CurrentUser.Role != "Admin")
            {
                System.Windows.MessageBox.Show("У вас нет прав на удаление!");
                return;
            }

            Main.AllUsers.Users.Remove(User);
            Main.AllUsers.SaveChanges();

            if (Main.FindName("Parent") is StackPanel stackPanel)
            {
                stackPanel.Children.Remove(this);
            }
        }

        public void HideButtons()
        {
            EditButton.Visibility = Visibility.Collapsed;
            DeleteButton.Visibility = Visibility.Collapsed;
        }
    }
}