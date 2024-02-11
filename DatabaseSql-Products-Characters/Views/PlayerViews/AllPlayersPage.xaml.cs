using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class AllPlayersPage : UserControl
    {
        public AllPlayersPage()
        {
            InitializeComponent();
        }
        public AllPlayersPage(AllPlayersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
