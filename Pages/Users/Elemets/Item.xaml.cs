using System.Linq;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Users.Elements
{
    public partial class Item : UserControl
    {
        public ClubsContext AllClub = new ClubsContext();
        Main Main;
        Models.Users User;

        public Item(Models.Users User, Main Main)
        {
            InitializeComponent();

            this.FIO.Text = User.FIO;
            this.RentDate.Text = User.RentStart.ToString("yyyy-MM-dd");
            this.RentTime.Text = User.RentStart.ToString("HH:mm");
            this.Duration.Text = User.Duration.ToString();

            var club = AllClub.Clubs.Where(x => x.Id == User.IdClub).FirstOrDefault();
            if (club != null)
            {
                this.Club.Text = club.Name;
            }
            else
            {
                this.Club.Text = "Клуб не найден";
            }

            this.Main = Main;
            this.User = User;
        }

        private void EditUser(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Users.Add(this.Main, this.User));

        private void DeleteUser(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.AllUsers.Users.Remove(User);
            Main.AllUsers.SaveChanges();
            Main.Parent.Children.Remove(this);
        }
    }
}