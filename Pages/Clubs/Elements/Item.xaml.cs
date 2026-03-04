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

            this.Name.Text = club.Name;
            this.Address.Text = club.Address;
            this.WorkTime.Text = club.WorkTime;
        }

        private void EditClub(object sender, System.Windows.RoutedEventArgs e) =>
            MainWindow.init.OpenPages(new Pages.Clubs.Add(this.Main, this.Club));

        public void DeleteClub(object sender, System.Windows.RoutedEventArgs e)
        {

            Main.AllClub.Clubs.Remove(Club);

            Main.AllClub.SaveChanges();

            Main.Parent.Children.Remove(this);
        }
    }
}