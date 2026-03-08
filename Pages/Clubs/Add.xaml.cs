using System;
using System.Windows;
using System.Windows.Controls;

namespace pr_26_Toshmatov.Pages.Clubs
{
    public partial class Add : Page
    {
        Main Main;
        Models.Clubs Club;

        public Add(Main Main, Models.Clubs Club = null)
        {
            InitializeComponent();
            this.Main = Main;

            if (Club != null)
            {
                this.Club = Club;
                this.ClubName.Text = Club.Name;
                this.Address.Text = Club.Address;
                this.WorkTime.Text = Club.WorkTime;
            }
        }

        private void AddClub(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClubName.Text) ||
                string.IsNullOrWhiteSpace(Address.Text) ||
                string.IsNullOrWhiteSpace(WorkTime.Text))
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (this.Club == null)
                {
                    Club = new Models.Clubs();
                    Club.Name = this.ClubName.Text;
                    Club.Address = this.Address.Text;
                    Club.WorkTime = this.WorkTime.Text;
                    this.Main.AllClub.Clubs.Add(this.Club);
                }
                else
                {
                    Club.Name = this.ClubName.Text;
                    Club.Address = this.Address.Text;
                    Club.WorkTime = this.WorkTime.Text;
                }

                this.Main.AllClub.SaveChanges();
                MainWindow.init.OpenPages(new Pages.Clubs.Main());
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show($"Ошибка сохранения: {innerMessage}", "Ошибка");
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Clubs.Main());
        }
    }
}