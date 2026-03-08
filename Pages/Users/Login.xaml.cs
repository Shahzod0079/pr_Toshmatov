using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages
{
    public partial class Login : Page
    {
        AuthContext auth = new AuthContext();
        public static Models.SystemUser CurrentUser { get; private set; }

        public Login()
        {
            InitializeComponent();
            CurrentUser = null;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ErrorText.Text = "Введите логин и пароль!";
                ErrorText.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                var user = auth.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    CurrentUser = user;

                    if (user.Role == "Admin")
                    {
                        MainWindow.init.OpenPages(new Pages.Clubs.Main());
                    }
                    else
                    {
                        MainWindow.init.OpenPages(new Pages.Clubs.ViewOnly());
                    }
                }
                else
                {
                    ErrorText.Text = "Неверный логин или пароль!";
                    ErrorText.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                ErrorText.Text = $"Ошибка БД: {ex.Message}";
                ErrorText.Visibility = Visibility.Visible;
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Register());
        }
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}