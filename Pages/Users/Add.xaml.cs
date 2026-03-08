using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using pr_26_Toshmatov.Classes;
using Microsoft.EntityFrameworkCore;

namespace pr_26_Toshmatov.Pages.Users
{
    public partial class Add : Page
    {
        public ClubsContext AllClub = new ClubsContext();
        public Models.Users User;
        public Main Main;

        // ТОЛЬКО ОДИН КОНСТРУКТОР!
        public Add(Main Main, Models.Users User = null)
        {
            InitializeComponent();

            this.Main = Main;

            // Создаем список с "Выберите..." в начале
            var items = new System.Collections.Generic.List<object>();
            items.Add(new { Id = -1, Name = "Выберите ..." });

            foreach (Models.Clubs club in AllClub.Clubs)
            {
                items.Add(new { Id = club.Id, Name = club.Name });
            }

            Clubs.ItemsSource = items;
            Clubs.DisplayMemberPath = "Name";
            Clubs.SelectedValuePath = "Id";
            Clubs.SelectedIndex = 0;

            if (User != null)
            {
                this.User = User;
                this.FIO.Text = User.FIO;
                this.RentDate.Text = User.RentStart.ToString("yyyy-MM-dd");
                this.RentTime.Text = User.RentStart.ToString("HH:mm");
                this.Duration.Text = User.Duration.ToString();

                Clubs.SelectedValue = User.IdClub;
                BtnSave.Content = "Изменить";
            }
        }

        private void SaveUser(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (Clubs.SelectedValue == null || (int)Clubs.SelectedValue == -1)
                {
                    MessageBox.Show("Выберите клуб!");
                    return;
                }

                DateTime DTRentStart;
                if (!DateTime.TryParse(this.RentDate.Text, out DTRentStart))
                {
                    MessageBox.Show("Неверная дата!");
                    return;
                }

                TimeSpan time;
                if (!TimeSpan.TryParse(this.RentTime.Text, out time))
                {
                    MessageBox.Show("Неверное время!");
                    return;
                }
                DTRentStart = DTRentStart.Add(time);

                int duration;
                if (!int.TryParse(this.Duration.Text, out duration))
                {
                    MessageBox.Show("Неверная продолжительность!");
                    return;
                }

                var selectedClub = AllClub.Clubs.FirstOrDefault(x => x.Id == (int)Clubs.SelectedValue);
                if (selectedClub == null) return;

                if (this.User == null)
                {
                    User = new Models.Users();
                    User.FIO = this.FIO.Text;
                    User.RentStart = DTRentStart;
                    User.Duration = duration;
                    User.IdClub = selectedClub.Id;

                    this.Main.AllUsers.Users.Add(this.User);
                }
                else
                {
                    User.FIO = this.FIO.Text;
                    User.RentStart = DTRentStart;
                    User.Duration = duration;
                    User.IdClub = selectedClub.Id;
                }

                this.Main.AllUsers.SaveChanges();
                MainWindow.init.OpenPages(new Pages.Users.Main());
            }
            catch (DbUpdateException ex)
            {
                string errorMessage = "Ошибка сохранения в БД:\n";
                errorMessage += ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show(errorMessage, "Ошибка БД");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void Cancel(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(new Pages.Users.Main());
        }
    }
}