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
                // Очищаем предыдущую ошибку
                ErrorText.Text = "";

                // Проверка заполнения полей
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

                // Проверка совпадения паролей
                if (PasswordBox.Password != ConfirmPasswordBox.Password)
                {
                    ErrorText.Text = "Пароли не совпадают!";
                    return;
                }

                // Проверка длины пароля
                if (PasswordBox.Password.Length < 3)
                {
                    ErrorText.Text = "Пароль должен быть не менее 3 символов!";
                    return;
                }

                // Проверка, существует ли уже такой логин
                var existingUser = auth.Users.FirstOrDefault(u => u.Login == LoginBox.Text);
                if (existingUser != null)
                {
                    ErrorText.Text = "Пользователь с таким логином уже существует!";
                    return;
                }

                // Создание нового пользователя
                var newUser = new SystemUser
                {
                    FIO = FIOBox.Text,
                    Login = LoginBox.Text,
                    Password = PasswordBox.Password, // В реальном проекте нужно хешировать!
                    Role = "User" // По умолчанию обычный пользователь
                };

                // Добавление в БД
                auth.Users.Add(newUser);
                auth.SaveChanges();

                // Показываем сообщение об успехе
                MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти.",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат на страницу входа
                MainWindow.init.OpenPages(new Pages.Login());
            }
            catch (Exception ex)
            {
                ErrorText.Text = $"Ошибка: {ex.Message}";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Возврат на страницу входа
            MainWindow.init.OpenPages(new Pages.Login());
        }
    }
}