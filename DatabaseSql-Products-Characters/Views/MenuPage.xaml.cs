using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class MenuPage : UserControl
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        public MenuPage(MenuPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
