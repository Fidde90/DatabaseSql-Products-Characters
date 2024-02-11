using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
