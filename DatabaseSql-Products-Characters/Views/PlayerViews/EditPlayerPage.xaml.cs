using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class EditPlayerPage : UserControl
    {
        public EditPlayerPage()
        {
            InitializeComponent();
        }

        public EditPlayerPage(EditPlayerViewModel viewModel)
        {    
            InitializeComponent();        
            DataContext = viewModel;
        }
    }
}
