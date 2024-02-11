using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class PlayerDetailsPage : UserControl
    {
        public PlayerDetailsPage()
        {
            InitializeComponent();
        }
        public PlayerDetailsPage(PlayerDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
