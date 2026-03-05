using System.Linq;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Users.Elements
{
    public partial class Item : UserControl
    {
        ClubsContext AllClub = new ClubsContext();
        Main Main;
        Models.Users User;

        public Item(Models.Users user, Main main)
        {
            InitializeComponent();

            this.Main = main;
            this.User = user;

            this.FIO.Text = user.FIO;
            this.RentDate.Text = user.RentStart.ToString("yyyy-MM-dd");
            this.Duration.Text = user.Duration.ToString();

            var club = AllClub.Clubs.FirstOrDefault(x => x.Id == user.IdClub);
            this.Club.Text = club != null ? club.Name : "Клуб не найден";
        }

        private void EditUser(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Add(this.Main, this.User));
        }

        private void DeleteUser(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.AllUsers.Users.Remove(User);
            Main.AllUsers.SaveChanges();
            Main.Parent.Children.Remove(this);
        }
    }
}