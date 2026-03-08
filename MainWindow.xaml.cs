using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Pages;

namespace pr_26_Toshmatov
{
    public partial class MainWindow : Window
    {

        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(new Pages.Login());
        }

        public void OpenPages(Page page)
        {
            frame.Navigate(page);
        }



        private void OpenClubs(object sender, RoutedEventArgs e)
        {
            if (Login.CurrentUser != null) 
                OpenPages(new Pages.Clubs.Main());
            else
                MessageBox.Show("Пожалуйста, сначала входите или зарегистрируйтесь!");
        }

        private void OpenUsers(object sender, RoutedEventArgs e)
        {
            if (Login.CurrentUser != null)
                OpenPages(new Pages.Users.Main());
            else
                MessageBox.Show("Пожалуйста, сначала входите или зарегистрируйтесь!");
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Pages.Login.Logout(); 
            OpenPages(new Pages.Login());
        }
    }
}