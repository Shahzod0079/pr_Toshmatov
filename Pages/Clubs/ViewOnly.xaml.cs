using System.Windows.Controls;
using pr_26_Toshmatov.Classes;

namespace pr_26_Toshmatov.Pages.Clubs
{
    public partial class ViewOnly : Page
    {
        public ViewOnly()
        {
            InitializeComponent();

            var context = new ClubsContext();
            foreach (var club in context.Clubs)
            {
                Parent.Children.Add(new Elements.Item(club, null)); // без кнопок
            }
        }
    }
}