using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

                this.Name.Text = Club.Name;

                this.Address.Text = Club.Address;

                this.WorkTime.Text = Club.WorkTime;

                BthAdd.Content = "Изменить";
            }
        }

        private void AddClub(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) ||
                string.IsNullOrWhiteSpace(Address.Text) ||
                string.IsNullOrWhiteSpace(WorkTime.Text))
            {
                MessageBox.Show("Заполните все поля!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (this.Club == null)
            {

                Club = new Models.Clubs();

                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;

                this.Main.AllClub.Clubs.Add(this.Club);
            }
            else
            {
                Club.Name = this.Name.Text;
                Club.Address = this.Address.Text;
                Club.WorkTime = this.WorkTime.Text;
            }

            this.Main.AllClub.SaveChanges();

            MainWindow.init.OpenPages(new Pages.Clubs.Main());
        }

        private void Cancel(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Clubs.Main());
        }
    }
}