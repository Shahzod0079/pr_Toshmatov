using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;
using pr_26_Toshmatov.Models;

namespace pr_26_Toshmatov.Pages
{
    public partial class Register : Page
    {
        AuthContext auth = new AuthContext();

        public Register()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ErrorText.Text = "";

                if (string.IsNullOrWhiteSpace(FIOBox.Text))
                {
                    ErrorText.Text = "Введите ФИО!";
                    return;
                }

                if (string.IsNullOrWhiteSpace(LoginBox.Text))
                {
                    ErrorText.Text = "Введите логин!";
                    return;
                }

                if (string.IsNullOrWhiteSpace(PasswordBox.Password))
                {
                    ErrorText.Text = "Введите пароль!";
                    return;
                }

                if (PasswordBox.Password != ConfirmPasswordBox.Password)
                {
                    ErrorText.Text = "Пароли не совпадают!";
                    return;
                }

                if (PasswordBox.Password.Length < 3)
                {
                    ErrorText.Text = "Пароль должен быть не менее 3 символов!";
                    return;
                }

                var existingUser = auth.Users.FirstOrDefault(u => u.Login == LoginBox.Text);
                if (existingUser != null)
                {
                    ErrorText.Text = "Пользователь с таким логином уже существует!";
                    return;
                }

                var newUser = new SystemUser
                {
                    FIO = FIOBox.Text,
                    Login = LoginBox.Text,
                    Password = PasswordBox.Password, 
                    Role = "User" 
                };

                auth.Users.Add(newUser);
                auth.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти.",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow.init.OpenPages(new Pages.Login());
            }
            catch (Exception ex)
            {
                ErrorText.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

            MainWindow.init.OpenPages(new Pages.Login());
        }
    }
}