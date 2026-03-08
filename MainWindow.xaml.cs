using System.Windows;
using System.Windows.Controls;

namespace pr_26_Toshmatov
{
    public partial class MainWindow : Window
    {

        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(new Pages.Clubs.Main());
        }

        public void OpenPages(Page page)
        {
            frame.Navigate(page);
        }



        private void OpenClubs(object sender, RoutedEventArgs e) =>
            OpenPages(new Pages.Clubs.Main());

        private void OpenUsers(object sender, RoutedEventArgs e) =>
            OpenPages(new Pages.Users.Main());
    }
}