using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class EditPage : UserControl
    {
        public EditPage()
        {
            InitializeComponent();
        }
        public EditPage(EditPageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
