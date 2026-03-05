using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Users
{
    public partial class Main : Page
    {
        public UserContext AllUsers = new UserContext();

        public Main()
        {
            InitializeComponent();

            foreach (Models.Users User in AllUsers.Users)
            {
                Parent.Children.Add(new Elements.Item(User, this));
            }
        }

        private void AddUser(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Add(this));
        }
    }
}