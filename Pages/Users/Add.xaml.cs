using System;
using System.Linq;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Users
{
    public partial class Add : Page
    {
        public ClubsContext AllClub = new ClubsContext();
        public Models.Users User;
        public Main Main;
        public ClubsContext AllUsers { get; set; }

        public Add(Main Main, Models.Users User = null)
        {
            InitializeComponent();
            AllUsers = new ClubsContext();

            this.Main = Main;

            Clubs.Items.Add("Выберите ...");

            foreach (Models.Clubs Club in AllClub.Clubs)
            {
                Clubs.Items.Add(Club.Name);
            }

            if (User != null)
            {
                this.User = User;
                this.FIO.Text = User.FIO;
                this.RentDate.Text = User.RentStart.ToString("yyyy-MM-dd");
                this.RentTime.Text = User.RentStart.ToString("HH:mm");
                this.Duration.Text = User.Duration.ToString();

                var clubName = AllClub.Clubs.Where(x => x.Id == User.IdClub).First().Name;
                Clubs.SelectedItem = clubName;

                BtnSave.Content = "Изменить";
            }
        }

        private void SaveUser(object sender, System.Windows.RoutedEventArgs e)
        {
            DateTime DTRentStart = new DateTime();
            DateTime.TryParse(this.RentDate.Text, out DTRentStart);
            DTRentStart = DTRentStart.Add(TimeSpan.Parse(this.RentTime.Text));

            if (this.User == null)
            {
                User = new Models.Users();
                User.FIO = this.FIO.Text;
                User.RentStart = DTRentStart;
                User.Duration = Convert.ToInt32(this.Duration.Text);
                User.IdClub = AllClub.Clubs.Where(x => x.Name == Clubs.SelectedItem).First().Id;

                this.Main.AllUsers.Users.Add(this.User);
            }
            else
            {
                User.FIO = this.FIO.Text;
                User.RentStart = DTRentStart;
                User.Duration = Convert.ToInt32(this.Duration.Text);
                User.IdClub = AllClub.Clubs.Where(x => x.Name == Clubs.SelectedItem).First().Id;
            }

            this.Main.AllUsers.SaveChanges();

            MainWindow.init.OpenPages(new Pages.Users.Main());
        }

        private void Cancel(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Main());
        }
    }
}