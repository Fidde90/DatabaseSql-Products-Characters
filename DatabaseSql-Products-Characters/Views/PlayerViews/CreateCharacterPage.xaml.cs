using DatabaseSql_Products_Characters.ViewModels;
using System.Windows.Controls;

namespace DatabaseSql_Products_Characters.Views
{
    public partial class CreateCharacterPage : UserControl
    {
        public CreateCharacterPage()
        {
            InitializeComponent();
        }

        public CreateCharacterPage(CreateCharacterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }    
    }
}
